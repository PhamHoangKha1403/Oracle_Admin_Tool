using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.Components
{
    public class PatientView : UserControl
    {
        private DataGridView dgvPatient;
        private Button btnEdit;
        private Label lblTitle;

        public PatientView()
        {
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;
            this.Padding = new Padding(20);

            lblTitle = new Label
            {
                Text = "Patient Diagnostic Information",
                Font = new Font("Segoe UI Semibold", 16F),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            this.Controls.Add(lblTitle);

            btnEdit = new Button
            {
                Text = "Edit Information",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 60),
                Size = new Size(180, 35),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += BtnEdit_Click;
            this.Controls.Add(btnEdit);

            dgvPatient = new DataGridView
            {
                Location = new Point(20, 110),
                Size = new Size(800, 150),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(dgvPatient);
        }

        private void LoadData()
        {
            try
            {
                var pOut = new Oracle.ManagedDataAccess.Client.OracleParameter("p_cursor", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor) { Direction = ParameterDirection.Output };
                DataTable dt = DbConnection.Instance.ExecuteQuery("BEGIN sp_BN_Select_BENHNHAN(:p_cursor); END;", new[] { pOut }, CommandType.Text);
                dgvPatient.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patient data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPatient.Rows.Count == 0) return;

            var row = dgvPatient.Rows[0]; 
            
            using (Form editForm = new Form())
            {
                editForm.Text = "Edit Patient Record";
                editForm.Size = new Size(400, 500);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                editForm.MaximizeBox = false;

                int y = 20;
                TextBox txtSoNha = AddField(editForm, "House No:", row.Cells["SO_NHA"].Value?.ToString(), ref y);
                TextBox txtTenDuong = AddField(editForm, "Street:", row.Cells["TEN_DUONG"].Value?.ToString(), ref y);
                TextBox txtQuanHuyen = AddField(editForm, "District:", row.Cells["QUAN_HUYEN"].Value?.ToString(), ref y);
                TextBox txtTinhTp = AddField(editForm, "City/Prov:", row.Cells["TINH_TP"].Value?.ToString(), ref y);
                TextBox txtTienSu = AddField(editForm, "Medical History:", row.Cells["TIEN_SU_BENH"].Value?.ToString(), ref y);
                TextBox txtTienSuGD = AddField(editForm, "Family History:", row.Cells["TIEN_SU_BENH_GD"].Value?.ToString(), ref y);
                TextBox txtDiUng = AddField(editForm, "Allergies:", row.Cells["DI_UNG_THUOC"].Value?.ToString(), ref y);

                Button btnSave = new Button { Text = "Save Changes", Location = new Point(140, y + 20), Size = new Size(120, 35) };
                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_SONHA", txtSoNha.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TENDUONG", txtTenDuong.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_QUANHUYEN", txtQuanHuyen.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TINHTP", txtTinhTp.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENH", txtTienSu.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENHGD", txtTienSuGD.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_DIUNGTHUOC", txtDiUng.Text)
                        };

                        DbConnection.Instance.ExecuteNonQuery("BEGIN sp_BN_Update_BENHNHAN(:p_SONHA, :p_TENDUONG, :p_QUANHUYEN, :p_TINHTP, :p_TIENSUBENH, :p_TIENSUBENHGD, :p_DIUNGTHUOC); END;", parameters, CommandType.Text);
                        MessageBox.Show("Updated successfully!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        editForm.DialogResult = DialogResult.OK;
                        editForm.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Update error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                editForm.Controls.Add(btnSave);

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private TextBox AddField(Form form, string label, string value, ref int y)
        {
            Label lbl = new Label { Text = label, Location = new Point(20, y + 3), AutoSize = true };
            TextBox txt = new TextBox { Text = value, Location = new Point(130, y), Size = new Size(220, 25) };
            form.Controls.Add(lbl);
            form.Controls.Add(txt);
            y += 40;
            return txt;
        }
    }
}
