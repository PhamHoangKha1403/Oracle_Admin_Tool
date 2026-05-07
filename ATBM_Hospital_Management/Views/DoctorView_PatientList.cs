using ATBM_Hospital_Management.Database;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATBM_Hospital_Management.Views.Components
{
    public partial class DoctorView_PatientList : UserControl
    {
        private readonly DbConnection _db;
        private DataTable _currentPatients;
        private TextBox txtSearch;
        private Button btnSearch;
        private Panel pnlSearch;

        public DoctorView_PatientList(string userName = "")
        {
            InitializeComponent();
            _db = DbConnection.Instance;

            if (lblTitle != null)
                lblTitle.Text = "DANH SÁCH BỆNH NHÂN";

            if (!string.IsNullOrWhiteSpace(userName))
                lblUserName.Text = userName;

            // Đợi form render xong mới chạy
            this.Load += (s, e) =>
            {
                this.BeginInvoke(new Action(async () =>
                {
                    lblTitle.AutoSize = false;
                    lblTitle.Height = 100;

                    ShowMainListView();
                    SetupDataGrid();
                    SetActiveNav(btnBenhNhan);
                    await LoadPatients();
                }));
            };
        }

        public async void DoctorView_Load(object sender, EventArgs e)
        {
            lblTitle.AutoSize = false;
            lblTitle.Height = 100;

            ShowMainListView();

            SetupDataGrid();

            LoadPatients();

            SetActiveNav(btnBenhNhan);
        }

        private void SetupSearchBar()
        {
            pnlSearch = new Panel { Dock = DockStyle.Top, Height = 50 };
            pnlSearch.Padding = new Padding(0, 0, 0, 10);
            
            txtSearch = new TextBox 
            { 
                Location = new Point(0, 10), 
                Width = 300, 
                Font = new Font("Segoe UI", 12f),
                ForeColor = Color.Gray,
                Text = "Nhập mã, tên hoặc CCCD..."
            };
            txtSearch.Enter += (s, e) => { if (txtSearch.Text == "Nhập mã, tên hoặc CCCD...") { txtSearch.Text = ""; txtSearch.ForeColor = Color.Black; } };
            txtSearch.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtSearch.Text)) { txtSearch.Text = "Nhập mã, tên hoặc CCCD..."; txtSearch.ForeColor = Color.Gray; } };
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnSearch.PerformClick(); } };

            btnSearch = new Button
            {
                Location = new Point(310, 8),
                Size = new Size(100, 32),
                Text = "Tìm kiếm",
                BackColor = Color.FromArgb(47, 121, 138),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold)
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += btnSearch_Click;

            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(btnSearch);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (_currentPatients == null) return;
            string search = txtSearch.Text.Trim();
            if (search == "Nhập mã, tên hoặc CCCD..." || string.IsNullOrWhiteSpace(search))
            {
                _currentPatients.DefaultView.RowFilter = "";
            }
            else
            {
                _currentPatients.DefaultView.RowFilter = $"MA_BN LIKE '%{search}%' OR HO_TEN LIKE '%{search}%' OR CCCD LIKE '%{search}%'";
            }
        }

        // Hàm này đảm bảo giao diện danh sách hiện ra
        private void ShowMainListView()
        {
            panelContent.Controls.Clear();

            if (pnlMainList != null)
            {
                // 2. Dọn dẹp pnlMainList để xây dựng lại cấu trúc (Tránh bị trùng lặp control)
                pnlMainList.Controls.Clear();
                pnlMainList.Dock = DockStyle.Fill;

                dataGridView1.Dock = DockStyle.Fill;

                if (pnlSearch == null) SetupSearchBar();

                pnlMainList.Controls.Add(dataGridView1); // Nằm dưới cùng
                pnlMainList.Controls.Add(pnlSearch);     // Giữa
                pnlMainList.Controls.Add(lblTitle);      // Nằm trên cùng

                pnlMainList.Visible = true;
                panelContent.Controls.Add(pnlMainList);
                pnlMainList.BringToFront();
            }
        }
        private void SetupDataGrid()
        {
            pnlMainList.Padding = new Padding(0, 40, 0, 0);
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Dock = DockStyle.Fill;

            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = 100;
            dataGridView1.ColumnHeadersVisible = true; 

            // Style header
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 151, 167);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Fill mode
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- THIẾT LẬP CÁC CỘT ---
            AddColumn("MA_BN", "MÃ BN", "MA_BN", 150);
            AddColumn("TEN_BN", "TÊN BN", "HO_TEN", 400);
            AddColumn("PHAI", "PHÁI", "PHAI", 50);
            AddColumn("NGAY_SINH", "NGÀY SINH", "NGAY_SINH", 250);
            AddColumn("CCCD", "CCCD", "CCCD", 250);
            AddColumn("SO_NHA", "SỐ", "SO_NHA", 50);
            AddColumn("TEN_DUONG", "ĐƯỜNG", "TEN_DUONG", 120);
            AddColumn("QUAN_HUYEN", "QUẬN/HUYỆN", "QUAN_HUYEN", 220);
            AddColumn("TINH_TP", "TỈNH/TP", "TINH_TP", 100);
            AddColumn("TIEN_SU_BENH", "TIỀN SỬ BỆNH", "TIEN_SU_BENH", 200);
            AddColumn("DI_UNG_THUOC", "DỊ ỨNG", "DI_UNG_THUOC", 200);
            
            // Thao tác (Cố định nút bấm)
            var btnCol = new DataGridViewButtonColumn
            {
                Name = "btnChiTiet",
                HeaderText = "THAO TÁC",
                Text = "Chi tiết",
                UseColumnTextForButtonValue = true,
                FillWeight = 80,
                FlatStyle = FlatStyle.Flat

            };

            Color customColor = Color.FromArgb(47, 121, 138);
            btnCol.DefaultCellStyle.BackColor = customColor;
            btnCol.DefaultCellStyle.ForeColor = Color.White; // Chữ trắng cho nổi bật
            btnCol.DefaultCellStyle.SelectionBackColor = customColor; // Giữ màu khi được chọn
            btnCol.DefaultCellStyle.SelectionForeColor = Color.White;

            // Căn giữa nút trong ô
            btnCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns.Add(btnCol); // Cho phép chữ tự xuống dòng nếu cột quá hẹp
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            // Tự động giãn dòng theo nội dung
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.RowTemplate.Height = 40;
        }

        // Hàm phụ trợ để code sạch hơn
        private void AddColumn(string name, string header, string property, int weight)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataPropertyName = property,
                FillWeight = weight, // Đây là cái quyết định tỷ lệ rộng/hẹp
                MinimumWidth = Math.Max(80, header.Length * 10) // ← SỬA: tự tính theo header
            };
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            dataGridView1.Columns.Add(col);
        }

        private async Task LoadPatients()
        {
            lblRecordCount.Text = "Đang tải...";

            DataTable result = await Task.Run(() =>
            {
                var p_cursor = new OracleParameter
                {
                    ParameterName = "p_cursor",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.Output
                };
                return _db.ExecuteQuery(
                    "ADMIN_PH2.sp_BS_Select_BENHNHAN",
                    new[] { p_cursor },
                    CommandType.StoredProcedure);
            });

            // Tắt render trong lúc bind để tránh lag
            dataGridView1.SuspendLayout();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            _currentPatients = result;
            dataGridView1.DataSource = _currentPatients;

            dataGridView1.ResumeLayout();

            lblRecordCount.Text = $"Tổng: {(_currentPatients?.Rows.Count ?? 0)} bệnh nhân";

            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // Hàm này dùng để cập nhật giao diện nút bấm
        private void SetActiveNav(Button active)
        {
            if (active == null || btnBenhNhan == null || btnHoSoBenhAn == null) return;
            Button[] navButtons = { btnBenhNhan, btnHoSoBenhAn };

            foreach (Button btn in navButtons)
            {
                bool on = (btn == active);

                // Cập nhật giao diện nút
                btn.Font = new Font("Segoe UI", 11f, on ? FontStyle.Bold : FontStyle.Regular);
                btn.ForeColor = on ? Color.White : Color.FromArgb(180, 238, 248);
                btn.FlatAppearance.BorderSize = on ? 2 : 0;
                btn.FlatAppearance.BorderColor = Color.White;
            }
        }

        // Hàm dùng để nạp trang mới vào vùng nội dung (panelContent)
        public void ShowPage(UserControl page)
        {
            if (page == null) return;

            // 1. Ẩn tất cả các trang đang có trong panelContent thay vì Dispose chúng
            foreach (Control ctrl in panelContent.Controls)
            {
                ctrl.Visible = false;
            }

            // 2. Nếu trang này chưa có trong panel thì mới thêm vào
            if (!panelContent.Controls.Contains(page))
            {
                page.Dock = DockStyle.Fill;
                panelContent.Controls.Add(page);
            }

            // 3. Hiện trang cần dùng và đưa lên trên cùng
            page.Visible = true;
            page.BringToFront();

            // 4. Nếu trang có hàm nạp lại dữ liệu, bạn có thể gọi ở đây
            // hoặc xử lý bên trong sự kiện VisibleChanged của trang đó
        }

        private void btnBenhNhan_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnBenhNhan);
            ShowMainListView();
            LoadPatients();
        }

        private void btnHoSoBenhAn_Click(object sender, EventArgs e)
        {
            SetActiveNav(btnHoSoBenhAn);

            // Dispose page cũ nếu có trước khi tạo mới
            var listPage = new DoctorView_HealthRecordList(this);
            ShowPage(listPage);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnChiTiet")
            {
                string maBn = dataGridView1.Rows[e.RowIndex].Cells["MA_BN"].Value?.ToString();

                // Truyền 'this' để trang Detail biết trang cha là ai và giữ được Nav Bar
                DoctorView_PatientDetail detailPage = new DoctorView_PatientDetail(maBn, this);
                this.ShowPage(detailPage);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}