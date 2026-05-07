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

        // Services Tab Controls
        private DataGridView dgvServices;
        private Button btnUpdateResult;

        // Audit Log Tab Controls
        private DataGridView dgvAuditLog;

        public TechnicianView()
        {
            InitializeUI();
            LoadServicesData();
            LoadAuditLogData();
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
            tabControl.TabPages.Add(tabServices);
            tabControl.TabPages.Add(tabAuditLog);

            InitializeServicesTab();
            InitializeAuditLogTab();
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
            };

            tabAuditLog.Controls.Add(pnlTopAudit);
            tabAuditLog.Controls.Add(dgvAuditLog);
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
