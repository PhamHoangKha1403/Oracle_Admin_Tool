using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.Components
{
    public class PatientView : UserControl
    {
        private Label lblTitle;
        private Button btnEdit;
        private Panel infoPanel;

        private TextBox txtSoNha;
        private TextBox txtTenDuong;
        private TextBox txtQuanHuyen;
        private TextBox txtTinhTp;
        private TextBox txtTienSu;
        private TextBox txtTienSuGD;
        private TextBox txtDiUng;

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

            infoPanel = new Panel
            {
                Location = new Point(20, 110),
                Size = new Size(550, 340),
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle
            };

            int y = 20;

            txtSoNha = AddInfoField(infoPanel, "House No:", ref y);
            txtTenDuong = AddInfoField(infoPanel, "Street:", ref y);
            txtQuanHuyen = AddInfoField(infoPanel, "District:", ref y);
            txtTinhTp = AddInfoField(infoPanel, "City/Prov:", ref y);
            txtTienSu = AddInfoField(infoPanel, "Medical History:", ref y);
            txtTienSuGD = AddInfoField(infoPanel, "Family History:", ref y);
            txtDiUng = AddInfoField(infoPanel, "Allergies:", ref y);

            this.Controls.Add(infoPanel);
        }

        private TextBox AddInfoField(Control parent, string label, ref int y)
        {
            Label lbl = new Label
            {
                Text = label,
                Location = new Point(20, y + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };

            TextBox txt = new TextBox
            {
                Location = new Point(170, y),
                Size = new Size(320, 27),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);

            y += 42;
            return txt;
        }

        private void LoadData()
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
                    "BEGIN sp_BN_Select_BENHNHAN(:p_cursor); END;",
                    new[] { pOut },
                    CommandType.Text);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txtSoNha.Text = row["SO_NHA"]?.ToString();
                    txtTenDuong.Text = row["TEN_DUONG"]?.ToString();
                    txtQuanHuyen.Text = row["QUAN_HUYEN"]?.ToString();
                    txtTinhTp.Text = row["TINH_TP"]?.ToString();
                    txtTienSu.Text = row["TIEN_SU_BENH"]?.ToString();
                    txtTienSuGD.Text = row["TIEN_SU_BENH_GD"]?.ToString();
                    txtDiUng.Text = row["DI_UNG_THUOC"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading patient data: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            using (Form editForm = new Form())
            {
                editForm.Text = "Edit Patient Information";
                editForm.Size = new Size(420, 500);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                editForm.MaximizeBox = false;
                editForm.BackColor = Color.White;

                int y = 20;

                TextBox editSoNha = AddEditField(editForm, "House No:", txtSoNha.Text, ref y);
                TextBox editTenDuong = AddEditField(editForm, "Street:", txtTenDuong.Text, ref y);
                TextBox editQuanHuyen = AddEditField(editForm, "District:", txtQuanHuyen.Text, ref y);
                TextBox editTinhTp = AddEditField(editForm, "City/Prov:", txtTinhTp.Text, ref y);
                TextBox editTienSu = AddEditField(editForm, "Medical History:", txtTienSu.Text, ref y);
                TextBox editTienSuGD = AddEditField(editForm, "Family History:", txtTienSuGD.Text, ref y);
                TextBox editDiUng = AddEditField(editForm, "Allergies:", txtDiUng.Text, ref y);

                Button btnSave = new Button
                {
                    Text = "Save Changes",
                    Location = new Point(140, y + 20),
                    Size = new Size(130, 40),
                    BackColor = Color.SeaGreen,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };

                btnSave.FlatAppearance.BorderSize = 0;

                btnSave.Click += (s, args) =>
                {
                    try
                    {
                        var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                        {
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_SONHA", editSoNha.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TENDUONG", editTenDuong.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_QUANHUYEN", editQuanHuyen.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TINHTP", editTinhTp.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENH", editTienSu.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_TIENSUBENHGD", editTienSuGD.Text),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("p_DIUNGTHUOC", editDiUng.Text)
                        };

                        DbConnection.Instance.ExecuteNonQuery(
                            "BEGIN sp_BN_Update_BENHNHAN(:p_SONHA, :p_TENDUONG, :p_QUANHUYEN, :p_TINHTP, :p_TIENSUBENH, :p_TIENSUBENHGD, :p_DIUNGTHUOC); END;",
                            parameters,
                            CommandType.Text);

                        MessageBox.Show(
                            "Updated successfully!",
                            "Information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        editForm.DialogResult = DialogResult.OK;
                        editForm.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Update error: " + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                };

                editForm.Controls.Add(btnSave);

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private TextBox AddEditField(Form form, string label, string value, ref int y)
        {
            Label lbl = new Label
            {
                Text = label,
                Location = new Point(20, y + 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };

            TextBox txt = new TextBox
            {
                Text = value,
                Location = new Point(150, y),
                Size = new Size(220, 27),
                Font = new Font("Segoe UI", 10F)
            };

            form.Controls.Add(lbl);
            form.Controls.Add(txt);

            y += 42;
            return txt;
        }
    }
}