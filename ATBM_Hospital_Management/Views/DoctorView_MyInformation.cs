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
                    textBox8.Text = row["TEN_KHOA"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin cá nhân: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ giao diện (giả sử bạn có 2 TextBox này)
            string queQuan = textBox6.Text.Trim();
            string soDT = textBox7.Text.Trim();

            // Kiểm tra dữ liệu đầu vào cơ bản
            if (string.IsNullOrEmpty(queQuan) || string.IsNullOrEmpty(soDT))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            try
            {
                // 2. Thiết lập các tham số cho Stored Procedure
                var parameters = new Oracle.ManagedDataAccess.Client.OracleParameter[]
                {
            new Oracle.ManagedDataAccess.Client.OracleParameter("p_QUEQUAN", Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2) { Value = queQuan },
            new Oracle.ManagedDataAccess.Client.OracleParameter("p_SODT", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2) { Value = soDT }
                };

                // 3. Thực thi Procedure thông qua lớp DbConnection
                // Lưu ý: Vì SP này chỉ thực hiện UPDATE nên dùng ExecuteNonQuery
                int result = DbConnection.Instance.ExecuteNonQuery(
                    "BEGIN sp_NV_Update_NHANVIEN(:p_QUEQUAN, :p_SODT); END;",
                    parameters,
                    CommandType.Text
                );

                MessageBox.Show("Cập nhật thông tin cá nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // (Tùy chọn) Load lại dữ liệu để hiển thị thông tin mới nhất
                // LoadPersonalInformation(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}