using ATBM_Hospital_Management.Database;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATBM_Hospital_Management.Views.Components
{
    public partial class DoctorView_HealthRecordList : UserControl
    {
        private readonly DbConnection _db;
        private DoctorView_PatientList _parentView;
        private DataTable _currentRecords;

        public DoctorView_HealthRecordList(DoctorView_PatientList parent)
        {
            InitializeComponent();
            _db = DbConnection.Instance;
            _parentView = parent;

            SetupDataGrid();

            btnSearch.Click += btnSearch_Click;

            this.Load += async (s, e) =>
            {
                await Task.Delay(50); // nhường UI thread
                await LoadHSBA();
            };
        }

        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (_currentRecords == null) return;
            string search = txtSearch.Text.Trim();
            if (search == "Nhập mã HSBA hoặc mã BN..." || string.IsNullOrWhiteSpace(search))
            {
                _currentRecords.DefaultView.RowFilter = "";
            }
            else
            {
                _currentRecords.DefaultView.RowFilter = $"MA_HSBA LIKE '%{search}%' OR MA_BN LIKE '%{search}%'";
            }
        }

        private void SetupDataGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Dock = DockStyle.Fill; // Luôn để Fill

            // Cấu hình để hiện Header
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.EnableHeadersVisualStyles = false; // Để tự định nghĩa màu
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = 50;

            // Style cho Header
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(47, 121, 138);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Cấu hình chung
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BorderStyle = BorderStyle.None;

            // Thêm các cột
            AddColumn("MA_HSBA", "Mã HSBA", "MA_HSBA", 100);
            AddColumn("MA_BN", "Mã BN", "MA_BN", 100);
            AddColumn("NGAY", "Ngày lập", "NGAY", 120);
            AddColumn("CHAN_DOAN", "Chẩn đoán", "CHAN_DOAN", 200);
            AddColumn("DIEU_TRI", "Điều trị", "DIEU_TRI", 200);
            AddColumn("KET_LUAN", "Kết luận", "KET_LUAN", 200);

            // Cột nút bấm
            var btnCol = new DataGridViewButtonColumn
            {
                Name = "btnXemThem",
                HeaderText = "Thao tác",
                Text = "Xem thêm",
                UseColumnTextForButtonValue = true,
                FillWeight = 80,
                FlatStyle = FlatStyle.Flat
            };
            btnCol.DefaultCellStyle.BackColor = Color.FromArgb(47, 121, 138);
            btnCol.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Columns.Add(btnCol);

            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void AddColumn(string name, string header, string property, int weight)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataPropertyName = property,
                FillWeight = weight,
                MinimumWidth = Math.Max(80, header.Length * 10)
            };
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            dataGridView1.Columns.Add(col);
        }

        private async Task LoadHSBA()
        {
            try
            {
                lblRecordCount.Text = "Đang tải...";

                DataTable dt = await Task.Run(() =>
                {
                    var p_cursor = new OracleParameter
                    {
                        ParameterName = "p_cursor",
                        OracleDbType = OracleDbType.RefCursor,
                        Direction = ParameterDirection.Output
                    };

                    return _db.ExecuteQuery(
                        "ADMIN_PH2.sp_BS_Select_HSBA",
                        new[] { p_cursor },
                        CommandType.StoredProcedure);
                });

                // Đảm bảo thao tác trên UI Thread
                this.BeginInvoke(new MethodInvoker(() => {
                    _currentRecords = dt;
                    dataGridView1.DataSource = null; // Reset trước khi gán mới
                    dataGridView1.DataSource = _currentRecords;

                    lblRecordCount.Text = $"Tổng: {(dt?.Rows.Count ?? 0)} hồ sơ";

                    // Ép DataGridView vẽ lại ngay lập tức
                    dataGridView1.Refresh();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnXemThem")
            {
                string maHSBA = dataGridView1.Rows[e.RowIndex].Cells["MA_HSBA"].Value?.ToString();
                string maBN = dataGridView1.Rows[e.RowIndex].Cells["MA_BN"].Value?.ToString();

                // Truyền thêm 'this' để trang HealthRecord biết phải quay về đây
                var healthRecordPage = new DoctorView_HealthRecord(maHSBA, maBN, _parentView, this);
                healthRecordPage.SetBreadcrumb("DANH SÁCH HỒ SƠ BỆNH ÁN / CHI TIẾT HỒ SƠ BỆNH ÁN");
                _parentView.ShowPage(healthRecordPage);
            }
        }

        private void pnlSearch_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}