using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class RegisterView : UserControl
    {
        private readonly AccountService _accountService;
        private DataTable _allEmployees;
        private const string SearchPlaceholder = "Search by ID or name...";

        public RegisterView()
        {
            InitializeComponent();
            _accountService = new AccountService();

            dgvEmployees.SelectionChanged += DgvEmployees_SelectionChanged;
            dgvEmployees.CellFormatting += DgvEmployees_CellFormatting;

            SetCreateEnabled(false);
            LoadData();
        }

        // ── Placeholder ───────────────────────────────────────────────────

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == SearchPlaceholder)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.FromArgb(40, 40, 40);
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Text = SearchPlaceholder;
                txtSearch.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

        // ── Load data ─────────────────────────────────────────────────────

        private void LoadData()
        {
            try
            {
                lblStatus.ForeColor = Color.FromArgb(80, 80, 80);
                lblStatus.Text = "Loading...";

                DataTable allNv = _accountService.GetAllEmployees();
                DataTable noAccount = _accountService.GetEmployeesWithoutAccount();

                _allEmployees = allNv.Copy();
                if (!_allEmployees.Columns.Contains("STATUS"))
                    _allEmployees.Columns.Add("STATUS", typeof(string));

                var noAccountSet = new System.Collections.Generic.HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);
                foreach (DataRow r in noAccount.Rows)
                    noAccountSet.Add(r["ID"].ToString());

                foreach (DataRow r in _allEmployees.Rows)
                    r["STATUS"] = noAccountSet.Contains(r["ID"].ToString()) ? "No account" : "Active";

                BindGrid(_allEmployees);
                lblStatus.Text = $"{_allEmployees.Rows.Count} employees  |  {noAccount.Rows.Count} without account";
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = Color.FromArgb(180, 0, 0);
                lblStatus.Text = "Error loading data: " + ex.Message;
            }
        }

        private void BindGrid(DataTable dt)
        {
            dgvEmployees.DataSource = null;
            dgvEmployees.DataSource = dt;

            if (dgvEmployees.Columns.Contains("ID"))       dgvEmployees.Columns["ID"].HeaderText       = "ID";
            if (dgvEmployees.Columns.Contains("FULL_NAME"))      dgvEmployees.Columns["FULL_NAME"].HeaderText      = "Full Name";
            if (dgvEmployees.Columns.Contains("ROLE"))     dgvEmployees.Columns["ROLE"].HeaderText     = "Role";
            if (dgvEmployees.Columns.Contains("DEPT")) dgvEmployees.Columns["DEPT"].HeaderText = "Department";
            if (dgvEmployees.Columns.Contains("STATUS"))      dgvEmployees.Columns["STATUS"].HeaderText      = "Account";

            SetCreateEnabled(false);
        }

        // ── Search ────────────────────────────────────────────────────────

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_allEmployees == null) return;
            string q = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(q) || q == SearchPlaceholder)
            {
                BindGrid(_allEmployees);
                return;
            }
            q = q.ToLower();
            DataTable filtered = _allEmployees.Clone();
            foreach (DataRow row in _allEmployees.Rows)
            {
                if (row["ID"].ToString().ToLower().Contains(q) ||
                    row["FULL_NAME"].ToString().ToLower().Contains(q))
                    filtered.ImportRow(row);
            }
            BindGrid(filtered);
        }

        // ── Cell formatting ───────────────────────────────────────────────

        private void DgvEmployees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!dgvEmployees.Columns.Contains("STATUS")) return;
            if (e.ColumnIndex != dgvEmployees.Columns["STATUS"].Index) return;
            if (e.Value == null) return;

            e.CellStyle.ForeColor = e.Value.ToString() == "Active"
                ? Color.FromArgb(0, 140, 0)
                : Color.FromArgb(180, 0, 0);
        }

        // ── Selection ─────────────────────────────────────────────────────

        private void DgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0) { SetCreateEnabled(false); return; }
            string status = dgvEmployees.SelectedRows[0].Cells["STATUS"].Value?.ToString() ?? "";
            SetCreateEnabled(status == "No account");
        }

        private void SetCreateEnabled(bool enabled)
        {
            btnCreate.Enabled = enabled;
            if (enabled)
            {
                btnCreate.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
                btnCreate.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                btnCreate.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
                btnCreate.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            }
        }

        // ── Buttons ───────────────────────────────────────────────────────

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0) return;
            string maNv = dgvEmployees.SelectedRows[0].Cells["ID"].Value?.ToString();
            if (string.IsNullOrEmpty(maNv)) return;

            try
            {
                _accountService.CreateAccountForEmployee(maNv);
                MessageBox.Show(
                    $"Account created for {maNv}.\nDefault password: {maNv}23127@",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCreateAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Create accounts for all employees without one?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int count = _accountService.CreateAllAccounts();
                MessageBox.Show($"{count} account(s) created successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != SearchPlaceholder) txtSearch.Text = "";
            LoadData();
        }
    }
}
