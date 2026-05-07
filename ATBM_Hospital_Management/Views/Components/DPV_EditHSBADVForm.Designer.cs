using System.Drawing;
using System.Windows.Forms;

namespace ATBM_Hospital_Management.Views.Components
{
    partial class DPV_EditHSBADVForm
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
            this.lblLoaiDV = new System.Windows.Forms.Label();
            this.txtLoaiDV = new System.Windows.Forms.TextBox();
            this.lblNgayDV = new System.Windows.Forms.Label();
            this.dtpNgayDV = new System.Windows.Forms.DateTimePicker();
            this.lblMaKTV = new System.Windows.Forms.Label();
            this.txtMaKTV = new System.Windows.Forms.TextBox();
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
            // lblLoaiDV
            // 
            this.lblLoaiDV.AutoSize = true;
            this.lblLoaiDV.Location = new System.Drawing.Point(20, 63);
            this.lblLoaiDV.Name = "lblLoaiDV";
            this.lblLoaiDV.Size = new System.Drawing.Size(48, 13);
            this.lblLoaiDV.TabIndex = 2;
            this.lblLoaiDV.Text = "Loại DV:";
            // 
            // txtLoaiDV
            // 
            this.txtLoaiDV.Location = new System.Drawing.Point(160, 60);
            this.txtLoaiDV.Name = "txtLoaiDV";
            this.txtLoaiDV.ReadOnly = true;
            this.txtLoaiDV.Size = new System.Drawing.Size(230, 20);
            this.txtLoaiDV.TabIndex = 3;
            // 
            // lblNgayDV
            // 
            this.lblNgayDV.AutoSize = true;
            this.lblNgayDV.Location = new System.Drawing.Point(20, 103);
            this.lblNgayDV.Name = "lblNgayDV";
            this.lblNgayDV.Size = new System.Drawing.Size(53, 13);
            this.lblNgayDV.TabIndex = 4;
            this.lblNgayDV.Text = "Ngày DV:";
            // 
            // dtpNgayDV
            // 
            this.dtpNgayDV.Enabled = false;
            this.dtpNgayDV.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayDV.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayDV.Location = new System.Drawing.Point(160, 100);
            this.dtpNgayDV.Name = "dtpNgayDV";
            this.dtpNgayDV.Size = new System.Drawing.Size(230, 20);
            this.dtpNgayDV.TabIndex = 5;
            // 
            // lblMaKTV
            // 
            this.lblMaKTV.AutoSize = true;
            this.lblMaKTV.Location = new System.Drawing.Point(20, 143);
            this.lblMaKTV.Name = "lblMaKTV";
            this.lblMaKTV.Size = new System.Drawing.Size(71, 13);
            this.lblMaKTV.TabIndex = 6;
            this.lblMaKTV.Text = "Mã KTV mới:";
            // 
            // txtMaKTV
            // 
            this.txtMaKTV.Location = new System.Drawing.Point(160, 140);
            this.txtMaKTV.Name = "txtMaKTV";
            this.txtMaKTV.Size = new System.Drawing.Size(230, 20);
            this.txtMaKTV.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(160, 190);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Cập nhật";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // DPV_EditHSBADVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(440, 250);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtMaKTV);
            this.Controls.Add(this.lblMaKTV);
            this.Controls.Add(this.dtpNgayDV);
            this.Controls.Add(this.lblNgayDV);
            this.Controls.Add(this.txtLoaiDV);
            this.Controls.Add(this.lblLoaiDV);
            this.Controls.Add(this.txtMaHSBA);
            this.Controls.Add(this.lblMaHSBA);
            this.Name = "DPV_EditHSBADVForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật dịch vụ";
            this.Load += new System.EventHandler(this.DPV_EditHSBADVForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public System.Windows.Forms.Label lblMaHSBA;
        public System.Windows.Forms.TextBox txtMaHSBA;
        public System.Windows.Forms.Label lblLoaiDV;
        public System.Windows.Forms.TextBox txtLoaiDV;
        public System.Windows.Forms.Label lblNgayDV;
        public System.Windows.Forms.DateTimePicker dtpNgayDV;
        public System.Windows.Forms.Label lblMaKTV;
        public System.Windows.Forms.TextBox txtMaKTV;
        public System.Windows.Forms.Button btnSave;
    }
}
