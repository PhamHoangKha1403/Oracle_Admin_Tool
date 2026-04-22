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

        public CoordinatorView()
        {
            InitializeUI();
            LoadPatientData();
            LoadHSBAData();
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
            tabControl.TabPages.Add(tabPatient);
            tabControl.TabPages.Add(tabHSBA);

            InitializePatientTab();
            InitializeHSBATab();
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

            btnAddPatient = new Button { Text = "ADD PATIENT", Size = new Size(200, 40), BackColor = Color.SeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 0, 15, 0) };
            btnEditPatient = new Button { Text = "EDIT PATIENT", Size = new Size(200, 40), BackColor = Color.DodgerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0) };

            btnAddPatient.FlatAppearance.BorderSize = 0;
            btnEditPatient.FlatAppearance.BorderSize = 0;

            btnAddPatient.Click += BtnAddPatient_Click;
            btnEditPatient.Click += BtnEditPatient_Click;

            pnlButtons.Controls.Add(btnAddPatient);
            pnlButtons.Controls.Add(btnEditPatient);
            tabPatient.Controls.Add(pnlButtons);

            dgvPatient = new DataGridView
            {
                Location = new Point(15, 65),
                Size = new Size(800, 300),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
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

            btnAddHSBA = new Button { Text = "ADD RECORD", Size = new Size(200, 40), BackColor = Color.SeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 0, 15, 0) };
            btnEditHSBA = new Button { Text = "EDIT RECORD", Size = new Size(200, 40), BackColor = Color.DodgerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0, 0, 15, 0) };
            btnEditHSBADV = new Button { Text = "EDIT SERVICE", Size = new Size(200, 40), BackColor = Color.Orange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0) };

            btnAddHSBA.FlatAppearance.BorderSize = 0;
            btnEditHSBA.FlatAppearance.BorderSize = 0;
            btnEditHSBADV.FlatAppearance.BorderSize = 0;

            btnAddHSBA.Click += BtnAddHSBA_Click;
            btnEditHSBA.Click += BtnEditHSBA_Click;
            btnEditHSBADV.Click += BtnEditHSBADV_Click;

            pnlHSBAButtons.Controls.Add(btnAddHSBA);
            pnlHSBAButtons.Controls.Add(btnEditHSBA);
            pnlHSBAButtons.Controls.Add(btnEditHSBADV);
            tabHSBA.Controls.Add(pnlHSBAButtons);

            Label lbl1 = new Label { Text = "Medical Records List", Location = new Point(15, 60), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            tabHSBA.Controls.Add(lbl1);

            dgvHSBA = new DataGridView
            {
                Location = new Point(15, 85),
                Size = new Size(800, 150),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            tabHSBA.Controls.Add(dgvHSBA);

            Label lbl2 = new Label { Text = "Medical Services List", Location = new Point(15, 250), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            tabHSBA.Controls.Add(lbl2);

            dgvHSBADV = new DataGridView
            {
                Location = new Point(15, 275),
                Size = new Size(800, 150),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            tabHSBA.Controls.Add(dgvHSBADV);
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

        private TextBox AddField(Form form, string label, string defaultValue, ref int y, bool isReadOnly = false)
        {
            Label lbl = new Label { Text = label, Location = new Point(20, y + 3), AutoSize = true };
            TextBox txt = new TextBox { Text = defaultValue, Location = new Point(140, y), Size = new Size(210, 25), ReadOnly = isReadOnly };
            form.Controls.Add(lbl);
            form.Controls.Add(txt);
            y += 40;
            return txt;
        }

        private void BtnAddPatient_Click(object sender, EventArgs e)
        {
            using (Form f = new Form() { Text = "Add Patient", Size = new Size(400, 520), StartPosition = FormStartPosition.CenterParent })
            {
                int y = 20;
                TextBox txtMaBN = AddField(f, "Patient ID:", "", ref y);
                TextBox txtSoNha = AddField(f, "House No:", "", ref y);
                TextBox txtTenDuong = AddField(f, "Street:", "", ref y);
                TextBox txtQuanHuyen = AddField(f, "District:", "", ref y);
                TextBox txtTinhTp = AddField(f, "City/Prov:", "", ref y);
                TextBox txtTienSu = AddField(f, "Med History:", "", ref y);
                TextBox txtTienSuGD = AddField(f, "Fam History:", "", ref y);
                TextBox txtDiUng = AddField(f, "Allergies:", "", ref y);

                Button btnSave = new Button { Text = "Save", Location = new Point(140, y + 10), Size = new Size(100, 35) };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MABN", txtMaBN.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_SONHA", txtSoNha.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TENDUONG", txtTenDuong.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_QUANHUYEN", txtQuanHuyen.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TINHTP", txtTinhTp.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENH", txtTienSu.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENHGD", txtTienSuGD.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_DIUNGTHUOC", txtDiUng.Text)
                        };

                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Insert_BENHNHAN(:p_MABN, :p_SONHA, :p_TENDUONG, :p_QUANHUYEN, :p_TINHTP, :p_TIENSUBENH, :p_TIENSUBENHGD, :p_DIUNGTHUOC); END;", parameters, CommandType.Text);
                        MessageBox.Show("Added successfully!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                };
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK) LoadPatientData();
            }
        }

        private void BtnEditPatient_Click(object sender, EventArgs e)
        {
            if (dgvPatient.SelectedRows.Count == 0) { MessageBox.Show("Select a patient."); return; }
            var row = dgvPatient.SelectedRows[0];

            using (Form f = new Form() { Text = "Edit Patient", Size = new Size(400, 520), StartPosition = FormStartPosition.CenterParent })
            {
                int y = 20;
                TextBox txtMaBN = AddField(f, "Patient ID:", row.Cells["MA_BN"].Value?.ToString(), ref y, true);
                TextBox txtSoNha = AddField(f, "House No:", row.Cells["SO_NHA"].Value?.ToString(), ref y);
                TextBox txtTenDuong = AddField(f, "Street:", row.Cells["TEN_DUONG"].Value?.ToString(), ref y);
                TextBox txtQuanHuyen = AddField(f, "District:", row.Cells["QUAN_HUYEN"].Value?.ToString(), ref y);
                TextBox txtTinhTp = AddField(f, "City/Prov:", row.Cells["TINH_TP"].Value?.ToString(), ref y);
                TextBox txtTienSu = AddField(f, "Med History:", row.Cells["TIEN_SU_BENH"].Value?.ToString(), ref y);
                TextBox txtTienSuGD = AddField(f, "Fam History:", row.Cells["TIEN_SU_BENH_GD"].Value?.ToString(), ref y);
                TextBox txtDiUng = AddField(f, "Allergies:", row.Cells["DI_UNG_THUOC"].Value?.ToString(), ref y);

                Button btnSave = new Button { Text = "Update", Location = new Point(140, y + 10), Size = new Size(100, 35) };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MABN", txtMaBN.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_SONHA", txtSoNha.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TENDUONG", txtTenDuong.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_QUANHUYEN", txtQuanHuyen.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TINHTP", txtTinhTp.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENH", txtTienSu.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENHGD", txtTienSuGD.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_DIUNGTHUOC", txtDiUng.Text)
                        };

                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Update_BENHNHAN(:p_MABN, :p_SONHA, :p_TENDUONG, :p_QUANHUYEN, :p_TINHTP, :p_TIENSUBENH, :p_TIENSUBENHGD, :p_DIUNGTHUOC); END;", parameters, CommandType.Text);
                        MessageBox.Show("Updated successfully!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                };
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK) LoadPatientData();
            }
        }

        private void BtnAddHSBA_Click(object sender, EventArgs e)
        {
            using (Form f = new Form() { Text = "Add Record", Size = new Size(400, 520), StartPosition = FormStartPosition.CenterParent })
            {
                int y = 20;
                TextBox txtMaHSBA = AddField(f, "Record ID:", "", ref y);
                TextBox txtMaBN = AddField(f, "Patient ID:", "", ref y);
                
                Label lblNgay = new Label { Text = "Date:", Location = new Point(20, y + 3), AutoSize = true };
                DateTimePicker dtpNgay = new DateTimePicker { Location = new Point(140, y), Size = new Size(210, 25), Format = DateTimePickerFormat.Short };
                f.Controls.Add(lblNgay); f.Controls.Add(dtpNgay); y += 40;

                TextBox txtChanDoan = AddField(f, "Diagnosis:", "", ref y);
                TextBox txtDieuTri = AddField(f, "Treatment:", "", ref y);
                TextBox txtMaBS = AddField(f, "Doctor ID:", "", ref y);
                TextBox txtMaKhoa = AddField(f, "Department ID:", "", ref y);
                TextBox txtKetLuan = AddField(f, "Conclusion:", "", ref y);

                Button btnSave = new Button { Text = "Save", Location = new Point(140, y + 10), Size = new Size(100, 35) };
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
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAKHOA", txtMaKhoa.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_KETLUAN", txtKetLuan.Text)
                        };
                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Insert_HSBA(:p_MAHSBA, :p_MABN, :p_NGAY, :p_CHANDOAN, :p_DIEUTRI, :p_MABS, :p_MAKHOA, :p_KETLUAN); END;", parameters, CommandType.Text);
                        MessageBox.Show("Record added successfully!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                };
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK) LoadHSBAData();
            }
        }

        private void BtnEditHSBA_Click(object sender, EventArgs e)
        {
            if (dgvHSBA.SelectedRows.Count == 0) { MessageBox.Show("Select a record."); return; }
            var row = dgvHSBA.SelectedRows[0];

            using (Form f = new Form() { Text = "Update Record", Size = new Size(400, 300), StartPosition = FormStartPosition.CenterParent })
            {
                int y = 20;
                TextBox txtMaHSBA = AddField(f, "Record ID:", row.Cells["MA_HSBA"].Value?.ToString(), ref y, true);
                TextBox txtMaKhoa = AddField(f, "Department ID:", row.Cells["MA_KHOA"].Value?.ToString(), ref y);
                TextBox txtMaBS = AddField(f, "Doctor ID:", row.Cells["MA_BS"].Value?.ToString(), ref y);

                Button btnSave = new Button { Text = "Update", Location = new Point(140, y + 10), Size = new Size(100, 35) };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAHSBA", txtMaHSBA.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAKHOA", txtMaKhoa.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MABS", txtMaBS.Text)
                        };
                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Update_HSBA(:p_MAHSBA, :p_MAKHOA, :p_MABS); END;", parameters, CommandType.Text);
                        MessageBox.Show("Updated successfully!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                };
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK) LoadHSBAData();
            }
        }

        private void BtnEditHSBADV_Click(object sender, EventArgs e)
        {
            if (dgvHSBADV.SelectedRows.Count == 0) { MessageBox.Show("Select a service record."); return; }
            var row = dgvHSBADV.SelectedRows[0];

            using (Form f = new Form() { Text = "Update Technician Service", Size = new Size(400, 320), StartPosition = FormStartPosition.CenterParent })
            {
                int y = 20;
                TextBox txtMaHSBA = AddField(f, "Record ID:", row.Cells["MA_HSBA"].Value?.ToString(), ref y, true);
                TextBox txtLoaiDV = AddField(f, "Service Type:", row.Cells["LOAI_DV"].Value?.ToString(), ref y, true);
                
                DateTime dtNgayDV;
                DateTime.TryParse(row.Cells["NGAY_DV"].Value?.ToString(), out dtNgayDV);
                
                Label lblNgay = new Label { Text = "Service Date:", Location = new Point(20, y + 3), AutoSize = true };
                DateTimePicker dtpNgay = new DateTimePicker { Location = new Point(140, y), Size = new Size(210, 25), Format = DateTimePickerFormat.Custom, CustomFormat="dd/MM/yyyy", Value = dtNgayDV > DateTime.MinValue ? dtNgayDV : DateTime.Now, Enabled = false };
                f.Controls.Add(lblNgay); f.Controls.Add(dtpNgay); y += 40;

                TextBox txtMaKTV = AddField(f, "New Tech ID:", row.Cells["MA_KTV"].Value?.ToString(), ref y);

                Button btnSave = new Button { Text = "Update", Location = new Point(140, y + 10), Size = new Size(100, 35) };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAHSBA", txtMaHSBA.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_LOAIDV", txtLoaiDV.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_NGAYDV", dtpNgay.Value.Date),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAKTV", txtMaKTV.Text)
                        };
                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Update_HSBADV(:p_MAHSBA, :p_LOAIDV, :p_NGAYDV, :p_MAKTV); END;", parameters, CommandType.Text);
                        MessageBox.Show("Updated successfully!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                };
                f.Controls.Add(btnSave);
                if (f.ShowDialog() == DialogResult.OK) LoadHSBAData();
            }
        }
    }
}
