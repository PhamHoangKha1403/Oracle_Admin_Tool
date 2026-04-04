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
                string sql = "SELECT OWNER FROM ALL_TABLES WHERE TABLE_NAME = 'NHAN_VIEN' AND ROWNUM = 1";
                object result = _db.ExecuteScalar(sql);
                if (result != null && result != DBNull.Value)
                    return result.ToString();
            }
            catch { }
            // Fallback: current user owns the table
            return DbConnection.Instance.GetCurrentUser().ToUpper();
        }

        private string GetVietnameseMessage(OracleException ex)
        {
            if (ex.Number == 20001) return "Employee ID cannot be empty.";
            if (ex.Number == 20002) return "Employee ID does not exist in NHAN_VIEN.";
            return "Oracle error: " + ex.Message;
        }

        // ── Public API ────────────────────────────────────────────────────

        /// <summary>
        /// Creates an Oracle account for a single employee by MA_NV.
        /// Default password: MA_NV + "23127@"
        /// </summary>
        public void CreateAccountForEmployee(string maNv)
        {
            try
            {
                OracleParameter[] p = {
                    new OracleParameter("p_ma_nv", OracleDbType.Varchar2) { Value = maNv }
                };
                _db.ExecuteNonQuery("sp_dba_create_user", p, CommandType.StoredProcedure);
            }
            catch (OracleException ex)
            {
                throw new Exception(GetVietnameseMessage(ex), ex);
            }
        }

        /// <summary>
        /// Creates Oracle accounts for all employees in NHAN_VIEN.
        /// Returns the number of new accounts created.
        /// </summary>
        public int CreateAllAccounts()
        {
            try
            {
                OracleParameter pOut = new OracleParameter("p_so_luong_tao", OracleDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                _db.ExecuteNonQuery("sp_dba_createall_user", new[] { pOut }, CommandType.StoredProcedure);
                return pOut.Value != DBNull.Value ? Convert.ToInt32(pOut.Value) : 0;
            }
            catch (OracleException ex)
            {
                throw new Exception("Error creating all accounts: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Returns employees in NHAN_VIEN that do not yet have an Oracle account.
        /// Uses ALL_TABLES to find the correct schema owner automatically.
        /// </summary>
        public DataTable GetEmployeesWithoutAccount()
        {
            string owner = GetTableOwner();
            string sql = $@"SELECT nv.MA_NV, nv.HO_TEN, nv.VAI_TRO, nv.CHUYEN_KHOA
                            FROM ""{owner}"".NHAN_VIEN nv
                            WHERE NOT EXISTS (
                                SELECT 1 FROM DBA_USERS du
                                WHERE du.USERNAME = UPPER(nv.MA_NV)
                            )
                            ORDER BY nv.VAI_TRO, nv.MA_NV";
            return _db.ExecuteQuery(sql);
        }

        /// <summary>
        /// Returns all employees in NHAN_VIEN.
        /// </summary>
        public DataTable GetAllEmployees()
        {
            string owner = GetTableOwner();
            string sql = $@"SELECT MA_NV, HO_TEN, VAI_TRO, CHUYEN_KHOA
                            FROM ""{owner}"".NHAN_VIEN
                            ORDER BY VAI_TRO, MA_NV";
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
