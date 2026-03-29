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
            string spName = "sp_ViewUsers";
            OracleParameter[] p = {
                new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            return _db.ExecuteQuery(spName, p, CommandType.StoredProcedure);
        }

        public DataTable GetRoles()
        {
            string spName = "sp_ViewRoles";
            OracleParameter[] p = {
                new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output)
            };
            return _db.ExecuteQuery(spName, p, CommandType.StoredProcedure);
        }

        public void CreateUser(string username, string password, string roleName = null)
        {
            string spName = "sp_CreateUser";

            object dbRole = string.IsNullOrWhiteSpace(roleName) ? (object)DBNull.Value : roleName;

            OracleParameter[] p = {
                new OracleParameter("p_username", username),
                new OracleParameter("p_password", password),
                new OracleParameter("p_role", dbRole)
            };

                _db.ExecuteNonQuery(spName, p, CommandType.StoredProcedure);
        }

        public void DropUser(string username)
        {
            string spName = "sp_DropUser";
            OracleParameter[] p = {
                new OracleParameter("p_username", username)
            };
            _db.ExecuteNonQuery(spName, p, CommandType.StoredProcedure);
        }

        public void ChangeUserPassword(string username, string newPassword)
        {
            string spName = "sp_ChangeUserPassword";
            OracleParameter[] p = {
                new OracleParameter("p_username", username),
                new OracleParameter("p_new_password", newPassword)
            };
            _db.ExecuteNonQuery(spName, p, CommandType.StoredProcedure);
        }

        public void CreateRole(string roleName)
        {
            string spName = "sp_CreateRole";
            OracleParameter[] p = {
                new OracleParameter("p_role_name", roleName)
            };
            _db.ExecuteNonQuery(spName, p, CommandType.StoredProcedure);
        }

        public void DropRole(string roleName)
        {
            string spName = "sp_DropRole";
            OracleParameter[] p = {
                new OracleParameter("p_role_name", roleName)
            };
            _db.ExecuteNonQuery(spName, p, CommandType.StoredProcedure);
        }

        public void ChangeRolePassword(string roleName, string newPassword)
        {
            string spName = "sp_ChangeRolePassword";
            object dbPassword = string.IsNullOrWhiteSpace(newPassword) ? (object)DBNull.Value : newPassword;

            OracleParameter[] p = {
                new OracleParameter("p_role_name", roleName),
                new OracleParameter("p_new_password", dbPassword)
            };

            _db.ExecuteNonQuery(spName, p, CommandType.StoredProcedure);
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
