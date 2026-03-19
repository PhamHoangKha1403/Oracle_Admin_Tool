using System;
using System.Drawing;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class LoginForm : Form
    {
        // Placeholder text constants
        private const string PlaceholderUsername = "Oracle username";
        private const string PlaceholderPassword = "Oracle password";
        private const string PlaceholderHost = "e.g. localhost";
        private const string PlaceholderPort = "e.g. 1521";
        private const string PlaceholderServiceName = "e.g. XEPDB1";

        private static readonly Color PlaceholderColor = Color.FromArgb(160, 160, 160);
        private static readonly Color InputColor = Color.FromArgb(30, 30, 30);

        public LoginForm()
        {
            InitializeComponent();
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

            string username = txtUsername.Text == PlaceholderUsername ? "" : txtUsername.Text.Trim();
            string password = txtPassword.Text == PlaceholderPassword ? "" : txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblStatus.ForeColor = Color.FromArgb(180, 0, 0);
                lblStatus.Text = "Username and Password are required.";
                return;
            }

            string host = (txtHost.Text == PlaceholderHost || string.IsNullOrWhiteSpace(txtHost.Text))
                ? "localhost" : txtHost.Text.Trim();
            string port = (txtPort.Text == PlaceholderPort || string.IsNullOrWhiteSpace(txtPort.Text))
                ? "1521" : txtPort.Text.Trim();
            string serviceName = (txtServiceName.Text == PlaceholderServiceName || string.IsNullOrWhiteSpace(txtServiceName.Text))
                ? "XEPDB1" : txtServiceName.Text.Trim();

            try
            {
                DbConnection.Instance.OpenConnection(username, password, host, port, serviceName);
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
