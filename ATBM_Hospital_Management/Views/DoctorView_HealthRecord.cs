using ATBM_Hospital_Management.Database;
using ATBM_Hospital_Management.Views.Components;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ATBM_Hospital_Management.Views
{
    public partial class DoctorView_HealthRecord : UserControl // Đổi sang UserControl để kế thừa Nav Bar
    {
        private readonly DbConnection _db;
        private string _maHSBA;
        private string _maBN;
        private DoctorView_PatientList _parentView;
        private DataTable _dtDonThuoc;

        public DoctorView_HealthRecord(string maHSBA, string maBN, DoctorView_PatientList parent)
        {
            InitializeComponent();
            _db = DbConnection.Instance;
            _maHSBA = maHSBA;
            _maBN = maBN;
            _parentView = parent;

            this.Load += DoctorView_HealthRecord_Load;

            // ĐĂNG KÝ SỰ KIỆN CLICK CHO CÁC BẢNG (Bắt buộc phải có)
            dataGridView1.CellContentClick += dataGridView1_CellContentClick; // Cho Dịch vụ
            dataGridView2.CellContentClick += dataGridView2_CellContentClick; // Cho Đơn thuốc

            // Đăng ký các sự kiện khác
            button5.Click += button5_Click; // Nút Thêm thuốc
            button6.Click += button6_Click; // Nút Lưu thuốc
            button2.Click += button2_Click; // Nút Thêm dịch vụ
            button1.Click += button1_Click; // Nút Lưu HSBA

            // Xử lý nút Back (label10)
            label10.Cursor = Cursors.Hand;
            label10.Click += (s, e) => {
                // Quay lại trang Thông tin bệnh nhân
                var detailPage = new Components.DoctorView_PatientDetail(_maBN, _parentView);
                _parentView.ShowPage(detailPage);
            };
        }

        private void DoctorView_HealthRecord_Load(object sender, EventArgs e)
        {
            SetupDataGrids();
            LoadHSBADetail();
            LoadDonThuoc();
            LoadHSBADV();
        }


        private void ConfigureTable(DataGridView dgv)
        {
            // 1. Loại bỏ các phần thừa để khít bảng
            dgv.RowHeadersVisible = false;      // Bỏ cột trống bên trái
            dgv.AllowUserToAddRows = false;     // Bỏ dòng trống dưới cùng
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Lấp đầy khoảng xám bên phải

            // 2. Tùy chỉnh màu sắc cho giống mẫu
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.FixedSingle; // Hoặc None nếu muốn phẳng hẳn
            dgv.GridColor = Color.LightGray;    // Màu đường kẻ giữa các ô

            // 3. Header (Dòng tiêu đề)
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White; // Hoặc màu xanh nhạt theo mẫu
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // 4. Căn giữa nội dung cho đẹp
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // Hàm này dùng để tính toán lại chiều cao của Grid dựa trên số dòng
        private void AdjustGridHeight(DataGridView dgv, int maxHeight)
        {
            // Đảm bảo không tính dòng trống cuối cùng
            dgv.AllowUserToAddRows = false;

            // Tính tổng chiều cao dựa trên DisplayRectangle để chính xác hơn
            int height = dgv.ColumnHeadersHeight;

            // Nếu dùng DataSource, ta duyệt qua Rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Visible) height += row.Height;
            }

            // Cộng thêm bù đắp cho Border và các đường kẻ giữa các dòng (GridLines)
            // 2 là độ dày border trên/dưới, 1 là cho dòng tiêu đề
            int finalHeight = height + dgv.Padding.Top + dgv.Padding.Bottom + 3;

            if (finalHeight > maxHeight)
            {
                dgv.Height = maxHeight;
                dgv.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                dgv.Height = finalHeight;
                dgv.ScrollBars = ScrollBars.None;
            }

            // Ép DataGridView vẽ lại ngay lập tức
            dgv.Refresh();
        }
        private void SetupDataGrids()
        {
            // Cấu hình chung cho dataGridView2
            dataGridView2.AutoGenerateColumns = false; // TẮT TỰ ĐỘNG TẠO CỘT
            dataGridView2.Columns.Clear();
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.BackgroundColor = Color.White;

            // Cột 1: Tên thuốc
            DataGridViewTextBoxColumn colTen = new DataGridViewTextBoxColumn();
            colTen.DataPropertyName = "TEN_THUOC"; // Tên cột chính xác trong DB
            colTen.HeaderText = "Tên thuốc";
            colTen.Name = "TEN_THUOC";
            colTen.FillWeight = 45;
            dataGridView2.Columns.Add(colTen);

            // Cột 2: Liều dùng
            DataGridViewTextBoxColumn colLieu = new DataGridViewTextBoxColumn();
            colLieu.DataPropertyName = "LIEU_DUNG"; // Tên cột chính xác trong DB
            colLieu.HeaderText = "Liều dùng";
            colLieu.Name = "LIEU_DUNG";
            colLieu.FillWeight = 45;
            dataGridView2.Columns.Add(colLieu);

            // Cột 3: Nút Xóa (Thùng rác)
            DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
            btnDel.Name = "btnXoaThuoc";
            btnDel.HeaderText = "";
            btnDel.Text = "🗑";
            btnDel.UseColumnTextForButtonValue = true;
            btnDel.FillWeight = 10;
            btnDel.FlatStyle = FlatStyle.Flat;
            dataGridView2.Columns.Add(btnDel);
        }
        private void LoadHSBADetail()
        {
            try
            {
                OracleParameter p_ma_bn = new OracleParameter("p_ma_bn", OracleDbType.Varchar2) { Value = _maBN };
                OracleParameter p_cursor = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);
                DataTable dt = _db.ExecuteQuery("ADMIN_PH2.sp_BS_Select_HSBA_Theo_BN", new[] { p_ma_bn, p_cursor }, CommandType.StoredProcedure);

                DataRow[] rows = dt.Select($"MA_HSBA = '{_maHSBA}'");
                if (rows.Length > 0)
                {
                    textBox1.Text = rows[0]["MA_HSBA"].ToString();
                    textBox2.Text = rows[0]["MA_BN"].ToString();
                    textBox3.Text = rows[0]["NGAY"].ToString();
                    richTextBox1.Text = rows[0]["CHAN_DOAN"].ToString();
                    richTextBox2.Text = rows[0]["DIEU_TRI"].ToString();
                    richTextBox3.Text = rows[0]["KET_LUAN"].ToString();

                    // Khóa các trường ID
                    textBox1.ReadOnly = textBox2.ReadOnly = textBox3.ReadOnly = true;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi nạp HSBA: " + ex.Message); }
        }

        // --- PHẦN 1: CẬP NHẬT HSBA ---
        private void button1_Click(object sender, EventArgs e) // Nút Lưu HSBA
        {
            try
            {
                OracleParameter[] pars = {
                    new OracleParameter("p_ma_hsba", _maHSBA),
                    new OracleParameter("p_chan_doan", richTextBox1.Text),
                    new OracleParameter("p_dieu_tri", richTextBox2.Text),
                    new OracleParameter("p_ket_luan", richTextBox3.Text)
                };
                _db.ExecuteNonQuery("ADMIN_PH2.sp_BS_Update_HSBA", pars, CommandType.StoredProcedure);
                MessageBox.Show("Cập nhật HSBA thành công!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu HSBA: " + ex.Message); }
        }

        // --- PHẦN 2: ĐƠN THUỐC ---
        private void LoadDonThuoc()
        {
            try
            {
                OracleParameter p_ma_hsba = new OracleParameter("p_ma_hsba", _maHSBA);
                OracleParameter p_cursor = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);

                _dtDonThuoc = _db.ExecuteQuery("ADMIN_PH2.sp_BS_Select_DONTHUOC",
                                                new[] { p_ma_hsba, p_cursor },
                                                CommandType.StoredProcedure);

                // Đảm bảo AutoGenerateColumns là false trước khi gán DataSource
                dataGridView2.AutoGenerateColumns = false;
                dataGridView2.DataSource = _dtDonThuoc;

                // Gọi hàm tính chiều cao để bảng khít với dữ liệu
                AdjustGridHeight(dataGridView2, 300);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e) // Nút "Thêm thuốc"
        {
            // Tạo nhanh Dialog nhập liệu
            Form prompt = new Form() { Width = 400, Height = 250, Text = "Thêm thuốc vào đơn", StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog };
            Label lbl1 = new Label() { Left = 20, Top = 20, Text = "Tên thuốc:", Width = 100 };
            TextBox txtTen = new TextBox() { Left = 20, Top = 45, Width = 340 };
            Label lbl2 = new Label() { Left = 20, Top = 85, Text = "Liều dùng:", Width = 100 };
            TextBox txtLieu = new TextBox() { Left = 20, Top = 110, Width = 340 };
            Button btnConfirm = new Button() { Text = "Xác nhận", Left = 260, Width = 100, Top = 160, DialogResult = DialogResult.OK };

            prompt.Controls.AddRange(new Control[] { lbl1, txtTen, lbl2, txtLieu, btnConfirm });
            prompt.AcceptButton = btnConfirm;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(txtTen.Text) || string.IsNullOrWhiteSpace(txtLieu.Text)) return;

                try
                {
                    OracleParameter[] p = {
                new OracleParameter("p_MA_HSBA", _maHSBA),
                new OracleParameter("p_TENTHUOC", txtTen.Text.Trim()),
                new OracleParameter("p_LIEUDUNG", txtLieu.Text.Trim())
            };
                    _db.ExecuteNonQuery("ADMIN_PH2.sp_BS_Insert_DONTHUOC", p, CommandType.StoredProcedure);

                    LoadDonThuoc(); // Nạp lại bảng ngay lập tức
                    AdjustGridHeight(dataGridView2, 300);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi thêm: " + ex.Message); }
            }
        }

        private void button6_Click(object sender, EventArgs e) // Nút LƯU (Chỉ Update)
        {
            dataGridView2.EndEdit();
            int updateCount = 0;
            try
            {
                foreach (DataRow row in _dtDonThuoc.Rows)
                {
                    if (row.RowState == DataRowState.Modified)
                    {
                        OracleParameter[] p = {
                    new OracleParameter("p_MA_HSBA", _maHSBA),
                    new OracleParameter("p_NGAY_DT_CU", OracleDbType.Date) { Value = row["NGAY_DT", DataRowVersion.Original] },
                    new OracleParameter("p_TENTHUOC_CU", row["TEN_THUOC", DataRowVersion.Original].ToString()),
                    new OracleParameter("p_TENTHUOC_MOI", row["TEN_THUOC"].ToString()),
                    new OracleParameter("p_LIEUDUNG", row["LIEU_DUNG"].ToString())
                };
                        _db.ExecuteNonQuery("ADMIN_PH2.sp_BS_Update_DONTHUOC", p, CommandType.StoredProcedure);
                        updateCount++;
                    }
                }

                if (updateCount > 0)
                {
                    MessageBox.Show($"Đã cập nhật thành công {updateCount} thay đổi!");
                    LoadDonThuoc();
                }
                else
                {
                    MessageBox.Show("Không có thay đổi nào để lưu.");
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi cập nhật: " + ex.Message); }
        }

        // --- PHẦN 3: DỊCH VỤ (HSBA_DV) ---
        private void LoadHSBADV()
        {
            try
            {
                OracleParameter p_ma_hsba = new OracleParameter("p_ma_hsba", _maHSBA);
                OracleParameter p_cursor = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);

                DataTable dt = _db.ExecuteQuery("ADMIN_PH2.sp_BS_Select_HSBADV",
                                                 new[] { p_ma_hsba, p_cursor },
                                                 CommandType.StoredProcedure);

                dataGridView1.DataSource = dt;

                // Thiết lập các cột hiển thị (Nếu chưa làm trong Designer)
                if (dataGridView1.Columns.Count > 0 && !dataGridView1.Columns.Contains("btnXoaDV"))
                {
                    dataGridView1.Columns["LOAI_DV"].HeaderText = "Loại dịch vụ";
                    dataGridView1.Columns["NGAY_DV"].HeaderText = "Ngày thực hiện";
                    dataGridView1.Columns["KET_QUA"].HeaderText = "Kết quả";
                    dataGridView1.Columns["MA_KTV"].HeaderText = "Mã KTV";

                    // Thêm nút xóa cuối bảng
                    DataGridViewButtonColumn btnXoa = new DataGridViewButtonColumn
                    {
                        Name = "btnXoaDV",
                        HeaderText = "",
                        Text = "🗑",
                        UseColumnTextForButtonValue = true,
                        FillWeight = 15,
                        FlatStyle = FlatStyle.Flat
                    };
                    dataGridView1.Columns.Add(btnXoa);
                }

                // Fix lỗi hở bảng: Tự co giãn chiều cao theo dữ liệu
                AdjustGridHeight(dataGridView1, 250); // Giới hạn cao tối đa 250px
            }
            catch (Exception ex) { MessageBox.Show("Lỗi nạp dịch vụ: " + ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e) // Nút "Thêm dịch vụ"
        {
            // Tạo nhanh 1 Dialog nhập liệu
            Form prompt = new Form() { Width = 400, Height = 300, Text = "Thêm dịch vụ mới", StartPosition = FormStartPosition.CenterParent };
            Label lbl1 = new Label() { Left = 20, Top = 20, Text = "Loại dịch vụ:" };
            TextBox txtLoai = new TextBox() { Left = 20, Top = 50, Width = 340 };
            Label lbl2 = new Label() { Left = 20, Top = 90, Text = "Mã KTV:" };
            TextBox txtKTV = new TextBox() { Left = 20, Top = 120, Width = 340 };
            Button btnConfirm = new Button() { Text = "Xác nhận", Left = 260, Width = 100, Top = 180, DialogResult = DialogResult.OK };

            prompt.Controls.AddRange(new Control[] { lbl1, txtLoai, lbl2, txtKTV, btnConfirm });

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    OracleParameter[] p = {
                new OracleParameter("p_ma_hsba", _maHSBA),
                new OracleParameter("p_loai_dv", txtLoai.Text),
                new OracleParameter("p_ngay_dv", OracleDbType.Date) { Value = DateTime.Now },
                new OracleParameter("p_ma_ktv", txtKTV.Text),
                new OracleParameter("p_ket_qua", DBNull.Value)
            };
                    _db.ExecuteNonQuery("ADMIN_PH2.sp_BS_Insert_HSBADV", p, CommandType.StoredProcedure);
                    LoadHSBADV();
                    MessageBox.Show("Đã thêm dịch vụ!");
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu click vào hàng tiêu đề hoặc không phải nút Xóa thì thoát
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnXoaDV")
            {
                try
                {
                    // Lấy dữ liệu trực tiếp từ dòng hiện tại
                    var row = dataGridView1.Rows[e.RowIndex];
                    string loaiDV = row.Cells["LOAI_DV"].Value?.ToString();

                    // Xử lý ngày tháng cẩn thận để truyền vào Oracle
                    DateTime ngayDV;
                    if (!DateTime.TryParse(row.Cells["NGAY_DV"].Value?.ToString(), out ngayDV))
                    {
                        MessageBox.Show("Không thể xác định ngày dịch vụ để xóa.");
                        return;
                    }

                    DialogResult confirm = MessageBox.Show($"Xóa dịch vụ '{loaiDV}' này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        OracleParameter[] p = {
                    new OracleParameter("p_ma_hsba", OracleDbType.Varchar2) { Value = _maHSBA },
                    new OracleParameter("p_loai_dv", OracleDbType.Varchar2) { Value = loaiDV },
                    new OracleParameter("p_ngay_dv", OracleDbType.Date) { Value = ngayDV }
                };

                        _db.ExecuteNonQuery("ADMIN_PH2.sp_BS_Delete_HSBADV", p, CommandType.StoredProcedure);
                        MessageBox.Show("Đã xóa dịch vụ thành công!");

                        LoadHSBADV(); // Tải lại bảng và tính lại chiều cao
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dịch vụ: " + ex.Message);
                }
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Kiểm tra đúng tên cột nút Xóa
            if (dataGridView2.Columns[e.ColumnIndex].Name == "btnXoaThuoc")
            {
                var row = _dtDonThuoc.Rows[e.RowIndex];

                // Nếu dòng này đã tồn tại trong DB (không phải dòng mới thêm chưa lưu)
                if (row.RowState != DataRowState.Added)
                {
                    DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa thuốc này?", "Xác nhận", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            OracleParameter[] p = {
                        new OracleParameter("p_MA_HSBA", _maHSBA),
                        new OracleParameter("p_NGAY_DT", OracleDbType.Date) { Value = row["NGAY_DT"] },
                        new OracleParameter("p_TENTHUOC", row["TEN_THUOC"].ToString()),
                        new OracleParameter("p_LIEUDUNG", row["LIEU_DUNG"]?.ToString() ?? "NULL")
                    };
                            _db.ExecuteNonQuery("ADMIN_PH2.sp_BS_Delete_DONTHUOC", p, CommandType.StoredProcedure);
                            MessageBox.Show("Đã xóa thuốc!");
                            LoadDonThuoc();
                        }
                        catch (Exception ex) { MessageBox.Show("Lỗi xóa thuốc: " + ex.Message); }
                    }
                }
                else
                {
                    _dtDonThuoc.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
    }
}