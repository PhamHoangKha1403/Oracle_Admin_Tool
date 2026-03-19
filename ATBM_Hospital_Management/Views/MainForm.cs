using System;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            // Lazy-load: create view only when tab is first selected
            if (e.TabPage != null && e.TabPage.Controls.Count == 0)
                LoadTab(e.TabPage);
        }

        private void LoadTab(TabPage tab)
        {
            Control view = null;
            if (tab == tpDashboard)       view = new DashboardView();
            else if (tab == tpUsers)      view = new UserView();
            else if (tab == tpRoles)      view = new RoleView();
            else if (tab == tpObjects)    view = new ObjectView();
            else if (tab == tpGrant)      view = new GrantView();
            else if (tab == tpRevoke)     view = new RevokeView();
            else if (tab == tpPrivQuery)  view = new PrivilegeQueryView();

            if (view != null)
            {
                view.Dock = DockStyle.Fill;
                tab.Controls.Add(view);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                string user = DbConnection.Instance.GetCurrentUser() ?? "N/A";
                toolStripStatusLabel1.Text = $"Connected: {user.ToUpper()}";
            }
            catch
            {
                toolStripStatusLabel1.Text = "Connected";
            }

            // Load first tab (Dashboard) immediately
            LoadTab(tpDashboard);
        }
    }
}
