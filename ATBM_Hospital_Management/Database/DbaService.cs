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

        public void GrantPrivilege(string grantee, string privilege, string onObject = null, bool withOption = false, List<string> columns = null)
        {
            // Chuyển List thành chuỗi: "COL1,COL2"
            string colString = (columns != null && columns.Count > 0) ? string.Join(",", columns) : null;

            // SP nhận p_with_option là NUMBER, C# truyền 1 hoặc 0
            int p_with_option = withOption ? 1 : 0;

            using (OracleCommand cmd = new OracleCommand("SP_GRANT_PRIVILEGE_UI", (OracleConnection)_db.GetConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("p_principal", OracleDbType.Varchar2).Value = grantee;
                cmd.Parameters.Add("p_privilege", OracleDbType.Varchar2).Value = privilege;
                cmd.Parameters.Add("p_object_name", OracleDbType.Varchar2).Value = (object)onObject ?? DBNull.Value;
                cmd.Parameters.Add("p_columns", OracleDbType.Varchar2).Value = (object)colString ?? DBNull.Value;
                cmd.Parameters.Add("p_with_option", OracleDbType.Int32).Value = p_with_option;

                try
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi thực thi Grant: " + ex.Message);
                }
            }
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

        public DataTable GetUserSystemPrivs(string username)
        {
            string sql = "SELECT PRIVILEGE, ADMIN_OPTION FROM DBA_SYS_PRIVS WHERE GRANTEE = :username";
            OracleParameter[] p = { new OracleParameter("username", username.ToUpper()) };
            return _db.ExecuteQuery(sql, p);
        }

        public DataTable GetUserTabPrivs(string username)
        {
            string sql = "SELECT OWNER, TABLE_NAME, PRIVILEGE, GRANTABLE FROM DBA_TAB_PRIVS WHERE GRANTEE = :username";
            OracleParameter[] p = { new OracleParameter("username", username.ToUpper()) };
            return _db.ExecuteQuery(sql, p);
        }

        public DataTable GetUserRolePrivs(string username)
        {
            string sql = "SELECT GRANTED_ROLE, ADMIN_OPTION, DEFAULT_ROLE FROM DBA_ROLE_PRIVS WHERE GRANTEE = :username";
            OracleParameter[] p = { new OracleParameter("username", username.ToUpper()) };
            return _db.ExecuteQuery(sql, p);
        }

        /// <summary>
        /// Lấy danh sách bảng/view mà user có thể cấp quyền (SELECT/UPDATE)
        /// </summary>
        public DataTable GetObjectsForGrant()
        {
            string sql = "SELECT OBJECT_NAME, OBJECT_TYPE FROM ALL_OBJECTS WHERE OBJECT_TYPE IN ('TABLE', 'VIEW') AND OWNER = :currentUser";
            OracleParameter[] p = { new OracleParameter("currentUser", _db.GetCurrentUser().ToUpper()) };
            return _db.ExecuteQuery(sql, p);
        }

        // --- Requirement 9: Extended DBA methods ---

        // Lấy danh sách USER
        public DataTable GetUsersDetailed()
        {
            string sql = @"SELECT USERNAME, ACCOUNT_STATUS, CREATED 
               FROM DBA_USERS 
               WHERE ORACLE_MAINTAINED = 'N'
               ORDER BY USERNAME";

            return _db.ExecuteQuery(sql);
        }

        // Lấy danh sách ROLE
        public DataTable GetRolesDetailed()
        {
            string sql = @"SELECT ROLE
                   FROM DBA_ROLES
                   WHERE ORACLE_MAINTAINED = 'N'
                   ORDER BY ROLE";

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
