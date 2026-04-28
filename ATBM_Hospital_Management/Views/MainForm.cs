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
            else if (tab == tpRegister)   view = new RegisterView();

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

            // ======= DYNAMIC ROUTER =======
            string role = DbConnection.Instance.GetCurrentUserRole();
            
            if (role == "DBA")
            {
                // Giữ nguyên giao diện DBA hiện hành
                LoadTab(tpDashboard);
            }
            else
            {
                // Xóa toàn bộ tab của Phân hệ 1
                tabControl1.TabPages.Clear();
                
                // Mở không gian làm việc của Phân hệ 2
                TabPage tpMedical = new TabPage("Dashboard (" + role + ")");
                
                if (role == "RL_BENHNHAN")
                {
                    tpMedical.Controls.Add(new Components.PatientView { Dock = DockStyle.Fill });
                }
                else if (role == "RL_DIEUPHOIVIEN")
                {
                    tpMedical.Controls.Add(new Components.CoordinatorView { Dock = DockStyle.Fill });
                }else if (role == "RL_BACSI")
                {
                    tpMedical.Controls.Add(new Components.DoctorView_PatientList { Dock = DockStyle.Fill });
                }
                else
                {
                    Label lblNotice = new Label
                    {
                        Text = $"Hello!\nThe functional interface specifically for {role} is currently under development.\nPlease follow upcoming updates.",
                        Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular),
                        ForeColor = System.Drawing.Color.DimGray,
                        AutoSize = true,
                        Location = new System.Drawing.Point(50, 50)
                    };
                    tpMedical.Controls.Add(lblNotice);
                }
                
                tabControl1.TabPages.Add(tpMedical);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to log out?", "Logout",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            DbConnection.Instance.CloseConnection();
            
            // We hide this form and show the login form, or close this form entirely using Application.OpenForms
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
