namespace ATBM_Hospital_Management.Views.KTV
{
    partial class ServiceResultEntryView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.lblTitleBN = new System.Windows.Forms.Label();
            this.lblTitleMaHSBA = new System.Windows.Forms.Label();
            this.lblValMaHSBA = new System.Windows.Forms.Label();
            this.lblTitleHoTen = new System.Windows.Forms.Label();
            this.lblValHoTen = new System.Windows.Forms.Label();
            this.lblTitleNgaySinh = new System.Windows.Forms.Label();
            this.lblValNgaySinh = new System.Windows.Forms.Label();
            this.lblTitleGioiTinh = new System.Windows.Forms.Label();
            this.lblValGioiTinh = new System.Windows.Forms.Label();
            this.lblTitleDV = new System.Windows.Forms.Label();
            this.lblTitleTenDV = new System.Windows.Forms.Label();
            this.lblValTenDV = new System.Windows.Forms.Label();
            this.lblTitleNgayTH = new System.Windows.Forms.Label();
            this.lblValNgayTH = new System.Windows.Forms.Label();

            this.lblTitleKetQuaCu = new System.Windows.Forms.Label();
            this.txtKetQuaCu = new System.Windows.Forms.TextBox();
            this.pnlTextBoxCardCu = new System.Windows.Forms.Panel();

            this.lblTitleKetQuaMoi = new System.Windows.Forms.Label();
            this.txtKetQuaMoi = new System.Windows.Forms.TextBox();
            this.pnlTextBoxCardMoi = new System.Windows.Forms.Panel();

            this.btnHoanTat = new System.Windows.Forms.Button();

            this.pnlBackground.SuspendLayout();
            this.pnlTextBoxCardCu.SuspendLayout();
            this.pnlTextBoxCardMoi.SuspendLayout();
            this.SuspendLayout();

            System.Drawing.Font fontBoldHeader = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            System.Drawing.Font fontBoldLabel = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            System.Drawing.Font fontNormal = new System.Drawing.Font("Segoe UI", 11F);
            System.Drawing.Color colorDark = System.Drawing.Color.FromArgb(30, 30, 30);

            // pnlBackground
            this.pnlBackground.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Padding = new System.Windows.Forms.Padding(40);
            this.pnlBackground.AutoScroll = true;

            this.lblPageTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.Location = new System.Drawing.Point(40, 40);
            this.lblPageTitle.Size = new System.Drawing.Size(800, 50);
            this.lblPageTitle.Text = "KẾT QUẢ DỊCH VỤ";
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // SECTION: THÔNG TIN BỆNH NHÂN
            this.lblTitleBN.AutoSize = true; this.lblTitleBN.Font = fontBoldHeader; this.lblTitleBN.Location = new System.Drawing.Point(50, 100); this.lblTitleBN.Text = "THÔNG TIN BỆNH NHÂN";
            this.lblTitleMaHSBA.AutoSize = true; this.lblTitleMaHSBA.Font = fontNormal; this.lblTitleMaHSBA.Location = new System.Drawing.Point(80, 140); this.lblTitleMaHSBA.Text = "Mã HSBA:";
            this.lblValMaHSBA.AutoSize = true; this.lblValMaHSBA.Font = fontBoldLabel; this.lblValMaHSBA.Location = new System.Drawing.Point(180, 140);
            this.lblTitleHoTen.AutoSize = true; this.lblTitleHoTen.Font = fontNormal; this.lblTitleHoTen.Location = new System.Drawing.Point(80, 180); this.lblTitleHoTen.Text = "Họ tên:";
            this.lblValHoTen.AutoSize = true; this.lblValHoTen.Font = fontBoldLabel; this.lblValHoTen.Location = new System.Drawing.Point(180, 180);
            this.lblTitleNgaySinh.AutoSize = true; this.lblTitleNgaySinh.Font = fontNormal; this.lblTitleNgaySinh.Location = new System.Drawing.Point(750, 180); this.lblTitleNgaySinh.Text = "Ngày Sinh:";
            this.lblValNgaySinh.AutoSize = true; this.lblValNgaySinh.Font = fontNormal; this.lblValNgaySinh.Location = new System.Drawing.Point(850, 180);
            this.lblTitleGioiTinh.AutoSize = true; this.lblTitleGioiTinh.Font = fontNormal; this.lblTitleGioiTinh.Location = new System.Drawing.Point(1450, 180); this.lblTitleGioiTinh.Text = "Giới tính:";
            this.lblValGioiTinh.AutoSize = true; this.lblValGioiTinh.Font = fontNormal; this.lblValGioiTinh.Location = new System.Drawing.Point(1550, 180);

