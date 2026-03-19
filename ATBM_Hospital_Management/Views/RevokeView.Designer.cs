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
            this.cmbPrincipalType = new DropDownComboBox();
            this.lblPrincipal = new System.Windows.Forms.Label();
            this.cmbPrincipal = new DropDownComboBox();
            this.lblRevokeMode = new System.Windows.Forms.Label();
            this.cmbRevokeMode = new DropDownComboBox();
            this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
            this.btnRevoke = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlInner.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpBottom.SuspendLayout();
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
            this.lblPageTitle.Text = "Revoke Privileges";

            // pnlBody
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlBody.Controls.Add(this.pnlCard);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(20);
            this.pnlBody.TabIndex = 1;

            // pnlCard
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.pnlInner);
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.TabIndex = 0;

            // pnlInner
            this.pnlInner.BackColor = System.Drawing.Color.White;
            this.pnlInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInner.Padding = new System.Windows.Forms.Padding(20, 16, 20, 12);
            this.pnlInner.Name = "pnlInner";
            this.pnlInner.TabIndex = 0;
            this.pnlInner.Controls.Add(this.tlpMain);
            this.pnlInner.Controls.Add(this.tlpBottom);

            // tlpBottom — docked Bottom, 44px, button right-aligned
            this.tlpBottom.BackColor = System.Drawing.Color.White;
            this.tlpBottom.ColumnCount = 2;
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpBottom.Controls.Add(this.btnRevoke, 1, 0);
            this.tlpBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpBottom.Height = 44;
            this.tlpBottom.Margin = new System.Windows.Forms.Padding(0);
            this.tlpBottom.Padding = new System.Windows.Forms.Padding(0);
            this.tlpBottom.Name = "tlpBottom";
            this.tlpBottom.RowCount = 1;
            this.tlpBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.TabIndex = 2;

            this.btnRevoke.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRevoke.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.btnRevoke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevoke.FlatAppearance.BorderSize = 0;
            this.btnRevoke.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRevoke.ForeColor = System.Drawing.Color.White;
            this.btnRevoke.Name = "btnRevoke";
            this.btnRevoke.Size = new System.Drawing.Size(160, 36);
            this.btnRevoke.TabIndex = 0;
            this.btnRevoke.Text = "EXECUTE REVOKE";
            this.btnRevoke.UseVisualStyleBackColor = false;
            this.btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);

            // tlpMain — 4 rows, docked Fill
            System.Drawing.Font labelFont = new System.Drawing.Font("Segoe UI", 9.5F);
            System.Drawing.Color labelColor = System.Drawing.Color.FromArgb(30, 30, 30);

            this.tlpMain.BackColor = System.Drawing.Color.White;
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Padding = new System.Windows.Forms.Padding(0);
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblPrincipalType, 0, 0);
            this.tlpMain.Controls.Add(this.cmbPrincipalType, 1, 0);
            this.tlpMain.Controls.Add(this.lblPrincipal, 0, 1);
            this.tlpMain.Controls.Add(this.cmbPrincipal, 1, 1);
            this.tlpMain.Controls.Add(this.lblRevokeMode, 0, 2);
            this.tlpMain.Controls.Add(this.cmbRevokeMode, 1, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Height = 208;
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.TabIndex = 0;

            this.lblPrincipalType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPrincipalType.AutoSize = true;
            this.lblPrincipalType.Font = labelFont;
            this.lblPrincipalType.ForeColor = labelColor;
            this.lblPrincipalType.Name = "lblPrincipalType";
            this.lblPrincipalType.TabIndex = 0;
            this.lblPrincipalType.Text = "Principal type:";

            this.cmbPrincipalType.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cmbPrincipalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrincipalType.Font = labelFont;
            this.cmbPrincipalType.MaxDropDownItems = 10;
            this.cmbPrincipalType.Name = "cmbPrincipalType";
            this.cmbPrincipalType.TabIndex = 1;

            this.lblPrincipal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPrincipal.AutoSize = true;
            this.lblPrincipal.Font = labelFont;
            this.lblPrincipal.ForeColor = labelColor;
            this.lblPrincipal.Name = "lblPrincipal";
            this.lblPrincipal.TabIndex = 2;
            this.lblPrincipal.Text = "Principal:";

            this.cmbPrincipal.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cmbPrincipal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrincipal.Font = labelFont;
            this.cmbPrincipal.MaxDropDownItems = 6;
            this.cmbPrincipal.DropDownHeight = 120;
            this.cmbPrincipal.Name = "cmbPrincipal";
            this.cmbPrincipal.TabIndex = 3;

            this.lblRevokeMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRevokeMode.AutoSize = true;
            this.lblRevokeMode.Font = labelFont;
            this.lblRevokeMode.ForeColor = labelColor;
            this.lblRevokeMode.Name = "lblRevokeMode";
            this.lblRevokeMode.TabIndex = 4;
            this.lblRevokeMode.Text = "Revoke mode:";

            this.cmbRevokeMode.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cmbRevokeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRevokeMode.Font = labelFont;
            this.cmbRevokeMode.Name = "cmbRevokeMode";
            this.cmbRevokeMode.TabIndex = 5;

            // RevokeView
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "RevokeView";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlInner.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpBottom.ResumeLayout(false);
            this.tlpBottom.PerformLayout();
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
