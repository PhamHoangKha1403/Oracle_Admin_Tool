using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATBM_Hospital_Management.Views
{
    public partial class NotificationPopup : Form
    {
        public enum PopupType { Success, Error, Info }

        private PopupType _type;
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblMessage;
        private Button btnOK;
        private Panel pnlIcon;
        private Label lblIcon;

        public NotificationPopup(string title, string message, PopupType type = PopupType.Info)
        {
            _type = type;
            InitializeComponent();
            
            lblTitle.Text = title;
            lblMessage.Text = message;
            
            SetTypeStyle();
        }

        private void SetTypeStyle()
        {
            switch (_type)
            {
                case PopupType.Success:
                    lblIcon.ForeColor = Color.White;
                    btnOK.BackColor = Color.FromArgb(0, 128, 0);
                    break;
                case PopupType.Error:
                    lblIcon.ForeColor = Color.White;
                    btnOK.BackColor = Color.FromArgb(200, 0, 0);
                    break;
                default: // Info
                    lblIcon.ForeColor = Color.White;
                    btnOK.BackColor = Color.FromArgb(0, 120, 212);
                    break;
            }
        }

        private void InitializeComponent()
        {
            this.pnlIcon = new Panel();
            this.lblIcon = new Label();
            this.lblTitle = new Label();
            this.lblMessage = new Label();
            this.btnOK = new Button();
            this.SuspendLayout();

            // pnlIcon
            this.pnlIcon.Controls.Add(this.lblIcon);
            this.pnlIcon.Dock = DockStyle.Top;
            this.pnlIcon.Height = 60;
            this.pnlIcon.Name = "pnlIcon";
            this.pnlIcon.TabIndex = 0;

            // lblIcon
            this.lblIcon.AutoSize = true;
            this.lblIcon.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblIcon.ForeColor = Color.White;
            this.lblIcon.Location = new Point(15, 12);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new Size(30, 45);
            this.lblIcon.TabIndex = 0;
            this.lblIcon.Text = "✓";

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTitle.Location = new Point(15, 75);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(100, 21);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Success";

            // lblMessage
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new Font("Segoe UI", 9F);
            this.lblMessage.Location = new Point(15, 105);
            this.lblMessage.MaximumSize = new Size(350, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new Size(0, 0);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Message";

            // btnOK
            this.btnOK.FlatStyle = FlatStyle.Flat;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnOK.ForeColor = Color.White;
            this.btnOK.Location = new Point(300, 190);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(80, 32);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);

            // NotificationPopup
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(400, 240);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlIcon);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotificationPopup";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Notification";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
