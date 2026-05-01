using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.Components
{
    public partial class DoctorView_PatientList : UserControl
    {
        private readonly DbConnection _db;
        private DataTable _currentPatients;

        public DoctorView_PatientList(string userName = "")
        {
            InitializeComponent();
            _db = DbConnection.Instance;
            if (lblTitle != null)
            {
                lblTitle.Text = "DANH SÁCH BỆNH NHÂN";
            }
            if (!string.IsNullOrWhiteSpace(userName))
                lblUserName.Text = userName;

        }

        public void DoctorView_Load(object sender, EventArgs e)
        {
            lblTitle.AutoSize = false;
            lblTitle.Height = 100;

            ShowMainListView(); 

            SetupDataGrid();
            LoadPatients();
            SetActiveNav(btnBenhNhan);
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

                pnlMainList.Controls.Add(dataGridView1); // Nằm dưới cùng
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
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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

        private void LoadPatients()
        {
            try
            {
                OracleParameter p_cursor = new OracleParameter
                {
                    ParameterName = "p_cursor",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.Output
                };

                // Gọi đúng SP và truyền CommandType.StoredProcedure
                _currentPatients = _db.ExecuteQuery("ADMIN_PH2.sp_BS_Select_BENHNHAN", new[] { p_cursor }, CommandType.StoredProcedure);

                dataGridView1.DataSource = _currentPatients;
                lblRecordCount.Text = $"Tổng: {(_currentPatients?.Rows.Count ?? 0)} bệnh nhân";
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
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
            // Ẩn vùng danh sách đi thay vì xóa nó hoàn toàn để giữ trạng thái
            if (pnlMainList != null) pnlMainList.Visible = false;

            panelContent.Controls.Clear(); // Xóa các page Detail cũ (nếu có)
            if (page != null)
            {
                page.Dock = DockStyle.Fill;
                panelContent.Controls.Add(page);
                page.BringToFront();
            }
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

        
    }
}