using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class RevokeView : System.Windows.Forms.UserControl
    {
        private readonly DbaService _dbaService;
        private string _currentMode = "SYSTEM PRIVILEGE";

        private DropDownComboBox _cmbSysPriv;
        private DropDownComboBox _cmbOwner;
        private DropDownComboBox _cmbObjectName;
        private DropDownComboBox _cmbPrivType;
        private System.Windows.Forms.CheckedListBox _lstColumns;
        private DropDownComboBox _cmbRoleToRevoke;
        private System.Windows.Forms.Label _lblDynamic;

        public RevokeView()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.White;
            _dbaService = new DbaService();

            cmbPrincipalType.Items.AddRange(new object[] { "USER", "ROLE" });
            cmbRevokeMode.Items.AddRange(new object[] { "SYSTEM PRIVILEGE", "OBJECT PRIVILEGE", "ROLE" });
            cmbPrincipalType.SelectedIndex = 0;
            cmbRevokeMode.SelectedIndex = 0;

            cmbPrincipalType.SelectedIndexChanged += cmbPrincipalType_SelectedIndexChanged;
            cmbRevokeMode.SelectedIndexChanged += cmbRevokeMode_SelectedIndexChanged;

            LoadPrincipals();
            ShowSystemPrivPanel();
        }

        private void cmbPrincipalType_SelectedIndexChanged(object sender, EventArgs e) { LoadPrincipals(); }

        private void LoadPrincipals()
        {
            try
            {
                cmbPrincipal.Items.Clear();
                if (cmbPrincipalType.SelectedItem?.ToString() == "USER")
                {
                    DataTable dt = _dbaService.GetUsersDetailed();
                    foreach (DataRow row in dt.Rows) cmbPrincipal.Items.Add(row["USERNAME"].ToString());
                }
                else
                {
                    DataTable dt = _dbaService.GetRolesDetailed();
                    foreach (DataRow row in dt.Rows) cmbPrincipal.Items.Add(row["ROLE"].ToString());
                }
                if (cmbPrincipal.Items.Count > 0) cmbPrincipal.SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show("Error loading principal list: " + ex.Message); }
        }

        private void ClearDynamicRow()
        {
            var toRemove = new List<Control>();
            foreach (Control c in tlpMain.Controls)
                if (tlpMain.GetRow(c) == 3) toRemove.Add(c);
            foreach (var c in toRemove) { tlpMain.Controls.Remove(c); c.Dispose(); }
            _lblDynamic = null; _cmbSysPriv = null; _cmbOwner = null;
            _cmbObjectName = null; _cmbPrivType = null; _lstColumns = null; _cmbRoleToRevoke = null;
            tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 52F);
            tlpMain.Height = 208;
            tlpMain.PerformLayout();
        }

        private void cmbRevokeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentMode = cmbRevokeMode.SelectedItem?.ToString() ?? "SYSTEM PRIVILEGE";
            ClearDynamicRow();
            switch (_currentMode)
            {
                case "SYSTEM PRIVILEGE": ShowSystemPrivPanel(); break;
                case "OBJECT PRIVILEGE": ShowObjectPrivPanel(); break;
                case "ROLE": ShowRolePanel(); break;
            }
        }

        private void ShowSystemPrivPanel()
        {
            _lblDynamic = MakeLabel("System privilege:");
            _cmbSysPriv = new DropDownComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList, Font = new System.Drawing.Font("Segoe UI", 9.5F), MaxDropDownItems = 6, IntegralHeight = false };
            try
            {
                DataTable dt = _dbaService.GetSystemPrivileges();
                foreach (DataRow row in dt.Rows) _cmbSysPriv.Items.Add(row["PRIVILEGE"].ToString());
                if (_cmbSysPriv.Items.Count > 0) _cmbSysPriv.SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show("Error loading system privileges: " + ex.Message); }
            tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 52F);
            tlpMain.PerformLayout();
            tlpMain.Controls.Add(_lblDynamic, 0, 3);
            tlpMain.Controls.Add(_cmbSysPriv, 1, 3);
        }

        private void ShowObjectPrivPanel()
        {
            var subTlp = new TableLayoutPanel { Dock = DockStyle.Fill, BackColor = System.Drawing.Color.White, ColumnCount = 2, RowCount = 4, Margin = new System.Windows.Forms.Padding(0), Padding = new System.Windows.Forms.Padding(0) };
            subTlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            subTlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            subTlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            subTlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            subTlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            subTlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            subTlp.Controls.Add(MakeLabel("Owner:"), 0, 0);
            _cmbOwner = new DropDownComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList, Font = new System.Drawing.Font("Segoe UI", 9.5F), MaxDropDownItems = 10 };
            subTlp.Controls.Add(_cmbOwner, 1, 0);

            subTlp.Controls.Add(MakeLabel("Object:"), 0, 1);
            _cmbObjectName = new DropDownComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList, Font = new System.Drawing.Font("Segoe UI", 9.5F), MaxDropDownItems = 10 };
            subTlp.Controls.Add(_cmbObjectName, 1, 1);

            subTlp.Controls.Add(MakeLabel("Privilege:"), 0, 2);
            _cmbPrivType = new DropDownComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList, Font = new System.Drawing.Font("Segoe UI", 9.5F), MaxDropDownItems = 10 };
            _cmbPrivType.Items.AddRange(new object[] { "SELECT", "INSERT", "UPDATE", "DELETE", "EXECUTE" });
            _cmbPrivType.SelectedIndex = 0;
            subTlp.Controls.Add(_cmbPrivType, 1, 2);

            subTlp.Controls.Add(MakeLabel("Columns:"), 0, 3);
            _lstColumns = new CheckedListBox { Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom, Enabled = false };
            subTlp.Controls.Add(_lstColumns, 1, 3);

            _cmbOwner.SelectedIndexChanged += (s, e) => {
                _cmbObjectName.Items.Clear(); _lstColumns.Items.Clear();
                string owner = _cmbOwner.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(owner)) return;
                try { DataTable dt = _dbaService.GetObjects(owner); foreach (DataRow row in dt.Rows) _cmbObjectName.Items.Add(row["OBJECT_NAME"].ToString()); if (_cmbObjectName.Items.Count > 0) _cmbObjectName.SelectedIndex = 0; }
                catch (Exception ex) { MessageBox.Show("Error loading objects: " + ex.Message); }
            };
            _cmbObjectName.SelectedIndexChanged += (s, e) => {
                _lstColumns.Items.Clear();
                string owner = _cmbOwner.SelectedItem?.ToString(); string obj = _cmbObjectName.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(owner) || string.IsNullOrEmpty(obj)) return;
                try { DataTable dt = _dbaService.GetColumns(owner, obj); foreach (DataRow row in dt.Rows) _lstColumns.Items.Add(row["COLUMN_NAME"].ToString()); }
                catch (Exception ex) { MessageBox.Show("Error loading columns: " + ex.Message); }
            };
            _cmbPrivType.SelectedIndexChanged += (s, e) => { string priv = _cmbPrivType.SelectedItem?.ToString(); _lstColumns.Enabled = (priv == "SELECT" || priv == "UPDATE"); };

            try { DataTable dtAll = _dbaService.GetObjects(); var owners = new HashSet<string>(); foreach (DataRow row in dtAll.Rows) { string owner = row["OWNER"].ToString(); if (owners.Add(owner)) _cmbOwner.Items.Add(owner); } if (_cmbOwner.Items.Count > 0) _cmbOwner.SelectedIndex = 0; }
            catch (Exception ex) { MessageBox.Show("Error loading owners: " + ex.Message); }

            tlpMain.RowStyles[3] = new RowStyle(SizeType.Percent, 100F);
            tlpMain.Height = 208 + 200;
            tlpMain.PerformLayout();
            tlpMain.Controls.Add(subTlp, 0, 3);
            tlpMain.SetColumnSpan(subTlp, 2);
        }

        private void ShowRolePanel()
        {
            _lblDynamic = MakeLabel("Role:");
            _cmbRoleToRevoke = new DropDownComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList, Font = new System.Drawing.Font("Segoe UI", 9.5F), MaxDropDownItems = 10 };
            try { DataTable dt = _dbaService.GetRolesDetailed(); foreach (DataRow row in dt.Rows) _cmbRoleToRevoke.Items.Add(row["ROLE"].ToString()); if (_cmbRoleToRevoke.Items.Count > 0) _cmbRoleToRevoke.SelectedIndex = 0; }
            catch (Exception ex) { MessageBox.Show("Error loading roles: " + ex.Message); }
            tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 52F);
            tlpMain.PerformLayout();
            tlpMain.Controls.Add(_lblDynamic, 0, 3);
            tlpMain.Controls.Add(_cmbRoleToRevoke, 1, 3);
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {
            if (cmbPrincipal.SelectedItem == null) { MessageBox.Show("Please select a principal."); return; }
            string grantee = cmbPrincipal.SelectedItem.ToString();
            try
            {
                switch (_currentMode)
                {
                    case "SYSTEM PRIVILEGE":
                        if (_cmbSysPriv?.SelectedItem == null) { MessageBox.Show("Please select a system privilege."); return; }
                        _dbaService.RevokePrivilege(grantee, _cmbSysPriv.SelectedItem.ToString()); break;
                    case "OBJECT PRIVILEGE":
                        string owner = _cmbOwner?.SelectedItem?.ToString(); string obj = _cmbObjectName?.SelectedItem?.ToString(); string priv = _cmbPrivType?.SelectedItem?.ToString();
                        if (string.IsNullOrEmpty(owner) || string.IsNullOrEmpty(obj) || string.IsNullOrEmpty(priv)) { MessageBox.Show("Please select owner, object, and privilege."); return; }
                        string onObject = owner + "." + obj;
                        List<string> cols = null;
                        if (_lstColumns != null && _lstColumns.Enabled && _lstColumns.CheckedItems.Count > 0) { cols = new List<string>(); foreach (var item in _lstColumns.CheckedItems) cols.Add(item.ToString()); }
                        _dbaService.RevokePrivilege(grantee, priv, onObject, cols); break;
                    case "ROLE":
                        if (_cmbRoleToRevoke?.SelectedItem == null) { MessageBox.Show("Please select a role."); return; }
                        _dbaService.RevokePrivilege(grantee, _cmbRoleToRevoke.SelectedItem.ToString()); break;
                }
                MessageBox.Show("Privilege revoked successfully.");
            }
            catch (Exception ex) { MessageBox.Show("Error revoking privilege: " + ex.Message); }
        }

        private static Label MakeLabel(string text)
        {
            return new Label { Text = text, Font = new System.Drawing.Font("Segoe UI", 9.5F), ForeColor = System.Drawing.Color.FromArgb(30, 30, 30), BackColor = System.Drawing.Color.White, Anchor = AnchorStyles.Left, AutoSize = true };
        }
    }
}
