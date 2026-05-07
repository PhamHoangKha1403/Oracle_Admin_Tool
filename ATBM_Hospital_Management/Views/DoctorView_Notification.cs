using ATBM_Hospital_Management.Database;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ATBM_Hospital_Management.Views
{
    // Đảm bảo ở đây là UserControl
    public partial class DoctorView_Notification : UserControl
    {
        private readonly DbConnection _db;
        private UserControl _parent; // Lưu tham chiếu trang cha

        // Constructor mặc định
        public DoctorView_Notification()
        {
            InitializeComponent();
            _db = DbConnection.Instance;

            // Đăng ký sự kiện load
            this.Load += DoctorView_Notification_Load;
        }

        // Constructor có tham chiếu parent (thường dùng trong project của bạn)
        public DoctorView_Notification(UserControl parent) : this()
        {
            _parent = parent;
        }

        private void DoctorView_Notification_Load(object sender, EventArgs e)
        {
            // Đảm bảo label2 (Tiêu đề) nằm trên cùng và không bị đè
            label2.Dock = DockStyle.Top;
            label2.Height = 100; // Tăng chiều cao vùng tiêu đề
            label2.TextAlign = ContentAlignment.MiddleCenter;

            // Đặt bảng ở dưới tiêu đề
            dataGridView1.Dock = DockStyle.Fill;

            // Tạo một khoảng cách nhỏ giữa tiêu đề và bảng
            this.Padding = new Padding(20, 20, 20, 20);

            SetupGridStyle();
            LoadNotifications();
        }

        private void SetupGridStyle()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.EnableHeadersVisualStyles = false; // BẮT BUỘC có dòng này để đổi màu Header

            // --- Thiết lập màu sắc Header theo yêu cầu của bạn ---
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = 50; // Chỉnh độ cao của hàng tiêu đề cột

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 151, 167);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Font cho các dòng dữ liệu
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.RowTemplate.Height = 40;
        }

        private void LoadNotifications()
        {
            try
            {
                OracleParameter p_cursor = new OracleParameter
                {
                    ParameterName = "p_cursor",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.Output
                };

                DataTable dt = _db.ExecuteQuery("SP_GET_THONGBAO", new[] { p_cursor }, CommandType.StoredProcedure);

                if (dt != null)
                {
                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Columns["MA_NV"] != null)
                        dataGridView1.Columns["MA_NV"].Visible = false;

                    // Đặt tên cột tiếng Việt
                    if (dataGridView1.Columns["MA_TB"] != null) dataGridView1.Columns["MA_TB"].HeaderText = "Mã TB";
                    if (dataGridView1.Columns["NOI_DUNG"] != null) dataGridView1.Columns["NOI_DUNG"].HeaderText = "Nội dung thông báo";
                    if (dataGridView1.Columns["NGAY_GIO"] != null) dataGridView1.Columns["NGAY_GIO"].HeaderText = "Ngày giờ";
                    if (dataGridView1.Columns["DIA_DIEM"] != null) dataGridView1.Columns["DIA_DIEM"].HeaderText = "Địa điểm";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải thông báo: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}