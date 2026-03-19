using System;
using System.Data;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class ObjectView : UserControl
    {
        private readonly DbaService _dbaService;
        private DataTable _allObjects;

        public ObjectView()
        {
            InitializeComponent();
            _dbaService = new DbaService();
            try
            {
                LoadFilters();
                LoadObjects();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Initialization error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ObjectView_Load(object sender, EventArgs e)
        {
            // Sync button height to actual combobox height at runtime
            int h = cmbOwnerFilter.Height;
            btnRefresh.Height = h;
            int top = (pnlToolbar.Height - h) / 2;
            cmbOwnerFilter.Top = top;
            cmbObjectTypeFilter.Top = top;
            btnRefresh.Top = top;
            lblOwnerFilter.Top = top + (h - lblOwnerFilter.Height) / 2;
            lblObjectTypeFilter.Top = top + (h - lblObjectTypeFilter.Height) / 2;
        }

        private void LoadFilters()
        {
            try
            {
                DataTable dt = _dbaService.GetObjects();
                _allObjects = dt;

                // Populate owner filter
                cmbOwnerFilter.Items.Clear();
                cmbOwnerFilter.Items.Add("(All)");
                var seenOwners = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow row in dt.Rows)
                {
                    string owner = row["OWNER"]?.ToString();
                    if (!string.IsNullOrEmpty(owner) && seenOwners.Add(owner))
                        cmbOwnerFilter.Items.Add(owner);
                }
                cmbOwnerFilter.SelectedIndex = 0;

                // Populate object type filter
                cmbObjectTypeFilter.Items.Clear();
                cmbObjectTypeFilter.Items.Add("(All)");
                var seenTypes = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow row in dt.Rows)
                {
                    string objType = row["OBJECT_TYPE"]?.ToString();
                    if (!string.IsNullOrEmpty(objType) && seenTypes.Add(objType))
                        cmbObjectTypeFilter.Items.Add(objType);
                }
                cmbObjectTypeFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading filters: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadObjects()
        {
            try
            {
                if (_allObjects == null)
                    _allObjects = _dbaService.GetObjects();

                string ownerSel = cmbOwnerFilter.SelectedItem?.ToString();
                string typeSel = cmbObjectTypeFilter.SelectedItem?.ToString();
                string ownerFilter = (ownerSel == "(All)" || ownerSel == null) ? null : ownerSel;
                string typeFilter = (typeSel == "(All)" || typeSel == null) ? null : typeSel;

                // Filter in-memory
                string filter = "";
                if (ownerFilter != null)
                    filter += $"OWNER = '{ownerFilter}'";
                if (typeFilter != null)
                {
                    if (filter.Length > 0) filter += " AND ";
                    filter += $"OBJECT_TYPE = '{typeFilter}'";
                }

                DataView dv = _allObjects.DefaultView;
                dv.RowFilter = filter;
                dgvObjects.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading objects: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbFilter_Changed(object sender, EventArgs e)
        {
            LoadObjects();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                _allObjects = null;
                LoadFilters();
                LoadObjects();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
