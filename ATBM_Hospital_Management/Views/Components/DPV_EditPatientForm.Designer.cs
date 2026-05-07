using System.Drawing;
using System.Windows.Forms;

namespace ATBM_Hospital_Management.Views.Components
{
    partial class DPV_EditPatientForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblMaBN = new System.Windows.Forms.Label();
            this.txtMaBN = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblPhai = new System.Windows.Forms.Label();
            this.cmbPhai = new System.Windows.Forms.ComboBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblSoNha = new System.Windows.Forms.Label();
            this.txtSoNha = new System.Windows.Forms.TextBox();
            this.lblTenDuong = new System.Windows.Forms.Label();
            this.txtTenDuong = new System.Windows.Forms.TextBox();
            this.lblQuanHuyen = new System.Windows.Forms.Label();
            this.txtQuanHuyen = new System.Windows.Forms.TextBox();
            this.lblTinhTp = new System.Windows.Forms.Label();
            this.txtTinhTp = new System.Windows.Forms.TextBox();
            this.lblTienSu = new System.Windows.Forms.Label();
            this.txtTienSu = new System.Windows.Forms.TextBox();
            this.lblTienSuGD = new System.Windows.Forms.Label();
            this.txtTienSuGD = new System.Windows.Forms.TextBox();
            this.lblDiUng = new System.Windows.Forms.Label();
            this.txtDiUng = new System.Windows.Forms.TextBox();
            this.lblChuyenKhoa = new System.Windows.Forms.Label();
            this.cmbChuyenKhoa = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMaBN
            // 
            this.lblMaBN.AutoSize = true;
            this.lblMaBN.Location = new System.Drawing.Point(20, 23);
            this.lblMaBN.Name = "lblMaBN";
            this.lblMaBN.Size = new System.Drawing.Size(43, 13);
            this.lblMaBN.TabIndex = 0;
            this.lblMaBN.Text = "Mã BN:";
            // 
            // txtMaBN
            // 
            this.txtMaBN.Location = new System.Drawing.Point(160, 20);
            this.txtMaBN.Name = "txtMaBN";
            this.txtMaBN.ReadOnly = true;
            this.txtMaBN.Size = new System.Drawing.Size(230, 20);
            this.txtMaBN.TabIndex = 1;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(20, 63);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(42, 13);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(160, 60);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(230, 20);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblPhai
            // 
            this.lblPhai.AutoSize = true;
            this.lblPhai.Location = new System.Drawing.Point(20, 103);
            this.lblPhai.Name = "lblPhai";
            this.lblPhai.Size = new System.Drawing.Size(31, 13);
            this.lblPhai.TabIndex = 4;
            this.lblPhai.Text = "Phái:";
            // 
            // cmbPhai
            // 
            this.cmbPhai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPhai.FormattingEnabled = true;
            this.cmbPhai.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.cmbPhai.Location = new System.Drawing.Point(160, 100);
            this.cmbPhai.Name = "cmbPhai";
            this.cmbPhai.Size = new System.Drawing.Size(230, 21);
            this.cmbPhai.TabIndex = 5;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Location = new System.Drawing.Point(20, 143);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(57, 13);
            this.lblNgaySinh.TabIndex = 6;
            this.lblNgaySinh.Text = "Ngày sinh:";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(160, 140);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(230, 20);
            this.dtpNgaySinh.TabIndex = 7;
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Location = new System.Drawing.Point(20, 183);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(39, 13);
            this.lblCCCD.TabIndex = 8;
            this.lblCCCD.Text = "CCCD:";
            // 
            // txtCCCD
            // 
            this.txtCCCD.Location = new System.Drawing.Point(160, 180);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(230, 20);
            this.txtCCCD.TabIndex = 9;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(20, 223);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(32, 13);
            this.lblSDT.TabIndex = 10;
            this.lblSDT.Text = "SĐT:";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(160, 220);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(230, 20);
            this.txtSDT.TabIndex = 11;
            // 
            // lblSoNha
            // 
            this.lblSoNha.AutoSize = true;
            this.lblSoNha.Location = new System.Drawing.Point(20, 263);
            this.lblSoNha.Name = "lblSoNha";
            this.lblSoNha.Size = new System.Drawing.Size(44, 13);
            this.lblSoNha.TabIndex = 12;
            this.lblSoNha.Text = "Số nhà:";
            // 
            // txtSoNha
            // 
            this.txtSoNha.Location = new System.Drawing.Point(160, 260);
            this.txtSoNha.Name = "txtSoNha";
            this.txtSoNha.Size = new System.Drawing.Size(230, 20);
            this.txtSoNha.TabIndex = 13;
            // 
            // lblTenDuong
            // 
            this.lblTenDuong.AutoSize = true;
            this.lblTenDuong.Location = new System.Drawing.Point(20, 303);
            this.lblTenDuong.Name = "lblTenDuong";
            this.lblTenDuong.Size = new System.Drawing.Size(63, 13);
            this.lblTenDuong.TabIndex = 14;
            this.lblTenDuong.Text = "Tên đường:";
            // 
            // txtTenDuong
            // 
            this.txtTenDuong.Location = new System.Drawing.Point(160, 300);
            this.txtTenDuong.Name = "txtTenDuong";
            this.txtTenDuong.Size = new System.Drawing.Size(230, 20);
            this.txtTenDuong.TabIndex = 15;
            // 
            // lblQuanHuyen
            // 
            this.lblQuanHuyen.AutoSize = true;
            this.lblQuanHuyen.Location = new System.Drawing.Point(20, 343);
            this.lblQuanHuyen.Name = "lblQuanHuyen";
            this.lblQuanHuyen.Size = new System.Drawing.Size(72, 13);
            this.lblQuanHuyen.TabIndex = 16;
            this.lblQuanHuyen.Text = "Quận/Huyện:";
            // 
            // txtQuanHuyen
            // 
            this.txtQuanHuyen.Location = new System.Drawing.Point(160, 340);
            this.txtQuanHuyen.Name = "txtQuanHuyen";
            this.txtQuanHuyen.Size = new System.Drawing.Size(230, 20);
            this.txtQuanHuyen.TabIndex = 17;
            // 
            // lblTinhTp
            // 
            this.lblTinhTp.AutoSize = true;
            this.lblTinhTp.Location = new System.Drawing.Point(20, 383);
            this.lblTinhTp.Name = "lblTinhTp";
            this.lblTinhTp.Size = new System.Drawing.Size(51, 13);
            this.lblTinhTp.TabIndex = 18;
            this.lblTinhTp.Text = "Tỉnh/TP:";
            // 
            // txtTinhTp
            // 
            this.txtTinhTp.Location = new System.Drawing.Point(160, 380);
            this.txtTinhTp.Name = "txtTinhTp";
            this.txtTinhTp.Size = new System.Drawing.Size(230, 20);
            this.txtTinhTp.TabIndex = 19;
            // 
            // lblTienSu
            // 
            this.lblTienSu.AutoSize = true;
            this.lblTienSu.Location = new System.Drawing.Point(20, 423);
            this.lblTienSu.Name = "lblTienSu";
            this.lblTienSu.Size = new System.Drawing.Size(71, 13);
            this.lblTienSu.TabIndex = 20;
            this.lblTienSu.Text = "Tiền sử bệnh:";
            // 
            // txtTienSu
            // 
            this.txtTienSu.Location = new System.Drawing.Point(160, 420);
            this.txtTienSu.Name = "txtTienSu";
            this.txtTienSu.Size = new System.Drawing.Size(230, 20);
            this.txtTienSu.TabIndex = 21;
            // 
            // lblTienSuGD
            // 
            this.lblTienSuGD.AutoSize = true;
            this.lblTienSuGD.Location = new System.Drawing.Point(20, 463);
            this.lblTienSuGD.Name = "lblTienSuGD";
            this.lblTienSuGD.Size = new System.Drawing.Size(66, 13);
            this.lblTienSuGD.TabIndex = 22;
            this.lblTienSuGD.Text = "TS bệnh GĐ:";
            // 
            // txtTienSuGD
            // 
            this.txtTienSuGD.Location = new System.Drawing.Point(160, 460);
            this.txtTienSuGD.Name = "txtTienSuGD";
            this.txtTienSuGD.Size = new System.Drawing.Size(230, 20);
            this.txtTienSuGD.TabIndex = 23;
            // 
            // lblDiUng
            // 
            this.lblDiUng.AutoSize = true;
            this.lblDiUng.Location = new System.Drawing.Point(20, 503);
            this.lblDiUng.Name = "lblDiUng";
            this.lblDiUng.Size = new System.Drawing.Size(73, 13);
            this.lblDiUng.TabIndex = 24;
            this.lblDiUng.Text = "Dị ứng thuốc:";
            // 
            // txtDiUng
            // 
            this.txtDiUng.Location = new System.Drawing.Point(160, 500);
            this.txtDiUng.Name = "txtDiUng";
            this.txtDiUng.Size = new System.Drawing.Size(230, 20);
            this.txtDiUng.TabIndex = 25;
            // 
            // lblChuyenKhoa
            // 
            this.lblChuyenKhoa.AutoSize = true;
            this.lblChuyenKhoa.Location = new System.Drawing.Point(20, 543);
            this.lblChuyenKhoa.Name = "lblChuyenKhoa";
            this.lblChuyenKhoa.Size = new System.Drawing.Size(73, 13);
            this.lblChuyenKhoa.TabIndex = 26;
            this.lblChuyenKhoa.Text = "Chuyên khoa:";
            // 
            // cmbChuyenKhoa
            // 
            this.cmbChuyenKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChuyenKhoa.FormattingEnabled = true;
            this.cmbChuyenKhoa.Location = new System.Drawing.Point(160, 540);
            this.cmbChuyenKhoa.Name = "cmbChuyenKhoa";
            this.cmbChuyenKhoa.Size = new System.Drawing.Size(230, 21);
            this.cmbChuyenKhoa.TabIndex = 27;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(160, 590);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "Cập nhật";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // DPV_EditPatientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(440, 650);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbChuyenKhoa);
            this.Controls.Add(this.lblChuyenKhoa);
            this.Controls.Add(this.txtDiUng);
            this.Controls.Add(this.lblDiUng);
            this.Controls.Add(this.txtTienSuGD);
            this.Controls.Add(this.lblTienSuGD);
            this.Controls.Add(this.txtTienSu);
            this.Controls.Add(this.lblTienSu);
            this.Controls.Add(this.txtTinhTp);
            this.Controls.Add(this.lblTinhTp);
            this.Controls.Add(this.txtQuanHuyen);
            this.Controls.Add(this.lblQuanHuyen);
            this.Controls.Add(this.txtTenDuong);
            this.Controls.Add(this.lblTenDuong);
            this.Controls.Add(this.txtSoNha);
            this.Controls.Add(this.lblSoNha);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.lblSDT);
            this.Controls.Add(this.txtCCCD);
            this.Controls.Add(this.lblCCCD);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.lblNgaySinh);
            this.Controls.Add(this.cmbPhai);
            this.Controls.Add(this.lblPhai);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.txtMaBN);
            this.Controls.Add(this.lblMaBN);
            this.Name = "DPV_EditPatientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật bệnh nhân";
            this.Load += new System.EventHandler(this.DPV_EditPatientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public System.Windows.Forms.Label lblMaBN;
        public System.Windows.Forms.TextBox txtMaBN;
        public System.Windows.Forms.Label lblHoTen;
        public System.Windows.Forms.TextBox txtHoTen;
        public System.Windows.Forms.Label lblPhai;
        public System.Windows.Forms.ComboBox cmbPhai;
        public System.Windows.Forms.Label lblNgaySinh;
        public System.Windows.Forms.DateTimePicker dtpNgaySinh;
        public System.Windows.Forms.Label lblCCCD;
        public System.Windows.Forms.TextBox txtCCCD;
        public System.Windows.Forms.Label lblSDT;
        public System.Windows.Forms.TextBox txtSDT;
        public System.Windows.Forms.Label lblSoNha;
        public System.Windows.Forms.TextBox txtSoNha;
        public System.Windows.Forms.Label lblTenDuong;
        public System.Windows.Forms.TextBox txtTenDuong;
        public System.Windows.Forms.Label lblQuanHuyen;
        public System.Windows.Forms.TextBox txtQuanHuyen;
        public System.Windows.Forms.Label lblTinhTp;
        public System.Windows.Forms.TextBox txtTinhTp;
        public System.Windows.Forms.Label lblTienSu;
        public System.Windows.Forms.TextBox txtTienSu;
        public System.Windows.Forms.Label lblTienSuGD;
        public System.Windows.Forms.TextBox txtTienSuGD;
        public System.Windows.Forms.Label lblDiUng;
        public System.Windows.Forms.TextBox txtDiUng;
        public System.Windows.Forms.Label lblChuyenKhoa;
        public System.Windows.Forms.ComboBox cmbChuyenKhoa;
        public System.Windows.Forms.Button btnSave;
    }
}
