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
        public const string DB_HOST = "localhost";
        public const string DB_PORT = "1522";
        public const string DB_SID  = "XEPDB1";    // PDB name của Oracle XE
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
                throw new Exception("Oracle connection error: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Mở kết nối Oracle với thông tin host/port/serviceName tùy chỉnh.
        /// </summary>
        public void OpenConnection(string username, string password, string host, string port, string serviceName)
        {
            try
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                    _connection.Close();

                string connStr = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME={serviceName})));User Id={username};Password={password};";
                _connection = new OracleConnection(connStr);
                _connection.Open();
                _currentUser = username;
            }
            catch (OracleException ex)
            {
                throw new Exception("Oracle connection error: " + ex.Message, ex);
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
        /// Phân tích role hiện tại từ SESSION_ROLES.
        /// Trả về "DBA", "RL_BACSI", "RL_BENHNHAN", v.v.
        /// </summary>
        public string GetCurrentUserRole()
        {
            try
            {
                // Check if DBA
                object dbaCheck = ExecuteScalar("SELECT COUNT(*) FROM SESSION_ROLES WHERE ROLE = 'DBA'");
                if (dbaCheck != null && Convert.ToInt32(dbaCheck) > 0) return "DBA";

                // Otherwise, get the custom hospital role (like RL_BACSI, RL_BENHNHAN, ...)
                object role = ExecuteScalar("SELECT ROLE FROM SESSION_ROLES WHERE ROLE LIKE 'RL_%' AND ROWNUM = 1");
                if (role != null && role != DBNull.Value) return role.ToString();
                
                return "UNKNOWN";
            }
            catch
            {
                return "ERROR";
            }
        }

        /// <summary>
        /// Chạy câu SELECT, trả về DataTable.
        /// </summary>
        public DataTable ExecuteQuery(string sql, OracleParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            DataTable dt = new DataTable();
            using (OracleCommand cmd = new OracleCommand(sql, _connection))
            {
                cmd.BindByName = true;
                // Thêm dòng này để C# biết đang gọi SQL thường hay Stored Procedure
                cmd.CommandType = commandType;

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
        public int ExecuteNonQuery(string sql, OracleParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            using (OracleCommand cmd = new OracleCommand(sql, _connection))
            {
                cmd.BindByName = true;
                // Thêm dòng này để Oracle biết đang chạy Raw SQL hay Stored Procedure
                cmd.CommandType = commandType;

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
                cmd.BindByName = true;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Chạy Stored Procedure có RefCursor output (cho các query trả về cursor).
        /// </summary>
        public DataTable ExecuteRefCursorQuery(string spName, OracleParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (OracleCommand cmd = new OracleCommand(spName, _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                try
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error executing stored procedure '{spName}': {ex.Message}", ex);
                }
            }
            return dt;
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
