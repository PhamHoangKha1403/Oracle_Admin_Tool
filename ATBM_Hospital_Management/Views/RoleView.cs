using System;
using System.Data;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class RoleView : UserControl
    {
        private readonly DbaService _dbaService;
        private DataTable _allRoles;

        public RoleView()
        {
            InitializeComponent();
            _dbaService = new DbaService();
            LoadRoles();
        }

        private void RoleView_Load(object sender, EventArgs e) { }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search role..." && txtSearch.ForeColor == System.Drawing.Color.FromArgb(120, 120, 120))
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Text = "Search role...";
                txtSearch.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            }
        }

        private void LoadRoles()
        {
            try
            {
                _allRoles = _dbaService.GetRolesDetailed();
                dgvRoles.DataSource = _allRoles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading roles: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_allRoles == null) return;

            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword) || keyword == "Search role...")
            {
                dgvRoles.DataSource = _allRoles;
                return;
            }

            DataTable filtered = _allRoles.Clone();
            foreach (DataRow row in _allRoles.Rows)
            {
                if (row["ROLE"].ToString().IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    filtered.ImportRow(row);
            }
            dgvRoles.DataSource = filtered;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRoles();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string roleName = ShowInputDialog("Create New Role", "Enter role name:");
            if (roleName == null) return;

            if (string.IsNullOrEmpty(roleName))
            {
                MessageBox.Show("Role name cannot be empty.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _dbaService.CreateRole(roleName);
                LoadRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating role: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            if (dgvRoles.CurrentRow == null)
            {
                MessageBox.Show("Please select a role.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string roleName = dgvRoles.CurrentRow.Cells["ROLE"].Value?.ToString();
            if (string.IsNullOrEmpty(roleName)) return;

            var confirm = MessageBox.Show($"Are you sure you want to drop role '{roleName}'?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                _dbaService.DropRole(roleName);
                LoadRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error dropping role: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ShowInputDialog(string title, string prompt)
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
                var txt = new TextBox { Left = 10, Top = 40, Width = 280 };
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
