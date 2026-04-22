using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace ATBM_Hospital_Management.Database
{
    /// <summary>
    /// Service to manage Employee and Patient profiles using logged-in Session.
    /// Calls Phase 2 stored procedures.
    /// </summary>
    public class UserService
    {
        private readonly DbConnection _db;

        public UserService()
        {
            _db = DbConnection.Instance;
        }

        // ── Public API ────────────────────────────────────────────────────

        /// <summary>
        /// Gets the currently logged-in Employee's profile.
        /// Throws if user is not an employee.
        /// </summary>
        public DataTable GetMyEmployeeProfile()
        {
            try
            {
                OracleParameter pOut = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                return _db.ExecuteQuery("BEGIN sp_NV_Select_NHANVIEN(:p_cursor); END;", new[] { pOut }, CommandType.Text);
            }
            catch (OracleException ex)
            {
                throw new Exception("Error retrieving Employee profile: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Updates the currently logged-in Employee's profile.
        /// </summary>
        public void UpdateMyEmployeeProfile(string queQuan, string sdt)
        {
            try
            {
                OracleParameter[] p = {
                    new OracleParameter("p_QUEQUAN", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(queQuan) ? DBNull.Value : (object)queQuan },
                    new OracleParameter("p_SODT", OracleDbType.Varchar2) { Value = string.IsNullOrEmpty(sdt) ? DBNull.Value : (object)sdt }
                };
                _db.ExecuteNonQuery("BEGIN sp_NV_Update_NHANVIEN(:p_QUEQUAN, :p_SODT); END;", p, CommandType.Text);
            }
            catch (OracleException ex)
            {
                throw new Exception("Error updating Employee profile: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the currently logged-in Patient's detailed profile.
        /// Throws if user is not a patient.
        /// </summary>
        public DataTable GetMyPatientProfile()
        {
            try
            {
                OracleParameter pOut = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };
                return _db.ExecuteQuery("BEGIN sp_BN_Select_BENHNHAN(:p_cursor); END;", new[] { pOut }, CommandType.Text);
            }
            catch (OracleException ex)
            {
                throw new Exception("Error retrieving Patient profile: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Updates the currently logged-in Patient's detailed profile.
        /// </summary>
        public void UpdateMyPatientProfile(string soNha, string tenDuong, string quanHuyen, string tinhTp, string tienSuBenh, string tienSuBenhGd, string diUngThuoc)
        {
            try
            {
                OracleParameter[] p = {
                    new OracleParameter("p_SONHA", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(soNha) ? DBNull.Value : (object)soNha },
                    new OracleParameter("p_TENDUONG", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(tenDuong) ? DBNull.Value : (object)tenDuong },
                    new OracleParameter("p_QUANHUYEN", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(quanHuyen) ? DBNull.Value : (object)quanHuyen },
                    new OracleParameter("p_TINHTP", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(tinhTp) ? DBNull.Value : (object)tinhTp },
                    new OracleParameter("p_TIENSUBENH", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(tienSuBenh) ? DBNull.Value : (object)tienSuBenh },
                    new OracleParameter("p_TIENSUBENHGD", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(tienSuBenhGd) ? DBNull.Value : (object)tienSuBenhGd },
                    new OracleParameter("p_DIUNGTHUOC", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(diUngThuoc) ? DBNull.Value : (object)diUngThuoc }
                };
                _db.ExecuteNonQuery("BEGIN sp_BN_Update_BENHNHAN(:p_SONHA, :p_TENDUONG, :p_QUANHUYEN, :p_TINHTP, :p_TIENSUBENH, :p_TIENSUBENHGD, :p_DIUNGTHUOC); END;", p, CommandType.Text);
            }
            catch (OracleException ex)
            {
                throw new Exception("Error updating Patient profile: " + ex.Message, ex);
            }
        }
    }
}
