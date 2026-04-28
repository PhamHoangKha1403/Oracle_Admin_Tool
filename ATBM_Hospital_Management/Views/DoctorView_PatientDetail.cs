using ATBM_Hospital_Management.Database;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ATBM_Hospital_Management.Views.Components
{
    public partial class DoctorView_PatientDetail : UserControl
    {
        private readonly DbConnection _db;
        private string _maBN;
        private DoctorView_PatientList _parentView;

        public DoctorView_PatientDetail(string maBN, DoctorView_PatientList parent)
        {
            InitializeComponent();
            _db = DbConnection.Instance;
            _maBN = maBN;
            _parentView = parent;

            this.Load += DoctorView_PatientDetail_Load;
        }

        private void DoctorView_PatientDetail_Load(object sender, EventArgs e)
        {
            LoadPatientInfo();
            LoadHSBA();
        }

        private void LoadPatientInfo()
        {
            try
            {
                OracleParameter p_ma_bn = new OracleParameter("p_ma_bn", OracleDbType.Varchar2) { Value = _maBN };
                OracleParameter p_cursor = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);

                DataTable dt = _db.ExecuteQuery("ADMIN_PH2.sp_BS_Get_BENHNHAN_Detail",
                    new[] { p_ma_bn, p_cursor }, CommandType.StoredProcedure);

                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    // Mapping đúng tên cột từ SP mới
                    textBox1.Text = r["MA_BN"].ToString();
                    textBox2.Text = r["HO_TEN"].ToString();
                    textBox3.Text = r["PHAI"].ToString();
                    textBox4.Text = r["NGAY_SINH"] != DBNull.Value ? Convert.ToDateTime(r["NGAY_SINH"]).ToString("dd/MM/yyyy") : "";
                    textBox5.Text = r["CCCD"].ToString();
                    textBox6.Text = r["SO_NHA"].ToString();
                    textBox7.Text = r["TEN_DUONG"].ToString();
                    textBox8.Text = r["QUAN_HUYEN"].ToString();
                    textBox9.Text = r["TINH_TP"].ToString();
                    richTextBox1.Text = r["TIEN_SU_BENH"].ToString();
                    richTextBox2.Text = r["TIEN_SU_BENH_GD"].ToString();
                    richTextBox3.Text = r["DI_UNG_THUOC"].ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hiển thị: " + ex.Message); }
        }
        private void LoadHSBA()
        {
            try
            {
                // 1. Gọi SP lấy dữ liệu
                OracleParameter p_ma_bn = new OracleParameter("p_ma_bn", OracleDbType.Varchar2) { Value = _maBN };
                OracleParameter p_cursor = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);
                DataTable dt = _db.ExecuteQuery("ADMIN_PH2.sp_BS_Select_HSBA_Theo_BN", new[] { p_ma_bn, p_cursor }, CommandType.StoredProcedure);

                dataGridView1.DataSource = dt;

                // 2. Thêm cột nút "Xem thêm" nếu chưa có
                if (!dataGridView1.Columns.Contains("btnXemThem"))
                {
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.Name = "btnXemThem";
                    btn.HeaderText = "";
                    btn.Text = "Xem thêm";
                    btn.UseColumnTextForButtonValue = true;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Width = 100;
                    Color customColor = Color.FromArgb(47, 121, 138);

                    btn.DefaultCellStyle.BackColor = customColor;
                    btn.DefaultCellStyle.ForeColor = Color.White; // Chữ trắng cho nổi bật
                    btn.DefaultCellStyle.SelectionBackColor = customColor; // Giữ màu khi được chọn
                    btn.DefaultCellStyle.SelectionForeColor = Color.White;
                    dataGridView1.Columns.Add(btn);
                }

                // 3. LOGIC QUAN TRỌNG: Tự điều chỉnh chiều cao
                int rowCount = dataGridView1.Rows.Count;
                int headerHeight = dataGridView1.ColumnHeadersHeight;
                int rowHeight = (rowCount > 0) ? dataGridView1.Rows[0].Height : 25;

                // Tính toán chiều cao cần thiết
                int targetHeight = headerHeight + (rowHeight * rowCount) + 5;

                // Nếu chiều cao vượt quá 400, nó sẽ bị MaximumSize chặn lại và hiện Scrollbar
                // Nếu nhỏ hơn 400, nó sẽ co lại vừa khít số dòng
                dataGridView1.Height = targetHeight;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // Sự kiện click vào nút "Xem thêm"
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "btnXemThem")
            {
                string maHSBA = dataGridView1.Rows[e.RowIndex].Cells["MA_HSBA"].Value?.ToString();
                string maBN = _maBN; // Lấy từ biến toàn cục của trang Detail

                // Khởi tạo trang HSBA chi tiết
                var hsbaPage = new DoctorView_HealthRecord(maHSBA, maBN, _parentView);

                // Dùng hàm ShowPage của trang cha (PatientList) để hiển thị đè lên vùng nội dung
                _parentView.ShowPage(hsbaPage);
            }
        }

        // Xử lý nút LƯU (gọi sp_BS_Update_BENHNHAN)
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OracleParameter[] pars = {
                    new OracleParameter("p_MA_BN", OracleDbType.Varchar2) { Value = _maBN },
                    new OracleParameter("p_TIENSUBENH", OracleDbType.NVarchar2) { Value = richTextBox1.Text },
                    new OracleParameter("p_TIENSUBENHGD", OracleDbType.NVarchar2) { Value = richTextBox2.Text },
                    new OracleParameter("p_DIUNGTHUOC", OracleDbType.NVarchar2) { Value = richTextBox3.Text }
                };

                _db.ExecuteNonQuery("ADMIN_PH2.sp_BS_Update_BENHNHAN", pars, CommandType.StoredProcedure);
                MessageBox.Show("Cập nhật thông tin bệnh nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi cập nhật: " + ex.Message); }
        }

        // Trong file DoctorView_PatientDetail.cs

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_parentView != null)
            {
                // Gọi lại hàm Load của trang cha để hiện lại danh sách
                // Vì trong hàm DoctorView_Load của cha đã có lệnh panelContent.Controls.Clear() 
                // và nạp lại pnlMainList nên giao diện sẽ quay về đúng ban đầu.
                _parentView.DoctorView_Load(sender, e);

                // Hủy trang hiện tại để giải phóng tài nguyên
                this.Dispose();
            }
        }


    }
}