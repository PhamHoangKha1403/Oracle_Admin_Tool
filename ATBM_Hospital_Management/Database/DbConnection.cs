using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace ATBM_Hospital_Management.Database
{
    /// <summary>
    /// Class kết nối Oracle Database.
    /// 
    /// === HƯỚNG DẪN CHO CÁC THÀNH VIÊN ===
    /// 
    /// 1. Cài Oracle Database (hoặc dùng chung server của nhóm).
    /// 2. Mở file này, sửa 3 hằng số bên dưới cho đúng server:
    ///       DB_HOST   = IP hoặc hostname (ví dụ: "localhost" hoặc "192.168.1.100")
    ///       DB_PORT   = port Oracle listener (mặc định 1521)
    ///       DB_SID    = Service Name / SID (ví dụ: "ORCL", "XE", "ORCLPDB")
    /// 3. NuGet đã có Oracle.ManagedDataAccess (xem packages.config).
    ///    Nếu thiếu: Tools → NuGet Package Manager → Install "Oracle.ManagedDataAccess".
    /// 4. Dùng class này để kết nối:
    /// 
    ///       var conn = DbConnection.Instance;
    ///       conn.OpenConnection("NV003", "NV003");   // đăng nhập bằng Oracle user
    ///       DataTable dt = conn.ExecuteQuery("SELECT * FROM NHANVIEN");
    ///       conn.CloseConnection();
    /// 
    /// ========================================
    /// </summary>
    public class DbConnection
    {
        // ====== CẤU HÌNH - SỬA CHO ĐÚNG SERVER CỦA NHÓM ======
        private const string DB_HOST = "localhost";
        private const string DB_PORT = "1521";
        private const string DB_SID  = "XEPDB1";    // PDB name của Oracle XE
        // =======================================================

        private OracleConnection _connection;
        private string _currentUser;

        // Singleton
        private static DbConnection _instance;
        public static DbConnection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DbConnection();
                return _instance;
            }
        }

        private DbConnection() { }

        /// <summary>
        /// Tạo connection string từ username/password.
        /// </summary>
        private string BuildConnectionString(string username, string password)
        {
            return $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={DB_HOST})(PORT={DB_PORT}))(CONNECT_DATA=(SERVICE_NAME={DB_SID})));User Id={username};Password={password};";
        }

        /// <summary>
        /// Mở kết nối Oracle bằng tài khoản user.
        /// Oracle tự xác thực – không cần bảng tài khoản riêng.
        /// </summary>
        public void OpenConnection(string username, string password)
        {
            try
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                    _connection.Close();

                _connection = new OracleConnection(BuildConnectionString(username, password));
                _connection.Open();
                _currentUser = username;
            }
            catch (OracleException ex)
            {
                throw new Exception("Lỗi kết nối Oracle: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Lấy connection hiện tại (đã mở).
        /// </summary>
        public OracleConnection GetConnection()
        {
            return _connection;
        }

        /// <summary>
        /// Trả về username đang đăng nhập.
        /// </summary>
        public string GetCurrentUser()
        {
            return _currentUser;
        }

        /// <summary>
        /// Chạy câu SELECT, trả về DataTable.
        /// </summary>
        public DataTable ExecuteQuery(string sql, OracleParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (OracleCommand cmd = new OracleCommand(sql, _connection))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Chạy INSERT / UPDATE / DELETE / DDL, trả về số dòng ảnh hưởng.
        /// </summary>
        public int ExecuteNonQuery(string sql, OracleParameter[] parameters = null)
        {
            using (OracleCommand cmd = new OracleCommand(sql, _connection))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Chạy câu truy vấn trả về 1 giá trị (ví dụ: COUNT, MAX, ...).
        /// </summary>
        public object ExecuteScalar(string sql, OracleParameter[] parameters = null)
        {
            using (OracleCommand cmd = new OracleCommand(sql, _connection))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Đóng kết nối.
        /// </summary>
        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
                _currentUser = null;
            }
        }
    }
}
