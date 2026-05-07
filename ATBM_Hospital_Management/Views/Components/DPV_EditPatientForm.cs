using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.Components
{
    public partial class DPV_EditPatientForm : Form
    {
        private DataGridViewRow _row;
        private DataRowView _drv;

        public DPV_EditPatientForm(DataGridViewRow row)
        {
            InitializeComponent();
            _row = row;
            _drv = row.DataBoundItem as DataRowView;
        }

        private void DPV_EditPatientForm_Load(object sender, EventArgs e)
        {
            // 1. Pre-populate Fields from DataGridViewRow
            txtMaBN.Text = _row.Cells["MA_BN"].Value?.ToString();
            txtHoTen.Text = _drv != null ? _drv["HO_TEN"]?.ToString() : _row.Cells["HO_TEN"].Value?.ToString();

            string phai = _drv != null ? _drv["PHAI"]?.ToString() : _row.Cells["PHAI"].Value?.ToString();
            if (string.Equals(phai, "Nữ", StringComparison.OrdinalIgnoreCase))
                cmbPhai.SelectedItem = "Nữ";
            else
                cmbPhai.SelectedItem = "Nam";

            DateTime dtNgaySinh;
            string nsStr = _drv != null ? _drv["NGAY_SINH"]?.ToString() : _row.Cells["NGAY_SINH"].Value?.ToString();
            if (DateTime.TryParse(nsStr, out dtNgaySinh) && dtNgaySinh > DateTime.MinValue)
                dtpNgaySinh.Value = dtNgaySinh;
            else
                dtpNgaySinh.Value = DateTime.Now;

            txtCCCD.Text = _drv != null ? _drv["CCCD"]?.ToString() : _row.Cells["CCCD"].Value?.ToString();
            txtSDT.Text = _drv != null ? _drv["SDT"]?.ToString() : _row.Cells["SDT"].Value?.ToString();

            txtSoNha.Text = _row.Cells["SO_NHA"].Value?.ToString();
            txtTenDuong.Text = _row.Cells["TEN_DUONG"].Value?.ToString();
            txtQuanHuyen.Text = _row.Cells["QUAN_HUYEN"].Value?.ToString();
            txtTinhTp.Text = _row.Cells["TINH_TP"].Value?.ToString();
            txtTienSu.Text = _row.Cells["TIEN_SU_BENH"].Value?.ToString();
            txtTienSuGD.Text = _row.Cells["TIEN_SU_BENH_GD"].Value?.ToString();
            txtDiUng.Text = _row.Cells["DI_UNG_THUOC"].Value?.ToString();

            string currentChuyenKhoa = _drv != null ? _drv["CHUYEN_KHOA"]?.ToString() : "";

            // 2. Load Departments ComboBox
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
                if (!string.IsNullOrEmpty(currentChuyenKhoa))
                    cmbChuyenKhoa.SelectedValue = currentChuyenKhoa;
            }
            else
            {
                // Fallback Departments List
                DataTable dtFallback = new DataTable();
                dtFallback.Columns.Add("MA_KHOA");
                dtFallback.Columns.Add("TEN_KHOA");
                dtFallback.Rows.Add("TH", "Khoa tiêu hóa (TH)");
                dtFallback.Rows.Add("TK", "Khoa thần kinh (TK)");
                dtFallback.Rows.Add("TM", "Khoa tim mạch (TM)");

                cmbChuyenKhoa.DataSource = dtFallback;
                cmbChuyenKhoa.ValueMember = "MA_KHOA";
                cmbChuyenKhoa.DisplayMember = "TEN_KHOA";
                if (!string.IsNullOrEmpty(currentChuyenKhoa))
                    cmbChuyenKhoa.SelectedValue = currentChuyenKhoa;
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

                DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Update_BENHNHAN(:p_MABN, :p_HOTEN, :p_PHAI, :p_NGAYSINH, :p_CCCD, :p_SDT, :p_SONHA, :p_TENDUONG, :p_QUANHUYEN, :p_TINHTP, :p_TIENSUBENH, :p_TIENSUBENHGD, :p_DIUNGTHUOC, :p_CHUYENKHOA); END;", parameters, CommandType.Text);
                MessageBox.Show("Cập nhật bệnh nhân thành công!");
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
