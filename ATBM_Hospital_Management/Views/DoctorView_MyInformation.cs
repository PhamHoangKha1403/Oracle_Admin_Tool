using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class DoctorView_MyInformation : UserControl
    {
        private readonly DbConnection _db;
        private Control _parent; // Giữ tham chiếu trang cha nếu cần nav ngược lại

        public DoctorView_MyInformation(Control parent)
        {
            InitializeComponent();
            _db = DbConnection.Instance;
            _parent = parent;

            // Đăng ký sự kiện Load để lấy data ngay khi hiện trang
            this.Load += DoctorView_MyInformation_Load;
        }

        private void DoctorView_MyInformation_Load(object sender, EventArgs e)
        {
            LoadMyInfo();
        }

        private void LoadMyInfo()
        {
            try
            {
                OracleParameter p_cursor = new OracleParameter
                {
                    ParameterName = "p_cursor",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.Output
                };

                // Gọi SP lấy thông tin nhân viên
                DataTable dt = _db.ExecuteQuery("sp_NV_Select_NHANVIEN", new[] { p_cursor }, CommandType.StoredProcedure);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    // Gán data vào các TextBox (đảm bảo ID đúng với Designer)
                    textBox1.Text = row["MA_NV"].ToString();
                    textBox2.Text = row["HO_TEN"].ToString();
                    textBox3.Text = row["PHAI"].ToString();
                    textBox4.Text = Convert.ToDateTime(row["NGAY_SINH"]).ToString("dd/MM/yyyy");
                    textBox5.Text = row["CCCD"].ToString();
                    textBox6.Text = row["QUE_QUAN"].ToString();
                    textBox7.Text = row["SDT"].ToString();
                    // Nếu bảng NHAN_VIEN không có chuyên khoa, bạn có thể để trống hoặc join thêm bảng
                    textBox8.Text = row["TEN_KHOA"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin cá nhân: " + ex.Message);
            }
        }
    }
}