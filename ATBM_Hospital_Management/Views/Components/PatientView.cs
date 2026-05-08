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

        private TextBox txtHoTen;
        private TextBox txtPhai;
        private TextBox txtNgaySinh;
        private TextBox txtCCCD;
        private TextBox txtSDT;
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
                Text = "Thông tin bệnh nhân",
                Font = new Font("Segoe UI Semibold", 16F),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            this.Controls.Add(lblTitle);

            btnEdit = new Button
            {
                Text = "Chỉnh sửa thông tin",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 60),
                Size = new Size(200, 50),
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
                Size = new Size(550, 520),
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true
            };

            int y = 20;

            txtHoTen = AddInfoField(infoPanel, "Họ tên:", ref y);
            txtPhai = AddInfoField(infoPanel, "Phái:", ref y);
            txtNgaySinh = AddInfoField(infoPanel, "Ngày sinh:", ref y);
            txtCCCD = AddInfoField(infoPanel, "CCCD:", ref y);
            txtSDT = AddInfoField(infoPanel, "SĐT:", ref y);
            txtSoNha = AddInfoField(infoPanel, "Số nhà:", ref y);
            txtTenDuong = AddInfoField(infoPanel, "Tên đường:", ref y);
            txtQuanHuyen = AddInfoField(infoPanel, "Quận/Huyện:", ref y);
            txtTinhTp = AddInfoField(infoPanel, "Tỉnh/TP:", ref y);
            txtTienSu = AddInfoField(infoPanel, "Tiền sử bệnh:", ref y);
            txtTienSuGD = AddInfoField(infoPanel, "TS bệnh gia đình:", ref y);
            txtDiUng = AddInfoField(infoPanel, "Dị ứng thuốc:", ref y);

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

                string owner = DbConnection.Instance.GetTableOwner();
                DataTable dt = DbConnection.Instance.ExecuteQuery(
                    $"BEGIN {owner}.sp_BN_Select_BENHNHAN(:p_cursor); END;",
                    new[] { pOut },
                    CommandType.Text);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txtHoTen.Text = row["HO_TEN"]?.ToString();
                    txtPhai.Text = row["PHAI"]?.ToString();
                    
                    if (DateTime.TryParse(row["NGAY_SINH"]?.ToString(), out DateTime dtNgaySinh))
                        txtNgaySinh.Text = dtNgaySinh.ToString("dd/MM/yyyy");
                    else
                        txtNgaySinh.Text = row["NGAY_SINH"]?.ToString();
                        
                    txtCCCD.Text = row["CCCD"]?.ToString();
                    txtSDT.Text = row["SDT"]?.ToString();
                    
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
                    "Lỗi tải dữ liệu bệnh nhân: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            using (Form editForm = new Form())
            {
                editForm.Text = "Cập nhật thông tin bệnh nhân";
                editForm.Size = new Size(420, 500);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                editForm.MaximizeBox = false;
                editForm.BackColor = Color.White;

                int y = 20;

                TextBox editSoNha = AddEditField(editForm, "Số nhà:", txtSoNha.Text, ref y);
                TextBox editTenDuong = AddEditField(editForm, "Tên đường:", txtTenDuong.Text, ref y);
                TextBox editQuanHuyen = AddEditField(editForm, "Quận/Huyện:", txtQuanHuyen.Text, ref y);
                TextBox editTinhTp = AddEditField(editForm, "Tỉnh/TP:", txtTinhTp.Text, ref y);
                TextBox editTienSu = AddEditField(editForm, "Tiền sử bệnh:", txtTienSu.Text, ref y);
                TextBox editTienSuGD = AddEditField(editForm, "TS bệnh GĐ:", txtTienSuGD.Text, ref y);
                TextBox editDiUng = AddEditField(editForm, "Dị ứng thuốc:", txtDiUng.Text, ref y);

                Button btnSave = new Button
                {
                    Text = "Lưu",
                    Location = new Point(140, y + 20),
                    Size = new Size(120, 40),
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

                        string owner = DbConnection.Instance.GetTableOwner();
                        DbConnection.Instance.ExecuteNonQuery(
                            $"BEGIN {owner}.sp_BN_Update_BENHNHAN(:p_SONHA, :p_TENDUONG, :p_QUANHUYEN, :p_TINHTP, :p_TIENSUBENH, :p_TIENSUBENHGD, :p_DIUNGTHUOC); END;",
                            parameters,
                            CommandType.Text);

                        MessageBox.Show(
                            "Cập nhật thông tin bệnh nhân thành công!",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        editForm.DialogResult = DialogResult.OK;
                        editForm.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Lỗi khi cập nhật thông tin bệnh nhân: " + ex.Message,
                            "Lỗi",
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