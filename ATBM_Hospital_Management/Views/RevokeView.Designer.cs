namespace ATBM_Hospital_Management.Views
{
    partial class RevokeView
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
            this.pnlInner = new System.Windows.Forms.Panel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblPrincipalType = new System.Windows.Forms.Label();
            this.cmbPrincipalType = new ATBM_Hospital_Management.Views.DropDownComboBox();
            this.lblPrincipal = new System.Windows.Forms.Label();
            this.cmbPrincipal = new ATBM_Hospital_Management.Views.DropDownComboBox();
            this.lblRevokeMode = new System.Windows.Forms.Label();
            this.cmbRevokeMode = new ATBM_Hospital_Management.Views.DropDownComboBox();
            this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
            this.btnRevoke = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlInner.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblPageTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(22, 0, 18, 0);
            this.pnlHeader.Size = new System.Drawing.Size(2456, 70);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblPageTitle.Location = new System.Drawing.Point(22, 20);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(227, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Revoke Privileges";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlBody.Controls.Add(this.pnlCard);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 70);
            this.pnlBody.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(22, 25, 22, 25);
            this.pnlBody.Size = new System.Drawing.Size(2456, 562);
            this.pnlBody.TabIndex = 1;
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.pnlInner);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Location = new System.Drawing.Point(22, 25);
            this.pnlCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(2412, 512);
            this.pnlCard.TabIndex = 0;
            // 
            // pnlInner
            // 
            this.pnlInner.BackColor = System.Drawing.Color.White;
            this.pnlInner.Controls.Add(this.tlpMain);
            this.pnlInner.Controls.Add(this.tlpBottom);
            this.pnlInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInner.Location = new System.Drawing.Point(0, 0);
            this.pnlInner.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlInner.Name = "pnlInner";
            this.pnlInner.Padding = new System.Windows.Forms.Padding(22, 20, 22, 15);
            this.pnlInner.Size = new System.Drawing.Size(2410, 510);
            this.pnlInner.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.White;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblPrincipalType, 0, 0);
            this.tlpMain.Controls.Add(this.cmbPrincipalType, 1, 0);
            this.tlpMain.Controls.Add(this.lblPrincipal, 0, 1);
            this.tlpMain.Controls.Add(this.cmbPrincipal, 1, 1);
            this.tlpMain.Controls.Add(this.lblRevokeMode, 0, 2);
            this.tlpMain.Controls.Add(this.cmbRevokeMode, 1, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(22, 20);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(2366, 260);
            this.tlpMain.TabIndex = 0;
            // 
            // lblPrincipalType
            // 
            this.lblPrincipalType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPrincipalType.AutoSize = true;
            this.lblPrincipalType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPrincipalType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblPrincipalType.Location = new System.Drawing.Point(3, 20);
            this.lblPrincipalType.Name = "lblPrincipalType";
            this.lblPrincipalType.Size = new System.Drawing.Size(131, 25);
            this.lblPrincipalType.TabIndex = 0;
            this.lblPrincipalType.Text = "Principal type:";
            // 
            // cmbPrincipalType
            // 
            this.cmbPrincipalType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPrincipalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrincipalType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbPrincipalType.Location = new System.Drawing.Point(183, 16);
            this.cmbPrincipalType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbPrincipalType.MaxDropDownItems = 10;
            this.cmbPrincipalType.Name = "cmbPrincipalType";
            this.cmbPrincipalType.Size = new System.Drawing.Size(2180, 33);
            this.cmbPrincipalType.TabIndex = 1;
            // 
            // lblPrincipal
            // 
            this.lblPrincipal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPrincipal.AutoSize = true;
            this.lblPrincipal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPrincipal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblPrincipal.Location = new System.Drawing.Point(3, 85);
            this.lblPrincipal.Name = "lblPrincipal";
            this.lblPrincipal.Size = new System.Drawing.Size(90, 25);
            this.lblPrincipal.TabIndex = 2;
            this.lblPrincipal.Text = "Principal:";
            // 
            // cmbPrincipal
            // 
            this.cmbPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPrincipal.DropDownHeight = 120;
            this.cmbPrincipal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrincipal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbPrincipal.IntegralHeight = false;
            this.cmbPrincipal.Location = new System.Drawing.Point(183, 81);
            this.cmbPrincipal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbPrincipal.MaxDropDownItems = 6;
            this.cmbPrincipal.Name = "cmbPrincipal";
            this.cmbPrincipal.Size = new System.Drawing.Size(2180, 33);
            this.cmbPrincipal.TabIndex = 3;
            // 
            // lblRevokeMode
            // 
            this.lblRevokeMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRevokeMode.AutoSize = true;
            this.lblRevokeMode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblRevokeMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblRevokeMode.Location = new System.Drawing.Point(3, 150);
            this.lblRevokeMode.Name = "lblRevokeMode";
            this.lblRevokeMode.Size = new System.Drawing.Size(128, 25);
            this.lblRevokeMode.TabIndex = 4;
            this.lblRevokeMode.Text = "Revoke mode:";
            // 
            // cmbRevokeMode
            // 
            this.cmbRevokeMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRevokeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRevokeMode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbRevokeMode.Location = new System.Drawing.Point(183, 146);
            this.cmbRevokeMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbRevokeMode.Name = "cmbRevokeMode";
            this.cmbRevokeMode.Size = new System.Drawing.Size(2180, 33);
            this.cmbRevokeMode.TabIndex = 5;
            // 
            // tlpBottom
            // 
            this.tlpBottom.BackColor = System.Drawing.Color.White;
            this.tlpBottom.ColumnCount = 2;
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpBottom.Controls.Add(this.btnRevoke, 1, 0);
            this.tlpBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpBottom.Location = new System.Drawing.Point(22, 440);
            this.tlpBottom.Margin = new System.Windows.Forms.Padding(0);
            this.tlpBottom.Name = "tlpBottom";
            this.tlpBottom.RowCount = 1;
            this.tlpBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.Size = new System.Drawing.Size(2366, 55);
            this.tlpBottom.TabIndex = 2;
            // 
            // btnRevoke
            // 
            this.btnRevoke.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRevoke.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnRevoke.FlatAppearance.BorderSize = 0;
            this.btnRevoke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevoke.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRevoke.ForeColor = System.Drawing.Color.White;
            this.btnRevoke.Location = new System.Drawing.Point(2190, 4);
            this.btnRevoke.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRevoke.Name = "btnRevoke";
            this.btnRevoke.Size = new System.Drawing.Size(173, 47);
            this.btnRevoke.TabIndex = 0;
            this.btnRevoke.Text = "EXECUTE REVOKE";
            this.btnRevoke.UseVisualStyleBackColor = false;
            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);
            // 
            // RevokeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RevokeView";
            this.Size = new System.Drawing.Size(2456, 632);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlInner.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlInner;
        internal System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblPrincipalType;
        private DropDownComboBox cmbPrincipalType;
        private System.Windows.Forms.Label lblPrincipal;
        private DropDownComboBox cmbPrincipal;
        private System.Windows.Forms.Label lblRevokeMode;
        private DropDownComboBox cmbRevokeMode;
        private System.Windows.Forms.TableLayoutPanel tlpBottom;
        private System.Windows.Forms.Button btnRevoke;
    }
}
