using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class AuditView : UserControl
    {
        private bool _suppressToggleAction = false;
        private readonly DbaService _dbaService;
        private readonly Timer _refreshTimer;
        private DataTable _allAudit = new DataTable();
        private bool _isRefreshing;
        private string _lastTopSignature = string.Empty;

        private const string SearchPlaceholder = "Search by USER or TABLE...";

        public AuditView()
        {
            InitializeComponent();

            _dbaService = new DbaService();

            cbAuditTable.SelectedIndexChanged += AuditTable_Changed;
            cbAuditToggle.SelectedIndexChanged += AuditToggle_Changed;

            SetupUi();
            RefreshAudit(forceRebind: true);
            // After initial refresh, ensure toggle reflects DB state
            try { UpdateToggleState(); } catch { }

            _refreshTimer = new Timer();
            _refreshTimer.Interval = 1500;
            _refreshTimer.Tick += RefreshTimer_Tick;
            _refreshTimer.Start();

            this.Disposed += AuditView_Disposed;
        }

        private void AuditTable_Changed(object sender, EventArgs e)
        {
            // when selected table changes: update toggle state and reapply filters
            UpdateToggleState();
            ApplyFilters();
        }

        private void AuditToggle_Changed(object sender, EventArgs e)
        {
            if (_suppressToggleAction || _isRefreshing) return;

            string action = cbAuditToggle.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(action)) return;

            System.Diagnostics.Debug.WriteLine($"[AuditToggle_Changed] User selected: {action}");

            try
            {
                // Toggle applies to ALL tables only
                if (action.Equals("Bật", StringComparison.OrdinalIgnoreCase))
                {
                    System.Diagnostics.Debug.WriteLine($"[AuditToggle_Changed] Calling EnableAllAudit()");
                    _dbaService.EnableAllAudit();
                }
                else if (action.Equals("Tắt", StringComparison.OrdinalIgnoreCase))
                {
                    System.Diagnostics.Debug.WriteLine($"[AuditToggle_Changed] Calling DisableAllAudit()");
                    _dbaService.DisableAllAudit();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AuditToggle_Changed] Error: {ex.Message}");
                MessageBox.Show("Lỗi khi thay đổi trạng thái policy: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // refresh UI after action
            System.Diagnostics.Debug.WriteLine($"[AuditToggle_Changed] Refreshing audit and updating toggle state");
            RefreshAudit(forceRebind: true);
            // Ensure toggle reflects DB state after the change
            UpdateToggleState();
        }

        private void UpdateToggleState()
        {
            // Always check DB-wide policies (toggle controls all tables only)
            DataTable policies = null;
            try
            {
                policies = _dbaService.GetFgaPolicies(null);
                System.Diagnostics.Debug.WriteLine($"[UpdateToggleState] GetFgaPolicies returned {policies?.Rows.Count ?? 0} rows");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[UpdateToggleState] Error fetching policies: {ex.Message}");
                policies = null;
            }

            bool anyEnabled = false;
            if (policies != null && policies.Rows.Count > 0)
            {
                foreach (DataRow r in policies.Rows)
                {
                    var enabledVal = r.Table.Columns.Contains("ENABLED") ? (r["ENABLED"]?.ToString() ?? string.Empty) : string.Empty;
                    var policyName = r.Table.Columns.Contains("POLICY_NAME") ? (r["POLICY_NAME"]?.ToString() ?? string.Empty) : string.Empty;
                    System.Diagnostics.Debug.WriteLine($"[UpdateToggleState] Policy: {policyName}, ENABLED: {enabledVal}");
                    
                    if (!string.IsNullOrEmpty(enabledVal))
                    {
                        if (enabledVal.Equals("YES", StringComparison.OrdinalIgnoreCase) || enabledVal.Equals("Y", StringComparison.OrdinalIgnoreCase) || enabledVal.Equals("1"))
                        {
                            anyEnabled = true;
                            System.Diagnostics.Debug.WriteLine($"[UpdateToggleState] Found enabled policy: {policyName}");
                            break;
                        }
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"[UpdateToggleState] No policies returned from DB");
            }

            _suppressToggleAction = true;
            string newState = anyEnabled ? "Bật" : "Tắt";
            string oldState = cbAuditToggle.SelectedItem?.ToString() ?? "???";
            System.Diagnostics.Debug.WriteLine($"[UpdateToggleState] Changing toggle from '{oldState}' to '{newState}'");
            
            cbAuditToggle.SelectedItem = newState;
            _suppressToggleAction = false;
        }

        private void SetupUi()
        {
            _suppressToggleAction = true;

            txtAuditSearch.Text = SearchPlaceholder;
            txtAuditSearch.ForeColor = Color.FromArgb(120, 120, 120);

            colAuditUser.HeaderText = "DB_USER";
            colAuditColumn.HeaderText = "POLICY_NAME";
            colAuditAction.HeaderText = "STATEMENT_TYPE";
            colAuditTime.HeaderText = "TIMESTAMP";

            cbAuditTable.Items.Clear();
            cbAuditTable.Items.Add("Tất cả bảng");
            cbAuditTable.SelectedIndex = 0;

            cbAuditToggle.Items.Clear();
            // Toggle control for enabling/disabling audit (UI only)
            cbAuditToggle.Items.Add("Bật");
            cbAuditToggle.Items.Add("Tắt");
            // Do not pre-select; query DB at startup will set correct state
            cbAuditToggle.SelectedIndex = -1;

            // Make grid style consistent with other pages: fill columns, white background, subtle grid
            dgvAudit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAudit.BackgroundColor = Color.White;
            dgvAudit.BorderStyle = BorderStyle.None;
            dgvAudit.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvAudit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvAudit.EnableHeadersVisualStyles = false;
            dgvAudit.GridColor = Color.FromArgb(220, 220, 220);
            dgvAudit.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.FromArgb(245, 245, 245),
                ForeColor = Color.FromArgb(60, 60, 60),
                Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold),
                Padding = new Padding(8, 0, 0, 0),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            dgvAudit.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvAudit.ColumnHeadersHeight = 36;
            dgvAudit.DefaultCellStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(40, 40, 40),
                Font = new System.Drawing.Font("Segoe UI", 9F),
                SelectionBackColor = Color.FromArgb(220, 220, 220),
                SelectionForeColor = Color.FromArgb(20, 20, 20),
                Padding = new Padding(8, 0, 0, 0)
            };
            dgvAudit.RowTemplate.Height = 34;
            dgvAudit.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Distribute column widths and make POLICY_NAME a bit larger
            colAuditUser.FillWeight = 22F;
            colAuditColumn.FillWeight = 36F;
            colAuditAction.FillWeight = 20F;
            colAuditTime.FillWeight = 22F;

            _suppressToggleAction = false;
            UpdateToggleState();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"[RefreshTimer_Tick] Timer tick - updating toggle state");
            RefreshAudit(forceRebind: false);
            // Always update toggle state to reflect current DB policy state
            UpdateToggleState();
        }

        private void AuditView_Disposed(object sender, EventArgs e)
        {
            if (_refreshTimer != null)
            {
                _refreshTimer.Stop();
                _refreshTimer.Tick -= RefreshTimer_Tick;
            }
        }

        private void RefreshAudit(bool forceRebind)
        {
            if (_isRefreshing) return;

            try
            {
                _isRefreshing = true;

                DataTable latest = _dbaService.GetAudit();
                if (latest == null)
                    return;

                string newSignature = BuildTopSignature(latest);
                bool hasNewData = forceRebind || !string.Equals(newSignature, _lastTopSignature, StringComparison.Ordinal);
                if (!hasNewData)
                    return;

                _allAudit = latest;
                _lastTopSignature = newSignature;

                RebuildObjectFilterItems();
                ApplyFilters();
                // Keep the toggle in sync with DB state after refreshing audit data
                UpdateToggleState();
            }
            catch
            {
                // Keep silent for periodic refresh to avoid spamming popup dialogs.
            }
            finally
            {
                _isRefreshing = false;
            }
        }

        private string BuildTopSignature(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return "EMPTY";

            DataRow r = dt.Rows[0];
            return string.Join("|",
                r["DB_USER"]?.ToString() ?? string.Empty,
                r["OBJECT_NAME"]?.ToString() ?? string.Empty,
                r["POLICY_NAME"]?.ToString() ?? string.Empty,
                r["STATEMENT_TYPE"]?.ToString() ?? string.Empty,
                r["TIMESTAMP"]?.ToString() ?? string.Empty,
                dt.Rows.Count.ToString());
        }

        private void RebuildObjectFilterItems()
        {
            string current = cbAuditTable.SelectedItem?.ToString() ?? "Tất cả bảng";

            cbAuditTable.Items.Clear();
            cbAuditTable.Items.Add("Tất cả bảng");

            HashSet<string> seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataRow row in _allAudit.Rows)
            {
                string objectName = row["OBJECT_NAME"]?.ToString();
                if (!string.IsNullOrWhiteSpace(objectName) && seen.Add(objectName))
                {
                    cbAuditTable.Items.Add(objectName);
                }
            }

            int idx = cbAuditTable.Items.IndexOf(current);
            cbAuditTable.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private void ApplyFilters()
        {
            dgvAudit.Rows.Clear();
            if (_allAudit == null || _allAudit.Rows.Count == 0)
                return;

            string selectedObject = cbAuditTable.SelectedItem?.ToString() ?? "Tất cả bảng";
            // cbAuditToggle is for enable/disable actions in the UI; do not use it for filtering audit rows
            string query = txtAuditSearch.Text?.Trim() ?? string.Empty;
            bool hasQuery = !string.IsNullOrWhiteSpace(query) && !query.Equals(SearchPlaceholder, StringComparison.OrdinalIgnoreCase);
            string queryLower = query.ToLowerInvariant();

            foreach (DataRow row in _allAudit.Rows)
            {
                string dbUser = row["DB_USER"]?.ToString() ?? string.Empty;
                string objectName = row["OBJECT_NAME"]?.ToString() ?? string.Empty;
                string policyName = row["POLICY_NAME"]?.ToString() ?? string.Empty;
                string statementType = row["STATEMENT_TYPE"]?.ToString() ?? string.Empty;
                string sqlText = row["SQL_TEXT"]?.ToString() ?? string.Empty;

                if (!"Tất cả bảng".Equals(selectedObject, StringComparison.OrdinalIgnoreCase)
                    && !objectName.Equals(selectedObject, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // no-op: audit toggle does not filter statement types here

                if (hasQuery)
                {
                    bool matched = dbUser.ToLowerInvariant().Contains(queryLower)
                                   || objectName.ToLowerInvariant().Contains(queryLower)
                                   || policyName.ToLowerInvariant().Contains(queryLower)
                                   || statementType.ToLowerInvariant().Contains(queryLower)
                                   || sqlText.ToLowerInvariant().Contains(queryLower);
                    if (!matched)
                    {
                        continue;
                    }
                }

                string tsText = string.Empty;
                object tsValue = row["TIMESTAMP"];
                if (tsValue != null && tsValue != DBNull.Value)
                {
                    DateTime ts;
                    if (DateTime.TryParse(tsValue.ToString(), out ts))
                        tsText = ts.ToString("yyyy-MM-dd HH:mm:ss");
                    else
                        tsText = tsValue.ToString();
                }

                dgvAudit.Rows.Add(dbUser, policyName, statementType, tsText);
            }
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtAuditSearch.Text == SearchPlaceholder)
            {
                txtAuditSearch.Text = string.Empty;
                txtAuditSearch.ForeColor = Color.FromArgb(40, 40, 40);
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAuditSearch.Text))
            {
                txtAuditSearch.Text = SearchPlaceholder;
                txtAuditSearch.ForeColor = Color.FromArgb(120, 120, 120);
            }
        }

        private void pnlFilterRow_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblAuditToggle_Click(object sender, EventArgs e)
        {

        }

        private void dgvAudit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
