using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.Components
{
    public class TechnicianView : UserControl
    {
        private TabControl tabControl;
        private TabPage tabServices;
        private TabPage tabAuditLog;
        private TabPage tabThongBao;

        // Services Tab Controls
        private DataGridView dgvServices;
        private Button btnUpdateResult;

        // Audit Log Tab Controls
        private DataGridView dgvAuditLog;

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

        public TechnicianView()
        {
            InitializeUI();
            LoadServicesData();
            LoadAuditLogData();
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

            tabServices = new TabPage("MY SERVICES");
            tabAuditLog = new TabPage("EDIT HISTORY");
            tabThongBao = new TabPage("NOTIFICATIONS");
            tabProfile = new TabPage("MY PROFILE");
            tabControl.TabPages.Add(tabServices);
            tabControl.TabPages.Add(tabAuditLog);
            tabControl.TabPages.Add(tabThongBao);
            tabControl.TabPages.Add(tabProfile);

            InitializeServicesTab();
            InitializeAuditLogTab();
            InitializeThongBaoTab();
            InitializeProfileTab();
        }

        private void InitializeServicesTab()
        {
            tabServices.Padding = new Padding(15);
            tabServices.BackColor = Color.White;

            FlowLayoutPanel pnlTopServices = new FlowLayoutPanel
            {
                Location = new Point(15, 15),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            btnUpdateResult = new Button 
            { 
                Text = "UPDATE RESULT", 
                Size = new Size(160, 40), 
                BackColor = Color.DodgerBlue, 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 0, 15, 0)
            };
            btnUpdateResult.FlatAppearance.BorderSize = 0;
            btnUpdateResult.Click += BtnUpdateResult_Click;

            Button btnSearchServices = new Button 
            { 
                Text = "SEARCH", 
                Size = new Size(150, 40), 
                BackColor = Color.SeaGreen, 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0)
            };
            btnSearchServices.FlatAppearance.BorderSize = 0;

            TextBox txtSearchServices = new TextBox 
            { 
                AutoSize = false,
                Size = new Size(400, 40),
                Font = new Font("Segoe UI", 12F),
                Margin = new Padding(0, 0, 0, 0)
            };
            
            btnSearchServices.Click += (s, e) =>
            {
                if (dgvServices.DataSource is DataTable dt)
                {
                    string keyword = txtSearchServices.Text.Trim().Replace("'", "''");
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

            pnlTopServices.Controls.Add(btnUpdateResult);
            pnlTopServices.Controls.Add(btnSearchServices);
            pnlTopServices.Controls.Add(txtSearchServices);

            dgvServices = new DataGridView
            {
                Location = new Point(15, 70),
                Size = new Size(800, 400),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvServices.DataBindingComplete += (s, e) =>
            {
                if (dgvServices.Columns.Contains("MA_HSBA")) dgvServices.Columns["MA_HSBA"].HeaderText = "Mã HSBA";
                if (dgvServices.Columns.Contains("MA_DV")) dgvServices.Columns["MA_DV"].HeaderText = "Mã DV";
                if (dgvServices.Columns.Contains("NGAY_DV")) dgvServices.Columns["NGAY_DV"].HeaderText = "Ngày DV";
                if (dgvServices.Columns.Contains("MA_KTV")) dgvServices.Columns["MA_KTV"].HeaderText = "Mã KTV";
                if (dgvServices.Columns.Contains("KET_QUA")) dgvServices.Columns["KET_QUA"].HeaderText = "Kết quả";
                if (dgvServices.Columns.Contains("LOAI_DV")) dgvServices.Columns["LOAI_DV"].HeaderText = "Loại DV";
            };

            tabServices.Controls.Add(pnlTopServices);
            tabServices.Controls.Add(dgvServices);
        }

        private void InitializeAuditLogTab()
        {
            tabAuditLog.Padding = new Padding(15);
            tabAuditLog.BackColor = Color.White;

            FlowLayoutPanel pnlTopAudit = new FlowLayoutPanel
            {
                Location = new Point(15, 15),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            Label lblTitle = new Label 
            { 
                Text = "Lịch sử chỉnh sửa", 
                AutoSize = true, 
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Margin = new Padding(0, 10, 15, 0) // Padding top to align with 40px buttons
            };

            Button btnSearchAudit = new Button 
            { 
                Text = "SEARCH", 
                Size = new Size(150, 40), 
                BackColor = Color.SeaGreen, 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0)
            };
            btnSearchAudit.FlatAppearance.BorderSize = 0;

            TextBox txtSearchAudit = new TextBox 
            { 
                AutoSize = false,
                Size = new Size(400, 40),
                Font = new Font("Segoe UI", 12F),
                Margin = new Padding(0, 0, 0, 0)
            };

            btnSearchAudit.Click += (s, e) =>
            {
                if (dgvAuditLog.DataSource is DataTable dt)
                {
                    string keyword = txtSearchAudit.Text.Trim().Replace("'", "''");
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

            pnlTopAudit.Controls.Add(lblTitle);
            pnlTopAudit.Controls.Add(btnSearchAudit);
            pnlTopAudit.Controls.Add(txtSearchAudit);

            dgvAuditLog = new DataGridView
            {
                Location = new Point(15, 70),
                Size = new Size(800, 420),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvAuditLog.DataBindingComplete += (s, e) =>
            {
                if (dgvAuditLog.Columns.Contains("MAHSBA")) dgvAuditLog.Columns["MAHSBA"].HeaderText = "Mã HSBA";
                if (dgvAuditLog.Columns.Contains("LOAIDV")) dgvAuditLog.Columns["LOAIDV"].HeaderText = "Loại DV";
                if (dgvAuditLog.Columns.Contains("NGAYDV")) dgvAuditLog.Columns["NGAYDV"].HeaderText = "Ngày DV";
                if (dgvAuditLog.Columns.Contains("OLD_KETQUA")) dgvAuditLog.Columns["OLD_KETQUA"].HeaderText = "Kết quả cũ";
                if (dgvAuditLog.Columns.Contains("NEW_KETQUA")) dgvAuditLog.Columns["NEW_KETQUA"].HeaderText = "Kết quả mới";
                if (dgvAuditLog.Columns.Contains("NGAY_GHI")) dgvAuditLog.Columns["NGAY_GHI"].HeaderText = "Ngày sửa";

                // Thêm các cột cho FGA Audit (DBA_AUDIT_TRAIL) phòng trường hợp DB chưa update SP
                if (dgvAuditLog.Columns.Contains("DB_USER")) dgvAuditLog.Columns["DB_USER"].HeaderText = "Người dùng DB";
                if (dgvAuditLog.Columns.Contains("POLICY_NAME")) dgvAuditLog.Columns["POLICY_NAME"].HeaderText = "Tên Policy";
                if (dgvAuditLog.Columns.Contains("STATEMENT_TYPE")) dgvAuditLog.Columns["STATEMENT_TYPE"].HeaderText = "Loại thao tác";
                if (dgvAuditLog.Columns.Contains("TIMESTAMP")) dgvAuditLog.Columns["TIMESTAMP"].HeaderText = "Thời gian";
                if (dgvAuditLog.Columns.Contains("OBJECT_NAME")) dgvAuditLog.Columns["OBJECT_NAME"].HeaderText = "Bảng";
                if (dgvAuditLog.Columns.Contains("SQL_TEXT")) dgvAuditLog.Columns["SQL_TEXT"].HeaderText = "Lệnh SQL";
            };

            tabAuditLog.Controls.Add(pnlTopAudit);
            tabAuditLog.Controls.Add(dgvAuditLog);
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

        private TextBox AddField(Form form, string label, string defaultValue, ref int y, bool isReadOnly = false)
        {
            Label lbl = new Label { Text = label, Location = new Point(20, y + 3), AutoSize = true };
            TextBox txt = new TextBox { Text = defaultValue, Location = new Point(140, y), Size = new Size(210, 25), ReadOnly = isReadOnly };
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
                Location = new Point(140, y), 
                Size = new Size(210, 25),
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

        private void BtnEditProfile_Click(object sender, EventArgs e)
        {
            using (Form f = new Form()
            {
                Text = "Cập nhật thông tin cá nhân",
                Size = new Size(400, 260),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
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
                    Location = new Point(140, y + 15)
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

        private void LoadServicesData()
        {
            try
            {
                var pOut = new Oracle.ManagedDataAccess.Client.OracleParameter("p_cursor", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                dgvServices.DataSource = DbConnection.Instance.ExecuteQuery("BEGIN sp_KTV_Select_HSBADV(:p_cursor); END;", new[] { pOut }, CommandType.Text);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải danh sách dịch vụ: " + ex.Message); }
        }

        private void LoadAuditLogData()
        {
            try
            {
                var pOut = new Oracle.ManagedDataAccess.Client.OracleParameter("p_cursor", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                dgvAuditLog.DataSource = DbConnection.Instance.ExecuteQuery("BEGIN sp_KTV_Select_AuditLog(:p_cursor); END;", new[] { pOut }, CommandType.Text);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải lịch sử chỉnh sửa: " + ex.Message); }
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
                MessageBox.Show("Error loading notifications: " + ex.Message);
            }
        }

        private void BtnUpdateResult_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count == 0) 
            { 
                MessageBox.Show("Vui lòng chọn một dịch vụ để cập nhật."); 
                return; 
            }
            var row = dgvServices.SelectedRows[0];
            string currentKetQua = row.Cells["KET_QUA"].Value?.ToString() ?? "";

            using (Form f = new Form() { Text = "Cập nhật kết quả", Size = new Size(400, 300), StartPosition = FormStartPosition.CenterParent })
            {
                int y = 20;
                
                Label lblMaHSBA = new Label { Text = "Mã HSBA:", Location = new Point(20, y + 3), AutoSize = true };
                TextBox txtMaHSBA = new TextBox { Text = row.Cells["MA_HSBA"].Value?.ToString(), Location = new Point(140, y), Size = new Size(210, 25), ReadOnly = true };
                f.Controls.Add(lblMaHSBA); f.Controls.Add(txtMaHSBA); y += 40;

                Label lblLoaiDV = new Label { Text = "Loại DV:", Location = new Point(20, y + 3), AutoSize = true };
                TextBox txtLoaiDV = new TextBox { Text = row.Cells["LOAI_DV"].Value?.ToString(), Location = new Point(140, y), Size = new Size(210, 25), ReadOnly = true };
                f.Controls.Add(lblLoaiDV); f.Controls.Add(txtLoaiDV); y += 40;
                
                DateTime dtNgayDV = row.Cells["NGAY_DV"].Value is DateTime dt 
                    ? dt 
                    : (DateTime.TryParse(row.Cells["NGAY_DV"].Value?.ToString(), out DateTime parsed) ? parsed : DateTime.Now);
                Label lblKetQua = new Label { Text = "Kết quả:", Location = new Point(20, y + 3), AutoSize = true };
                TextBox txtKetQua = new TextBox { Text = currentKetQua, Location = new Point(140, y), Size = new Size(210, 25) };
                f.Controls.Add(lblKetQua); f.Controls.Add(txtKetQua); y += 40;

                Button btnSave = new Button { Text = "Update", Location = new Point(140, y + 10), Size = new Size(100, 35), BackColor = Color.DodgerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var updateParams = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAHSBA", txtMaHSBA.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_LOAIDV", txtLoaiDV.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_NGAYDV", dtNgayDV),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_KETQUA", txtKetQua.Text)
                        };

                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_KTV_Update_KETQUA(:p_MAHSBA, :p_LOAIDV, :p_NGAYDV, :p_KETQUA); END;", updateParams, CommandType.Text);

                        MessageBox.Show("Cập nhật kết quả thành công!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
                };
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK) 
                {
                    LoadServicesData();
                    LoadAuditLogData();
                }
            }
        }
    }
}
