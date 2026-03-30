using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task RevokePrivilegeAsync(string privilege, string tableName, string grantee, string type)
        {
            OracleConnection conn = _db.GetConnection();

            if (conn == null || conn.State != ConnectionState.Open)
            {
                throw new Exception("Kết nối cơ sở dữ liệu đã đóng hoặc chưa được khởi tạo.");
            }

            using (OracleCommand cmd = new OracleCommand("sp_RevokePrivilege", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("p_privilege", OracleDbType.Varchar2).Value = privilege;
                cmd.Parameters.Add("p_table_name", OracleDbType.Varchar2).Value = (object)tableName ?? DBNull.Value;
                cmd.Parameters.Add("p_grantee", OracleDbType.Varchar2).Value = grantee;
                cmd.Parameters.Add("p_type", OracleDbType.Varchar2).Value = type;

                try
                {

                    await cmd.ExecuteNonQueryAsync();
                }
                catch (OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        // --- Requirement 5: View Privileges ---

        public DataTable GetPrivileges(string principalName)
        {
            DataTable dt = new DataTable();
            
            OracleConnection conn = _db.GetConnection();
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
            return dt;
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
        public DataTable GetObjectsByGrantee(string grantee)
        {
            grantee = (grantee ?? "").Trim().ToUpper();
            string sql = @"
        SELECT DISTINCT OWNER, TABLE_NAME 
        FROM DBA_TAB_PRIVS 
        WHERE GRANTEE = :grantee1
           OR GRANTEE IN (SELECT GRANTED_ROLE FROM DBA_ROLE_PRIVS WHERE GRANTEE = :grantee2)
        
        UNION
        
        -- Thêm: lấy object có column-level privilege (UPDATE(col))
        SELECT DISTINCT OWNER, TABLE_NAME 
        FROM DBA_COL_PRIVS 
        WHERE GRANTEE = :grantee3
           OR GRANTEE IN (SELECT GRANTED_ROLE FROM DBA_ROLE_PRIVS WHERE GRANTEE = :grantee4)
        
        ORDER BY OWNER, TABLE_NAME";

            OracleParameter[] p = {
        new OracleParameter("grantee1", grantee),
        new OracleParameter("grantee2", grantee),
        new OracleParameter("grantee3", grantee),
        new OracleParameter("grantee4", grantee)
    };
            return _db.ExecuteQuery(sql, p);
        }

        public DataTable GetPrivilegesByObject(string grantee, string owner, string tableName)
        {
            grantee = (grantee ?? "").Trim().ToUpper();
            owner = (owner ?? "").Trim().ToUpper();
            tableName = (tableName ?? "").Trim().ToUpper();

            string sql = @"
        SELECT DISTINCT PRIVILEGE
        FROM DBA_TAB_PRIVS 
        WHERE (GRANTEE = :grantee1
           OR GRANTEE IN (SELECT GRANTED_ROLE FROM DBA_ROLE_PRIVS WHERE GRANTEE = :grantee2))
          AND OWNER = :owner1
          AND TABLE_NAME = :tableName1
        
        UNION
        
        -- Thêm: lấy column-level privilege, gộp lại thành 1 dòng 'UPDATE (col1, col2)'
        SELECT DISTINCT PRIVILEGE || ' (' || 
               LISTAGG(COLUMN_NAME, ', ') WITHIN GROUP (ORDER BY COLUMN_NAME) || ')'
        FROM DBA_COL_PRIVS
        WHERE (GRANTEE = :grantee3
           OR GRANTEE IN (SELECT GRANTED_ROLE FROM DBA_ROLE_PRIVS WHERE GRANTEE = :grantee4))
          AND OWNER = :owner2
          AND TABLE_NAME = :tableName2
        GROUP BY PRIVILEGE
        
        ORDER BY 1";

            OracleParameter[] p = {
        new OracleParameter("grantee1",   grantee),
        new OracleParameter("grantee2",   grantee),
        new OracleParameter("owner1",     owner),
        new OracleParameter("tableName1", tableName),
        new OracleParameter("grantee3",   grantee),
        new OracleParameter("grantee4",   grantee),
        new OracleParameter("owner2",     owner),
        new OracleParameter("tableName2", tableName)
    };
            return _db.ExecuteQuery(sql, p);
        }
    }
}
        
