using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.Components
{
    public partial class DPV_AddHSBAForm : Form
    {
        private DataTable _dtHSBA;
        private string _preselectedMaBN;

        public DPV_AddHSBAForm(DataTable dtHSBA, string preselectedMaBN)
        {
            InitializeComponent();
            _dtHSBA = dtHSBA;
            _preselectedMaBN = preselectedMaBN;
        }

        private void DPV_AddHSBAForm_Load(object sender, EventArgs e)
        {
            // 1. Generate Next HSBA ID
            txtMaHSBA.Text = GenerateNextId(_dtHSBA, "MA_HSBA", "HSBA", "D6");

            // 2. Load Patient ID (If Any)
            if (!string.IsNullOrEmpty(_preselectedMaBN))
            {
                txtMaBN.Text = _preselectedMaBN;
            }

            // 3. Load Departments ComboBox
            DataTable dtKhoa = null;
            try
            {
                dtKhoa = DbConnection.Instance.ExecuteQuery("SELECT MA_KHOA, TEN_KHOA FROM ADMIN_PH2.KHOA", null, CommandType.Text);
            }
            catch { }

            if (dtKhoa != null && dtKhoa.Rows.Count > 0)
            {
                cmbKhoaKham.DataSource = dtKhoa;
                cmbKhoaKham.ValueMember = "MA_KHOA";
                cmbKhoaKham.DisplayMember = "TEN_KHOA";
            }
            else
            {
                // Fallback list
                DataTable dtFallback = new DataTable();
                dtFallback.Columns.Add("MA_KHOA");
                dtFallback.Columns.Add("TEN_KHOA");
                dtFallback.Rows.Add("TH", "Khoa tiêu hóa (TH)");
                dtFallback.Rows.Add("TK", "Khoa thần kinh (TK)");
                dtFallback.Rows.Add("TM", "Khoa tim mạch (TM)");

                cmbKhoaKham.DataSource = dtFallback;
                cmbKhoaKham.ValueMember = "MA_KHOA";
                cmbKhoaKham.DisplayMember = "TEN_KHOA";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBN.Text))
            {
                MessageBox.Show("Vui lòng nhập mã bệnh nhân.");
                return;
            }

            try
            {
                var parameters = new OracleParameter[]
                {
                    new OracleParameter("p_MAHSBA", txtMaHSBA.Text),
                    new OracleParameter("p_MABN", txtMaBN.Text),
                    new OracleParameter("p_NGAY", dtpNgay.Value),
                    new OracleParameter("p_CHANDOAN", txtChanDoan.Text),
                    new OracleParameter("p_DIEUTRI", txtDieuTri.Text),
                    new OracleParameter("p_MABS", txtMaBS.Text),
                    new OracleParameter("p_MAKHOA", cmbKhoaKham.SelectedValue?.ToString() ?? (object)DBNull.Value),
                    new OracleParameter("p_KETLUAN", txtKetLuan.Text)
                };

                DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Insert_HSBA(:p_MAHSBA, :p_MABN, :p_NGAY, :p_CHANDOAN, :p_DIEUTRI, :p_MABS, :p_MAKHOA, :p_KETLUAN); END;", parameters, CommandType.Text);
                MessageBox.Show("Thêm hồ sơ bệnh án thành công!");
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
// 
