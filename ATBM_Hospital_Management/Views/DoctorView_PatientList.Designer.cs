namespace ATBM_Hospital_Management.Views.Components
{
    partial class DoctorView_PatientList
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMainList;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnBenhNhan;
        private System.Windows.Forms.Button btnHoSoBenhAn;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.Label lblUserIcon;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.DataGridView dataGridView1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMainList = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnBenhNhan = new System.Windows.Forms.Button();
            this.btnHoSoBenhAn = new System.Windows.Forms.Button();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.lblUserIcon = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btn_thongBao = new System.Windows.Forms.Button();
            this.btnToi = new System.Windows.Forms.Button();
            this.pnlMainList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelTop.SuspendLayout();
            this.pnlUser.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainList
            // 
            this.pnlMainList.Controls.Add(this.dataGridView1);
            this.pnlMainList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainList.Location = new System.Drawing.Point(20, 5);
            this.pnlMainList.Name = "pnlMainList";
            this.pnlMainList.Size = new System.Drawing.Size(1240, 675);
            this.pnlMainList.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeight = 34;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1240, 675);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRecordCount.Location = new System.Drawing.Point(20, 650);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(1240, 30);
            this.lblRecordCount.TabIndex = 1;
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(198)))), ((int)(((byte)(218)))));
            this.panelTop.Controls.Add(this.btnToi);
            this.panelTop.Controls.Add(this.btn_thongBao);
            this.panelTop.Controls.Add(this.btnBenhNhan);
            this.panelTop.Controls.Add(this.btnHoSoBenhAn);
            this.panelTop.Controls.Add(this.pnlUser);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1280, 60);
            this.panelTop.TabIndex = 1;
            // 
            // btnBenhNhan
            // 
            this.btnBenhNhan.FlatAppearance.BorderSize = 0;
            this.btnBenhNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBenhNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBenhNhan.Location = new System.Drawing.Point(493, 9);
            this.btnBenhNhan.Name = "btnBenhNhan";
            this.btnBenhNhan.Size = new System.Drawing.Size(174, 42);
            this.btnBenhNhan.TabIndex = 0;
            this.btnBenhNhan.Text = "BỆNH NHÂN";
            this.btnBenhNhan.Click += new System.EventHandler(this.btnBenhNhan_Click);
            // 
            // btnHoSoBenhAn
            // 
            this.btnHoSoBenhAn.FlatAppearance.BorderSize = 0;
            this.btnHoSoBenhAn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoSoBenhAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoSoBenhAn.Location = new System.Drawing.Point(684, 9);
            this.btnHoSoBenhAn.Name = "btnHoSoBenhAn";
            this.btnHoSoBenhAn.Size = new System.Drawing.Size(275, 42);
            this.btnHoSoBenhAn.TabIndex = 1;
            this.btnHoSoBenhAn.Text = "HỒ SƠ BỆNH ÁN";
            this.btnHoSoBenhAn.Click += new System.EventHandler(this.btnHoSoBenhAn_Click);
            // 
            // pnlUser
            // 
            this.pnlUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlUser.Controls.Add(this.lblUserIcon);
            this.pnlUser.Controls.Add(this.lblUserName);
            this.pnlUser.Location = new System.Drawing.Point(2090, 0);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(260, 60);
            this.pnlUser.TabIndex = 2;
            // 
            // lblUserIcon
            // 
            this.lblUserIcon.Location = new System.Drawing.Point(0, 0);
            this.lblUserIcon.Name = "lblUserIcon";
            this.lblUserIcon.Size = new System.Drawing.Size(100, 23);
            this.lblUserIcon.TabIndex = 0;
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(0, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(100, 23);
            this.lblUserName.TabIndex = 1;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelContent.Controls.Add(this.lblRecordCount);
            this.panelContent.Controls.Add(this.pnlMainList);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 100);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(20, 5, 20, 20);
            this.panelContent.Size = new System.Drawing.Size(1280, 700);
            this.panelContent.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 60);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1280, 40);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "DANH SÁCH BỆNH NHÂN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_thongBao
            // 
            this.btn_thongBao.FlatAppearance.BorderSize = 0;
            this.btn_thongBao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_thongBao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_thongBao.Location = new System.Drawing.Point(930, 9);
            this.btn_thongBao.Name = "btn_thongBao";
            this.btn_thongBao.Size = new System.Drawing.Size(203, 42);
            this.btn_thongBao.TabIndex = 3;
            this.btn_thongBao.Text = "THÔNG BÁO";
            this.btn_thongBao.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnToi
            // 
            this.btnToi.FlatAppearance.BorderSize = 0;
            this.btnToi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToi.Location = new System.Drawing.Point(1119, 9);
            this.btnToi.Name = "btnToi";
            this.btnToi.Size = new System.Drawing.Size(141, 42);
            this.btnToi.TabIndex = 4;
            this.btnToi.Text = "TÔI";
            this.btnToi.Click += new System.EventHandler(this.button2_Click);
            // 
            // DoctorView_PatientList
            // 
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panelTop);
            this.Name = "DoctorView_PatientList";
            this.Size = new System.Drawing.Size(1280, 800);
            this.Load += new System.EventHandler(this.DoctorView_Load);
            this.pnlMainList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.pnlUser.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btn_thongBao;
        private System.Windows.Forms.Button btnToi;
    }
}