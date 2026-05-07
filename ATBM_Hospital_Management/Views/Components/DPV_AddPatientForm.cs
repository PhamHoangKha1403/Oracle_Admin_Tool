using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.Components
{
    public partial class DPV_AddPatientForm : Form
    {
        private DataTable _dtPatients;

        public DPV_AddPatientForm(DataTable dtPatients)
        {
            InitializeComponent();
            _dtPatients = dtPatients;
        }

        private void DPV_AddPatientForm_Load(object sender, EventArgs e)
        {
            // 1. Generate Next Patient ID
            txtMaBN.Text = GenerateNextId(_dtPatients, "MA_BN", "BN", "D6");

            // 2. Load Gender Dropdown Defaults
            cmbPhai.SelectedIndex = 0; // Default to "Nam"

            // 3. Load Departments Dropdown
            DataTable dtKhoa = null;
            try
            {
                dtKhoa = DbConnection.Instance.ExecuteQuery("SELECT MA_KHOA, TEN_KHOA FROM ADMIN_PH2.KHOA", null, CommandType.Text);
            }
            catch { }

            if (dtKhoa != null && dtKhoa.Rows.Count > 0)
            {
                cmbChuyenKhoa.DataSource = dtKhoa;
                cmbChuyenKhoa.ValueMember = "MA_KHOA";
                cmbChuyenKhoa.DisplayMember = "TEN_KHOA";
            }
            else
            {
                // FALLBACK: If DB synonym/permissions aren't compiled yet, guarantee dropdown options using hardcoded known defaults!
                DataTable dtFallback = new DataTable();
                dtFallback.Columns.Add("MA_KHOA");
                dtFallback.Columns.Add("TEN_KHOA");
                dtFallback.Rows.Add("TH", "Khoa tiêu hóa (TH)");
                dtFallback.Rows.Add("TK", "Khoa thần kinh (TK)");
                dtFallback.Rows.Add("TM", "Khoa tim mạch (TM)");

                cmbChuyenKhoa.DataSource = dtFallback;
                cmbChuyenKhoa.ValueMember = "MA_KHOA";
                cmbChuyenKhoa.DisplayMember = "TEN_KHOA";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.");
                return;
            }

            try
            {
                var parameters = new OracleParameter[]
                {
                    new OracleParameter("p_MABN", txtMaBN.Text),
                    new OracleParameter("p_HOTEN", txtHoTen.Text),
                    new OracleParameter("p_PHAI", cmbPhai.SelectedItem?.ToString()),
                    new OracleParameter("p_NGAYSINH", dtpNgaySinh.Value),
                    new OracleParameter("p_CCCD", txtCCCD.Text),
                    new OracleParameter("p_SDT", txtSDT.Text),
                    new OracleParameter("p_SONHA", txtSoNha.Text),
                    new OracleParameter("p_TENDUONG", txtTenDuong.Text),
                    new OracleParameter("p_QUANHUYEN", txtQuanHuyen.Text),
                    new OracleParameter("p_TINHTP", txtTinhTp.Text),
                    new OracleParameter("p_TIENSUBENH", txtTienSu.Text),
                    new OracleParameter("p_TIENSUBENHGD", txtTienSuGD.Text),
                    new OracleParameter("p_DIUNGTHUOC", txtDiUng.Text),
                    new OracleParameter("p_CHUYENKHOA", cmbChuyenKhoa.SelectedValue?.ToString() ?? (object)DBNull.Value)
                };

                DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Insert_BENHNHAN(:p_MABN, :p_HOTEN, :p_PHAI, :p_NGAYSINH, :p_CCCD, :p_SDT, :p_SONHA, :p_TENDUONG, :p_QUANHUYEN, :p_TINHTP, :p_TIENSUBENH, :p_TIENSUBENHGD, :p_DIUNGTHUOC, :p_CHUYENKHOA); END;", parameters, CommandType.Text);
                MessageBox.Show("Thêm bệnh nhân thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
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
    }
}
