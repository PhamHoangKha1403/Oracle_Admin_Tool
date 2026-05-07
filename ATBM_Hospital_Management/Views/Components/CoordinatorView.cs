using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.Components
{
    public class CoordinatorView : UserControl
    {
        private TabControl tabControl;
        private TabPage tabPatient;
        private TabPage tabHSBA;
        private TabPage tabThongBao;

        // Patient Tab Controls
        private DataGridView dgvPatient;
        private Button btnAddPatient;
        private Button btnEditPatient;

        // HSBA Tab Controls
        private DataGridView dgvHSBA;
        private DataGridView dgvHSBADV;
        private Button btnAddHSBA;
        private Button btnEditHSBA;
        private Button btnEditHSBADV;

        // ThongBao Tab Controls
        private DataGridView dgvThongBao;
        private Button btnRefresh;

        private TabPage tabProfile;

        private Panel profilePanel;

        private Button btnEditProfile;

        private Label lblMaNVValue;
        private Label lblHoTenValue;
        private Label lblPhaiValue;
        private Label lblNgaySinhValue;
        private Label lblQueQuanValue;
        private Label lblSDTValue;
        private Label lblChuyenKhoaValue;

        public CoordinatorView()
        {
            InitializeUI();
            LoadPatientData();
            LoadHSBAData();
            LoadThongBao();
            LoadProfile();
        }

        private void InitializeUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;

            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(15, 8),
                Font = new Font("Segoe UI", 10F)
            };
            this.Controls.Add(tabControl);

            tabPatient = new TabPage("PATIENTS");
            tabHSBA = new TabPage("MEDICAL RECORDS");
            tabThongBao = new TabPage("NOTIFICATIONS");
            tabProfile = new TabPage("MY PROFILE");
            tabControl.TabPages.Add(tabPatient);
            tabControl.TabPages.Add(tabHSBA);
            tabControl.TabPages.Add(tabThongBao);
            tabControl.TabPages.Add(tabProfile);

            InitializePatientTab();
            InitializeHSBATab();
            InitializeThongBaoTab();
            InitializeProfileTab();
        }

        private void InitializePatientTab()
        {
            tabPatient.Padding = new Padding(15);
            tabPatient.BackColor = Color.White;

            FlowLayoutPanel pnlButtons = new FlowLayoutPanel
            {
                Location = new Point(15, 15),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            btnAddPatient = new Button { Text = "ADD PATIENT", Size = new Size(150, 40), BackColor = Color.DodgerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 0, 10, 0) };
            btnAddPatient.FlatAppearance.BorderSize = 0;
            btnAddPatient.Click += BtnAddPatient_Click;

            btnEditPatient = new Button { Text = "EDIT PATIENT", Size = new Size(150, 40), BackColor = Color.DarkOrange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 0, 15, 0) };
            btnEditPatient.FlatAppearance.BorderSize = 0;
            btnEditPatient.Click += BtnEditPatient_Click;

            Button btnSearchPatient = new Button { Text = "SEARCH", Size = new Size(120, 40), BackColor = Color.SeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0) };
            TextBox txtSearchPatient = new TextBox { AutoSize = false, Size = new Size(300, 40), Font = new Font("Segoe UI", 12F), Margin = new Padding(0) };
            btnSearchPatient.FlatAppearance.BorderSize = 0;

            btnSearchPatient.Click += (s, e) =>
            {
                if (dgvPatient.DataSource is DataTable dt)
                {
                    string keyword = txtSearchPatient.Text.Trim().Replace("'", "''");
                    if (string.IsNullOrEmpty(keyword)) dt.DefaultView.RowFilter = "";
                    else
                    {
                        var filters = new System.Collections.Generic.List<string>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (col.DataType == typeof(string))
                                filters.Add($"[{col.ColumnName}] LIKE '%{keyword}%'");
                        }
                        dt.DefaultView.RowFilter = string.Join(" OR ", filters);
                    }
                }
            };

            pnlButtons.Controls.Add(btnAddPatient);
            pnlButtons.Controls.Add(btnEditPatient);
            pnlButtons.Controls.Add(btnSearchPatient);
            pnlButtons.Controls.Add(txtSearchPatient);
            tabPatient.Controls.Add(pnlButtons);

            dgvPatient = new DataGridView
            {
                Location = new Point(15, 65),
                Size = new Size(800, 300),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvPatient.DataBindingComplete += (s, e) =>
            {
                if (dgvPatient.Columns.Contains("MA_BN")) dgvPatient.Columns["MA_BN"].HeaderText = "Mã BN";
                if (dgvPatient.Columns.Contains("HO_TEN")) dgvPatient.Columns["HO_TEN"].HeaderText = "Họ tên";
                if (dgvPatient.Columns.Contains("PHAI")) dgvPatient.Columns["PHAI"].HeaderText = "Phái";
                if (dgvPatient.Columns.Contains("NGAY_SINH")) dgvPatient.Columns["NGAY_SINH"].HeaderText = "Ngày sinh";
                if (dgvPatient.Columns.Contains("CCCD")) dgvPatient.Columns["CCCD"].HeaderText = "CCCD";
                if (dgvPatient.Columns.Contains("SDT")) dgvPatient.Columns["SDT"].HeaderText = "SĐT";
                if (dgvPatient.Columns.Contains("SO_NHA")) dgvPatient.Columns["SO_NHA"].HeaderText = "Số nhà";
                if (dgvPatient.Columns.Contains("TEN_DUONG")) dgvPatient.Columns["TEN_DUONG"].HeaderText = "Tên đường";
                if (dgvPatient.Columns.Contains("QUAN_HUYEN")) dgvPatient.Columns["QUAN_HUYEN"].HeaderText = "Quận/Huyện";
                if (dgvPatient.Columns.Contains("TINH_TP")) dgvPatient.Columns["TINH_TP"].HeaderText = "Tỉnh/TP";
                if (dgvPatient.Columns.Contains("TIEN_SU_BENH")) dgvPatient.Columns["TIEN_SU_BENH"].HeaderText = "Tiền sử bệnh";
                if (dgvPatient.Columns.Contains("TIEN_SU_BENH_GD")) dgvPatient.Columns["TIEN_SU_BENH_GD"].HeaderText = "Tiền sử gia đình";
                if (dgvPatient.Columns.Contains("DI_UNG_THUOC")) dgvPatient.Columns["DI_UNG_THUOC"].HeaderText = "Dị ứng thuốc";
                if (dgvPatient.Columns.Contains("CHUYEN_KHOA")) dgvPatient.Columns["CHUYEN_KHOA"].HeaderText = "Chuyên khoa";
            };
            tabPatient.Controls.Add(dgvPatient);
        }

        private void InitializeHSBATab()
        {
            tabHSBA.Padding = new Padding(15);
            tabHSBA.BackColor = Color.White;

            FlowLayoutPanel pnlHSBAButtons = new FlowLayoutPanel
            {
                Location = new Point(15, 15),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            btnAddHSBA = new Button { Text = "ADD RECORD", Size = new Size(130, 40), BackColor = Color.DodgerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 0, 10, 0) };
            btnAddHSBA.FlatAppearance.BorderSize = 0;
            btnAddHSBA.Click += BtnAddHSBA_Click;

            btnEditHSBA = new Button { Text = "EDIT RECORD", Size = new Size(130, 40), BackColor = Color.DarkOrange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 0, 10, 0) };
            btnEditHSBA.FlatAppearance.BorderSize = 0;
            btnEditHSBA.Click += BtnEditHSBA_Click;
        
            btnEditHSBADV = new Button { Text = "EDIT SERVICE", Size = new Size(130, 40), BackColor = Color.Orange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 0, 15, 0) };
            btnEditHSBADV.FlatAppearance.BorderSize = 0;
            btnEditHSBADV.Click += BtnEditHSBADV_Click;

            Button btnSearchHSBA = new Button { Text = "SEARCH", Size = new Size(100, 40), BackColor = Color.SeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0) };
            TextBox txtSearchHSBA = new TextBox { AutoSize = false, Size = new Size(250, 40), Font = new Font("Segoe UI", 12F), Margin = new Padding(0) };
            btnSearchHSBA.FlatAppearance.BorderSize = 0;

            btnSearchHSBA.Click += (s, e) =>
            {
                if (dgvHSBA.DataSource is DataTable dt)
                {
                    string keyword = txtSearchHSBA.Text.Trim().Replace("'", "''");
                    if (string.IsNullOrEmpty(keyword)) dt.DefaultView.RowFilter = "";
                    else
                    {
                        var filters = new System.Collections.Generic.List<string>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (col.DataType == typeof(string))
                                filters.Add($"[{col.ColumnName}] LIKE '%{keyword}%'");
                        }
                        dt.DefaultView.RowFilter = string.Join(" OR ", filters);
                    }
                }
            };

            pnlHSBAButtons.Controls.Add(btnAddHSBA);
            pnlHSBAButtons.Controls.Add(btnEditHSBA);
            pnlHSBAButtons.Controls.Add(btnEditHSBADV);
            pnlHSBAButtons.Controls.Add(btnSearchHSBA);
            pnlHSBAButtons.Controls.Add(txtSearchHSBA);
            tabHSBA.Controls.Add(pnlHSBAButtons);

            SplitContainer splitContainer = new SplitContainer
            {
                Location = new Point(15, 65),
                Size = new Size(800, 500),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 250
            };

            Label lbl1 = new Label { Text = "Danh sách Hồ sơ bệnh án", Font = new Font("Segoe UI Semibold", 12F), Dock = DockStyle.Top, Padding = new Padding(0, 0, 0, 10) };
            dgvHSBA = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvHSBA.DataBindingComplete += (s, e) =>
            {
                if (dgvHSBA.Columns.Contains("MA_HSBA")) dgvHSBA.Columns["MA_HSBA"].HeaderText = "Mã HSBA";
                if (dgvHSBA.Columns.Contains("MA_BN")) dgvHSBA.Columns["MA_BN"].HeaderText = "Mã BN";
                if (dgvHSBA.Columns.Contains("NGAY")) dgvHSBA.Columns["NGAY"].HeaderText = "Ngày khám";
                if (dgvHSBA.Columns.Contains("CHAN_DOAN")) dgvHSBA.Columns["CHAN_DOAN"].HeaderText = "Chẩn đoán";
                if (dgvHSBA.Columns.Contains("DIEU_TRI")) dgvHSBA.Columns["DIEU_TRI"].HeaderText = "Điều trị";
                if (dgvHSBA.Columns.Contains("MA_BS")) dgvHSBA.Columns["MA_BS"].HeaderText = "Mã BS";
                if (dgvHSBA.Columns.Contains("MA_KHOA")) dgvHSBA.Columns["MA_KHOA"].HeaderText = "Mã khoa";
                if (dgvHSBA.Columns.Contains("KET_LUAN")) dgvHSBA.Columns["KET_LUAN"].HeaderText = "Kết luận";
            };
            splitContainer.Panel1.Controls.Add(lbl1);
            splitContainer.Panel1.Controls.Add(dgvHSBA);

            Label lbl2 = new Label { Text = "Dịch vụ của Hồ sơ bệnh án", Font = new Font("Segoe UI Semibold", 12F), Dock = DockStyle.Top, Padding = new Padding(0, 0, 0, 10) };
            dgvHSBADV = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvHSBADV.DataBindingComplete += (s, e) =>
            {
                if (dgvHSBADV.Columns.Contains("MA_HSBA")) dgvHSBADV.Columns["MA_HSBA"].HeaderText = "Mã HSBA";
                if (dgvHSBADV.Columns.Contains("MA_DV")) dgvHSBADV.Columns["MA_DV"].HeaderText = "Mã DV";
                if (dgvHSBADV.Columns.Contains("NGAY_DV")) dgvHSBADV.Columns["NGAY_DV"].HeaderText = "Ngày DV";
                if (dgvHSBADV.Columns.Contains("MA_KTV")) dgvHSBADV.Columns["MA_KTV"].HeaderText = "Mã KTV";
                if (dgvHSBADV.Columns.Contains("KET_QUA")) dgvHSBADV.Columns["KET_QUA"].HeaderText = "Kết quả";
                if (dgvHSBADV.Columns.Contains("LOAI_DV")) dgvHSBADV.Columns["LOAI_DV"].HeaderText = "Loại DV";
            };
            splitContainer.Panel2.Controls.Add(lbl2);
            splitContainer.Panel2.Controls.Add(dgvHSBADV);

            tabHSBA.Controls.Add(splitContainer);
        }

        private void InitializeThongBaoTab()
        {
            tabThongBao.Padding = new Padding(15);
            tabThongBao.BackColor = Color.White;
            btnRefresh = new Button { Text = "REFRESH", Size = new Size(150, 40), BackColor = Color.SeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Location = new Point(15, 15) };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => LoadThongBao();
            tabThongBao.Controls.Add(btnRefresh);
            dgvThongBao = new DataGridView
            {
                Location = new Point(15, 65),
                Size = new Size(800, 360),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvThongBao.DataBindingComplete += (s, e) =>
            {
                if (dgvThongBao.Columns.Contains("MA_TB")) dgvThongBao.Columns["MA_TB"].HeaderText = "Mã TB";
                if (dgvThongBao.Columns.Contains("NOI_DUNG")) dgvThongBao.Columns["NOI_DUNG"].HeaderText = "Nội dung";
                if (dgvThongBao.Columns.Contains("NGAY_GIO")) dgvThongBao.Columns["NGAY_GIO"].HeaderText = "Ngày giờ";
                if (dgvThongBao.Columns.Contains("DIA_DIEM")) dgvThongBao.Columns["DIA_DIEM"].HeaderText = "Địa điểm";
                if (dgvThongBao.Columns.Contains("NGUOI_TAO")) dgvThongBao.Columns["NGUOI_TAO"].HeaderText = "Người tạo";
                if (dgvThongBao.Columns.Contains("NGAY_TAO")) dgvThongBao.Columns["NGAY_TAO"].HeaderText = "Ngày tạo";
            };
            tabThongBao.Controls.Add(dgvThongBao);
        }

        private void InitializeProfileTab()
        {
            tabProfile.Padding = new Padding(20);
            tabProfile.BackColor = Color.White;

            btnEditProfile = new Button
            {
                Text = "UPDATE PROFILE",
                Size = new Size(180, 42),
                BackColor = Color.DarkOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 20)
            };

            btnEditProfile.FlatAppearance.BorderSize = 0;
            btnEditProfile.Click += BtnEditProfile_Click;

            tabProfile.Controls.Add(btnEditProfile);

            profilePanel = new Panel
            {
                Location = new Point(20, 80),
                Size = new Size(760, 450),
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            Label lblTitle = new Label
            {
                Text = "PERSONAL INFORMATION",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.DodgerBlue,
                AutoSize = true,
                Location = new Point(25, 25)
            };

            profilePanel.Controls.Add(lblTitle);

            int y = 90;

            lblMaNVValue = AddProfileRow("Mã nhân viên:", ref y);
            lblHoTenValue = AddProfileRow("Họ tên:", ref y);
            lblPhaiValue = AddProfileRow("Phái:", ref y);
            lblNgaySinhValue = AddProfileRow("Ngày sinh:", ref y);
            lblQueQuanValue = AddProfileRow("Quê quán:", ref y);
            lblSDTValue = AddProfileRow("SĐT:", ref y);
            lblChuyenKhoaValue = AddProfileRow("Chuyên khoa:", ref y);

            tabProfile.Controls.Add(profilePanel);
        }

        private Label AddProfileRow(string title, ref int y)
        {
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(40, y)
            };

            Label lblValue = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 11F),
                AutoSize = true,
                ForeColor = Color.DimGray,
                Location = new Point(240, y)
            };

            profilePanel.Controls.Add(lblTitle);
            profilePanel.Controls.Add(lblValue);

            y += 45;

            return lblValue;
        }

        private void LoadProfile()
        {
            try
            {
                var pOut = new Oracle.ManagedDataAccess.Client.OracleParameter(
                    "p_cursor",
                    Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };

                DataTable dt = DbConnection.Instance.ExecuteQuery(
                    "BEGIN sp_NV_Select_NHANVIEN(:p_cursor); END;",
                    new[] { pOut },
                    CommandType.Text
                );

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    lblMaNVValue.Text = row["MA_NV"]?.ToString();
                    lblHoTenValue.Text = row["HO_TEN"]?.ToString();
                    lblPhaiValue.Text = row["PHAI"]?.ToString();

                    DateTime ns;
                    if (DateTime.TryParse(row["NGAY_SINH"]?.ToString(), out ns))
                    {
                        lblNgaySinhValue.Text = ns.ToString("dd/MM/yyyy");
                    }

                    lblQueQuanValue.Text = row["QUE_QUAN"]?.ToString();
                    lblSDTValue.Text = row["SDT"]?.ToString();
                    lblChuyenKhoaValue.Text = row["TEN_KHOA"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin nhân viên: " + ex.Message);
            }
        }

        private void LoadPatientData()
        {
            try
            {
                var pOut = new Oracle.ManagedDataAccess.Client.OracleParameter("p_cursor", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                dgvPatient.DataSource = DbConnection.Instance.ExecuteQuery("BEGIN sp_DPV_Select_BENHNHAN(:p_cursor); END;", new[] { pOut }, CommandType.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadHSBAData()
        {
            try
            {
                var pOutHSBA = new Oracle.ManagedDataAccess.Client.OracleParameter("p_cursor", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor) { Direction = ParameterDirection.Output };
                dgvHSBA.DataSource = DbConnection.Instance.ExecuteQuery("BEGIN sp_DPV_Select_HSBA(:p_cursor); END;", new[] { pOutHSBA }, CommandType.Text);
                
                var pOutHSBADV = new Oracle.ManagedDataAccess.Client.OracleParameter("p_cursor", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor) { Direction = ParameterDirection.Output };
                dgvHSBADV.DataSource = DbConnection.Instance.ExecuteQuery("BEGIN sp_DPV_Select_HSBADV(:p_cursor); END;", new[] { pOutHSBADV }, CommandType.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadThongBao()
        {
            try
            {
                var pOut = new Oracle.ManagedDataAccess.Client.OracleParameter(
                    "p_cursor",
                    Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };

                DataTable dt = DbConnection.Instance.ExecuteQuery(
                    "BEGIN SP_GET_THONGBAO(:p_cursor); END;",
                    new[] { pOut },
                    CommandType.Text
                );

                if (dt != null)
                {
                    dgvThongBao.DataSource = dt;

                    // Kiểm tra nếu cột MA_NV tồn tại thì ẩn nó đi
                    if (dgvThongBao.Columns["MA_NV"] != null)
                    {
                        dgvThongBao.Columns["MA_NV"].Visible = false;
                    }

                    // Tiện tay chỉnh lại Header cho đẹp luôn bạn nhé
                    if (dgvThongBao.Columns["MA_TB"] != null) dgvThongBao.Columns["MA_TB"].HeaderText = "Mã TB";
                    if (dgvThongBao.Columns["NOI_DUNG"] != null) dgvThongBao.Columns["NOI_DUNG"].HeaderText = "Nội Dung";
                    if (dgvThongBao.Columns["NGAY_GIO"] != null) dgvThongBao.Columns["NGAY_GIO"].HeaderText = "Thời Gian";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông báo: " + ex.Message);
            }
        }

        private TextBox AddField(Form form, string label, string defaultValue, ref int y, bool isReadOnly = false)
        {
            Label lbl = new Label { Text = label, Location = new Point(20, y + 3), AutoSize = true };
            TextBox txt = new TextBox { Text = defaultValue, Location = new Point(160, y), Size = new Size(230, 25), ReadOnly = isReadOnly };
            form.Controls.Add(lbl);
            form.Controls.Add(txt);
            y += 40;
            return txt;
        }

        private ComboBox AddComboBoxField(Form form, string label, string selectedValue, DataTable dtSource, string valueMember, string displayMember, ref int y)
        {
            Label lbl = new Label { Text = label, Location = new Point(20, y + 3), AutoSize = true };
            ComboBox cmb = new ComboBox 
            { 
                Location = new Point(160, y), 
                Size = new Size(230, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = dtSource,
                ValueMember = valueMember,
                DisplayMember = displayMember
            };
            if (!string.IsNullOrEmpty(selectedValue))
            {
                cmb.SelectedValue = selectedValue;
            }
            form.Controls.Add(lbl);
            form.Controls.Add(cmb);
            y += 40;
            return cmb;
        }

        private ComboBox AddSimpleComboBoxField(Form form, string label, string selectedValue, string[] items, ref int y)
        {
            Label lbl = new Label { Text = label, Location = new Point(20, y + 3), AutoSize = true };
            ComboBox cmb = new ComboBox 
            { 
                Location = new Point(160, y), 
                Size = new Size(230, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmb.Items.AddRange(items);
            if (!string.IsNullOrEmpty(selectedValue))
            {
                cmb.SelectedItem = selectedValue;
            }
            else if (items.Length > 0)
            {
                cmb.SelectedIndex = 0;
            }
            form.Controls.Add(lbl);
            form.Controls.Add(cmb);
            y += 40;
            return cmb;
        }

        private string GenerateNextId(DataTable dt, string columnName, string defaultPrefix, string defaultFormat)
        {
            string newId = defaultPrefix + 1.ToString(defaultFormat);
            if (dt == null) return newId;

            try
            {
                int maxNumeric = 0;
                string currentPrefix = "";
                int padLength = 0;

                foreach (DataRow row in dt.Rows)
                {
                    if (row[columnName] == DBNull.Value) continue;
                    string idStr = row[columnName].ToString();
                    var match = System.Text.RegularExpressions.Regex.Match(idStr, @"^([A-Za-z_]+)(\d+)$");
                    if (match.Success)
                    {
                        int num = int.Parse(match.Groups[2].Value);
                        if (num > maxNumeric)
                        {
                            maxNumeric = num;
                            currentPrefix = match.Groups[1].Value;
                            if (match.Groups[2].Value.StartsWith("0"))
                                padLength = match.Groups[2].Value.Length;
                            else
                                padLength = 0;
                        }
                    }
                }

                if (maxNumeric > 0)
                {
                    if (padLength > 0)
                        newId = currentPrefix + (maxNumeric + 1).ToString().PadLeft(padLength, '0');
                    else
                        newId = currentPrefix + (maxNumeric + 1).ToString();
                }
            }
            catch { }
            return newId;
        }

        private void BtnAddPatient_Click(object sender, EventArgs e)
        {
            using (Form f = new Form() { Text = "Thêm bệnh nhân", Size = new Size(440, 780), StartPosition = FormStartPosition.CenterParent, AutoScroll = true })
            {
                int y = 20;
                string newMaBN = GenerateNextId(dgvPatient.DataSource as DataTable, "MA_BN", "BN", "D6");

                TextBox txtMaBN = AddField(f, "Mã BN:", newMaBN, ref y, true);
                TextBox txtHoTen = AddField(f, "Họ tên:", "", ref y);
                ComboBox cmbPhai = AddSimpleComboBoxField(f, "Phái:", "Nam", new string[] { "Nam", "Nữ" }, ref y);
                
                Label lblNgaySinh = new Label { Text = "Ngày sinh:", Location = new Point(20, y + 3), AutoSize = true };
                DateTimePicker dtpNgaySinh = new DateTimePicker { Location = new Point(160, y), Size = new Size(230, 25), Format = DateTimePickerFormat.Short };
                f.Controls.Add(lblNgaySinh); f.Controls.Add(dtpNgaySinh); y += 40;
                
                TextBox txtCCCD = AddField(f, "CCCD:", "", ref y);
                TextBox txtSDT = AddField(f, "SĐT:", "", ref y);
                
                TextBox txtSoNha = AddField(f, "Số nhà:", "", ref y);
                TextBox txtTenDuong = AddField(f, "Tên đường:", "", ref y);
                TextBox txtQuanHuyen = AddField(f, "Quận/Huyện:", "", ref y);
                TextBox txtTinhTp = AddField(f, "Tỉnh/TP:", "", ref y);
                TextBox txtTienSu = AddField(f, "Tiền sử bệnh:", "", ref y);
                TextBox txtTienSuGD = AddField(f, "TS bệnh GĐ:", "", ref y);
                TextBox txtDiUng = AddField(f, "Dị ứng thuốc:", "", ref y);
                
                DataTable dtKhoa = null;
                try { dtKhoa = DbConnection.Instance.ExecuteQuery("SELECT MA_KHOA, TEN_KHOA FROM ADMIN_PH2.KHOA", null, CommandType.Text); } catch { }
                ComboBox cmbChuyenKhoa = null;
                TextBox txtChuyenKhoa = null;
                if (dtKhoa != null && dtKhoa.Rows.Count > 0)
                {
                    cmbChuyenKhoa = AddComboBoxField(f, "Chuyên khoa:", "", dtKhoa, "MA_KHOA", "TEN_KHOA", ref y);
                }
                else
                {
                    txtChuyenKhoa = AddField(f, "Mã chuyên khoa:", "", ref y);
                }

                Button btnSave = new Button { Text = "Save", Location = new Point(160, y + 10), Size = new Size(100, 35) };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MABN", txtMaBN.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_HOTEN", txtHoTen.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_PHAI", cmbPhai.SelectedItem?.ToString()),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_NGAYSINH", dtpNgaySinh.Value),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_CCCD", txtCCCD.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_SDT", txtSDT.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_SONHA", txtSoNha.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TENDUONG", txtTenDuong.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_QUANHUYEN", txtQuanHuyen.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TINHTP", txtTinhTp.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENH", txtTienSu.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENHGD", txtTienSuGD.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_DIUNGTHUOC", txtDiUng.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_CHUYENKHOA", cmbChuyenKhoa != null ? cmbChuyenKhoa.SelectedValue?.ToString() : (string.IsNullOrEmpty(txtChuyenKhoa.Text) ? (object)DBNull.Value : txtChuyenKhoa.Text))
                        };

                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Insert_BENHNHAN(:p_MABN, :p_HOTEN, :p_PHAI, :p_NGAYSINH, :p_CCCD, :p_SDT, :p_SONHA, :p_TENDUONG, :p_QUANHUYEN, :p_TINHTP, :p_TIENSUBENH, :p_TIENSUBENHGD, :p_DIUNGTHUOC, :p_CHUYENKHOA); END;", parameters, CommandType.Text);
                        MessageBox.Show("Thêm thành công!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
                };
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK) LoadPatientData();
            }
        }

        private void BtnEditPatient_Click(object sender, EventArgs e)
        {
            if (dgvPatient.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn bệnh nhân."); return; }
            var row = dgvPatient.SelectedRows[0];
            DataRowView drv = row.DataBoundItem as DataRowView;

            using (Form f = new Form() { Text = "Cập nhật bệnh nhân", Size = new Size(440, 780), StartPosition = FormStartPosition.CenterParent, AutoScroll = true })
            {
                int y = 20;
                TextBox txtMaBN = AddField(f, "Mã BN:", row.Cells["MA_BN"].Value?.ToString(), ref y, true);
                
                string hoTen = drv != null ? drv["HO_TEN"]?.ToString() : row.Cells["HO_TEN"].Value?.ToString();
                TextBox txtHoTen = AddField(f, "Họ tên:", hoTen, ref y);
                
                string phai = drv != null ? drv["PHAI"]?.ToString() : row.Cells["PHAI"].Value?.ToString();
                ComboBox cmbPhai = AddSimpleComboBoxField(f, "Phái:", phai, new string[] { "Nam", "Nữ" }, ref y);
                
                DateTime dtNgaySinh;
                string nsStr = drv != null ? drv["NGAY_SINH"]?.ToString() : row.Cells["NGAY_SINH"].Value?.ToString();
                DateTime.TryParse(nsStr, out dtNgaySinh);
                Label lblNgaySinh = new Label { Text = "Ngày sinh:", Location = new Point(20, y + 3), AutoSize = true };
                DateTimePicker dtpNgaySinh = new DateTimePicker { Location = new Point(160, y), Size = new Size(230, 25), Format = DateTimePickerFormat.Short, Value = dtNgaySinh > DateTime.MinValue ? dtNgaySinh : DateTime.Now };
                f.Controls.Add(lblNgaySinh); f.Controls.Add(dtpNgaySinh); y += 40;
                
                string cccd = drv != null ? drv["CCCD"]?.ToString() : row.Cells["CCCD"].Value?.ToString();
                TextBox txtCCCD = AddField(f, "CCCD:", cccd, ref y);
                
                string sdt = drv != null ? drv["SDT"]?.ToString() : row.Cells["SDT"].Value?.ToString();
                TextBox txtSDT = AddField(f, "SĐT:", sdt, ref y);
                
                TextBox txtSoNha = AddField(f, "Số nhà:", row.Cells["SO_NHA"].Value?.ToString(), ref y);
                TextBox txtTenDuong = AddField(f, "Tên đường:", row.Cells["TEN_DUONG"].Value?.ToString(), ref y);
                TextBox txtQuanHuyen = AddField(f, "Quận/Huyện:", row.Cells["QUAN_HUYEN"].Value?.ToString(), ref y);
                TextBox txtTinhTp = AddField(f, "Tỉnh/TP:", row.Cells["TINH_TP"].Value?.ToString(), ref y);
                TextBox txtTienSu = AddField(f, "Tiền sử bệnh:", row.Cells["TIEN_SU_BENH"].Value?.ToString(), ref y);
                TextBox txtTienSuGD = AddField(f, "TS bệnh GĐ:", row.Cells["TIEN_SU_BENH_GD"].Value?.ToString(), ref y);
                TextBox txtDiUng = AddField(f, "Dị ứng thuốc:", row.Cells["DI_UNG_THUOC"].Value?.ToString(), ref y);
                
                string chuyenKhoa = drv != null ? drv["CHUYEN_KHOA"]?.ToString() : "";
                DataTable dtKhoa = null;
                try { dtKhoa = DbConnection.Instance.ExecuteQuery("SELECT MA_KHOA, TEN_KHOA FROM ADMIN_PH2.KHOA", null, CommandType.Text); } catch { }
                ComboBox cmbChuyenKhoa = null;
                TextBox txtChuyenKhoa = null;
                if (dtKhoa != null && dtKhoa.Rows.Count > 0)
                {
                    cmbChuyenKhoa = AddComboBoxField(f, "Chuyên khoa:", chuyenKhoa, dtKhoa, "MA_KHOA", "TEN_KHOA", ref y);
                }
                else
                {
                    txtChuyenKhoa = AddField(f, "Mã chuyên khoa:", chuyenKhoa, ref y);
                }

                Button btnSave = new Button { Text = "Update", Location = new Point(160, y + 10), Size = new Size(100, 35) };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MABN", txtMaBN.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_HOTEN", txtHoTen.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_PHAI", cmbPhai.SelectedItem?.ToString()),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_NGAYSINH", dtpNgaySinh.Value),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_CCCD", txtCCCD.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_SDT", txtSDT.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_SONHA", txtSoNha.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TENDUONG", txtTenDuong.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_QUANHUYEN", txtQuanHuyen.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TINHTP", txtTinhTp.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENH", txtTienSu.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENHGD", txtTienSuGD.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_DIUNGTHUOC", txtDiUng.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_CHUYENKHOA", cmbChuyenKhoa != null ? cmbChuyenKhoa.SelectedValue?.ToString() : (string.IsNullOrEmpty(txtChuyenKhoa.Text) ? (object)DBNull.Value : txtChuyenKhoa.Text))
                        };

                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Update_BENHNHAN(:p_MABN, :p_HOTEN, :p_PHAI, :p_NGAYSINH, :p_CCCD, :p_SDT, :p_SONHA, :p_TENDUONG, :p_QUANHUYEN, :p_TINHTP, :p_TIENSUBENH, :p_TIENSUBENHGD, :p_DIUNGTHUOC, :p_CHUYENKHOA); END;", parameters, CommandType.Text);
                        MessageBox.Show("Cập nhật thành công!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
                };
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK) LoadPatientData();
            }
        }

        private void BtnAddHSBA_Click(object sender, EventArgs e)
        {
            using (Form f = new Form() { Text = "Thêm Hồ sơ bệnh án", Size = new Size(440, 550), StartPosition = FormStartPosition.CenterParent, AutoScroll = true })
            {
                int y = 20;
                string newMaHSBA = GenerateNextId(dgvHSBA.DataSource as DataTable, "MA_HSBA", "HS", "D6");

                string defaultMaBN = "";
                if (dgvPatient != null && dgvPatient.SelectedRows.Count > 0) {
                    defaultMaBN = dgvPatient.SelectedRows[0].Cells["MA_BN"].Value?.ToString() ?? "";
                }

                TextBox txtMaHSBA = AddField(f, "Mã HSBA:", newMaHSBA, ref y, true);
                TextBox txtMaBN = AddField(f, "Mã BN:", defaultMaBN, ref y);
                
                Label lblNgay = new Label { Text = "Ngày khám:", Location = new Point(20, y + 3), AutoSize = true };
                DateTimePicker dtpNgay = new DateTimePicker { Location = new Point(160, y), Size = new Size(230, 25), Format = DateTimePickerFormat.Short };
                f.Controls.Add(lblNgay); f.Controls.Add(dtpNgay); y += 40;

                TextBox txtChanDoan = AddField(f, "Chẩn đoán:", "", ref y);
                TextBox txtDieuTri = AddField(f, "Điều trị:", "", ref y);
                TextBox txtMaBS = AddField(f, "Mã BS:", "", ref y);
                
                DataTable dtKhoa = null;
                try { dtKhoa = DbConnection.Instance.ExecuteQuery("SELECT MA_KHOA, TEN_KHOA FROM ADMIN_PH2.KHOA", null, CommandType.Text); } catch { }
                ComboBox cmbMaKhoa = null;
                TextBox txtMaKhoa = null;
                if (dtKhoa != null && dtKhoa.Rows.Count > 0)
                {
                    cmbMaKhoa = AddComboBoxField(f, "Khoa khám:", "", dtKhoa, "MA_KHOA", "TEN_KHOA", ref y);
                }
                else
                {
                    txtMaKhoa = AddField(f, "Mã khoa:", "", ref y);
                }
                
                TextBox txtKetLuan = AddField(f, "Kết luận:", "", ref y);

                Button btnSave = new Button { Text = "Save", Location = new Point(160, y + 10), Size = new Size(100, 35) };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAHSBA", txtMaHSBA.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MABN", txtMaBN.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_NGAY", dtpNgay.Value),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_CHANDOAN", txtChanDoan.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_DIEUTRI", txtDieuTri.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MABS", txtMaBS.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAKHOA", cmbMaKhoa != null ? cmbMaKhoa.SelectedValue?.ToString() : txtMaKhoa.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_KETLUAN", txtKetLuan.Text)
                        };
                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Insert_HSBA(:p_MAHSBA, :p_MABN, :p_NGAY, :p_CHANDOAN, :p_DIEUTRI, :p_MABS, :p_MAKHOA, :p_KETLUAN); END;", parameters, CommandType.Text);
                        MessageBox.Show("Thêm thành công!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
                };
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK) LoadHSBAData();
            }
        }

        private void BtnEditHSBA_Click(object sender, EventArgs e)
        {
            if (dgvHSBA.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn hồ sơ."); return; }
            var row = dgvHSBA.SelectedRows[0];

            using (Form f = new Form() { Text = "Cập nhật HSBA", Size = new Size(440, 330), StartPosition = FormStartPosition.CenterParent, AutoScroll = true })
            {
                int y = 20;
                TextBox txtMaHSBA = AddField(f, "Mã HSBA:", row.Cells["MA_HSBA"].Value?.ToString(), ref y, true);
                
                string currentKhoa = row.Cells["MA_KHOA"].Value?.ToString();
                DataTable dtKhoa = null;
                try { dtKhoa = DbConnection.Instance.ExecuteQuery("SELECT MA_KHOA, TEN_KHOA FROM ADMIN_PH2.KHOA", null, CommandType.Text); } catch { }
                ComboBox cmbMaKhoa = null;
                TextBox txtMaKhoa = null;
                if (dtKhoa != null && dtKhoa.Rows.Count > 0)
                {
                    cmbMaKhoa = AddComboBoxField(f, "Khoa khám:", currentKhoa, dtKhoa, "MA_KHOA", "TEN_KHOA", ref y);
                }
                else
                {
                    txtMaKhoa = AddField(f, "Mã khoa:", currentKhoa, ref y);
                }
                
                TextBox txtMaBS = AddField(f, "Mã BS:", row.Cells["MA_BS"].Value?.ToString(), ref y);

                Button btnSave = new Button { Text = "Update", Location = new Point(160, y + 10), Size = new Size(100, 35) };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAHSBA", txtMaHSBA.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAKHOA", cmbMaKhoa != null ? cmbMaKhoa.SelectedValue?.ToString() : txtMaKhoa.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MABS", txtMaBS.Text)
                        };
                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Update_HSBA(:p_MAHSBA, :p_MAKHOA, :p_MABS); END;", parameters, CommandType.Text);
                        MessageBox.Show("Cập nhật thành công!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
                };
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK) LoadHSBAData();
            }
        }

        private void BtnEditHSBADV_Click(object sender, EventArgs e)
        {
            if (dgvHSBADV.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn dịch vụ."); return; }
            var row = dgvHSBADV.SelectedRows[0];

            using (Form f = new Form() { Text = "Cập nhật dịch vụ", Size = new Size(440, 350), StartPosition = FormStartPosition.CenterParent, AutoScroll = true })
            {
                int y = 20;
                TextBox txtMaHSBA = AddField(f, "Mã HSBA:", row.Cells["MA_HSBA"].Value?.ToString(), ref y, true);
                TextBox txtLoaiDV = AddField(f, "Loại DV:", row.Cells["LOAI_DV"].Value?.ToString(), ref y, true);
                
                DateTime dtNgayDV;
                DateTime.TryParse(row.Cells["NGAY_DV"].Value?.ToString(), out dtNgayDV);
                
                Label lblNgay = new Label { Text = "Ngày DV:", Location = new Point(20, y + 3), AutoSize = true };
                DateTimePicker dtpNgay = new DateTimePicker { Location = new Point(160, y), Size = new Size(230, 25), Format = DateTimePickerFormat.Custom, CustomFormat="dd/MM/yyyy", Value = dtNgayDV > DateTime.MinValue ? dtNgayDV : DateTime.Now, Enabled = false };
                f.Controls.Add(lblNgay); f.Controls.Add(dtpNgay); y += 40;

                TextBox txtMaKTV = AddField(f, "Mã KTV mới:", row.Cells["MA_KTV"].Value?.ToString(), ref y);

                Button btnSave = new Button { Text = "Update", Location = new Point(160, y + 10), Size = new Size(100, 35) };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAHSBA", txtMaHSBA.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_LOAIDV", txtLoaiDV.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_NGAYDV", dtpNgay.Value),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAKTV", txtMaKTV.Text)
                        };
                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Update_HSBADV(:p_MAHSBA, :p_LOAIDV, :p_NGAYDV, :p_MAKTV); END;", parameters, CommandType.Text);
                        MessageBox.Show("Cập nhật thành công!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
                };
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK) LoadHSBAData();
            }
        }

        private void BtnEditProfile_Click(object sender, EventArgs e)
        {
            using (Form f = new Form()
            {
                Text = "Cập nhật thông tin cá nhân",
                Size = new Size(440, 290),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                AutoScroll = true
            })
            {
                int y = 20;

                string maNV = lblMaNVValue.Text;
                string queQuan = lblQueQuanValue.Text;
                string sdt = lblSDTValue.Text;

                TextBox txtMaNV = AddField(
                    f,
                    "Mã NV:",
                    maNV,
                    ref y,
                    true
                );

                TextBox txtQueQuan = AddField(
                    f,
                    "Quê quán:",
                    queQuan,
                    ref y
                );

                TextBox txtSDT = AddField(
                    f,
                    "SĐT:",
                    sdt,
                    ref y
                );

                Button btnSave = new Button
                {
                    Text = "UPDATE",
                    Size = new Size(120, 38),
                    BackColor = Color.DodgerBlue,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(160, y + 15)
                };

                btnSave.FlatAppearance.BorderSize = 0;

                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_QUEQUAN", txtQueQuan.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_SODT", txtSDT.Text)
                        };

                        DbConnection.Instance.ExecuteNonQuery(
                            "BEGIN sp_NV_Update_NHANVIEN(:p_QUEQUAN, :p_SODT); END;",
                            parameters,
                            CommandType.Text
                        );

                        MessageBox.Show("Cập nhật thành công!");

                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                };

                f.Controls.Add(btnSave);

                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadProfile();
                }
            }
        }
    }
}
