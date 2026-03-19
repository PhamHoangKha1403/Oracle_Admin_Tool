namespace ATBM_Hospital_Management.Views
{
    partial class ObjectView
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
            this.lblOwnerFilter = new System.Windows.Forms.Label();
            this.cmbOwnerFilter = new System.Windows.Forms.ComboBox();
            this.lblObjectTypeFilter = new System.Windows.Forms.Label();
            this.cmbObjectTypeFilter = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvObjects = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjects)).BeginInit();
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
            this.lblPageTitle.Text = "Objects";

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
            this.pnlCard.Controls.Add(this.dgvObjects);
            this.pnlCard.Controls.Add(this.pnlToolbar);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.TabIndex = 0;

            // pnlToolbar — plain Panel, controls positioned absolutely for precise vertical centering
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 46;
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.TabIndex = 0;
            this.pnlToolbar.Controls.Add(this.lblOwnerFilter);
            this.pnlToolbar.Controls.Add(this.cmbOwnerFilter);
            this.pnlToolbar.Controls.Add(this.lblObjectTypeFilter);
            this.pnlToolbar.Controls.Add(this.cmbObjectTypeFilter);
            this.pnlToolbar.Controls.Add(this.btnRefresh);

            System.Drawing.Font ctrlFont = new System.Drawing.Font("Segoe UI", 9.5F);
            int ctrlH = 26;
            int top = (46 - ctrlH) / 2; // vertically centered

            this.lblOwnerFilter.AutoSize = true;
            this.lblOwnerFilter.Font = ctrlFont;
            this.lblOwnerFilter.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblOwnerFilter.Location = new System.Drawing.Point(16, top + 4);
            this.lblOwnerFilter.Name = "lblOwnerFilter";
            this.lblOwnerFilter.TabIndex = 0;
            this.lblOwnerFilter.Text = "Owner:";

            this.cmbOwnerFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOwnerFilter.Font = ctrlFont;
            this.cmbOwnerFilter.Location = new System.Drawing.Point(72, top);
            this.cmbOwnerFilter.Name = "cmbOwnerFilter";
            this.cmbOwnerFilter.Size = new System.Drawing.Size(180, ctrlH);
            this.cmbOwnerFilter.TabIndex = 1;
            this.cmbOwnerFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_Changed);

            this.lblObjectTypeFilter.AutoSize = true;
            this.lblObjectTypeFilter.Font = ctrlFont;
            this.lblObjectTypeFilter.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblObjectTypeFilter.Location = new System.Drawing.Point(268, top + 4);
            this.lblObjectTypeFilter.Name = "lblObjectTypeFilter";
            this.lblObjectTypeFilter.TabIndex = 2;
            this.lblObjectTypeFilter.Text = "Object type:";

            this.cmbObjectTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObjectTypeFilter.Font = ctrlFont;
            this.cmbObjectTypeFilter.Location = new System.Drawing.Point(358, top);
            this.cmbObjectTypeFilter.Name = "cmbObjectTypeFilter";
            this.cmbObjectTypeFilter.Size = new System.Drawing.Size(160, ctrlH);
            this.cmbObjectTypeFilter.TabIndex = 3;
            this.cmbObjectTypeFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_Changed);

            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.Location = new System.Drawing.Point(534, top);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, ctrlH);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // dgvObjects
            this.dgvObjects.AllowUserToAddRows = false;
            this.dgvObjects.AllowUserToDeleteRows = false;
            this.dgvObjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvObjects.BackgroundColor = System.Drawing.Color.White;
            this.dgvObjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvObjects.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvObjects.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvObjects.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle()
            {
                BackColor = System.Drawing.Color.FromArgb(245, 245, 245),
                ForeColor = System.Drawing.Color.FromArgb(30, 30, 30),
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold),
                SelectionBackColor = System.Drawing.Color.FromArgb(245, 245, 245),
                SelectionForeColor = System.Drawing.Color.FromArgb(30, 30, 30),
                Padding = new System.Windows.Forms.Padding(8, 0, 0, 0)
            };
            this.dgvObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvObjects.ColumnHeadersHeight = 36;
            this.dgvObjects.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle()
            {
                BackColor = System.Drawing.Color.White,
                ForeColor = System.Drawing.Color.FromArgb(30, 30, 30),
                Font = new System.Drawing.Font("Segoe UI", 9F),
                SelectionBackColor = System.Drawing.Color.FromArgb(220, 220, 220),
                SelectionForeColor = System.Drawing.Color.FromArgb(20, 20, 20),
                Padding = new System.Windows.Forms.Padding(8, 0, 0, 0)
            };
            this.dgvObjects.GridColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.dgvObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvObjects.EnableHeadersVisualStyles = false;
            this.dgvObjects.MultiSelect = false;
            this.dgvObjects.Name = "dgvObjects";
            this.dgvObjects.ReadOnly = true;
            this.dgvObjects.RowHeadersVisible = false;
            this.dgvObjects.RowTemplate.Height = 34;
            this.dgvObjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvObjects.TabIndex = 1;

            // ObjectView
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "ObjectView";
            this.Load += new System.EventHandler(this.ObjectView_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjects)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblOwnerFilter;
        private System.Windows.Forms.ComboBox cmbOwnerFilter;
        private System.Windows.Forms.Label lblObjectTypeFilter;
        private System.Windows.Forms.ComboBox cmbObjectTypeFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvObjects;
    }
}
