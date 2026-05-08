using System;
using System.Data;
using System.Windows.Forms;
using ATBM_Hospital_Management.Database;

namespace ATBM_Hospital_Management.Views
{
    public partial class SendNotificationView : UserControl
    {
        private readonly DbaService _dbaService;

        public SendNotificationView()
        {
            InitializeComponent();
            _dbaService = new DbaService();
            LoadSecurityLabels();
        }

        private void LoadSecurityLabels()
        {
            try
            {
                DataTable labels = _dbaService.GetOLSLabels();
                cmbSecurityLabel.DataSource = labels;
                cmbSecurityLabel.DisplayMember = "LABEL_NAME";
                cmbSecurityLabel.ValueMember = "LABEL_NAME";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading security labels: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                ShowPopup("Validation Error", "Notification ID is required", NotificationPopup.PopupType.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtContent.Text))
            {
                ShowPopup("Validation Error", "Content is required", NotificationPopup.PopupType.Error);
                return;
            }

            if (cmbSecurityLabel.SelectedItem == null)
            {
                ShowPopup("Validation Error", "Security Label (Send to) is required", NotificationPopup.PopupType.Error);
                return;
            }

            try
            {
                string notificationId = txtId.Text.Trim();
                string content = txtContent.Text.Trim();
                string location = txtLocation.Text.Trim();
                string securityLabel = cmbSecurityLabel.SelectedValue?.ToString() ?? "NV";

                _dbaService.AddNotification(notificationId, content, location, securityLabel);

                ShowPopup("Notification Sent Successfully", 
                    $"Notification '{notificationId}' sent to: {securityLabel}",
                    NotificationPopup.PopupType.Success);
                
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowPopup("Error", ex.Message, NotificationPopup.PopupType.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ShowPopup(string title, string message, NotificationPopup.PopupType type)
        {
            using (NotificationPopup popup = new NotificationPopup(title, message, type))
            {
                popup.ShowDialog(this);
            }
        }

        private void ClearForm()
        {
            txtId.Clear();
            txtContent.Clear();
            txtLocation.Clear();
            cmbSecurityLabel.SelectedIndex = 0;
            txtId.Focus();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
