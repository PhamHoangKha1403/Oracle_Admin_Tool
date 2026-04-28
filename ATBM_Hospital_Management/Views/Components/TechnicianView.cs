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

            btnUpdateResult = new Button 
            { 
                Text = "UPDATE RESULT", 
                Size = new Size(200, 40), 
                Location = new Point(15, 15),
                BackColor = Color.DodgerBlue, 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat 
            };
            btnUpdateResult.FlatAppearance.BorderSize = 0;
            btnUpdateResult.Click += BtnUpdateResult_Click;

            tabServices.Controls.Add(btnUpdateResult);

            dgvServices = new DataGridView
            {
                Location = new Point(15, 65),
                Size = new Size(800, 400),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            tabServices.Controls.Add(dgvServices);
        }

        private void InitializeAuditLogTab()
        {
            tabAuditLog.Padding = new Padding(15);
            tabAuditLog.BackColor = Color.White;

            Label lblTitle = new Label 
            { 
                Text = "Edit History (Audit Log)", 
                Location = new Point(15, 15), 
                AutoSize = true, 
                Font = new Font("Segoe UI", 12F, FontStyle.Bold) 
            };
            tabAuditLog.Controls.Add(lblTitle);

            dgvAuditLog = new DataGridView
            {
                Location = new Point(15, 45),
                Size = new Size(800, 420),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
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
            catch (Exception ex) { MessageBox.Show("Error loading services: " + ex.Message); }
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
            catch (Exception ex) { MessageBox.Show("Error loading audit log: " + ex.Message); }
        }

        private void BtnUpdateResult_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count == 0) 
            { 
                MessageBox.Show("Please select a service record to update."); 
                return; 
            }
            var row = dgvServices.SelectedRows[0];
            string currentKetQua = row.Cells["KET_QUA"].Value?.ToString() ?? "";

            using (Form f = new Form() { Text = "Update Result", Size = new Size(400, 300), StartPosition = FormStartPosition.CenterParent })
            {
                int y = 20;
                
                Label lblMaHSBA = new Label { Text = "Record ID:", Location = new Point(20, y + 3), AutoSize = true };
                TextBox txtMaHSBA = new TextBox { Text = row.Cells["MA_HSBA"].Value?.ToString(), Location = new Point(140, y), Size = new Size(210, 25), ReadOnly = true };
                f.Controls.Add(lblMaHSBA); f.Controls.Add(txtMaHSBA); y += 40;

                Label lblLoaiDV = new Label { Text = "Service Type:", Location = new Point(20, y + 3), AutoSize = true };
                TextBox txtLoaiDV = new TextBox { Text = row.Cells["LOAI_DV"].Value?.ToString(), Location = new Point(140, y), Size = new Size(210, 25), ReadOnly = true };
                f.Controls.Add(lblLoaiDV); f.Controls.Add(txtLoaiDV); y += 40;
                
                DateTime dtNgayDV;
                DateTime.TryParse(row.Cells["NGAY_DV"].Value?.ToString(), out dtNgayDV);

                Label lblKetQua = new Label { Text = "Result:", Location = new Point(20, y + 3), AutoSize = true };
                TextBox txtKetQua = new TextBox { Text = currentKetQua, Location = new Point(140, y), Size = new Size(210, 25) };
                f.Controls.Add(lblKetQua); f.Controls.Add(txtKetQua); y += 40;

                Button btnSave = new Button { Text = "Update", Location = new Point(140, y + 10), Size = new Size(100, 35), BackColor = Color.DodgerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        // 1. Update KETQUA
                        var updateParams = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAHSBA", txtMaHSBA.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_LOAIDV", txtLoaiDV.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_NGAYDV", dtNgayDV.Date),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_KETQUA", txtKetQua.Text)
                        };

                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_KTV_Update_KETQUA(:p_MAHSBA, :p_LOAIDV, :p_NGAYDV, :p_KETQUA); END;", updateParams, CommandType.Text);

                        // 2. Log Audit in C# (Ghi vết bằng C#)
                        string currentUser = DbConnection.Instance.GetCurrentUser();
                        var auditParams = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAHSBA", txtMaHSBA.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_LOAIDV", txtLoaiDV.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_NGAYDV", dtNgayDV.Date),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_MAKTV", currentUser),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_OLDKETQUA", currentKetQua),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_NEWKETQUA", txtKetQua.Text)
                        };

                        string auditSql = "INSERT INTO AUDIT_HSBADV_LOG (MAHSBA, LOAIDV, NGAYDV, MAKTV, OLD_KETQUA, NEW_KETQUA) " +
                                          "VALUES (:p_MAHSBA, :p_LOAIDV, :p_NGAYDV, :p_MAKTV, :p_OLDKETQUA, :p_NEWKETQUA)";
                        
                        DbConnection.Instance.ExecuteNonQuery(auditSql, auditParams, CommandType.Text);

                        MessageBox.Show("Result updated and audited successfully!");
                        f.DialogResult = DialogResult.OK;
                        f.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
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
