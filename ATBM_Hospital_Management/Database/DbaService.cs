using System;
using System.Data;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace ATBM_Hospital_Management.Database
{
    public class DbaService
    {
        private readonly DbConnection _db;

        public DbaService()
        {
            _db = DbConnection.Instance;
        }

        // --- Requirement 1 & 2: User/Role Management ---

        public DataTable GetUsers()
        {
            string sql = "SELECT USERNAME, ACCOUNT_STATUS, CREATED FROM ALL_USERS ORDER BY USERNAME";
            return _db.ExecuteQuery(sql);
        }

        public DataTable GetRoles()
        {
            string sql = "SELECT ROLE, ROLE_ID FROM DBA_ROLES ORDER BY ROLE";
            return _db.ExecuteQuery(sql);
        }

        public void CreateUser(string username, string password)
        {
            string sql = $"CREATE USER {username} IDENTIFIED BY {password}";
            _db.ExecuteNonQuery(sql);
        }

        public void DropUser(string username)
        {
            string sql = $"DROP USER {username} CASCADE";
            _db.ExecuteNonQuery(sql);
        }

        public void ChangeUserPassword(string username, string newPassword)
        {
            string sql = $"ALTER USER {username} IDENTIFIED BY {newPassword}";
            _db.ExecuteNonQuery(sql);
        }

        public void CreateRole(string roleName)
        {
            string sql = $"CREATE ROLE {roleName}";
            _db.ExecuteNonQuery(sql);
        }

        public void DropRole(string roleName)
        {
            string sql = $"DROP ROLE {roleName}";
            _db.ExecuteNonQuery(sql);
        }

        // --- Requirement 3 & 4: Privileges ---

        public void GrantPrivilege(string grantee, string privilege, string onObject = null, bool withGrantOption = false, IEnumerable<string> columns = null)
        {
            string sql = $"GRANT {privilege}";
            var colList = columns != null ? new List<string>(columns) : null;
            bool hasColumns = colList != null && colList.Count > 0 && !string.IsNullOrEmpty(onObject);
            if (hasColumns)
            {
                sql += $" ({string.Join(",", colList)}) ON {onObject}";
            }
            else if (!string.IsNullOrEmpty(onObject))
            {
                sql += $" ON {onObject}";
            }
            sql += $" TO {grantee}";
            if (withGrantOption)
            {
                sql += (onObject != null) ? " WITH GRANT OPTION" : " WITH ADMIN OPTION";
            }
            _db.ExecuteNonQuery(sql);
        }

        public void RevokePrivilege(string grantee, string privilege, string onObject = null, IEnumerable<string> columns = null)
        {
            string sql = $"REVOKE {privilege}";
            var colList = columns != null ? new List<string>(columns) : null;
            bool hasColumns = colList != null && colList.Count > 0 && !string.IsNullOrEmpty(onObject);
            if (hasColumns)
            {
                sql += $" ({string.Join(",", colList)}) ON {onObject}";
            }
            else if (!string.IsNullOrEmpty(onObject))
            {
                sql += $" ON {onObject}";
            }
            sql += $" FROM {grantee}";
            _db.ExecuteNonQuery(sql);
        }

        // --- Requirement 5: View Privileges ---

        public DataTable GetPrivileges(string principalName)
        {
            DataTable dt = new DataTable();
            
            // Giả sử class _db của bạn quản lý ConnectionString
            using (OracleConnection conn = new OracleConnection(_db.ConnectionString)) 
            {
                using (OracleCommand cmd = new OracleCommand("VIEW_ALL_PRIVILEGES_ALL", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Xử lý logic: Nếu chuỗi rỗng hoặc là chữ "ALL" (tùy giao diện của bạn) thì truyền NULL
                    object paramValue;
                    if (string.IsNullOrWhiteSpace(principalName) || principalName.ToUpper() == "ALL")
                    {
                        paramValue = DBNull.Value;
                    }
                    else
                    {
                        paramValue = principalName.ToUpper();
                    }

                    // 1. Thêm tham số IN (p_grantee)
                    cmd.Parameters.Add(new OracleParameter("p_grantee", OracleDbType.Varchar2)).Value = paramValue;

                    // 2. Thêm tham số OUT (c_all)
                    OracleParameter outParam = new OracleParameter("c_all", OracleDbType.RefCursor);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        private void btnViewPrivileges_Click(object sender, EventArgs e)
        {
            // Lấy tên Principal từ Combobox
            string selectedPrincipal = cboPrincipal.Text; 
            
            // Lấy dữ liệu và đổ vào DataGridView
            DataTable dtPrivileges = GetPrivileges(selectedPrincipal);
            dataGridView1.DataSource = dtPrivileges;
            
            // Kiểm tra xem có dữ liệu không để báo lỗi (như thông báo "No privileges found" ở dưới cùng màn hình của bạn)
            if (dtPrivileges.Rows.Count == 0)
            {
                lblStatus.Text = $"No privileges found for: {selectedPrincipal}";
            }
            else
            {
                lblStatus.Text = $"Showing privileges for: {(string.IsNullOrEmpty(selectedPrincipal) ? "ALL" : selectedPrincipal)}";
            }
        }

        // --- Requirement 9: Extended DBA methods ---

        public DataTable GetUsersDetailed()
        {
            string sql = "SELECT USERNAME, ACCOUNT_STATUS, CREATED, DEFAULT_TABLESPACE, TEMPORARY_TABLESPACE, PROFILE FROM DBA_USERS ORDER BY USERNAME";
            return _db.ExecuteQuery(sql);
        }

        public DataTable GetRolesDetailed()
        {
            string sql = "SELECT ROLE, PASSWORD_REQUIRED, AUTHENTICATION_TYPE FROM DBA_ROLES ORDER BY ROLE";
            return _db.ExecuteQuery(sql);
        }

        public DataTable GetObjects(string owner = null)
        {
            string sql = "SELECT OWNER, OBJECT_NAME, OBJECT_TYPE, STATUS, CREATED FROM DBA_OBJECTS WHERE OBJECT_TYPE IN ('TABLE','VIEW','PROCEDURE','FUNCTION')";
            if (!string.IsNullOrEmpty(owner))
            {
                sql += " AND OWNER = :owner";
                sql += " ORDER BY OWNER, OBJECT_NAME";
                OracleParameter[] p = { new OracleParameter("owner", owner.ToUpper()) };
                return _db.ExecuteQuery(sql, p);
            }
            sql += " ORDER BY OWNER, OBJECT_NAME";
            return _db.ExecuteQuery(sql);
        }

        public DataTable GetColumns(string owner, string objectName)
        {
            string sql = "SELECT COLUMN_NAME FROM ALL_TAB_COLUMNS WHERE OWNER = :owner AND TABLE_NAME = :objectName ORDER BY COLUMN_ID";
            OracleParameter[] p =
            {
                new OracleParameter("owner", owner.ToUpper()),
                new OracleParameter("objectName", objectName.ToUpper())
            };
            return _db.ExecuteQuery(sql, p);
        }

        public DataTable GetSystemPrivileges()
        {
            string sql = "SELECT NAME AS PRIVILEGE FROM SYSTEM_PRIVILEGE_MAP ORDER BY NAME";
            return _db.ExecuteQuery(sql);
        }

        public DataTable GetColPrivs(string grantee)
        {
            string sql = "SELECT TABLE_NAME, COLUMN_NAME, PRIVILEGE, GRANTABLE FROM DBA_COL_PRIVS WHERE GRANTEE = :grantee";
            OracleParameter[] p = { new OracleParameter("grantee", grantee.ToUpper()) };
            return _db.ExecuteQuery(sql, p);
        }

        public void SetUserTablespaces(string username, string defaultTablespace, string tempTablespace)
        {
            string sql = $"ALTER USER {username} DEFAULT TABLESPACE {defaultTablespace} TEMPORARY TABLESPACE {tempTablespace}";
            _db.ExecuteNonQuery(sql);
        }

        public void LockUnlockUser(string username, bool lockUser)
        {
            string action = lockUser ? "LOCK" : "UNLOCK";
            string sql = $"ALTER USER {username} ACCOUNT {action}";
            _db.ExecuteNonQuery(sql);
        }

        public DataTable GetDashboardInfo()
        {
            string sql = "SELECT SYS_CONTEXT('USERENV','SESSION_USER') AS SESSION_USER, SYS_CONTEXT('USERENV','DB_NAME') AS DB_NAME, TO_CHAR(SYSDATE,'DD/MM/YYYY HH24:MI:SS') AS SERVER_TIME, BANNER AS VERSION_BANNER FROM V$VERSION WHERE ROWNUM=1";
            return _db.ExecuteQuery(sql);
        }
    }
}
