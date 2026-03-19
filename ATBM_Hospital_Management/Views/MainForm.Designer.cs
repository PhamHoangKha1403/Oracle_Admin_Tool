namespace ATBM_Hospital_Management.Views
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpDashboard = new System.Windows.Forms.TabPage();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.tpRoles = new System.Windows.Forms.TabPage();
            this.tpObjects = new System.Windows.Forms.TabPage();
            this.tpGrant = new System.Windows.Forms.TabPage();
            this.tpRevoke = new System.Windows.Forms.TabPage();
            this.tpPrivQuery = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            // tabControl1
            this.tabControl1.Controls.Add(this.tpDashboard);
            this.tabControl1.Controls.Add(this.tpUsers);
            this.tabControl1.Controls.Add(this.tpRoles);
            this.tabControl1.Controls.Add(this.tpObjects);
            this.tabControl1.Controls.Add(this.tpGrant);
            this.tabControl1.Controls.Add(this.tpRevoke);
            this.tabControl1.Controls.Add(this.tpPrivQuery);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1100, 698);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1_Selected);

            // tpDashboard
            this.tpDashboard.Name = "tpDashboard";
            this.tpDashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tpDashboard.TabIndex = 0;
            this.tpDashboard.Text = "Overview";
            this.tpDashboard.UseVisualStyleBackColor = true;

            // tpUsers
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.TabIndex = 1;
            this.tpUsers.Text = "Users";
            this.tpUsers.UseVisualStyleBackColor = true;

            // tpRoles
            this.tpRoles.Name = "tpRoles";
            this.tpRoles.Padding = new System.Windows.Forms.Padding(3);
            this.tpRoles.TabIndex = 2;
            this.tpRoles.Text = "Roles";
            this.tpRoles.UseVisualStyleBackColor = true;

            // tpObjects
            this.tpObjects.Name = "tpObjects";
            this.tpObjects.Padding = new System.Windows.Forms.Padding(3);
            this.tpObjects.TabIndex = 3;
            this.tpObjects.Text = "Objects";
            this.tpObjects.UseVisualStyleBackColor = true;

            // tpGrant
            this.tpGrant.Name = "tpGrant";
            this.tpGrant.Padding = new System.Windows.Forms.Padding(3);
            this.tpGrant.TabIndex = 4;
            this.tpGrant.Text = "Grant";
            this.tpGrant.UseVisualStyleBackColor = true;

            // tpRevoke
            this.tpRevoke.Name = "tpRevoke";
            this.tpRevoke.Padding = new System.Windows.Forms.Padding(3);
            this.tpRevoke.TabIndex = 5;
            this.tpRevoke.Text = "Revoke";
            this.tpRevoke.UseVisualStyleBackColor = true;

            // tpPrivQuery
            this.tpPrivQuery.Name = "tpPrivQuery";
            this.tpPrivQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tpPrivQuery.TabIndex = 6;
            this.tpPrivQuery.Text = "Privileges";
            this.tpPrivQuery.UseVisualStyleBackColor = true;

            // statusStrip1
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.toolStripStatusLabel1 });
            this.statusStrip1.Location = new System.Drawing.Point(0, 720);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1100, 22);
            this.statusStrip1.TabIndex = 1;

            // toolStripStatusLabel1
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Text = "Not connected";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 742);
            this.MinimumSize = new System.Drawing.Size(1100, 720);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Text = "Oracle DBA Management Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpDashboard;
        private System.Windows.Forms.TabPage tpUsers;
        private System.Windows.Forms.TabPage tpRoles;
        private System.Windows.Forms.TabPage tpObjects;
        private System.Windows.Forms.TabPage tpGrant;
        private System.Windows.Forms.TabPage tpRevoke;
        private System.Windows.Forms.TabPage tpPrivQuery;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
