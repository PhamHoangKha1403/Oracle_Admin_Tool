using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace ATBM_Hospital_Management.Database
{
    /// <summary>
    /// Service quản lý nghiệp vụ của Kỹ thuật viên (KTV).
    /// Sử dụng Session đang đăng nhập thông qua DbConnection.Instance.
    /// </summary>
    public class TechnicianService
    {
        private readonly DbConnection _db;

        public TechnicianService()
        {
            _db = DbConnection.Instance;
        }

        // ── Public API ────────────────────────────────────────────────────

        /// <summary>
        /// Lấy danh sách hồ sơ dịch vụ và bệnh nhân liên quan mà KTV hiện tại được phân công.
        /// </summary>
        public DataTable GetServiceRecords()
        {
            try
            {
                OracleParameter pOut = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                };

                // Lưu ý: Dùng tiền tố ADMIN_PH2 do trong script CSDL chưa tạo SYNONYM cho SP của KTV
                return _db.ExecuteQuery("BEGIN ADMIN_PH2.sp_KTV_Select_HSBADV(:p_cursor); END;", new[] { pOut }, CommandType.Text);
            }
            catch (OracleException ex)
            {
                throw new Exception("Lỗi khi lấy danh sách hồ sơ dịch vụ: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Cập nhật kết quả vào HSBA_DV.
        /// </summary>
        public void UpdateServiceResult(string maHSBA, string loaiDV, DateTime ngayDV, string ketQua)
        {
            try
            {
                OracleParameter[] p = {
                    new OracleParameter("p_MA_HSBA", OracleDbType.Varchar2) { Value = maHSBA },
                    new OracleParameter("p_LOAI_DV", OracleDbType.Varchar2) { Value = loaiDV },
                    new OracleParameter("p_NGAY_DV", OracleDbType.Date) { Value = ngayDV },
                    // Xử lý chuỗi rỗng chuyển thành DBNull để update vào Database
                    new OracleParameter("p_KET_QUA", OracleDbType.Varchar2) { Value = string.IsNullOrEmpty(ketQua) ? DBNull.Value : (object)ketQua }
                };

                _db.ExecuteNonQuery("BEGIN ADMIN_PH2.sp_KTV_Update_HSBADV(:p_MA_HSBA, :p_LOAI_DV, :p_NGAY_DV, :p_KET_QUA); END;", p, CommandType.Text);
            }
            catch (OracleException ex)
            {
                // Bắt mã lỗi 20002 được định nghĩa bằng RAISE_APPLICATION_ERROR trong procedure
                if (ex.Number == 20002)
                {
                    throw new Exception("Không tìm thấy dịch vụ hoặc bạn không được phân công thực hiện dịch vụ này.");
                }
                throw new Exception("Lỗi cập nhật kết quả dịch vụ: " + ex.Message, ex);
            }
        }
    }
}