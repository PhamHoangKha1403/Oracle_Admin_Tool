namespace ATBM_Hospital_Management.Views
{
    partial class PrivilegeQueryView
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblPrincipalType = new System.Windows.Forms.Label();
            this.cmbPrincipalType = new System.Windows.Forms.ComboBox();
            this.lblPrincipal = new System.Windows.Forms.Label();
            this.cmbPrincipal = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tabPrivileges = new System.Windows.Forms.TabControl();
            this.tpSystemPrivs = new System.Windows.Forms.TabPage();
            this.dgvSystemPrivs = new System.Windows.Forms.DataGridView();
            this.tpRoleGrants = new System.Windows.Forms.TabPage();
            this.dgvRoleGrants = new System.Windows.Forms.DataGridView();
            this.tpObjectPrivs = new System.Windows.Forms.TabPage();
            this.dgvObjectPrivs = new System.Windows.Forms.DataGridView();
            this.tpColumnPrivs = new System.Windows.Forms.TabPage();
            this.dgvColumnPrivs = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.tabPrivileges.SuspendLayout();
            this.tpSystemPrivs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSystemPrivs)).BeginInit();
            this.tpRoleGrants.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleGrants)).BeginInit();
            this.tpObjectPrivs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectPrivs)).BeginInit();
            this.tpColumnPrivs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnPrivs)).BeginInit();
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
            this.lblPageTitle.Text = "Privileges";

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
            this.pnlCard.Controls.Add(this.tabPrivileges);
            this.pnlCard.Controls.Add(this.lblStatus);
            this.pnlCard.Controls.Add(this.pnlTop);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.TabIndex = 0;

            // pnlTop — filter toolbar, plain Panel with absolute positioning
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.lblPrincipalType);
            this.pnlTop.Controls.Add(this.cmbPrincipalType);
            this.pnlTop.Controls.Add(this.lblPrincipal);
            this.pnlTop.Controls.Add(this.cmbPrincipal);
            this.pnlTop.Controls.Add(this.btnSearch);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 46;
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.TabIndex = 0;

            System.Drawing.Font ctrlFont = new System.Drawing.Font("Segoe UI", 9.5F);
            int ctrlH = 26;
            int top = (46 - ctrlH) / 2;

            // lblPrincipalType
            this.lblPrincipalType.AutoSize = true;
            this.lblPrincipalType.Font = ctrlFont;
            this.lblPrincipalType.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblPrincipalType.Location = new System.Drawing.Point(16, top + 4);
            this.lblPrincipalType.Name = "lblPrincipalType";
            this.lblPrincipalType.Text = "Principal type:";
            this.lblPrincipalType.TabIndex = 0;

            // cmbPrincipalType
            this.cmbPrincipalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrincipalType.Font = ctrlFont;
            this.cmbPrincipalType.Location = new System.Drawing.Point(108, top);
            this.cmbPrincipalType.Name = "cmbPrincipalType";
            this.cmbPrincipalType.Size = new System.Drawing.Size(110, ctrlH);
            this.cmbPrincipalType.TabIndex = 1;

            // lblPrincipal
            this.lblPrincipal.AutoSize = true;
            this.lblPrincipal.Font = ctrlFont;
            this.lblPrincipal.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblPrincipal.Location = new System.Drawing.Point(232, top + 4);
            this.lblPrincipal.Name = "lblPrincipal";
            this.lblPrincipal.Text = "Principal:";
            this.lblPrincipal.TabIndex = 2;

            // cmbPrincipal — DropDown so user can type to search
            this.cmbPrincipal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbPrincipal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPrincipal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPrincipal.Font = ctrlFont;
            this.cmbPrincipal.Location = new System.Drawing.Point(296, top);
            this.cmbPrincipal.Name = "cmbPrincipal";
            this.cmbPrincipal.Size = new System.Drawing.Size(220, ctrlH);
            this.cmbPrincipal.TabIndex = 3;

            // btnSearch
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSearch.Location = new System.Drawing.Point(530, top);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(130, ctrlH);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "VIEW PRIVILEGES";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // lblStatus
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "";
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Height = 20;
            this.lblStatus.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);

            // tabPrivileges
            this.tabPrivileges.Controls.Add(this.tpSystemPrivs);
            this.tabPrivileges.Controls.Add(this.tpRoleGrants);
            this.tabPrivileges.Controls.Add(this.tpObjectPrivs);
            this.tabPrivileges.Controls.Add(this.tpColumnPrivs);
            this.tabPrivileges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPrivileges.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabPrivileges.Name = "tabPrivileges";
            this.tabPrivileges.TabIndex = 1;

            System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle()
            {
                BackColor = System.Drawing.Color.FromArgb(245, 245, 245),
                ForeColor = System.Drawing.Color.FromArgb(60, 60, 60),
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold),
                SelectionBackColor = System.Drawing.Color.FromArgb(245, 245, 245),
                SelectionForeColor = System.Drawing.Color.FromArgb(60, 60, 60),
                Padding = new System.Windows.Forms.Padding(8, 0, 0, 0)
            };
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle()
            {
                BackColor = System.Drawing.Color.White,
                ForeColor = System.Drawing.Color.FromArgb(40, 40, 40),
                Font = new System.Drawing.Font("Segoe UI", 9F),
                SelectionBackColor = System.Drawing.Color.FromArgb(220, 220, 220),
                SelectionForeColor = System.Drawing.Color.FromArgb(20, 20, 20),
                Padding = new System.Windows.Forms.Padding(8, 0, 0, 0)
            };

            // tpSystemPrivs
            this.tpSystemPrivs.Controls.Add(this.dgvSystemPrivs);
            this.tpSystemPrivs.Name = "tpSystemPrivs";
            this.tpSystemPrivs.Text = "System Privileges";
            this.tpSystemPrivs.TabIndex = 0;

            this.dgvSystemPrivs.AllowUserToAddRows = false;
            this.dgvSystemPrivs.AllowUserToDeleteRows = false;
            this.dgvSystemPrivs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSystemPrivs.BackgroundColor = System.Drawing.Color.White;
            this.dgvSystemPrivs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSystemPrivs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSystemPrivs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSystemPrivs.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvSystemPrivs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSystemPrivs.ColumnHeadersHeight = 36;
            this.dgvSystemPrivs.DefaultCellStyle = dgvCellStyle;
            this.dgvSystemPrivs.GridColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.dgvSystemPrivs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSystemPrivs.EnableHeadersVisualStyles = false;
            this.dgvSystemPrivs.Name = "dgvSystemPrivs";
            this.dgvSystemPrivs.ReadOnly = true;
            this.dgvSystemPrivs.RowHeadersVisible = false;
            this.dgvSystemPrivs.RowTemplate.Height = 34;
            this.dgvSystemPrivs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSystemPrivs.TabIndex = 0;

            // tpRoleGrants
            this.tpRoleGrants.Controls.Add(this.dgvRoleGrants);
            this.tpRoleGrants.Name = "tpRoleGrants";
            this.tpRoleGrants.Text = "Role Grants";
            this.tpRoleGrants.TabIndex = 1;

            this.dgvRoleGrants.AllowUserToAddRows = false;
            this.dgvRoleGrants.AllowUserToDeleteRows = false;
            this.dgvRoleGrants.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoleGrants.BackgroundColor = System.Drawing.Color.White;
            this.dgvRoleGrants.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRoleGrants.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRoleGrants.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvRoleGrants.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvRoleGrants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRoleGrants.ColumnHeadersHeight = 36;
            this.dgvRoleGrants.DefaultCellStyle = dgvCellStyle;
            this.dgvRoleGrants.GridColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.dgvRoleGrants.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoleGrants.EnableHeadersVisualStyles = false;
            this.dgvRoleGrants.Name = "dgvRoleGrants";
            this.dgvRoleGrants.ReadOnly = true;
            this.dgvRoleGrants.RowHeadersVisible = false;
            this.dgvRoleGrants.RowTemplate.Height = 34;
            this.dgvRoleGrants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoleGrants.TabIndex = 0;

            // tpObjectPrivs
            this.tpObjectPrivs.Controls.Add(this.dgvObjectPrivs);
            this.tpObjectPrivs.Name = "tpObjectPrivs";
            this.tpObjectPrivs.Text = "Object Privileges";
            this.tpObjectPrivs.TabIndex = 2;

            this.dgvObjectPrivs.AllowUserToAddRows = false;
            this.dgvObjectPrivs.AllowUserToDeleteRows = false;
            this.dgvObjectPrivs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvObjectPrivs.BackgroundColor = System.Drawing.Color.White;
            this.dgvObjectPrivs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvObjectPrivs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvObjectPrivs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvObjectPrivs.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvObjectPrivs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvObjectPrivs.ColumnHeadersHeight = 36;
            this.dgvObjectPrivs.DefaultCellStyle = dgvCellStyle;
            this.dgvObjectPrivs.GridColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.dgvObjectPrivs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvObjectPrivs.EnableHeadersVisualStyles = false;
            this.dgvObjectPrivs.Name = "dgvObjectPrivs";
            this.dgvObjectPrivs.ReadOnly = true;
            this.dgvObjectPrivs.RowHeadersVisible = false;
            this.dgvObjectPrivs.RowTemplate.Height = 34;
            this.dgvObjectPrivs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvObjectPrivs.TabIndex = 0;

            // tpColumnPrivs
            this.tpColumnPrivs.Controls.Add(this.dgvColumnPrivs);
            this.tpColumnPrivs.Name = "tpColumnPrivs";
            this.tpColumnPrivs.Text = "Column Privileges";
            this.tpColumnPrivs.TabIndex = 3;

            this.dgvColumnPrivs.AllowUserToAddRows = false;
            this.dgvColumnPrivs.AllowUserToDeleteRows = false;
            this.dgvColumnPrivs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvColumnPrivs.BackgroundColor = System.Drawing.Color.White;
            this.dgvColumnPrivs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvColumnPrivs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvColumnPrivs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvColumnPrivs.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvColumnPrivs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvColumnPrivs.ColumnHeadersHeight = 36;
            this.dgvColumnPrivs.DefaultCellStyle = dgvCellStyle;
            this.dgvColumnPrivs.GridColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.dgvColumnPrivs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColumnPrivs.EnableHeadersVisualStyles = false;
            this.dgvColumnPrivs.Name = "dgvColumnPrivs";
            this.dgvColumnPrivs.ReadOnly = true;
            this.dgvColumnPrivs.RowHeadersVisible = false;
            this.dgvColumnPrivs.RowTemplate.Height = 34;
            this.dgvColumnPrivs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvColumnPrivs.TabIndex = 0;

            // PrivilegeQueryView
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "PrivilegeQueryView";
            this.Load += new System.EventHandler(this.PrivilegeQueryView_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.tabPrivileges.ResumeLayout(false);
            this.tpSystemPrivs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSystemPrivs)).EndInit();
            this.tpRoleGrants.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleGrants)).EndInit();
            this.tpObjectPrivs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectPrivs)).EndInit();
            this.tpColumnPrivs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnPrivs)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblPrincipalType;
        private System.Windows.Forms.ComboBox cmbPrincipalType;
        private System.Windows.Forms.Label lblPrincipal;
        private System.Windows.Forms.ComboBox cmbPrincipal;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TabControl tabPrivileges;
        private System.Windows.Forms.TabPage tpSystemPrivs;
        private System.Windows.Forms.DataGridView dgvSystemPrivs;
        private System.Windows.Forms.TabPage tpRoleGrants;
        private System.Windows.Forms.DataGridView dgvRoleGrants;
        private System.Windows.Forms.TabPage tpObjectPrivs;
        private System.Windows.Forms.DataGridView dgvObjectPrivs;
        private System.Windows.Forms.TabPage tpColumnPrivs;
        private System.Windows.Forms.DataGridView dgvColumnPrivs;
        private System.Windows.Forms.Label lblStatus;
    }
}
