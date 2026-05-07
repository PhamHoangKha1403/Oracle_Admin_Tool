using System.Drawing;
using System.Windows.Forms;

namespace ATBM_Hospital_Management.Views.Components
{
    partial class DPV_AddHSBAForm
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
            this.lblMaHSBA = new System.Windows.Forms.Label();
            this.txtMaHSBA = new System.Windows.Forms.TextBox();
            this.lblMaBN = new System.Windows.Forms.Label();
            this.txtMaBN = new System.Windows.Forms.TextBox();
            this.lblNgay = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.lblChanDoan = new System.Windows.Forms.Label();
            this.txtChanDoan = new System.Windows.Forms.TextBox();
            this.lblDieuTri = new System.Windows.Forms.Label();
            this.txtDieuTri = new System.Windows.Forms.TextBox();
            this.lblMaBS = new System.Windows.Forms.Label();
            this.txtMaBS = new System.Windows.Forms.TextBox();
            this.lblKhoaKham = new System.Windows.Forms.Label();
            this.cmbKhoaKham = new System.Windows.Forms.ComboBox();
            this.lblKetLuan = new System.Windows.Forms.Label();
            this.txtKetLuan = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMaHSBA
            // 
            this.lblMaHSBA.AutoSize = true;
            this.lblMaHSBA.Location = new System.Drawing.Point(20, 23);
            this.lblMaHSBA.Name = "lblMaHSBA";
            this.lblMaHSBA.Size = new System.Drawing.Size(57, 13);
            this.lblMaHSBA.TabIndex = 0;
            this.lblMaHSBA.Text = "Mã HSBA:";
            // 
            // txtMaHSBA
            // 
            this.txtMaHSBA.Location = new System.Drawing.Point(160, 20);
            this.txtMaHSBA.Name = "txtMaHSBA";
            this.txtMaHSBA.ReadOnly = true;
            this.txtMaHSBA.Size = new System.Drawing.Size(230, 20);
            this.txtMaHSBA.TabIndex = 1;
            // 
            // lblMaBN
            // 
            this.lblMaBN.AutoSize = true;
            this.lblMaBN.Location = new System.Drawing.Point(20, 63);
            this.lblMaBN.Name = "lblMaBN";
            this.lblMaBN.Size = new System.Drawing.Size(43, 13);
            this.lblMaBN.TabIndex = 2;
            this.lblMaBN.Text = "Mã BN:";
            // 
            // txtMaBN
            // 
            this.txtMaBN.Location = new System.Drawing.Point(160, 60);
            this.txtMaBN.Name = "txtMaBN";
            this.txtMaBN.Size = new System.Drawing.Size(230, 20);
            this.txtMaBN.TabIndex = 3;
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(20, 103);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(64, 13);
            this.lblNgay.TabIndex = 4;
            this.lblNgay.Text = "Ngày khám:";
            // 
            // dtpNgay
            // 
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgay.Location = new System.Drawing.Point(160, 100);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(230, 20);
            this.dtpNgay.TabIndex = 5;
            // 
            // lblChanDoan
            // 
            this.lblChanDoan.AutoSize = true;
            this.lblChanDoan.Location = new System.Drawing.Point(20, 143);
            this.lblChanDoan.Name = "lblChanDoan";
            this.lblChanDoan.Size = new System.Drawing.Size(63, 13);
            this.lblChanDoan.TabIndex = 6;
            this.lblChanDoan.Text = "Chẩn đoán:";
            // 
            // txtChanDoan
            // 
            this.txtChanDoan.Location = new System.Drawing.Point(160, 140);
            this.txtChanDoan.Name = "txtChanDoan";
            this.txtChanDoan.Size = new System.Drawing.Size(230, 20);
            this.txtChanDoan.TabIndex = 7;
            // 
            // lblDieuTri
            // 
            this.lblDieuTri.AutoSize = true;
            this.lblDieuTri.Location = new System.Drawing.Point(20, 183);
            this.lblDieuTri.Name = "lblDieuTri";
            this.lblDieuTri.Size = new System.Drawing.Size(43, 13);
            this.lblDieuTri.TabIndex = 8;
            this.lblDieuTri.Text = "Điều trị:";
            // 
            // txtDieuTri
            // 
            this.txtDieuTri.Location = new System.Drawing.Point(160, 180);
            this.txtDieuTri.Name = "txtDieuTri";
            this.txtDieuTri.Size = new System.Drawing.Size(230, 20);
            this.txtDieuTri.TabIndex = 9;
            // 
            // lblMaBS
            // 
            this.lblMaBS.AutoSize = true;
            this.lblMaBS.Location = new System.Drawing.Point(20, 223);
            this.lblMaBS.Name = "lblMaBS";
            this.lblMaBS.Size = new System.Drawing.Size(43, 13);
            this.lblMaBS.TabIndex = 10;
            this.lblMaBS.Text = "Mã BS:";
            // 
            // txtMaBS
            // 
            this.txtMaBS.Location = new System.Drawing.Point(160, 220);
            this.txtMaBS.Name = "txtMaBS";
            this.txtMaBS.Size = new System.Drawing.Size(230, 20);
            this.txtMaBS.TabIndex = 11;
            // 
            // lblKhoaKham
            // 
            this.lblKhoaKham.AutoSize = true;
            this.lblKhoaKham.Location = new System.Drawing.Point(20, 263);
            this.lblKhoaKham.Name = "lblKhoaKham";
            this.lblKhoaKham.Size = new System.Drawing.Size(63, 13);
            this.lblKhoaKham.TabIndex = 12;
            this.lblKhoaKham.Text = "Khoa khám:";
            // 
            // cmbKhoaKham
            // 
            this.cmbKhoaKham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKhoaKham.FormattingEnabled = true;
            this.cmbKhoaKham.Location = new System.Drawing.Point(160, 260);
            this.cmbKhoaKham.Name = "cmbKhoaKham";
            this.cmbKhoaKham.Size = new System.Drawing.Size(230, 21);
            this.cmbKhoaKham.TabIndex = 13;
            // 
            // lblKetLuan
            // 
            this.lblKetLuan.AutoSize = true;
            this.lblKetLuan.Location = new System.Drawing.Point(20, 303);
            this.lblKetLuan.Name = "lblKetLuan";
            this.lblKetLuan.Size = new System.Drawing.Size(49, 13);
            this.lblKetLuan.TabIndex = 14;
            this.lblKetLuan.Text = "Kết luận:";
            // 
            // txtKetLuan
            // 
            this.txtKetLuan.Location = new System.Drawing.Point(160, 300);
            this.txtKetLuan.Name = "txtKetLuan";
            this.txtKetLuan.Size = new System.Drawing.Size(230, 20);
            this.txtKetLuan.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(160, 350);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // DPV_AddHSBAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(440, 420);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtKetLuan);
            this.Controls.Add(this.lblKetLuan);
            this.Controls.Add(this.cmbKhoaKham);
            this.Controls.Add(this.lblKhoaKham);
            this.Controls.Add(this.txtMaBS);
            this.Controls.Add(this.lblMaBS);
            this.Controls.Add(this.txtDieuTri);
            this.Controls.Add(this.lblDieuTri);
            this.Controls.Add(this.txtChanDoan);
            this.Controls.Add(this.lblChanDoan);
            this.Controls.Add(this.dtpNgay);
            this.Controls.Add(this.lblNgay);
            this.Controls.Add(this.txtMaBN);
            this.Controls.Add(this.lblMaBN);
            this.Controls.Add(this.txtMaHSBA);
            this.Controls.Add(this.lblMaHSBA);
            this.Name = "DPV_AddHSBAForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm Hồ sơ bệnh án";
            this.Load += new System.EventHandler(this.DPV_AddHSBAForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public System.Windows.Forms.Label lblMaHSBA;
        public System.Windows.Forms.TextBox txtMaHSBA;
        public System.Windows.Forms.Label lblMaBN;
        public System.Windows.Forms.TextBox txtMaBN;
        public System.Windows.Forms.Label lblNgay;
        public System.Windows.Forms.DateTimePicker dtpNgay;
        public System.Windows.Forms.Label lblChanDoan;
        public System.Windows.Forms.TextBox txtChanDoan;
        public System.Windows.Forms.Label lblDieuTri;
        public System.Windows.Forms.TextBox txtDieuTri;
        public System.Windows.Forms.Label lblMaBS;
        public System.Windows.Forms.TextBox txtMaBS;
        public System.Windows.Forms.Label lblKhoaKham;
        public System.Windows.Forms.ComboBox cmbKhoaKham;
        public System.Windows.Forms.Label lblKetLuan;
        public System.Windows.Forms.TextBox txtKetLuan;
        public System.Windows.Forms.Button btnSave;
    }
}
