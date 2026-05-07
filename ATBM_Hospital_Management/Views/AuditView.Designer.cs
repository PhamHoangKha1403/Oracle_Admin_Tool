using System.Windows.Forms;

namespace ATBM_Hospital_Management.Views
{
    partial class AuditView
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
            this.lblTable = new System.Windows.Forms.Label();
            this.txtAuditSearch = new System.Windows.Forms.TextBox();
            this.cbAuditTable = new System.Windows.Forms.ComboBox();
            this.lblAuditToggle = new System.Windows.Forms.Label();
            this.cbAuditToggle = new System.Windows.Forms.ComboBox();
            this.dgvAudit = new System.Windows.Forms.DataGridView();
            this.colAuditUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuditObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuditColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuditAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuditTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReturnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.pnlFilterRow = new System.Windows.Forms.Panel();
            this.pnlSearchRow = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlFilterRow.SuspendLayout();
            this.pnlSearchRow.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTable
            // 
            this.lblTable.Location = new System.Drawing.Point(16, 13);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(70, 29);
            this.lblTable.TabIndex = 0;
            this.lblTable.Text = "Bảng:";
            // 
            // txtAuditSearch
            // 
            this.txtAuditSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAuditSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAuditSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAuditSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.txtAuditSearch.Location = new System.Drawing.Point(96, 6);
            this.txtAuditSearch.Name = "txtAuditSearch";
            this.txtAuditSearch.Size = new System.Drawing.Size(854, 34);
            this.txtAuditSearch.TabIndex = 0;
            this.txtAuditSearch.Text = " Search by ID or name...";
            this.txtAuditSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.txtAuditSearch.Enter += new System.EventHandler(this.TxtSearch_Enter);
            this.txtAuditSearch.Leave += new System.EventHandler(this.TxtSearch_Leave);
            // 
            // cbAuditTable
            // 
            this.cbAuditTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuditTable.Location = new System.Drawing.Point(87, 9);
            this.cbAuditTable.Name = "cbAuditTable";
            this.cbAuditTable.Size = new System.Drawing.Size(180, 28);
            this.cbAuditTable.TabIndex = 1;
            // 
            // lblAuditToggle
            // 
            this.lblAuditToggle.Location = new System.Drawing.Point(339, 13);
            this.lblAuditToggle.Name = "lblAuditToggle";
            this.lblAuditToggle.Size = new System.Drawing.Size(120, 23);
            this.lblAuditToggle.TabIndex = 2;
            this.lblAuditToggle.Text = "Bật/tắt audit:";
            this.lblAuditToggle.Click += new System.EventHandler(this.lblAuditToggle_Click);
            // 
            // cbAuditToggle
            // 
            this.cbAuditToggle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuditToggle.Location = new System.Drawing.Point(465, 9);
            this.cbAuditToggle.Name = "cbAuditToggle";
            this.cbAuditToggle.Size = new System.Drawing.Size(120, 28);
            this.cbAuditToggle.TabIndex = 3;
            // 
            // dgvAudit
            // 
            this.dgvAudit.AllowUserToAddRows = false;
            this.dgvAudit.AllowUserToDeleteRows = false;
            this.dgvAudit.ColumnHeadersHeight = 34;
            this.dgvAudit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAuditUser,
            this.colAuditObjectName,
            this.colAuditColumn,
            this.colAuditAction,
            this.colAuditTime,
            this.colReturnCode});
            this.dgvAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAudit.Location = new System.Drawing.Point(0, 90);
            this.dgvAudit.Name = "dgvAudit";
            this.dgvAudit.ReadOnly = true;
            this.dgvAudit.RowHeadersVisible = false;
            this.dgvAudit.RowHeadersWidth = 62;
            this.dgvAudit.Size = new System.Drawing.Size(966, 320);
            this.dgvAudit.TabIndex = 0;
            this.dgvAudit.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAudit_CellContentClick);
            // 
            // colAuditUser
            // 
            this.colAuditUser.HeaderText = "NGƯỜI DÙNG";
            this.colAuditUser.MinimumWidth = 8;
            this.colAuditUser.Name = "colAuditUser";
            this.colAuditUser.ReadOnly = true;
            this.colAuditUser.Width = 150;
            // 
            // colAuditObjectName
            // 
            this.colAuditObjectName.HeaderText = "ĐỐI TƯỢNG";
            this.colAuditObjectName.MinimumWidth = 8;
            this.colAuditObjectName.Name = "colAuditObjectName";
            this.colAuditObjectName.ReadOnly = true;
            this.colAuditObjectName.Width = 150;
            // 
            // colAuditColumn
            // 
            this.colAuditColumn.HeaderText = "POLICY";
            this.colAuditColumn.MinimumWidth = 8;
            this.colAuditColumn.Name = "colAuditColumn";
            this.colAuditColumn.ReadOnly = true;
            this.colAuditColumn.Width = 150;
            // 
            // colAuditAction
            // 
            this.colAuditAction.HeaderText = "HÀNH ĐỘNG";
            this.colAuditAction.MinimumWidth = 8;
            this.colAuditAction.Name = "colAuditAction";
            this.colAuditAction.ReadOnly = true;
            this.colAuditAction.Width = 150;
            // 
            // colAuditTime
            // 
            this.colAuditTime.HeaderText = "THỜI GIAN";
            this.colAuditTime.MinimumWidth = 8;
            this.colAuditTime.Name = "colAuditTime";
            this.colAuditTime.ReadOnly = true;
            this.colAuditTime.Width = 180;
            // 
            // colReturnCode
            // 
            this.colReturnCode.HeaderText = "TRẠNG THÁI";
            this.colReturnCode.MinimumWidth = 8;
            this.colReturnCode.Name = "colReturnCode";
            this.colReturnCode.ReadOnly = true;
            this.colReturnCode.Width = 150;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblPageTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 0, 16, 0);
            this.pnlHeader.Size = new System.Drawing.Size(1000, 56);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.Location = new System.Drawing.Point(20, 16);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(82, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Audit";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlBody.Controls.Add(this.pnlCard);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 56);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(16);
            this.pnlBody.Size = new System.Drawing.Size(1000, 444);
            this.pnlBody.TabIndex = 0;
            // 
            // pnlCard
            // 
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.dgvAudit);
            this.pnlCard.Controls.Add(this.pnlToolbar);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Location = new System.Drawing.Point(16, 16);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(968, 412);
            this.pnlCard.TabIndex = 0;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.pnlFilterRow);
            this.pnlToolbar.Controls.Add(this.pnlSearchRow);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(966, 90);
            this.pnlToolbar.TabIndex = 1;
            // 
            // pnlFilterRow
            // 
            this.pnlFilterRow.BackColor = System.Drawing.Color.White;
            this.pnlFilterRow.Controls.Add(this.lblTable);
            this.pnlFilterRow.Controls.Add(this.cbAuditTable);
            this.pnlFilterRow.Controls.Add(this.lblAuditToggle);
            this.pnlFilterRow.Controls.Add(this.cbAuditToggle);
            this.pnlFilterRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterRow.Location = new System.Drawing.Point(0, 40);
            this.pnlFilterRow.Name = "pnlFilterRow";
            this.pnlFilterRow.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlFilterRow.Size = new System.Drawing.Size(966, 50);
            this.pnlFilterRow.TabIndex = 0;
            this.pnlFilterRow.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlFilterRow_Paint);
            // 
            // pnlSearchRow
            // 
            this.pnlSearchRow.BackColor = System.Drawing.Color.White;
            this.pnlSearchRow.Controls.Add(this.txtAuditSearch);
            this.pnlSearchRow.Controls.Add(this.lblSearch);
            this.pnlSearchRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchRow.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchRow.Name = "pnlSearchRow";
            this.pnlSearchRow.Padding = new System.Windows.Forms.Padding(16, 6, 16, 4);
            this.pnlSearchRow.Size = new System.Drawing.Size(966, 40);
            this.pnlSearchRow.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Location = new System.Drawing.Point(16, 6);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(80, 30);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Tìm kiếm";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AuditView
            // 
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Name = "AuditView";
            this.Size = new System.Drawing.Size(1000, 500);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlFilterRow.ResumeLayout(false);
            this.pnlSearchRow.ResumeLayout(false);
            this.pnlSearchRow.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TextBox txtAuditSearch;
        private System.Windows.Forms.ComboBox cbAuditTable;
        private System.Windows.Forms.Label lblAuditToggle;
        private System.Windows.Forms.ComboBox cbAuditToggle;
        private System.Windows.Forms.DataGridView dgvAudit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditObjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReturnCode;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel pnlSearchRow;
        private System.Windows.Forms.Panel pnlFilterRow;
    }
}

