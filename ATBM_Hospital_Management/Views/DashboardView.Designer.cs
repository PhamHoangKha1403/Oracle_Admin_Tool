namespace ATBM_Hospital_Management.Views
{
    partial class DashboardView
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tlpCards = new System.Windows.Forms.TableLayoutPanel();
            this.cardSession = new System.Windows.Forms.Panel();
            this.tlpSession = new System.Windows.Forms.TableLayoutPanel();
            this.lblSessionHeader = new System.Windows.Forms.Label();
            this.lblSessionUser = new System.Windows.Forms.Label();
            this.lblDbName = new System.Windows.Forms.Label();
            this.lblVersionBanner = new System.Windows.Forms.Label();
            this.cardStats = new System.Windows.Forms.Panel();
            this.tlpStats = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatsHeader = new System.Windows.Forms.Label();
            this.lblTotalUsers = new System.Windows.Forms.Label();
            this.lblTotalRoles = new System.Windows.Forms.Label();
            this.cardServerTime = new System.Windows.Forms.Panel();
            this.tlpServerTime = new System.Windows.Forms.TableLayoutPanel();
            this.lblServerTimeHeader = new System.Windows.Forms.Label();
            this.lblServerTime = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tlpCards.SuspendLayout();
            this.cardSession.SuspendLayout();
            this.tlpSession.SuspendLayout();
            this.cardStats.SuspendLayout();
            this.tlpStats.SuspendLayout();
            this.cardServerTime.SuspendLayout();
            this.tlpServerTime.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Controls.Add(this.lblPageTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 56;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 0, 16, 0);
            this.pnlHeader.TabIndex = 0;

            this.lblPageTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblPageTitle.Location = new System.Drawing.Point(20, 16);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Text = "Overview";
            this.lblPageTitle.TabIndex = 0;

            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.Location = new System.Drawing.Point(700, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(84, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // lblError
            this.lblError.AutoSize = true;
            this.lblError.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(180, 0, 0);
            this.lblError.Name = "lblError";
            this.lblError.Padding = new System.Windows.Forms.Padding(20, 6, 0, 0);
            this.lblError.TabIndex = 2;
            this.lblError.Text = "";
            this.lblError.Visible = false;

            // pnlContent
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlContent.Controls.Add(this.tlpCards);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.pnlContent.TabIndex = 1;

            // tlpCards - 3 equal columns
            this.tlpCards.ColumnCount = 3;
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tlpCards.Controls.Add(this.cardSession, 0, 0);
            this.tlpCards.Controls.Add(this.cardStats, 1, 0);
            this.tlpCards.Controls.Add(this.cardServerTime, 2, 0);
            this.tlpCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpCards.Height = 240;
            this.tlpCards.Name = "tlpCards";
            this.tlpCards.RowCount = 1;
            this.tlpCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCards.TabIndex = 0;

            System.Drawing.Font cardHeaderFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            System.Drawing.Font cardBodyFont = new System.Drawing.Font("Segoe UI", 9.5F);
            System.Drawing.Color cardHeaderColor = System.Drawing.Color.FromArgb(30, 30, 30);
            System.Drawing.Color cardBodyColor = System.Drawing.Color.FromArgb(80, 80, 80);

            // cardSession
            this.cardSession.BackColor = System.Drawing.Color.White;
            this.cardSession.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardSession.Controls.Add(this.tlpSession);
            this.cardSession.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardSession.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cardSession.Name = "cardSession";
            this.cardSession.Padding = new System.Windows.Forms.Padding(16, 14, 16, 14);
            this.cardSession.TabIndex = 0;

            this.tlpSession.ColumnCount = 1;
            this.tlpSession.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSession.Controls.Add(this.lblSessionHeader, 0, 0);
            this.tlpSession.Controls.Add(this.lblSessionUser, 0, 1);
            this.tlpSession.Controls.Add(this.lblDbName, 0, 2);
            this.tlpSession.Controls.Add(this.lblVersionBanner, 0, 3);
            this.tlpSession.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSession.Name = "tlpSession";
            this.tlpSession.RowCount = 4;
            this.tlpSession.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpSession.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpSession.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpSession.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSession.TabIndex = 0;

            this.lblSessionHeader.AutoSize = true;
            this.lblSessionHeader.Font = cardHeaderFont;
            this.lblSessionHeader.ForeColor = cardHeaderColor;
            this.lblSessionHeader.Name = "lblSessionHeader";
            this.lblSessionHeader.TabIndex = 0;
            this.lblSessionHeader.Text = "Session Info";

            this.lblSessionUser.AutoSize = true;
            this.lblSessionUser.Font = cardBodyFont;
            this.lblSessionUser.ForeColor = cardBodyColor;
            this.lblSessionUser.Name = "lblSessionUser";
            this.lblSessionUser.TabIndex = 1;
            this.lblSessionUser.Text = "User: -";

            this.lblDbName.AutoSize = true;
            this.lblDbName.Font = cardBodyFont;
            this.lblDbName.ForeColor = cardBodyColor;
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.TabIndex = 2;
            this.lblDbName.Text = "Database: -";

            this.lblVersionBanner.AutoSize = true;
            this.lblVersionBanner.Font = cardBodyFont;
            this.lblVersionBanner.ForeColor = cardBodyColor;
            this.lblVersionBanner.Name = "lblVersionBanner";
            this.lblVersionBanner.TabIndex = 3;
            this.lblVersionBanner.Text = "Version: -";

            // cardStats
            this.cardStats.BackColor = System.Drawing.Color.White;
            this.cardStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardStats.Controls.Add(this.tlpStats);
            this.cardStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardStats.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.cardStats.Name = "cardStats";
            this.cardStats.Padding = new System.Windows.Forms.Padding(16, 14, 16, 14);
            this.cardStats.TabIndex = 1;

            this.tlpStats.ColumnCount = 1;
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStats.Controls.Add(this.lblStatsHeader, 0, 0);
            this.tlpStats.Controls.Add(this.lblTotalUsers, 0, 1);
            this.tlpStats.Controls.Add(this.lblTotalRoles, 0, 2);
            this.tlpStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStats.Name = "tlpStats";
            this.tlpStats.RowCount = 3;
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStats.TabIndex = 0;

            this.lblStatsHeader.AutoSize = true;
            this.lblStatsHeader.Font = cardHeaderFont;
            this.lblStatsHeader.ForeColor = cardHeaderColor;
            this.lblStatsHeader.Name = "lblStatsHeader";
            this.lblStatsHeader.TabIndex = 0;
            this.lblStatsHeader.Text = "Statistics";

            this.lblTotalUsers.AutoSize = true;
            this.lblTotalUsers.Font = cardBodyFont;
            this.lblTotalUsers.ForeColor = cardBodyColor;
            this.lblTotalUsers.Name = "lblTotalUsers";
            this.lblTotalUsers.TabIndex = 1;
            this.lblTotalUsers.Text = "Total users: -";

            this.lblTotalRoles.AutoSize = true;
            this.lblTotalRoles.Font = cardBodyFont;
            this.lblTotalRoles.ForeColor = cardBodyColor;
            this.lblTotalRoles.Name = "lblTotalRoles";
            this.lblTotalRoles.TabIndex = 2;
            this.lblTotalRoles.Text = "Total roles: -";

            // cardServerTime
            this.cardServerTime.BackColor = System.Drawing.Color.White;
            this.cardServerTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardServerTime.Controls.Add(this.tlpServerTime);
            this.cardServerTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardServerTime.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.cardServerTime.Name = "cardServerTime";
            this.cardServerTime.Padding = new System.Windows.Forms.Padding(16, 14, 16, 14);
            this.cardServerTime.TabIndex = 2;

            this.tlpServerTime.ColumnCount = 1;
            this.tlpServerTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpServerTime.Controls.Add(this.lblServerTimeHeader, 0, 0);
            this.tlpServerTime.Controls.Add(this.lblServerTime, 0, 1);
            this.tlpServerTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpServerTime.Name = "tlpServerTime";
            this.tlpServerTime.RowCount = 2;
            this.tlpServerTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpServerTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpServerTime.TabIndex = 0;

            this.lblServerTimeHeader.AutoSize = true;
            this.lblServerTimeHeader.Font = cardHeaderFont;
            this.lblServerTimeHeader.ForeColor = cardHeaderColor;
            this.lblServerTimeHeader.Name = "lblServerTimeHeader";
            this.lblServerTimeHeader.TabIndex = 0;
            this.lblServerTimeHeader.Text = "Server Time";

            this.lblServerTime.AutoSize = true;
            this.lblServerTime.Font = cardBodyFont;
            this.lblServerTime.ForeColor = cardBodyColor;
            this.lblServerTime.Name = "lblServerTime";
            this.lblServerTime.TabIndex = 1;
            this.lblServerTime.Text = "Time: -";

            // DashboardView
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.pnlHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "DashboardView";
            this.Size = new System.Drawing.Size(800, 600);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.tlpCards.ResumeLayout(false);
            this.cardSession.ResumeLayout(false);
            this.tlpSession.ResumeLayout(false);
            this.tlpSession.PerformLayout();
            this.cardStats.ResumeLayout(false);
            this.tlpStats.ResumeLayout(false);
            this.tlpStats.PerformLayout();
            this.cardServerTime.ResumeLayout(false);
            this.tlpServerTime.ResumeLayout(false);
            this.tlpServerTime.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TableLayoutPanel tlpCards;
        private System.Windows.Forms.Panel cardSession;
        private System.Windows.Forms.TableLayoutPanel tlpSession;
        private System.Windows.Forms.Label lblSessionHeader;
        private System.Windows.Forms.Label lblSessionUser;
        private System.Windows.Forms.Label lblDbName;
        private System.Windows.Forms.Label lblVersionBanner;
        private System.Windows.Forms.Panel cardStats;
        private System.Windows.Forms.TableLayoutPanel tlpStats;
        private System.Windows.Forms.Label lblStatsHeader;
        private System.Windows.Forms.Label lblTotalUsers;
        private System.Windows.Forms.Label lblTotalRoles;
        private System.Windows.Forms.Panel cardServerTime;
        private System.Windows.Forms.TableLayoutPanel tlpServerTime;
        private System.Windows.Forms.Label lblServerTimeHeader;
        private System.Windows.Forms.Label lblServerTime;
    }
}