            // SECTION: THÔNG TIN DỊCH VỤ
            this.lblTitleDV.AutoSize = true; this.lblTitleDV.Font = fontBoldHeader; this.lblTitleDV.Location = new System.Drawing.Point(50, 240); this.lblTitleDV.Text = "THÔNG TIN DỊCH VỤ";
            this.lblTitleTenDV.AutoSize = true; this.lblTitleTenDV.Font = fontNormal; this.lblTitleTenDV.Location = new System.Drawing.Point(80, 280); this.lblTitleTenDV.Text = "Loại dịch vụ:";
            this.lblValTenDV.AutoSize = true; this.lblValTenDV.Font = fontNormal; this.lblValTenDV.Location = new System.Drawing.Point(190, 280);
            this.lblTitleNgayTH.AutoSize = true; this.lblTitleNgayTH.Font = fontNormal; this.lblTitleNgayTH.Location = new System.Drawing.Point(80, 320); this.lblTitleNgayTH.Text = "Ngày thực hiện:";
            this.lblValNgayTH.AutoSize = true; this.lblValNgayTH.Font = fontNormal; this.lblValNgayTH.Location = new System.Drawing.Point(210, 320);

            // --- KẾT QUẢ CŨ ---
            this.lblTitleKetQuaCu.AutoSize = true;
            this.lblTitleKetQuaCu.Font = fontBoldLabel;
            this.lblTitleKetQuaCu.Location = new System.Drawing.Point(80, 375);
            this.lblTitleKetQuaCu.Text = "Kết quả cũ:";

            this.pnlTextBoxCardCu.BackColor = System.Drawing.Color.White;
            this.pnlTextBoxCardCu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTextBoxCardCu.Location = new System.Drawing.Point(200, 365);
            this.pnlTextBoxCardCu.Size = new System.Drawing.Size(1400, 50);
            this.pnlTextBoxCardCu.Controls.Add(this.txtKetQuaCu);

            this.txtKetQuaCu.Multiline = true;
            this.txtKetQuaCu.ReadOnly = true;
            this.txtKetQuaCu.Font = fontNormal;
            this.txtKetQuaCu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtKetQuaCu.BackColor = System.Drawing.Color.White;
            this.txtKetQuaCu.Location = new System.Drawing.Point(10, 10);
            this.txtKetQuaCu.Size = new System.Drawing.Size(675, 40);

            // --- KẾT QUẢ MỚI ---
            this.lblTitleKetQuaMoi.AutoSize = true;
            this.lblTitleKetQuaMoi.Font = fontBoldLabel;
            this.lblTitleKetQuaMoi.Location = new System.Drawing.Point(80, 455);
            this.lblTitleKetQuaMoi.Text = "Kết quả mới:";

            this.pnlTextBoxCardMoi.BackColor = System.Drawing.Color.White;
            this.pnlTextBoxCardMoi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTextBoxCardMoi.Location = new System.Drawing.Point(200, 445);
            this.pnlTextBoxCardMoi.Size = new System.Drawing.Size(1400, 170);
            this.pnlTextBoxCardMoi.Controls.Add(this.txtKetQuaMoi);

            this.txtKetQuaMoi.Multiline = true;
            this.txtKetQuaMoi.Font = fontNormal;
            this.txtKetQuaMoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtKetQuaMoi.Location = new System.Drawing.Point(10, 10);
            this.txtKetQuaMoi.Size = new System.Drawing.Size(675, 120);

