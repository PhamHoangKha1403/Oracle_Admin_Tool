using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.KTV
{
    public partial class ServiceRecordView : UserControl
    {
        private readonly TechnicianService _techService;
        private DataTable _allRecords;

        public ServiceRecordView()
        {
            InitializeComponent();                               
            _techService = new TechnicianService();
            LoadRecords();
        }

        private void ServiceRecordView_Load(object sender, EventArgs e)
        {
            this.ActiveControl = lblPageTitle;
            cboStatus.SelectedIndex = 0;
        }

        private void LoadRecords()
        {
            try
            {
                // Gọi DB: lấy danh sách (đã bao gồm các cột TEN_BN, NGAY_SINH_BN, GIOI_TINH_BN từ View)
                _allRecords = _techService.GetServiceRecords();

                // Tạo cột ảo TRANG_THAI
                if (!_allRecords.Columns.Contains("TRANG_THAI"))
                {
                    _allRecords.Columns.Add("TRANG_THAI", typeof(string));
                }

                foreach (DataRow row in _allRecords.Rows)
                {
                    string result = row["KET_QUA"]?.ToString();
                    row["TRANG_THAI"] = string.IsNullOrWhiteSpace(result) ? "Chưa hoàn thành" : "Hoàn thành";
                }

                dgvRecords.DataSource = _allRecords;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            if (_allRecords == null) return;
            string keyword = txtSearch.Text.Trim();
            string statusFilter = cboStatus.SelectedItem?.ToString();
            string filterExpr = "1=1";

            if (!string.IsNullOrEmpty(keyword) && keyword != "Tìm Hồ sơ bệnh án")
            {
                filterExpr += $" AND MA_HSBA LIKE '%{keyword}%'";
            }

            if (statusFilter == "Hoàn thành" || statusFilter == "Chưa hoàn thành")
            {
                filterExpr += $" AND TRANG_THAI = '{statusFilter}'";
            }

            _allRecords.DefaultView.RowFilter = filterExpr;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilter();
        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilter();

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Tìm Hồ sơ bệnh án")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.FromArgb(40, 40, 40);
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm Hồ sơ bệnh án";
                txtSearch.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

        private void dgvRecords_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvRecords.Columns[e.ColumnIndex].Name == "ActionBtn" && e.RowIndex >= 0)
            {
                string status = dgvRecords.Rows[e.RowIndex].Cells["TRANG_THAI"].Value?.ToString();
                e.Value = (status == "Hoàn thành") ? "Chỉnh sửa" : "Thực hiện";
            }
        }

        private void dgvRecords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex].Name == "ActionBtn" && e.RowIndex >= 0)
            {
                string maHSBA = dgvRecords.Rows[e.RowIndex].Cells["MA_HSBA"].Value?.ToString();
                string loaiDV = dgvRecords.Rows[e.RowIndex].Cells["LOAI_DV"].Value?.ToString();
                string ketQuaCu = dgvRecords.Rows[e.RowIndex].Cells["KET_QUA"].Value?.ToString();
                string status = dgvRecords.Rows[e.RowIndex].Cells["TRANG_THAI"].Value?.ToString();

                DateTime ngayDV;
                DateTime.TryParse(dgvRecords.Rows[e.RowIndex].Cells["NGAY_DV"].Value?.ToString(), out ngayDV);

                bool isEdit = (status == "Hoàn thành");

                // --- LẤY DỮ LIỆU BỆNH NHÂN TỪ ROW ẨN CỦA DATATABLE ---
                DataRowView rowView = (DataRowView)dgvRecords.Rows[e.RowIndex].DataBoundItem;
                string tenBN = rowView["TEN_BN"]?.ToString() ?? "Không có dữ liệu";
                string gioiTinhBN = rowView["GIOI_TINH_BN"]?.ToString() ?? "Không có dữ liệu";

                string ngaySinhBN = "";
                if (rowView["NGAY_SINH_BN"] != DBNull.Value)
                {
                    ngaySinhBN = Convert.ToDateTime(rowView["NGAY_SINH_BN"]).ToString("dd/MM/yyyy");
                }

                // Chuyển trang và đưa luôn 3 dữ liệu này qua
                var resultEntryView = new ServiceResultEntryView(maHSBA, loaiDV, ngayDV, ketQuaCu, isEdit, tenBN, ngaySinhBN, gioiTinhBN);

                var parentPanel = this.Parent as Panel;
                if (parentPanel != null)
                {
                    parentPanel.Controls.Clear();
                    parentPanel.Controls.Add(resultEntryView);
                }
            }
        }
    }
}