using System;
using System.Drawing;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class LoginForm : Form
    {
        // Placeholder text constants
        private string PlaceholderUsername => isAdminMode ? "Oracle DBA Username" : "Employee / Patient ID";
        private string PlaceholderPassword => isAdminMode ? "Oracle DBA Password" : "Password";
        private const string PlaceholderHost = "e.g. localhost";
        private const string PlaceholderPort = "e.g. 1521";
        private const string PlaceholderServiceName = "e.g. XEPDB1";

        private static readonly Color PlaceholderColor = Color.FromArgb(160, 160, 160);
        private static readonly Color InputColor = Color.FromArgb(30, 30, 30);

        private LinkLabel lnkToggleAdmin;
        private bool isAdminMode = false;

        public LoginForm()
        {
            InitializeComponent();
            SetupToggleLink();
            SetAdminMode(false);
        }

        private void SetupToggleLink()
        {
            lnkToggleAdmin = new LinkLabel
            {
                Text = "Login as System Administrator",
                AutoSize = true,
                BackColor = Color.White,
                LinkColor = Color.FromArgb(100, 100, 100),
                ActiveLinkColor = Color.FromArgb(30, 30, 30),
                Font = new Font("Segoe UI", 9F, FontStyle.Italic)
            };
            lnkToggleAdmin.LinkClicked += (s, e) => SetAdminMode(!isAdminMode);
            this.pnlCard.Controls.Add(lnkToggleAdmin);
            lnkToggleAdmin.BringToFront();
            lnkToggleAdmin.Location = new Point(32, pnlCard.Height - 35);
        }

        private void SetAdminMode(bool adminMode)
        {
            isAdminMode = adminMode;
            
            lblHost.Visible = adminMode;
            txtHost.Visible = adminMode;
            lblPort.Visible = adminMode;
            txtPort.Visible = adminMode;
            lblServiceName.Visible = adminMode;
            txtServiceName.Visible = adminMode;

            // Collapse rows in tableLayoutPanel1 (rows 2, 3, 4)
            tableLayoutPanel1.RowStyles[2].Height = adminMode ? 50F : 0F;
            tableLayoutPanel1.RowStyles[3].Height = adminMode ? 50F : 0F;
            tableLayoutPanel1.RowStyles[4].Height = adminMode ? 50F : 0F;

            if (adminMode)
            {
                lnkToggleAdmin.Text = "Back to Standard Login";
                lblTitle.Text = "Oracle DBA Login";
            }
            else
            {
                lnkToggleAdmin.Text = "Login as System Administrator";
                lblTitle.Text = "Hospital Access";
                
                // Hardcode standard connection strings for End Users
                txtHost.Text = "localhost";
                txtPort.Text = "1521";
                txtServiceName.Text = "XEPDB1";
            }
            
            // Refresh texts if they were placeholders
            if (txtUsername.ForeColor == PlaceholderColor) txtUsername.Text = PlaceholderUsername;
            if (txtPassword.ForeColor == PlaceholderColor) txtPassword.Text = PlaceholderPassword;
        }

        private void pnlCenter_Resize(object sender, EventArgs e)
        {
            pnlCard.Left = (pnlCenter.Width - pnlCard.Width) / 2;
            pnlCard.Top = (pnlCenter.Height - pnlCard.Height) / 2;
        }

        // --- Placeholder handlers ---

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == PlaceholderUsername)
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = InputColor;
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                txtUsername.Text = PlaceholderUsername;
                txtUsername.ForeColor = PlaceholderColor;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == PlaceholderPassword)
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = InputColor;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = PlaceholderPassword;
                txtPassword.ForeColor = PlaceholderColor;
            }
        }

        private void txtHost_Enter(object sender, EventArgs e)
        {
            if (txtHost.Text == PlaceholderHost)
            {
                txtHost.Text = "";
                txtHost.ForeColor = InputColor;
            }
        }

        private void txtHost_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHost.Text))
            {
                txtHost.Text = PlaceholderHost;
                txtHost.ForeColor = PlaceholderColor;
            }
        }

        private void txtPort_Enter(object sender, EventArgs e)
        {
            if (txtPort.Text == PlaceholderPort)
            {
                txtPort.Text = "";
                txtPort.ForeColor = InputColor;
            }
        }

        private void txtPort_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPort.Text))
            {
                txtPort.Text = PlaceholderPort;
                txtPort.ForeColor = PlaceholderColor;
            }
        }

        private void txtServiceName_Enter(object sender, EventArgs e)
        {
            if (txtServiceName.Text == PlaceholderServiceName)
            {
                txtServiceName.Text = "";
                txtServiceName.ForeColor = InputColor;
            }
        }

        private void txtServiceName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtServiceName.Text))
            {
                txtServiceName.Text = PlaceholderServiceName;
                txtServiceName.ForeColor = PlaceholderColor;
            }
        }

        // --- Connect ---

        private void btnConnect_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            // Use the getters for placeholders
            string username = txtUsername.Text == PlaceholderUsername ? "" : txtUsername.Text.Trim();
            string password = txtPassword.Text == PlaceholderPassword ? "" : txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblStatus.ForeColor = Color.FromArgb(180, 0, 0);
                lblStatus.Text = "Username and Password are required.";
                return;
            }

            string host = isAdminMode ? (txtHost.Text == PlaceholderHost || string.IsNullOrWhiteSpace(txtHost.Text) ? "localhost" : txtHost.Text.Trim()) : "localhost";
            string port = isAdminMode ? (txtPort.Text == PlaceholderPort || string.IsNullOrWhiteSpace(txtPort.Text) ? "1521" : txtPort.Text.Trim()) : "1521";
            string serviceName = isAdminMode ? (txtServiceName.Text == PlaceholderServiceName || string.IsNullOrWhiteSpace(txtServiceName.Text) ? "XEPDB1" : txtServiceName.Text.Trim()) : "XEPDB1";

            try
            {
                DbConnection.Instance.OpenConnection(username, password, host, port, serviceName);

                if (!isAdminMode)
                {
                    bool isDba = new Database.AccountService().IsDbaUser();
                    if (isDba)
                    {
                        DbConnection.Instance.CloseConnection();
                        throw new Exception("This account is an Oracle system user/DBA. Please use System Administrator login.");
                    }
                }

                new MainForm().Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = Color.FromArgb(180, 0, 0);
                lblStatus.Text = ex.Message;
            }
        }
    }
}
