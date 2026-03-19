namespace ATBM_Hospital_Management.Views
{
    partial class LoginForm
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
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblHost = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlCenter.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            // pnlCenter — ash gray background, fills the form
            this.pnlCenter.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Controls.Add(this.pnlCard);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.TabIndex = 0;
            this.pnlCenter.Resize += new System.EventHandler(this.pnlCenter_Resize);

            // pnlCard — white card, fixed size, no padding (children handle their own padding)
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlCard.Controls.Add(this.pnlForm);
            this.pnlCard.Controls.Add(this.pnlTitle);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(460, 510);
            this.pnlCard.TabIndex = 0;

            // pnlTitle — header section, full width, no side gaps
            this.pnlTitle.BackColor = System.Drawing.Color.White;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Height = 64;
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Padding = new System.Windows.Forms.Padding(32, 0, 32, 0);
            this.pnlTitle.TabIndex = 0;

            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblTitle.Location = new System.Drawing.Point(32, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Connect to Oracle";

            // pnlForm — contains the input table, fills remaining space
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.tableLayoutPanel1);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(32, 8, 32, 24);
            this.pnlForm.TabIndex = 1;

            // tableLayoutPanel1 — 2 cols: label(100px) | input(fill)
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblUsername, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtUsername, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPassword, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtPassword, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblHost, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtHost, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblPort, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtPort, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblServiceName, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtServiceName, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnConnect, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblStatus, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.TabIndex = 0;

            System.Drawing.Font labelFont = new System.Drawing.Font("Segoe UI", 9.5F);
            System.Drawing.Color labelColor = System.Drawing.Color.FromArgb(70, 70, 70);
            System.Drawing.Font inputFont = new System.Drawing.Font("Segoe UI", 10F);

            // Labels
            this.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = labelFont;
            this.lblUsername.ForeColor = labelColor;
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username";

            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = labelFont;
            this.lblPassword.ForeColor = labelColor;
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Password";

            this.lblHost.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHost.AutoSize = true;
            this.lblHost.Font = labelFont;
            this.lblHost.ForeColor = labelColor;
            this.lblHost.Name = "lblHost";
            this.lblHost.TabIndex = 5;
            this.lblHost.Text = "Host";

            this.lblPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPort.AutoSize = true;
            this.lblPort.Font = labelFont;
            this.lblPort.ForeColor = labelColor;
            this.lblPort.Name = "lblPort";
            this.lblPort.TabIndex = 7;
            this.lblPort.Text = "Port";

            this.lblServiceName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.Font = labelFont;
            this.lblServiceName.ForeColor = labelColor;
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.TabIndex = 9;
            this.lblServiceName.Text = "Service Name";

            // TextBoxes
            this.txtUsername.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = inputFont;
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.TabIndex = 2;
            this.txtUsername.Text = "Oracle username";
            this.txtUsername.Enter += new System.EventHandler(this.txtUsername_Enter);
            this.txtUsername.Leave += new System.EventHandler(this.txtUsername_Leave);

            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = inputFont;
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.TabIndex = 4;
            this.txtPassword.Text = "Oracle password";
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);

            this.txtHost.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHost.Font = inputFont;
            this.txtHost.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.txtHost.Name = "txtHost";
            this.txtHost.TabIndex = 6;
            this.txtHost.Text = "e.g. localhost";
            this.txtHost.Enter += new System.EventHandler(this.txtHost_Enter);
            this.txtHost.Leave += new System.EventHandler(this.txtHost_Leave);

            this.txtPort.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.Font = inputFont;
            this.txtPort.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.txtPort.Name = "txtPort";
            this.txtPort.TabIndex = 8;
            this.txtPort.Text = "e.g. 1521";
            this.txtPort.Enter += new System.EventHandler(this.txtPort_Enter);
            this.txtPort.Leave += new System.EventHandler(this.txtPort_Leave);

            this.txtServiceName.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtServiceName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServiceName.Font = inputFont;
            this.txtServiceName.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.TabIndex = 10;
            this.txtServiceName.Text = "e.g. XEPDB1";
            this.txtServiceName.Enter += new System.EventHandler(this.txtServiceName_Enter);
            this.txtServiceName.Leave += new System.EventHandler(this.txtServiceName_Leave);

            // btnConnect — dark flat button, right-aligned
            this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(110, 34);
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "CONNECT";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);

            // lblStatus — spans both columns
            this.tableLayoutPanel1.SetColumnSpan(this.lblStatus, 2);
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblStatus.AutoSize = false;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(180, 0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "";

            // LoginForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);
            this.ClientSize = new System.Drawing.Size(600, 560);
            this.Controls.Add(this.pnlCenter);
            this.MinimumSize = new System.Drawing.Size(600, 560);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Oracle DB Admin";
            this.pnlCenter.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblStatus;
    }
}
