namespace ATBM_Hospital_Management.Views.Components
{
    partial class DoctorView_HealthRecordList
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMainList;
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblRecordCount = new System.Windows.Forms.Label();

            this.pnlMainList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // ── lblTitle ──────────────────────────────────────────────────
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Height = 50;
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "DANH SÁCH HỒ SƠ BỆNH ÁN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── dataGridView1 ─────────────────────────────────────────────
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);

            // ── pnlMainList ───────────────────────────────────────────────
            this.pnlMainList.Controls.Add(this.dataGridView1);
            this.pnlMainList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainList.Name = "pnlMainList";
            this.pnlMainList.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.pnlMainList.TabIndex = 0;

            // ── lblRecordCount ────────────────────────────────────────────
            this.lblRecordCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRecordCount.Height = 30;
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.TabIndex = 1;
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ── UserControl ───────────────────────────────────────────────
            this.Controls.Add(this.pnlMainList);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.lblTitle);          // Dock Top → xếp sau Fill
            this.Name = "DoctorView_HealthRecordList";
            this.Size = new System.Drawing.Size(1280, 800);

            this.pnlMainList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}