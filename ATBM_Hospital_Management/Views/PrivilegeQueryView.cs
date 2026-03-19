using System;
using System.Data;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class PrivilegeQueryView : UserControl
    {
        private readonly DbaService _dbaService;

        public PrivilegeQueryView()
        {
            InitializeComponent();
            _dbaService = new DbaService();

            cmbPrincipalType.Items.AddRange(new[] { "USER", "ROLE" });
            cmbPrincipalType.SelectedIndex = 0;
            cmbPrincipalType.SelectedIndexChanged += (s, e) => LoadPrincipals();

            LoadPrincipals();
        }

        private void PrivilegeQueryView_Load(object sender, EventArgs e)
        {
            // Sync button height to actual combobox height at runtime
            int h = cmbPrincipalType.Height;
            btnSearch.Height = h;
            int top = (pnlTop.Height - h) / 2;
            cmbPrincipalType.Top = top;
            cmbPrincipal.Top = top;
            btnSearch.Top = top;
            lblPrincipalType.Top = top + (h - lblPrincipalType.Height) / 2;
            lblPrincipal.Top = top + (h - lblPrincipal.Height) / 2;
        }

        private void LoadPrincipals()
        {
            cmbPrincipal.Items.Clear();
            try
            {
                if (cmbPrincipalType.SelectedItem?.ToString() == "USER")
                {
                    DataTable dt = _dbaService.GetUsersDetailed();
                    foreach (DataRow row in dt.Rows)
                        cmbPrincipal.Items.Add(row["USERNAME"].ToString());
                }
                else
                {
                    DataTable dt = _dbaService.GetRolesDetailed();
                    foreach (DataRow row in dt.Rows)
                        cmbPrincipal.Items.Add(row["ROLE"].ToString());
                }

                if (cmbPrincipal.Items.Count > 0)
                    cmbPrincipal.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading principal list: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Support both typed text and selected item (DropDown style)
            string principal = cmbPrincipal.Text?.Trim();
            if (string.IsNullOrEmpty(principal)) return;

            try
            {
                dgvSystemPrivs.DataSource = _dbaService.GetUserSystemPrivs(principal);
                dgvRoleGrants.DataSource = _dbaService.GetUserRolePrivs(principal);
                dgvObjectPrivs.DataSource = _dbaService.GetUserTabPrivs(principal);
                dgvColumnPrivs.DataSource = _dbaService.GetColPrivs(principal);

                bool allEmpty =
                    dgvSystemPrivs.Rows.Count == 0 &&
                    dgvRoleGrants.Rows.Count == 0 &&
                    dgvObjectPrivs.Rows.Count == 0 &&
                    dgvColumnPrivs.Rows.Count == 0;

                lblStatus.Text = allEmpty ? "No privileges found for: " + principal : "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error querying privileges: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
