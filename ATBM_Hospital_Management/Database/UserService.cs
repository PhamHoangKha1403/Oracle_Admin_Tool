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
                return _db.ExecuteQuery("sp_NV_Select_NHANVIEN", new[] { pOut }, CommandType.StoredProcedure);
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
                    new OracleParameter("p_que_quan", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(queQuan) ? DBNull.Value : (object)queQuan },
                    new OracleParameter("p_sdt", OracleDbType.Varchar2) { Value = string.IsNullOrEmpty(sdt) ? DBNull.Value : (object)sdt }
                };
                _db.ExecuteNonQuery("sp_NV_Update_NHANVIEN", p, CommandType.StoredProcedure);
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
                return _db.ExecuteQuery("sp_BN_Select_BENHNHAN", new[] { pOut }, CommandType.StoredProcedure);
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
                    new OracleParameter("p_so_nha", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(soNha) ? DBNull.Value : (object)soNha },
                    new OracleParameter("p_ten_duong", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(tenDuong) ? DBNull.Value : (object)tenDuong },
                    new OracleParameter("p_quan_huyen", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(quanHuyen) ? DBNull.Value : (object)quanHuyen },
                    new OracleParameter("p_tinh_tp", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(tinhTp) ? DBNull.Value : (object)tinhTp },
                    new OracleParameter("p_tien_su_benh", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(tienSuBenh) ? DBNull.Value : (object)tienSuBenh },
                    new OracleParameter("p_tien_su_benh_gd", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(tienSuBenhGd) ? DBNull.Value : (object)tienSuBenhGd },
                    new OracleParameter("p_di_ung_thuoc", OracleDbType.NVarchar2) { Value = string.IsNullOrEmpty(diUngThuoc) ? DBNull.Value : (object)diUngThuoc }
                };
                _db.ExecuteNonQuery("sp_BN_Update_BENHNHAN", p, CommandType.StoredProcedure);
            }
            catch (OracleException ex)
            {
                throw new Exception("Error updating Patient profile: " + ex.Message, ex);
            }
        }
    }
}
