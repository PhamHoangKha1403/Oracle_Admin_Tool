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
        private System.Windows.Forms.Label _lblDynamic;

        public RevokeView()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.White;
            _dbaService = new DbaService();

            cmbPrincipalType.Items.AddRange(new object[] { "USER", "ROLE" });
            cmbRevokeMode.Items.AddRange(new object[] { "SYSTEM PRIVILEGE", "OBJECT PRIVILEGE"});
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
            _cmbObjectName = null; _cmbPrivType = null; 
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
            _cmbPrivType.Items.AddRange(new object[] { "SELECT", "INSERT", "UPDATE", "DELETE"});
            _cmbPrivType.SelectedIndex = 0;
            subTlp.Controls.Add(_cmbPrivType, 1, 2);

            

            _cmbOwner.SelectedIndexChanged += (s, e) => {
                _cmbObjectName.Items.Clear();
                string owner = _cmbOwner.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(owner)) return;
                try { DataTable dt = _dbaService.GetObjects(owner); foreach (DataRow row in dt.Rows) _cmbObjectName.Items.Add(row["OBJECT_NAME"].ToString()); if (_cmbObjectName.Items.Count > 0) _cmbObjectName.SelectedIndex = 0; }
                catch (Exception ex) { MessageBox.Show("Error loading objects: " + ex.Message); }
            };
            _cmbObjectName.SelectedIndexChanged += (s, e) => {
                string owner = _cmbOwner.SelectedItem?.ToString(); string obj = _cmbObjectName.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(owner) || string.IsNullOrEmpty(obj)) return;
                
            };
            _cmbPrivType.SelectedIndexChanged += (s, e) => { 
                string priv = _cmbPrivType.SelectedItem?.ToString(); 
                if (priv == "INSERT")
                {
                    MessageBox.Show(
                        "Note: Column-level INSERT may cause errors if required columns are missing.",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            };

            try { DataTable dtAll = _dbaService.GetObjects(); var owners = new HashSet<string>(); foreach (DataRow row in dtAll.Rows) { string owner = row["OWNER"].ToString(); if (owners.Add(owner)) _cmbOwner.Items.Add(owner); } if (_cmbOwner.Items.Count > 0) _cmbOwner.SelectedIndex = 0; }
            catch (Exception ex) { MessageBox.Show("Error loading owners: " + ex.Message); }

            tlpMain.RowStyles[3] = new RowStyle(SizeType.Percent, 100F);
            tlpMain.Height = 208 + 200;
            tlpMain.PerformLayout();
            tlpMain.Controls.Add(subTlp, 0, 3);
            tlpMain.SetColumnSpan(subTlp, 2);
        }

        private void btnRevoke_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn User/Role chưa
            if (cmbPrincipal.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn User hoặc Role cần thu hồi quyền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string grantee = cmbPrincipal.SelectedItem.ToString();

            try
            {
                // 2. Chuẩn hóa Type để gửi vào Procedure (Chỉ lấy "SYSTEM" hoặc "OBJECT")
                // Vì _currentMode có thể là "SYSTEM PRIVILEGE" hoặc "OBJECT PRIVILEGE"
                string procedureType = _currentMode.Contains("SYSTEM") ? "SYSTEM" : "OBJECT";

                switch (_currentMode)
                {
                    case "SYSTEM PRIVILEGE":
                        // Kiểm tra ComboBox quyền hệ thống (được tạo động trong ShowSystemPrivPanel)
                        if (_cmbSysPriv?.SelectedItem == null)
                        {
                            MessageBox.Show("Vui lòng chọn quyền hệ thống cần thu hồi.");
                            return;
                        }
                        string sysPriv = _cmbSysPriv.SelectedItem.ToString();

                        // Gọi Service: (Privilege, ObjectName, Grantee, Type, Columns)
         
                        _dbaService.RevokePrivilege(sysPriv, null, grantee, procedureType);
                        break;

                    case "OBJECT PRIVILEGE":
                        // Lấy thông tin từ các ComboBox động
                        string owner = _cmbOwner?.SelectedItem?.ToString();
                        string obj = _cmbObjectName?.SelectedItem?.ToString();
                        string priv = _cmbPrivType?.SelectedItem?.ToString();

                        if (string.IsNullOrEmpty(owner) || string.IsNullOrEmpty(obj) || string.IsNullOrEmpty(priv))
                        {
                            MessageBox.Show("Vui lòng chọn đầy đủ Owner, Object và Quyền.");
                            return;
                        }

                        // Gộp Owner và TableName thành "OWNER.TABLE_NAME"
                        string fullObjectName = $"{owner}.{obj}".ToUpper();

                        
                        _dbaService.RevokePrivilege(priv, fullObjectName, grantee, procedureType);
                        break;
                }

                MessageBox.Show($"Đã thu hồi quyền thành công từ {grantee}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // (Tùy chọn) Reset lại danh sách hoặc UI nếu cần
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi chi tiết từ Oracle (ví dụ lỗi ORA-XXXXX)
                MessageBox.Show("Lỗi khi thu hồi quyền: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Label MakeLabel(string text)
        {
            return new Label { Text = text, Font = new System.Drawing.Font("Segoe UI", 9.5F), ForeColor = System.Drawing.Color.FromArgb(30, 30, 30), BackColor = System.Drawing.Color.White, Anchor = AnchorStyles.Left, AutoSize = true };
        }
    }
}
