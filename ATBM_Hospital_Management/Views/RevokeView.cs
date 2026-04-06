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

            cmbPrincipal.SelectedIndexChanged += (s, e) => { RefreshObjectList(); };

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
                if (cmbPrincipal.Items.Count > 0)
                {
                    cmbPrincipal.SelectedIndex = 0;
                    RefreshObjectList();
                }
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
        private void RefreshObjectList()
        {
            // Kiểm tra các control đã được khởi tạo chưa (tránh lỗi null khi mới mở app)
            if (_cmbOwner == null || _cmbObjectName == null || _cmbPrivType == null ||
                cmbPrincipal.SelectedItem == null || _currentMode != "OBJECT PRIVILEGE") return;

            _cmbOwner.Items.Clear();
            _cmbObjectName.Items.Clear();
            _cmbPrivType.Items.Clear();

            string grantee = cmbPrincipal.SelectedItem.ToString();
            try
            {
                DataTable dtObjects = _dbaService.GetObjectsByGrantee(grantee);
                var owners = new HashSet<string>();
                foreach (DataRow row in dtObjects.Rows)
                {
                    string owner = row["OWNER"].ToString();
                    if (owners.Add(owner)) _cmbOwner.Items.Add(owner);
                }

                if (_cmbOwner.Items.Count > 0)
                {
                    _cmbOwner.SelectedIndex = 0; // Tự động kích hoạt sự kiện SelectedIndexChanged của Owner
                }
            }catch (Exception ex) { Console.WriteLine("User has no privileges: " + ex.Message); }
        }
        private void ShowObjectPrivPanel()
        {
            var subTlp = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 3, BackColor = System.Drawing.Color.White };
            subTlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            subTlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            for (int i = 0; i < 3; i++) subTlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));

            _cmbOwner = new DropDownComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList };
            _cmbObjectName = new DropDownComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList };
            _cmbPrivType = new DropDownComboBox { Anchor = AnchorStyles.Left | AnchorStyles.Right, DropDownStyle = ComboBoxStyle.DropDownList };

            subTlp.Controls.Add(MakeLabel("Owner:"), 0, 0); subTlp.Controls.Add(_cmbOwner, 1, 0);
            subTlp.Controls.Add(MakeLabel("Object:"), 0, 1); subTlp.Controls.Add(_cmbObjectName, 1, 1);
            subTlp.Controls.Add(MakeLabel("Privilege:"), 0, 2); subTlp.Controls.Add(_cmbPrivType, 1, 2);

            // SỰ KIỆN 1: KHI CHỌN OWNER
            _cmbOwner.SelectedIndexChanged += (s, e) =>
            {
                if (_cmbOwner.SelectedItem == null) return;

                _cmbObjectName.Items.Clear();
                _cmbPrivType.Items.Clear();

                string grantee = cmbPrincipal.SelectedItem?.ToString();
                string owner = _cmbOwner.SelectedItem.ToString();

                try
                {
                    DataTable dt = _dbaService.GetObjectsByGrantee(grantee);
                    foreach (DataRow row in dt.Rows)
                    {
                        // Dùng Equals để so sánh an toàn
                        if (row["OWNER"].ToString().Trim().Equals(owner.Trim(), StringComparison.OrdinalIgnoreCase))
                            _cmbObjectName.Items.Add(row["TABLE_NAME"].ToString());
                    }

                    if (_cmbObjectName.Items.Count > 0)
                    {
                        _cmbObjectName.SelectedIndex = -1; // Reset trước khi chọn
                        _cmbObjectName.SelectedIndex = 0;
                    }
                }
                catch { }
            };

            // SỰ KIỆN 2: KHI CHỌN OBJECT
            _cmbObjectName.SelectedIndexChanged += (s, e) => {
                _cmbPrivType.Items.Clear();

                if (_cmbObjectName.SelectedItem == null || _cmbOwner.SelectedItem == null) return;

                string obj = _cmbObjectName.SelectedItem.ToString();
                string owner = _cmbOwner.SelectedItem.ToString();
                string grantee = cmbPrincipal.SelectedItem?.ToString();

                try
                {
                    DataTable dtPrivs = _dbaService.GetPrivilegesByObject(grantee, owner, obj);

                    // Console.WriteLine($"Tìm thấy {dtPrivs.Rows.Count} quyền cho {obj}");

                    if (dtPrivs != null && dtPrivs.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtPrivs.Rows)
                        {
                            _cmbPrivType.Items.Add(row["PRIVILEGE"].ToString());
                        }
                        _cmbPrivType.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi nạp Privilege: " + ex.Message);
                }
            };

            // QUAN TRỌNG: Cài đặt Layout xong xuôi rồi mới gọi nạp dữ liệu
            tlpMain.RowStyles[3] = new RowStyle(SizeType.Absolute, 160F);
            tlpMain.Height = 208 + 160;
            tlpMain.Controls.Add(subTlp, 0, 3);
            tlpMain.SetColumnSpan(subTlp, 2);
            tlpMain.PerformLayout();

            // Cuối cùng mới gọi hàm này
            RefreshObjectList();
        }



        private async void btnRevoke_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đầu vào
            if (cmbPrincipal.SelectedItem == null)
            {
                MessageBox.Show("Please select a User or Role to revoke privilege from.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string grantee = cmbPrincipal.SelectedItem.ToString();
            btnRevoke.Enabled = false; // Vô hiệu hóa nút để tránh bấm nhiều lần khi đang xử lý

            try
            {
                // 2. Xác định loại quyền (SYSTEM hoặc OBJECT)
                string procedureType = _currentMode.Contains("SYSTEM") ? "SYSTEM" : "OBJECT";

                if (_currentMode == "SYSTEM PRIVILEGE")
                {
                    if (_cmbSysPriv?.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a system privilege to revoke.");
                        return;
                    }
                    string sysPriv = _cmbSysPriv.SelectedItem.ToString();

                    // Gọi hàm Async từ Service
                    await _dbaService.RevokePrivilegeAsync(sysPriv, null, grantee, procedureType);
                }
                else // OBJECT PRIVILEGE
                {
                    string owner = _cmbOwner?.SelectedItem?.ToString();
                    string obj = _cmbObjectName?.SelectedItem?.ToString();
                    string priv = _cmbPrivType?.SelectedItem?.ToString();

                    if (string.IsNullOrEmpty(owner) || string.IsNullOrEmpty(obj) || string.IsNullOrEmpty(priv))
                    {
                        MessageBox.Show("Please select Owner, Object, and Privilege.");
                        return;
                    }

                    if (priv.Contains("("))
                        priv = priv.Substring(0, priv.IndexOf("(")).Trim();
                    // Nếu là column-level privilege dạng "UPDATE (col1, col2)" thì chỉ lấy phần privilege name 
                    string fullObjectName = $"{owner}.{obj}".ToUpper();
                    
                    Console.WriteLine(priv);

                    // Gọi hàm Async từ Service
                    await _dbaService.RevokePrivilegeAsync(priv, fullObjectName, grantee, procedureType);
                }

                MessageBox.Show($"Successfully revoked privilege from {grantee}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error revoking privilege: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRevoke.Enabled = true; // Mở lại nút sau khi xử lý xong
            }
        }

        private static Label MakeLabel(string text)
        {
            return new Label { Text = text, Font = new System.Drawing.Font("Segoe UI", 9.5F), ForeColor = System.Drawing.Color.FromArgb(30, 30, 30), BackColor = System.Drawing.Color.White, Anchor = AnchorStyles.Left, AutoSize = true };
        }
    }
}
