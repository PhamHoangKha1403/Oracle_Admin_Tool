using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views.Components
{
    public partial class DPV_EditHSBADVForm : Form
    {
        private DataGridViewRow _row;

        public DPV_EditHSBADVForm(DataGridViewRow row)
        {
            InitializeComponent();
            _row = row;
        }

        private void DPV_EditHSBADVForm_Load(object sender, EventArgs e)
        {
            txtMaHSBA.Text = _row.Cells["MA_HSBA"].Value?.ToString();
            txtLoaiDV.Text = _row.Cells["LOAI_DV"].Value?.ToString();

            DateTime dtNgayDV;
            if (DateTime.TryParse(_row.Cells["NGAY_DV"].Value?.ToString(), out dtNgayDV) && dtNgayDV > DateTime.MinValue)
                dtpNgayDV.Value = dtNgayDV;
            else
                dtpNgayDV.Value = DateTime.Now;

            txtMaKTV.Text = _row.Cells["MA_KTV"].Value?.ToString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var parameters = new OracleParameter[]
                {
                    new OracleParameter("p_MAHSBA", txtMaHSBA.Text),
                    new OracleParameter("p_LOAIDV", txtLoaiDV.Text),
                    new OracleParameter("p_NGAYDV", dtpNgayDV.Value),
                    new OracleParameter("p_MAKTV", txtMaKTV.Text)
                };

                DbConnection.Instance.ExecuteNonQuery("BEGIN sp_DPV_Update_HSBADV(:p_MAHSBA, :p_LOAIDV, :p_NGAYDV, :p_MAKTV); END;", parameters, CommandType.Text);
                MessageBox.Show("Cập nhật dịch vụ thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
// 
