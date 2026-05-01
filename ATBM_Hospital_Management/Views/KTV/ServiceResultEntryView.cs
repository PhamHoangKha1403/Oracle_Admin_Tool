using System;
using System.Drawing;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.KTV
{
    public partial class ServiceResultEntryView : UserControl
    {
        private string _maHSBA;
        private string _loaiDV;
        private DateTime _ngayDV;
        private bool _isEdit;
        private TechnicianService _techService;

        public ServiceResultEntryView(string maHSBA, string loaiDV, DateTime ngayDV, string ketQuaCu, bool isEdit,
                                      string tenBN, string ngaySinhBN, string gioiTinhBN)
        {
            InitializeComponent();
            _techService = new TechnicianService();
            _maHSBA = maHSBA;
            _loaiDV = loaiDV;
            _ngayDV = ngayDV;
            _isEdit = isEdit;

            lblValMaHSBA.Text = _maHSBA;
            lblValHoTen.Text = tenBN;
            lblValNgaySinh.Text = ngaySinhBN;
            lblValGioiTinh.Text = gioiTinhBN;

            lblValTenDV.Text = _loaiDV;
            lblValNgayTH.Text = _ngayDV.ToString("dd/MM/yyyy");

            SetupLayout(ketQuaCu);
        }

        private void SetupLayout(string ketQuaCu)
        {
            if (_isEdit)
            {
                // Trạng thái: Chỉnh sửa
                lblTitleKetQuaMoi.Text = "Kết quả mới:";
                txtKetQuaCu.Text = ketQuaCu;

                // Hiện các ô kết quả cũ
                lblTitleKetQuaCu.Visible = true;
                pnlTextBoxCardCu.Visible = true;

            }
            else
            {
                // Trạng thái: Thực hiện
                lblTitleKetQuaMoi.Text = "Kết quả:";

                // Ẩn kết quả cũ
                lblTitleKetQuaCu.Visible = false;
                pnlTextBoxCardCu.Visible = false;

                // Kéo ô kết quả mới lên trên lấp chỗ trống
                int pushUpDistance = pnlTextBoxCardCu.Height + 20;
                lblTitleKetQuaMoi.Top -= pushUpDistance;
                pnlTextBoxCardMoi.Top -= pushUpDistance;
                pnlTextBoxCardMoi.Height += pushUpDistance;

            }
        }

        private void ServiceResultEntryView_Load(object sender, EventArgs e)
        {
            this.ActiveControl = lblPageTitle;
        }

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            string ketQua = txtKetQuaMoi.Text.Trim();
            if (string.IsNullOrEmpty(ketQua))
            {
                MessageBox.Show("Vui lòng nhập kết quả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _techService.UpdateServiceResult(_maHSBA, _loaiDV, _ngayDV, ketQua);
                MessageBox.Show("Lưu kết quả thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Back
                var parentPanel = this.Parent as Panel;
                if (parentPanel != null)
                {
                    parentPanel.Controls.Clear();
                    parentPanel.Controls.Add(new ServiceRecordView() { Dock = DockStyle.Fill });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}