using System;
using System.Data;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class PermissionView : UserControl
    {
        private readonly DbaService _dbaService;

        public PermissionView()
        {
            InitializeComponent();
            _dbaService = new DbaService();
            LoadGrantors();
        }

        private void LoadGrantors()
        {
            try
            {
                DataTable dtUsers = _dbaService.GetUsers();
                DataTable dtRoles = _dbaService.GetRoles();

                cmbGrantee.Items.Clear();
                foreach (DataRow row in dtUsers.Rows) cmbGrantee.Items.Add(row["USERNAME"].ToString());
                foreach (DataRow row in dtRoles.Rows) cmbGrantee.Items.Add(row["ROLE"].ToString());

                DataTable dtObjects = _dbaService.GetObjectsForGrant();
                cmbObject.Items.Clear();
                cmbObject.Items.Add("(System Privilege)");
                foreach (DataRow row in dtObjects.Rows) cmbObject.Items.Add(row["OBJECT_NAME"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void btnGrant_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Grant functionality is being implemented.");
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Revoke functionality is being implemented.");
        }
    }
}
