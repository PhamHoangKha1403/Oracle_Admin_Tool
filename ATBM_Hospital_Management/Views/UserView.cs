using System;
using System.Data;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class UserView : UserControl
    {
        private readonly DbaService _dbaService;
        private DataTable _allUsers;

        public UserView()
        {
            InitializeComponent();
            _dbaService = new DbaService();
            LoadUsers();
        }

        private void UserView_Load(object sender, EventArgs e) { }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search username..." && txtSearch.ForeColor == System.Drawing.Color.FromArgb(120, 120, 120))
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Text = "Search username...";
                txtSearch.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            }
        }

        private void LoadUsers()
        {
            try
            {
                _allUsers = _dbaService.GetUsersDetailed();
                dgvUsers.DataSource = _allUsers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_allUsers == null) return;

            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword) || keyword == "Search username...")
            {
                dgvUsers.DataSource = _allUsers;
                return;
            }

            DataTable filtered = _allUsers.Clone();
            foreach (DataRow row in _allUsers.Rows)
            {
                if (row["USERNAME"].ToString().IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    filtered.ImportRow(row);
            }
            dgvUsers.DataSource = filtered;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (var dlg = new Form())
            {
                dlg.Text = "Create New User";
                dlg.Size = new System.Drawing.Size(360, 280);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;

                var layout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 5,
                    Padding = new Padding(10)
                };
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

                var txtUser = new TextBox { Dock = DockStyle.Fill };
                var txtPass = new TextBox { Dock = DockStyle.Fill, UseSystemPasswordChar = true };
                var txtDefTs = new TextBox { Dock = DockStyle.Fill, Text = "USERS" };
                var txtTmpTs = new TextBox { Dock = DockStyle.Fill, Text = "TEMP" };

                layout.Controls.Add(new Label { Text = "Username:", Anchor = AnchorStyles.Left | AnchorStyles.Right }, 0, 0);
                layout.Controls.Add(txtUser, 1, 0);
                layout.Controls.Add(new Label { Text = "Password:", Anchor = AnchorStyles.Left | AnchorStyles.Right }, 0, 1);
                layout.Controls.Add(txtPass, 1, 1);
                layout.Controls.Add(new Label { Text = "Default Tablespace:", Anchor = AnchorStyles.Left | AnchorStyles.Right }, 0, 2);
                layout.Controls.Add(txtDefTs, 1, 2);
                layout.Controls.Add(new Label { Text = "Temp Tablespace:", Anchor = AnchorStyles.Left | AnchorStyles.Right }, 0, 3);
                layout.Controls.Add(txtTmpTs, 1, 3);

                var btnOk = new Button { Text = "Create", DialogResult = DialogResult.OK, Dock = DockStyle.Fill };
                layout.Controls.Add(btnOk, 1, 4);

                dlg.Controls.Add(layout);
                dlg.AcceptButton = btnOk;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    string username = txtUser.Text.Trim();
                    string password = txtPass.Text.Trim();
                    string defTs = txtDefTs.Text.Trim();
                    string tmpTs = txtTmpTs.Text.Trim();

                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Username and password cannot be empty.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        _dbaService.CreateUser(username, password);
                        _dbaService.SetUserTablespaces(username, defTs, tmpTs);
                        LoadUsers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error creating user: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Please select a user.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string username = dgvUsers.CurrentRow.Cells["USERNAME"].Value?.ToString();
            if (string.IsNullOrEmpty(username)) return;

            string newPassword = ShowInputDialog("Change Password", $"Enter new password for '{username}':", isPassword: true);
            if (newPassword == null) return;

            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Password cannot be empty.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _dbaService.ChangeUserPassword(username, newPassword);
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error changing password: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLockUnlock_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Please select a user.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string username = dgvUsers.CurrentRow.Cells["USERNAME"].Value?.ToString();
            string status = dgvUsers.CurrentRow.Cells["ACCOUNT_STATUS"].Value?.ToString();
            if (string.IsNullOrEmpty(username)) return;

            bool isLocked = string.Equals(status, "OPEN", StringComparison.OrdinalIgnoreCase);

            try
            {
                _dbaService.LockUnlockUser(username, isLocked);
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error locking/unlocking user: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Please select a user.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string username = dgvUsers.CurrentRow.Cells["USERNAME"].Value?.ToString();
            if (string.IsNullOrEmpty(username)) return;

            var confirm = MessageBox.Show($"Are you sure you want to drop user '{username}'?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                _dbaService.DropUser(username);
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error dropping user: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ShowInputDialog(string title, string prompt, bool isPassword = false)
        {
            using (var dlg = new Form())
            {
                dlg.Text = title;
                dlg.Size = new System.Drawing.Size(320, 150);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;

                var lbl = new Label { Text = prompt, Left = 10, Top = 15, Width = 280 };
                var txt = new TextBox
                {
                    Left = 10, Top = 40, Width = 280,
                    UseSystemPasswordChar = isPassword
                };
                var btnOk = new Button
                {
                    Text = "OK", DialogResult = DialogResult.OK,
                    Left = 130, Top = 75, Width = 75
                };
                var btnCancel = new Button
                {
                    Text = "Cancel", DialogResult = DialogResult.Cancel,
                    Left = 215, Top = 75, Width = 75
                };

                dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
                dlg.AcceptButton = btnOk;
                dlg.CancelButton = btnCancel;

                return dlg.ShowDialog(this) == DialogResult.OK ? txt.Text : null;
            }
        }
    }
}
