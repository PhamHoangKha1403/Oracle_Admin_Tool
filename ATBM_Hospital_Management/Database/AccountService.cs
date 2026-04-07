using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace ATBM_Hospital_Management.Database
{
    /// <summary>
    /// Service to create Oracle accounts for employees/patients.
    /// Calls sp_dba_create_user and sp_dba_createall_user stored procedures.
    /// Password is computed entirely inside Oracle SP — never passed through C#.
    /// </summary>
    public class AccountService
    {
        private readonly DbConnection _db;

        public AccountService()
        {
            _db = DbConnection.Instance;
        }

        // ── Helpers ───────────────────────────────────────────────────────

        /// <summary>
        /// Returns the schema owner of NHAN_VIEN table.
        /// Searches ALL_TABLES so it works regardless of which user is logged in.
        /// </summary>
        private string GetTableOwner()
        {
            try
            {
                string sql = "SELECT OWNER FROM ALL_TABLES WHERE TABLE_NAME = 'NHANVIEN' AND ROWNUM = 1";
                object result = _db.ExecuteScalar(sql);
                if (result != null && result != DBNull.Value)
                    return result.ToString();
            }
            catch { }
            // Fallback: current user owns the table
            return DbConnection.Instance.GetCurrentUser().ToUpper();
        }

        private string GetErrorMessage(OracleException ex)
        {
            if (ex.Number == 20001) return "ID cannot be empty.";
            if (ex.Number == 20002) return "ID does not exist in NHANVIEN or BENHNHAN.";
            return "Oracle connection error: " + ex.Message;
        }

        // ── Public API ────────────────────────────────────────────────────

        /// <summary>
        /// Creates an Oracle account for a single employee or patient by ID.
        /// Default password: ID + "23127@"
        /// </summary>
        public void CreateAccountForEmployee(string maNv)
        {
            try
            {
                OracleParameter[] p = {
                    new OracleParameter("p_id", OracleDbType.Varchar2) { Value = maNv }
                };
                _db.ExecuteNonQuery("sp_dba_create_user", p, CommandType.StoredProcedure);
            }
            catch (OracleException ex)
            {
                throw new Exception(GetErrorMessage(ex), ex);
            }
        }

        /// <summary>
        /// Creates Oracle accounts for all users in NHANVIEN and BENHNHAN.
        /// Returns the number of new accounts created.
        /// </summary>
        public int CreateAllAccounts()
        {
            try
            {
                OracleParameter pOut = new OracleParameter("p_so_luong_tao", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                _db.ExecuteNonQuery("sp_dba_createall_user", new[] { pOut }, CommandType.StoredProcedure);
                
                if (pOut.Value != null && pOut.Value != DBNull.Value)
                {
                    return int.Parse(pOut.Value.ToString());
                }
                return 0;
            }
            catch (OracleException ex)
            {
                throw new Exception("Error creating all accounts: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Returns users in NHANVIEN and BENHNHAN that do not yet have an Oracle account.
        /// Uses ALL_TABLES to find the correct schema owner automatically.
        /// </summary>
        public DataTable GetEmployeesWithoutAccount()
        {
            string owner = GetTableOwner();
            string sql = $@"SELECT MANV as ID, HOTEN as ""FULL_NAME"", VAITRO as ""ROLE"", CHUYENKHOA as ""DEPT""
                            FROM ""{owner}"".NHANVIEN
                            WHERE NOT EXISTS (
                                SELECT 1 FROM DBA_USERS du
                                WHERE du.USERNAME = UPPER(MANV)
                            )
                            UNION ALL
                            SELECT MABN as ID, TENBN as ""FULL_NAME"", N'Bệnh nhân' as ""ROLE"", CAST(NULL AS NVARCHAR2(50)) as ""DEPT""
                            FROM ""{owner}"".BENHNHAN
                            WHERE NOT EXISTS (
                                SELECT 1 FROM DBA_USERS du
                                WHERE du.USERNAME = UPPER(MABN)
                            )
                            ORDER BY ""ROLE"", ID";
            return _db.ExecuteQuery(sql);
        }

        /// <summary>
        /// Returns all users in NHANVIEN and BENHNHAN.
        /// </summary>
        public DataTable GetAllEmployees()
        {
            string owner = GetTableOwner();
            string sql = $@"SELECT MANV as ID, HOTEN as ""FULL_NAME"", VAITRO as ""ROLE"", CHUYENKHOA as ""DEPT""
                            FROM ""{owner}"".NHANVIEN
                            UNION ALL
                            SELECT MABN as ID, TENBN as ""FULL_NAME"", N'Bệnh nhân' as ""ROLE"", CAST(NULL AS NVARCHAR2(50)) as ""DEPT""
                            FROM ""{owner}"".BENHNHAN
                            ORDER BY ""ROLE"", ID";
            return _db.ExecuteQuery(sql);
        }

        /// <summary>
        /// Returns true if the current session has SELECT ANY DICTIONARY privilege (DBA-level).
        /// </summary>
        public bool IsDbaUser()
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM SESSION_PRIVS WHERE PRIVILEGE = 'SELECT ANY DICTIONARY'";
                object result = _db.ExecuteScalar(sql);
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
