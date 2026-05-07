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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlSearchRow = new System.Windows.Forms.Panel();
            this.pnlFilterRow = new System.Windows.Forms.Panel();

            this.colAuditUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuditColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuditAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuditTime = new System.Windows.Forms.DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).BeginInit();
            this.SuspendLayout();

            // ================= SEARCH ROW =================
            this.pnlSearchRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchRow.Height = 40;
            this.pnlSearchRow.Padding = new System.Windows.Forms.Padding(16, 6, 16, 4);
            this.pnlSearchRow.BackColor = System.Drawing.Color.White;

            this.lblSearch.AutoSize = false;
            this.lblSearch.Width = 80;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Text = "Tìm kiếm";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.txtAuditSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAuditSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAuditSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAuditSearch.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            this.txtAuditSearch.Text = "Search by ID or name...";
            this.txtAuditSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.txtAuditSearch.Enter += new System.EventHandler(this.TxtSearch_Enter);
            this.txtAuditSearch.Leave += new System.EventHandler(this.TxtSearch_Leave);

            this.pnlSearchRow.Controls.Add(this.txtAuditSearch);
            this.pnlSearchRow.Controls.Add(this.lblSearch);

            // ================= FILTER ROW =================
            this.pnlFilterRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterRow.Height = 50;
            this.pnlFilterRow.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlFilterRow.BackColor = System.Drawing.Color.White;

            this.lblTable.Text = "Bảng";
            this.lblTable.Width = 50;
            this.lblTable.Location = new System.Drawing.Point(16, 12);

            this.cbAuditTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuditTable.Location = new System.Drawing.Point(70, 12);
            this.cbAuditTable.Width = 180;

            this.lblAuditToggle.Text = "Bật/tắt audit";
            this.lblAuditToggle.Location = new System.Drawing.Point(270, 12);
            this.lblAuditToggle.Width = 90;

            this.cbAuditToggle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuditToggle.Location = new System.Drawing.Point(366, 12);
            this.cbAuditToggle.Width = 120;

            this.pnlFilterRow.Controls.Add(this.lblTable);
            this.pnlFilterRow.Controls.Add(this.cbAuditTable);
            this.pnlFilterRow.Controls.Add(this.lblAuditToggle);
            this.pnlFilterRow.Controls.Add(this.cbAuditToggle);

            // ================= DATAGRID =================
            this.dgvAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAudit.ReadOnly = true;
            this.dgvAudit.RowHeadersVisible = false;
            this.dgvAudit.AllowUserToAddRows = false;
            this.dgvAudit.AllowUserToDeleteRows = false;

            this.dgvAudit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colAuditUser,
                this.colAuditColumn,
                this.colAuditAction,
                this.colAuditTime
            });

            this.colAuditUser.HeaderText = "DB_USER";
            this.colAuditUser.Width = 200;

            this.colAuditColumn.HeaderText = "POLICY_NAME";
            this.colAuditColumn.Width = 300;

            this.colAuditAction.HeaderText = "STATEMENT_TYPE";
            this.colAuditAction.Width = 150;

            this.colAuditTime.HeaderText = "TIMESTAMP";
            this.colAuditTime.Width = 200;

            // ================= HEADER =================
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 56;
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 0, 16, 0);

            this.lblPageTitle.Text = "Audit";
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPageTitle.Location = new System.Drawing.Point(20, 16);
            this.lblPageTitle.AutoSize = true;

            this.pnlHeader.Controls.Add(this.lblPageTitle);

            // ================= TOOLBAR =================
            this.pnlToolbar.AutoSize = false;
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 90;
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(0);
            this.pnlToolbar.BackColor = System.Drawing.Color.White;

            // QUAN TRỌNG: thứ tự add
            this.pnlToolbar.Controls.Add(this.pnlFilterRow);
            this.pnlToolbar.Controls.Add(this.pnlSearchRow);

            // ================= CARD =================
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.pnlCard.Controls.Add(this.pnlToolbar);
            this.pnlCard.Controls.Add(this.dgvAudit);
            this.pnlToolbar.BringToFront();

            // ================= BODY =================
            this.pnlBody.Dock = DockStyle.Fill;
            this.pnlBody.Padding = new System.Windows.Forms.Padding(16);
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            this.pnlBody.Controls.Add(this.pnlCard);

            // ================= MAIN =================
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);

            this.Name = "AuditView";
            this.Size = new System.Drawing.Size(1000, 500);

            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtAuditSearch;
        private System.Windows.Forms.ComboBox cbAuditTable;
        private System.Windows.Forms.Label lblAuditToggle;
        private System.Windows.Forms.ComboBox cbAuditToggle;
        private System.Windows.Forms.DataGridView dgvAudit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditTime;
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

