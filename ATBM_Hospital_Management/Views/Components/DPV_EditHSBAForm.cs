using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.Components
{
    public partial class DPV_EditHSBAForm : Form
    {
        private DataGridViewRow _row;
        private DataRowView _drv;

        public DPV_EditHSBAForm(DataGridViewRow row)
        {
            InitializeComponent();
            _row = row;
            _drv = row.DataBoundItem as DataRowView;
        }

        private void DPV_EditHSBAForm_Load(object sender, EventArgs e)
        {
            // 1. Populate Fields
            txtMaHSBA.Text = _row.Cells["MA_HSBA"].Value?.ToString();
            txtMaBN.Text = _row.Cells["MA_BN"].Value?.ToString();

            DateTime dtNgay;
            string nsStr = _drv != null ? _drv["NGAY"]?.ToString() : _row.Cells["NGAY"].Value?.ToString();
            if (DateTime.TryParse(nsStr, out dtNgay) && dtNgay > DateTime.MinValue)
                dtpNgay.Value = dtNgay;
            else
                dtpNgay.Value = DateTime.Now;

            txtChanDoan.Text = _row.Cells["CHAN_DOAN"].Value?.ToString();
            txtDieuTri.Text = _row.Cells["DIEU_TRI"].Value?.ToString();
            txtMaBS.Text = _row.Cells["MA_BS"].Value?.ToString();
            txtKetLuan.Text = _row.Cells["KET_LUAN"].Value?.ToString();

            string currentKhoa = _row.Cells["MA_KHOA"].Value?.ToString();

            // 2. Load Departments ComboBox
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
                if (!string.IsNullOrEmpty(currentKhoa))
                    cmbKhoaKham.SelectedValue = currentKhoa;
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
                if (!string.IsNullOrEmpty(currentKhoa))
                    cmbKhoaKham.SelectedValue = currentKhoa;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var parameters = new OracleParameter[]
                {
                    new OracleParameter("p_MAHSBA", txtMaHSBA.Text),
                    new OracleParameter("p_MABS", txtMaBS.Text),
                    new OracleParameter("p_MAKHOA", cmbKhoaKham.SelectedValue?.ToString() ?? (object)DBNull.Value)
                };

                DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Update_HSBA(:p_MAHSBA, :p_MABS, :p_MAKHOA); END;", parameters, CommandType.Text);
                MessageBox.Show("Cập nhật hồ sơ bệnh án thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
// 
