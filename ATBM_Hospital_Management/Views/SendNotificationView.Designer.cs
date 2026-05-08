namespace ATBM_Hospital_Management.Views
{
    partial class SendNotificationView
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblContent = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblLabel = new System.Windows.Forms.Label();
            this.cmbSecurityLabel = new System.Windows.Forms.ComboBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnClear);
            this.pnlHeader.Controls.Add(this.btnSend);
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
            this.lblPageTitle.Text = "Send Notifications";
            this.lblPageTitle.TabIndex = 0;

            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.btnSend.FlatAppearance.BorderSize = 1;
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSend.Location = new System.Drawing.Point(690, 12);
            this.btnSend.Margin = new System.Windows.Forms.Padding(0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 32);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnClear.FlatAppearance.BorderSize = 1;
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.Location = new System.Drawing.Point(605, 12);
            this.btnClear.Margin = new System.Windows.Forms.Padding(0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 32);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // lblError
            this.lblError.AutoSize = true;
            this.lblError.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(180, 0, 0);
            this.lblError.Name = "lblError";
            this.lblError.Padding = new System.Windows.Forms.Padding(20, 6, 0, 0);
            this.lblError.TabIndex = 2;
            this.lblError.Text = "";
            this.lblError.Visible = false;

            // pnlContent
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.pnlContent.TabIndex = 1;

            // lblId
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblId.Location = new System.Drawing.Point(20, 20);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(80, 15);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "ID (MATB):";

            // txtId
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtId.Location = new System.Drawing.Point(120, 17);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(300, 22);
            this.txtId.TabIndex = 1;

            // lblContent
            this.lblContent.AutoSize = true;
            this.lblContent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblContent.Location = new System.Drawing.Point(20, 50);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(70, 15);
            this.lblContent.TabIndex = 2;
            this.lblContent.Text = "Content:";

            // txtContent
            this.txtContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContent.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtContent.Location = new System.Drawing.Point(120, 68);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(400, 120);
            this.txtContent.TabIndex = 3;

            // lblLocation
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocation.Location = new System.Drawing.Point(20, 200);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 15);
            this.lblLocation.TabIndex = 4;
            this.lblLocation.Text = "Location:";

            // txtLocation
            this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLocation.Location = new System.Drawing.Point(120, 197);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(300, 22);
            this.txtLocation.TabIndex = 5;

            // lblLabel
            this.lblLabel.AutoSize = true;
            this.lblLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLabel.Location = new System.Drawing.Point(20, 230);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(90, 15);
            this.lblLabel.TabIndex = 6;
            this.lblLabel.Text = "Send to (OLS):";

            // cmbSecurityLabel
            this.cmbSecurityLabel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSecurityLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbSecurityLabel.FormattingEnabled = true;
            this.cmbSecurityLabel.Location = new System.Drawing.Point(120, 227);
            this.cmbSecurityLabel.Name = "cmbSecurityLabel";
            this.cmbSecurityLabel.Size = new System.Drawing.Size(300, 23);
            this.cmbSecurityLabel.TabIndex = 7;

            // lblMessage
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            this.lblMessage.Location = new System.Drawing.Point(120, 260);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 15);
            this.lblMessage.TabIndex = 8;

            // Add controls to pnlContent
            this.pnlContent.Controls.Add(this.lblMessage);
            this.pnlContent.Controls.Add(this.cmbSecurityLabel);
            this.pnlContent.Controls.Add(this.lblLabel);
            this.pnlContent.Controls.Add(this.txtLocation);
            this.pnlContent.Controls.Add(this.lblLocation);
            this.pnlContent.Controls.Add(this.txtContent);
            this.pnlContent.Controls.Add(this.lblContent);
            this.pnlContent.Controls.Add(this.txtId);
            this.pnlContent.Controls.Add(this.lblId);

            // SendNotificationView
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Name = "SendNotificationView";
            this.Size = new System.Drawing.Size(800, 600);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.ComboBox cmbSecurityLabel;
        private System.Windows.Forms.Label lblMessage;
    }
}
