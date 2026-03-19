namespace ATBM_Hospital_Management.Views
{
    partial class GrantView
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
            this.lblGrantMode = new System.Windows.Forms.Label();
            this.cmbGrantMode = new DropDownComboBox();
            this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
            this.chkGrantOption = new System.Windows.Forms.CheckBox();
            this.btnGrant = new System.Windows.Forms.Button();
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
            this.lblPageTitle.Text = "Grant Privileges";

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

            // tlpBottom — docked Bottom, 44px
            this.tlpBottom.BackColor = System.Drawing.Color.White;
            this.tlpBottom.ColumnCount = 2;
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpBottom.Controls.Add(this.chkGrantOption, 0, 0);
            this.tlpBottom.Controls.Add(this.btnGrant, 1, 0);
            this.tlpBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpBottom.Height = 44;
            this.tlpBottom.Margin = new System.Windows.Forms.Padding(0);
            this.tlpBottom.Padding = new System.Windows.Forms.Padding(0);
            this.tlpBottom.Name = "tlpBottom";
            this.tlpBottom.RowCount = 1;
            this.tlpBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBottom.TabIndex = 2;

            this.chkGrantOption.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
            this.chkGrantOption.AutoSize = true;
            this.chkGrantOption.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkGrantOption.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.chkGrantOption.Name = "chkGrantOption";
            this.chkGrantOption.TabIndex = 0;
            this.chkGrantOption.Text = "Allow further grant (WITH GRANT/ADMIN OPTION)";
            this.chkGrantOption.UseVisualStyleBackColor = true;

            this.btnGrant.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGrant.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.btnGrant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrant.FlatAppearance.BorderSize = 0;
            this.btnGrant.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGrant.ForeColor = System.Drawing.Color.White;
            this.btnGrant.Name = "btnGrant";
            this.btnGrant.Size = new System.Drawing.Size(160, 36);
            this.btnGrant.TabIndex = 1;
            this.btnGrant.Text = "EXECUTE GRANT";
            this.btnGrant.UseVisualStyleBackColor = false;
            this.btnGrant.Click += new System.EventHandler(this.btnGrant_Click);

            // tlpMain — 4 rows, docked Fill
            // Row 0-2: fixed 52px each; Row 3: percent 100% (dynamic content)
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
            this.tlpMain.Controls.Add(this.lblGrantMode, 0, 2);
            this.tlpMain.Controls.Add(this.cmbGrantMode, 1, 2);
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

            this.lblGrantMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGrantMode.AutoSize = true;
            this.lblGrantMode.Font = labelFont;
            this.lblGrantMode.ForeColor = labelColor;
            this.lblGrantMode.Name = "lblGrantMode";
            this.lblGrantMode.TabIndex = 4;
            this.lblGrantMode.Text = "Grant mode:";

            this.cmbGrantMode.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cmbGrantMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrantMode.Font = labelFont;
            this.cmbGrantMode.Name = "cmbGrantMode";
            this.cmbGrantMode.TabIndex = 5;

            // GrantView
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "GrantView";
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
        private System.Windows.Forms.Label lblGrantMode;
        private DropDownComboBox cmbGrantMode;
        private System.Windows.Forms.TableLayoutPanel tlpBottom;
        private System.Windows.Forms.CheckBox chkGrantOption;
        private System.Windows.Forms.Button btnGrant;
    }
}
