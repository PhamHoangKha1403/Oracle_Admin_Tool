using System;
using System.Data;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class DashboardView : UserControl
    {
        private readonly DbaService _dbaService;
        private Timer _refreshTimer;

        public DashboardView()
        {
            InitializeComponent();
            _dbaService = new DbaService();
            SetupRefreshTimer();
            LoadAllData();
        }

        private void SetupRefreshTimer()
        {
            _refreshTimer = new Timer();
            _refreshTimer.Interval = 30000; // refresh server time every 30s
            _refreshTimer.Tick += (s, e) => RefreshServerTime();
            _refreshTimer.Start();
        }

        private void LoadAllData()
        {
            lblError.Visible = false;
            try
            {
                LoadDashboardInfo();
                LoadStats();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error loading data: " + ex.Message;
                lblError.Visible = true;
            }
        }

        private void LoadDashboardInfo()
        {
            DataTable dt = _dbaService.GetDashboardInfo();
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lblSessionUser.Text = "User: " + row["SESSION_USER"];
                lblDbName.Text = "Database: " + row["DB_NAME"];
                lblServerTime.Text = "Server time: " + row["SERVER_TIME"];
                lblVersionBanner.Text = "Version: " + row["VERSION_BANNER"];
            }
        }

        private void LoadStats()
        {
            DataTable usersTable = _dbaService.GetUsersDetailed();
            DataTable rolesTable = _dbaService.GetRolesDetailed();
            lblTotalUsers.Text = "Total users: " + usersTable.Rows.Count;
            lblTotalRoles.Text = "Total roles: " + rolesTable.Rows.Count;
        }

        private void RefreshServerTime()
        {
            try
            {
                DataTable dt = _dbaService.GetDashboardInfo();
                if (dt.Rows.Count > 0)
                    lblServerTime.Text = "Server time: " + dt.Rows[0]["SERVER_TIME"];
            }
            catch
            {
                // silently ignore timer refresh errors
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllData();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _refreshTimer?.Stop();
                _refreshTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
