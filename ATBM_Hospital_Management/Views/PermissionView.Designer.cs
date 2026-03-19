namespace ATBM_Hospital_Management.Views
{
    partial class PermissionView
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblGrantee = new System.Windows.Forms.Label();
            this.cmbGrantee = new System.Windows.Forms.ComboBox();
            this.lblObject = new System.Windows.Forms.Label();
            this.cmbObject = new System.Windows.Forms.ComboBox();
            this.lblPrivilege = new System.Windows.Forms.Label();
            this.txtPrivilege = new System.Windows.Forms.TextBox();
            this.chkWithGrantOption = new System.Windows.Forms.CheckBox();
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGrant = new System.Windows.Forms.Button();
            this.btnRevoke = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // tlpMain
            this.tlpMain.BackColor = System.Drawing.Color.White;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblTitle, 0, 0);
            this.tlpMain.Controls.Add(this.lblGrantee, 0, 1);
            this.tlpMain.Controls.Add(this.cmbGrantee, 1, 1);
            this.tlpMain.Controls.Add(this.lblObject, 0, 2);
            this.tlpMain.Controls.Add(this.cmbObject, 1, 2);
            this.tlpMain.Controls.Add(this.lblPrivilege, 0, 3);
            this.tlpMain.Controls.Add(this.txtPrivilege, 1, 3);
            this.tlpMain.Controls.Add(this.chkWithGrantOption, 1, 4);
            this.tlpMain.Controls.Add(this.pnlButtons, 1, 5);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.tlpMain.RowCount = 6;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.TabIndex = 0;
            // lblTitle
            this.tlpMain.SetColumnSpan(this.lblTitle, 2);
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Permission Management";
            // lblGrantee
            this.lblGrantee.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGrantee.AutoSize = true;
            this.lblGrantee.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGrantee.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblGrantee.Name = "lblGrantee";
            this.lblGrantee.TabIndex = 1;
            this.lblGrantee.Text = "Grantee:";
            // cmbGrantee
            this.cmbGrantee.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cmbGrantee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrantee.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbGrantee.Name = "cmbGrantee";
            this.cmbGrantee.TabIndex = 2;
            // lblObject
            this.lblObject.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblObject.AutoSize = true;
            this.lblObject.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblObject.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblObject.Name = "lblObject";
            this.lblObject.TabIndex = 3;
            this.lblObject.Text = "DB Object:";
            // cmbObject
            this.cmbObject.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObject.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbObject.Name = "cmbObject";
            this.cmbObject.TabIndex = 4;
            // lblPrivilege
            this.lblPrivilege.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPrivilege.AutoSize = true;
            this.lblPrivilege.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPrivilege.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblPrivilege.Name = "lblPrivilege";
            this.lblPrivilege.TabIndex = 5;
            this.lblPrivilege.Text = "Privilege:";
            // txtPrivilege
            this.txtPrivilege.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtPrivilege.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrivilege.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtPrivilege.Name = "txtPrivilege";
            this.txtPrivilege.TabIndex = 6;
            // chkWithGrantOption
            this.chkWithGrantOption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkWithGrantOption.AutoSize = true;
            this.chkWithGrantOption.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkWithGrantOption.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.chkWithGrantOption.Name = "chkWithGrantOption";
            this.chkWithGrantOption.TabIndex = 7;
            this.chkWithGrantOption.Text = "WITH GRANT OPTION";
            this.chkWithGrantOption.UseVisualStyleBackColor = true;
            // pnlButtons
            this.pnlButtons.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            this.pnlButtons.AutoSize = true;
            this.pnlButtons.Controls.Add(this.btnGrant);
            this.pnlButtons.Controls.Add(this.btnRevoke);
            this.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(0);
            this.pnlButtons.TabIndex = 8;
            this.pnlButtons.WrapContents = false;
            // btnGrant
            this.btnGrant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrant.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnGrant.FlatAppearance.BorderSize = 1;
            this.btnGrant.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.btnGrant.ForeColor = System.Drawing.Color.White;
            this.btnGrant.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnGrant.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnGrant.Name = "btnGrant";
            this.btnGrant.Size = new System.Drawing.Size(90, 30);
            this.btnGrant.TabIndex = 0;
            this.btnGrant.Text = "GRANT";
            this.btnGrant.UseVisualStyleBackColor = false;
            this.btnGrant.Click += new System.EventHandler(this.btnGrant_Click);
            // btnRevoke
            this.btnRevoke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevoke.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnRevoke.FlatAppearance.BorderSize = 1;
            this.btnRevoke.BackColor = System.Drawing.Color.White;
            this.btnRevoke.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnRevoke.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRevoke.Margin = new System.Windows.Forms.Padding(0);
            this.btnRevoke.Name = "btnRevoke";
            this.btnRevoke.Size = new System.Drawing.Size(90, 30);
            this.btnRevoke.TabIndex = 1;
            this.btnRevoke.Text = "REVOKE";
            this.btnRevoke.UseVisualStyleBackColor = false;
            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);
            // PermissionView
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpMain);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "PermissionView";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblGrantee;
        private System.Windows.Forms.ComboBox cmbGrantee;
        private System.Windows.Forms.Label lblObject;
        private System.Windows.Forms.ComboBox cmbObject;
        private System.Windows.Forms.Label lblPrivilege;
        private System.Windows.Forms.TextBox txtPrivilege;
        private System.Windows.Forms.CheckBox chkWithGrantOption;
        private System.Windows.Forms.FlowLayoutPanel pnlButtons;
        private System.Windows.Forms.Button btnGrant;
        private System.Windows.Forms.Button btnRevoke;
    }
}
