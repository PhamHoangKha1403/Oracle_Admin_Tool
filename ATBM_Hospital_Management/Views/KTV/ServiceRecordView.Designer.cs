namespace ATBM_Hospital_Management.Views.KTV
{
    partial class ServiceRecordView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle cellStyle = new System.Windows.Forms.DataGridViewCellStyle();

            this.pnlBackground = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.dgvRecords = new System.Windows.Forms.DataGridView();

            this.pnlBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            this.SuspendLayout();

            // pnlBackground
            this.pnlBackground.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlBackground.Controls.Add(this.cboStatus);
            this.pnlBackground.Controls.Add(this.txtSearch);
            this.pnlBackground.Controls.Add(this.dgvRecords);
            this.pnlBackground.Controls.Add(this.lblPageTitle);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Padding = new System.Windows.Forms.Padding(40);

            // lblPageTitle
            this.lblPageTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblPageTitle.Location = new System.Drawing.Point(40, 40);
            this.lblPageTitle.Size = new System.Drawing.Size(920, 50);
            this.lblPageTitle.Text = "DANH SÁCH HỒ SƠ BỆNH ÁN DỊCH VỤ";
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // txtSearch
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.txtSearch.Location = new System.Drawing.Point(40, 90);
            this.txtSearch.Size = new System.Drawing.Size(300, 32);
            this.txtSearch.Text = "Tìm Hồ sơ bệnh án";
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // cboStatus
            this.cboStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboStatus.Items.AddRange(new object[] { "Tất cả", "Hoàn thành", "Chưa hoàn thành" });
            this.cboStatus.Location = new System.Drawing.Point(760, 90); // Đã đẩy tọa độ sang phải cho khớp mỏ neo
            this.cboStatus.Size = new System.Drawing.Size(200, 33);
            this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);

            // dgvRecords
            this.dgvRecords.AllowUserToAddRows = false;
            this.dgvRecords.AllowUserToDeleteRows = false;
            this.dgvRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecords.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvRecords.MaximumSize = new System.Drawing.Size(1620, 550);

            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            headerStyle.BackColor = System.Drawing.Color.White;
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvRecords.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvRecords.ColumnHeadersHeight = 40;
            this.dgvRecords.EnableHeadersVisualStyles = false;

            cellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            cellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            cellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvRecords.DefaultCellStyle = cellStyle;

            this.dgvRecords.Location = new System.Drawing.Point(40, 140);
            this.dgvRecords.ReadOnly = true;
            this.dgvRecords.RowHeadersVisible = false;
            this.dgvRecords.RowTemplate.Height = 45;
            this.dgvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecords.Size = new System.Drawing.Size(920, 450);
            this.dgvRecords.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            this.dgvRecords.AutoGenerateColumns = false;
            this.dgvRecords.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecords_CellContentClick);
            this.dgvRecords.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRecords_CellFormatting);

            // --- ÉP KÍCH THƯỚC VÀ CHIA TỈ LỆ CÁC CỘT  ---

            // Cột Mã HSBA 
            this.dgvRecords.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                DataPropertyName = "MA_HSBA",
                Name = "MA_HSBA",
                HeaderText = "MÃ HSBA",
                SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable,
                FillWeight = 10,
                MinimumWidth = 50
            });
            // Cột Tên dịch vụ
            this.dgvRecords.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                DataPropertyName = "LOAI_DV",
                Name = "LOAI_DV",
                HeaderText = "TÊN DỊCH VỤ",
                SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable,
                FillWeight = 35,
                MinimumWidth = 100
            });
            // Cột Ngày thực hiện
            this.dgvRecords.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                DataPropertyName = "NGAY_DV",
                Name = "NGAY_DV",
                HeaderText = "NGÀY THỰC HIỆN",
                SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic,
                FillWeight = 15,
                MinimumWidth = 80
            });
            // Cột KET_QUA ẩn đi 
            this.dgvRecords.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                DataPropertyName = "KET_QUA",
                Name = "KET_QUA",
                Visible = false
            });
            // Cột Trạng thái
            this.dgvRecords.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                DataPropertyName = "TRANG_THAI",
                Name = "TRANG_THAI",
                HeaderText = "TRẠNG THÁI",
                SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable,
                FillWeight = 10,
                MinimumWidth = 80
            });
            // Cột Nút Action 
            System.Windows.Forms.DataGridViewButtonColumn btnAction = new System.Windows.Forms.DataGridViewButtonColumn();
            btnAction.Name = "ActionBtn";
            btnAction.HeaderText = "";
            btnAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAction.FillWeight = 10;
            btnAction.MinimumWidth = 40;
            this.dgvRecords.Columns.Add(btnAction);

            // Form setup
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MinimumSize = new System.Drawing.Size(900, 500); 
            this.Controls.Add(this.pnlBackground);
            this.Name = "ServiceRecordView";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.ServiceRecordView_Load);

            this.pnlBackground.ResumeLayout(false);
            this.pnlBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.DataGridView dgvRecords;
    }
}