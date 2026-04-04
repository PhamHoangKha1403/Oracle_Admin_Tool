namespace ATBM_Hospital_Management.Views
{
    partial class RegisterView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCreateAll = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblPageTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 56;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 0, 16, 0);
            this.pnlHeader.TabIndex = 0;

            this.lblPageTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblPageTitle.Location = new System.Drawing.Point(20, 16);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Create Accounts";

            // pnlBody
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlBody.Controls.Add(this.pnlCard);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(16);
            this.pnlBody.TabIndex = 1;

            // pnlCard
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.dgvEmployees);
            this.pnlCard.Controls.Add(this.lblStatus);
            this.pnlCard.Controls.Add(this.pnlToolbar);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.TabIndex = 0;

            // pnlToolbar
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 110;
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(16, 10, 16, 8);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.TabIndex = 0;
            this.pnlToolbar.Controls.Add(this.lblStatus);
            this.pnlToolbar.Controls.Add(this.pnlButtons);
            this.pnlToolbar.Controls.Add(this.txtSearch);

            // txtSearch
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.txtSearch.Height = 32;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Search by ID or name...";
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.TxtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.TxtSearch_Leave);

            // pnlButtons
            this.pnlButtons.AutoSize = false;
            this.pnlButtons.BackColor = System.Drawing.Color.White;
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.pnlButtons.Height = 38;
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.pnlButtons.WrapContents = false;
            this.pnlButtons.TabIndex = 1;
            this.pnlButtons.Controls.Add(this.btnCreate);
            this.pnlButtons.Controls.Add(this.btnCreateAll);
            this.pnlButtons.Controls.Add(this.btnRefresh);

            System.Drawing.Font btnFont = new System.Drawing.Font("Segoe UI", 9F);

            // btnCreate — dark primary
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.FlatAppearance.BorderSize = 0;
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Font = btnFont;
            this.btnCreate.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(130, 28);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "CREATE ACCOUNT";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Enabled = false;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);

            // btnCreateAll — outlined
            this.btnCreateAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnCreateAll.FlatAppearance.BorderSize = 1;
            this.btnCreateAll.BackColor = System.Drawing.Color.White;
            this.btnCreateAll.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnCreateAll.Font = btnFont;
            this.btnCreateAll.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnCreateAll.Name = "btnCreateAll";
            this.btnCreateAll.Size = new System.Drawing.Size(140, 28);
            this.btnCreateAll.TabIndex = 1;
            this.btnCreateAll.Text = "CREATE ALL";
            this.btnCreateAll.UseVisualStyleBackColor = false;
            this.btnCreateAll.Click += new System.EventHandler(this.BtnCreateAll_Click);

            // btnRefresh — outlined
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnRefresh.Font = btnFont;
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 28);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);

            // lblStatus — sits below buttons in toolbar
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblStatus.Height = 20;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "";

            // dgvEmployees
            this.dgvEmployees.AllowUserToAddRows = false;
            this.dgvEmployees.AllowUserToDeleteRows = false;
            this.dgvEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmployees.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmployees.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmployees.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvEmployees.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvEmployees.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle()
            {
                BackColor = System.Drawing.Color.FromArgb(245, 245, 245),
                ForeColor = System.Drawing.Color.FromArgb(60, 60, 60),
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold),
                SelectionBackColor = System.Drawing.Color.FromArgb(245, 245, 245),
                SelectionForeColor = System.Drawing.Color.FromArgb(60, 60, 60),
                Padding = new System.Windows.Forms.Padding(8, 0, 0, 0)
            };
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEmployees.ColumnHeadersHeight = 36;
            this.dgvEmployees.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle()
            {
                BackColor = System.Drawing.Color.White,
                ForeColor = System.Drawing.Color.FromArgb(40, 40, 40),
                Font = new System.Drawing.Font("Segoe UI", 9F),
                SelectionBackColor = System.Drawing.Color.FromArgb(220, 220, 220),
                SelectionForeColor = System.Drawing.Color.FromArgb(20, 20, 20),
                Padding = new System.Windows.Forms.Padding(8, 0, 0, 0)
            };
            this.dgvEmployees.GridColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.dgvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployees.EnableHeadersVisualStyles = false;
            this.dgvEmployees.MultiSelect = false;
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.ReadOnly = true;
            this.dgvEmployees.RowHeadersVisible = false;
            this.dgvEmployees.RowTemplate.Height = 34;
            this.dgvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.TabIndex = 1;

            // RegisterView
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "RegisterView";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.FlowLayoutPanel pnlButtons;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCreateAll;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.Label lblStatus;
    }
}