            // NÚT HOÀN TẤT
            this.btnHoanTat.BackColor = System.Drawing.Color.FromArgb(50, 120, 140);
            this.btnHoanTat.ForeColor = System.Drawing.Color.White;
            this.btnHoanTat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoanTat.FlatAppearance.BorderSize = 0;
            this.btnHoanTat.Font = fontBoldLabel;
            this.btnHoanTat.Location = new System.Drawing.Point(800, 650);
            this.btnHoanTat.Size = new System.Drawing.Size(140, 45);
            this.btnHoanTat.Text = "Hoàn tất";
            this.btnHoanTat.Click += new System.EventHandler(this.btnHoanTat_Click);

            // Gắn Controls
            this.pnlBackground.Controls.Add(this.lblPageTitle);
            this.pnlBackground.Controls.Add(this.lblTitleBN);
            this.pnlBackground.Controls.Add(this.lblTitleMaHSBA);
            this.pnlBackground.Controls.Add(this.lblValMaHSBA);
            this.pnlBackground.Controls.Add(this.lblTitleHoTen);
            this.pnlBackground.Controls.Add(this.lblValHoTen);
            this.pnlBackground.Controls.Add(this.lblTitleNgaySinh);
            this.pnlBackground.Controls.Add(this.lblValNgaySinh);
            this.pnlBackground.Controls.Add(this.lblTitleGioiTinh);
            this.pnlBackground.Controls.Add(this.lblValGioiTinh);
            this.pnlBackground.Controls.Add(this.lblTitleDV);
            this.pnlBackground.Controls.Add(this.lblTitleTenDV);
            this.pnlBackground.Controls.Add(this.lblValTenDV);
            this.pnlBackground.Controls.Add(this.lblTitleNgayTH);
            this.pnlBackground.Controls.Add(this.lblValNgayTH);

            this.pnlBackground.Controls.Add(this.lblTitleKetQuaCu);
            this.pnlBackground.Controls.Add(this.pnlTextBoxCardCu);
            this.pnlBackground.Controls.Add(this.lblTitleKetQuaMoi);
            this.pnlBackground.Controls.Add(this.pnlTextBoxCardMoi);

            this.pnlBackground.Controls.Add(this.btnHoanTat);

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            // --- KHÓA CỨNG KÍCH THƯỚC ---
            this.MaximumSize = new System.Drawing.Size(1700, 700);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Size = new System.Drawing.Size(1700, 700);

            this.Controls.Add(this.pnlBackground);
            this.Name = "ServiceResultEntryView";
            this.Load += new System.EventHandler(this.ServiceResultEntryView_Load);
            this.pnlBackground.ResumeLayout(false);
            this.pnlBackground.PerformLayout();
            this.pnlTextBoxCardCu.ResumeLayout(false);
            this.pnlTextBoxCardCu.PerformLayout();
            this.pnlTextBoxCardMoi.ResumeLayout(false);
            this.pnlTextBoxCardMoi.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblTitleBN, lblTitleMaHSBA, lblValMaHSBA, lblTitleHoTen, lblValHoTen, lblTitleNgaySinh, lblValNgaySinh, lblTitleGioiTinh, lblValGioiTinh;
        private System.Windows.Forms.Label lblTitleDV, lblTitleTenDV, lblValTenDV, lblTitleNgayTH, lblValNgayTH;

        private System.Windows.Forms.Label lblTitleKetQuaCu;
        private System.Windows.Forms.TextBox txtKetQuaCu;
        private System.Windows.Forms.Panel pnlTextBoxCardCu;

        private System.Windows.Forms.Label lblTitleKetQuaMoi;
        private System.Windows.Forms.TextBox txtKetQuaMoi;
        private System.Windows.Forms.Panel pnlTextBoxCardMoi;

        private System.Windows.Forms.Button btnHoanTat;
    }
}