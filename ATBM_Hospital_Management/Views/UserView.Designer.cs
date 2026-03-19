namespace ATBM_Hospital_Management.Views
{
    partial class UserView
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnLockUnlock = new System.Windows.Forms.Button();
            this.btnDrop = new System.Windows.Forms.Button();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
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
            this.lblPageTitle.Text = "Users";

            // pnlBody — ash gray background
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlBody.Controls.Add(this.pnlCard);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(16);
            this.pnlBody.TabIndex = 1;

            // pnlCard — white card with border
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.dgvUsers);
            this.pnlCard.Controls.Add(this.pnlToolbar);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.TabIndex = 0;

            // pnlToolbar
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 84;
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(16, 10, 16, 8);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.TabIndex = 0;
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
            this.txtSearch.Text = "Search username...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);

            // pnlButtons — FlowLayoutPanel
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
            this.pnlButtons.Controls.Add(this.btnRefresh);
            this.pnlButtons.Controls.Add(this.btnChangePassword);
            this.pnlButtons.Controls.Add(this.btnLockUnlock);
            this.pnlButtons.Controls.Add(this.btnDrop);

            System.Drawing.Font btnFont = new System.Drawing.Font("Segoe UI", 9F);

            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.FlatAppearance.BorderSize = 0;
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Font = btnFont;
            this.btnCreate.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(120, 28);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "CREATE USER";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);

            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnRefresh.Font = btnFont;
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 28);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnChangePassword.FlatAppearance.BorderSize = 1;
            this.btnChangePassword.BackColor = System.Drawing.Color.White;
            this.btnChangePassword.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnChangePassword.Font = btnFont;
            this.btnChangePassword.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(160, 28);
            this.btnChangePassword.TabIndex = 2;
            this.btnChangePassword.Text = "CHANGE PASSWORD";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);

            this.btnLockUnlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLockUnlock.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnLockUnlock.FlatAppearance.BorderSize = 1;
            this.btnLockUnlock.BackColor = System.Drawing.Color.White;
            this.btnLockUnlock.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnLockUnlock.Font = btnFont;
            this.btnLockUnlock.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnLockUnlock.Name = "btnLockUnlock";
            this.btnLockUnlock.Size = new System.Drawing.Size(130, 28);
            this.btnLockUnlock.TabIndex = 3;
            this.btnLockUnlock.Text = "LOCK / UNLOCK";
            this.btnLockUnlock.UseVisualStyleBackColor = false;
            this.btnLockUnlock.Click += new System.EventHandler(this.btnLockUnlock_Click);

            this.btnDrop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnDrop.FlatAppearance.BorderSize = 1;
            this.btnDrop.BackColor = System.Drawing.Color.White;
            this.btnDrop.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnDrop.Font = btnFont;
            this.btnDrop.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnDrop.Name = "btnDrop";
            this.btnDrop.Size = new System.Drawing.Size(100, 28);
            this.btnDrop.TabIndex = 4;
            this.btnDrop.Text = "DROP USER";
            this.btnDrop.UseVisualStyleBackColor = false;
            this.btnDrop.Click += new System.EventHandler(this.btnDrop_Click);

            // dgvUsers
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUsers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvUsers.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle() { BackColor = System.Drawing.Color.FromArgb(245, 245, 245), ForeColor = System.Drawing.Color.FromArgb(60, 60, 60), Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold), SelectionBackColor = System.Drawing.Color.FromArgb(245, 245, 245), SelectionForeColor = System.Drawing.Color.FromArgb(60, 60, 60), Padding = new System.Windows.Forms.Padding(8, 0, 0, 0) };
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUsers.ColumnHeadersHeight = 36;
            this.dgvUsers.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle() { BackColor = System.Drawing.Color.White, ForeColor = System.Drawing.Color.FromArgb(40, 40, 40), Font = new System.Drawing.Font("Segoe UI", 9F), SelectionBackColor = System.Drawing.Color.FromArgb(220, 220, 220), SelectionForeColor = System.Drawing.Color.FromArgb(20, 20, 20), Padding = new System.Windows.Forms.Padding(8, 0, 0, 0) };
            this.dgvUsers.GridColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.EnableHeadersVisualStyles = false;
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.RowTemplate.Height = 34;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.TabIndex = 1;

            // UserView
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "UserView";
            this.Load += new System.EventHandler(this.UserView_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
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
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnLockUnlock;
        private System.Windows.Forms.Button btnDrop;
        private System.Windows.Forms.DataGridView dgvUsers;
    }
}
