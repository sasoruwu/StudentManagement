namespace StudentManagement.UI.Forms
{
    partial class frmReports
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControlReport = new System.Windows.Forms.TabControl();
            this.tabClassStats = new System.Windows.Forms.TabPage();
            this.chartClassScore = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvClassStats = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLoadStats = new System.Windows.Forms.Button();
            this.tabStudentReport = new System.Windows.Forms.TabPage();
            this.dgvTranscript = new System.Windows.Forms.DataGridView();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblRank = new System.Windows.Forms.Label();
            this.lblGPA = new System.Windows.Forms.Label();
            this.lblStudentName = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnSearchStudent = new System.Windows.Forms.Button();
            this.txtStudentId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlReport.SuspendLayout();
            this.tabClassStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartClassScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassStats)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabStudentReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTranscript)).BeginInit();
            this.panelInfo.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlReport
            // 
            this.tabControlReport.Controls.Add(this.tabClassStats);
            this.tabControlReport.Controls.Add(this.tabStudentReport);
            this.tabControlReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlReport.Location = new System.Drawing.Point(0, 0);
            this.tabControlReport.Name = "tabControlReport";
            this.tabControlReport.SelectedIndex = 0;
            this.tabControlReport.Size = new System.Drawing.Size(900, 550);
            this.tabControlReport.TabIndex = 0;
            // 
            // tabClassStats
            // 
            this.tabClassStats.Controls.Add(this.chartClassScore);
            this.tabClassStats.Controls.Add(this.dgvClassStats);
            this.tabClassStats.Controls.Add(this.panel1);
            this.tabClassStats.Location = new System.Drawing.Point(4, 24);
            this.tabClassStats.Name = "tabClassStats";
            this.tabClassStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabClassStats.Size = new System.Drawing.Size(892, 522);
            this.tabClassStats.TabIndex = 0;
            this.tabClassStats.Text = "Thống kê Lớp học";
            this.tabClassStats.UseVisualStyleBackColor = true;
            // 
            // chartClassScore
            // 
            chartArea1.Name = "ChartArea1";
            this.chartClassScore.ChartAreas.Add(chartArea1);
            this.chartClassScore.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartClassScore.Legends.Add(legend1);
            this.chartClassScore.Location = new System.Drawing.Point(3, 203);
            this.chartClassScore.Name = "chartClassScore";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Điểm TB";
            this.chartClassScore.Series.Add(series1);
            this.chartClassScore.Size = new System.Drawing.Size(886, 316);
            this.chartClassScore.TabIndex = 2;
            this.chartClassScore.Text = "chart1";
            // 
            // dgvClassStats
            // 
            this.dgvClassStats.AllowUserToAddRows = false;
            this.dgvClassStats.AllowUserToDeleteRows = false;
            this.dgvClassStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClassStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClassStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvClassStats.Location = new System.Drawing.Point(3, 53);
            this.dgvClassStats.Name = "dgvClassStats";
            this.dgvClassStats.ReadOnly = true;
            this.dgvClassStats.RowHeadersVisible = false;
            this.dgvClassStats.RowTemplate.Height = 25;
            this.dgvClassStats.Size = new System.Drawing.Size(886, 150);
            this.dgvClassStats.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLoadStats);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(886, 50);
            this.panel1.TabIndex = 0;
            // 
            // btnLoadStats
            // 
            this.btnLoadStats.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLoadStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadStats.ForeColor = System.Drawing.Color.White;
            this.btnLoadStats.Location = new System.Drawing.Point(15, 10);
            this.btnLoadStats.Name = "btnLoadStats";
            this.btnLoadStats.Size = new System.Drawing.Size(120, 30);
            this.btnLoadStats.TabIndex = 0;
            this.btnLoadStats.Text = "Tải dữ liệu thống kê";
            this.btnLoadStats.UseVisualStyleBackColor = false;
            this.btnLoadStats.Click += new System.EventHandler(this.btnLoadStats_Click);
            // 
            // tabStudentReport
            // 
            this.tabStudentReport.Controls.Add(this.dgvTranscript);
            this.tabStudentReport.Controls.Add(this.panelInfo);
            this.tabStudentReport.Controls.Add(this.panelSearch);
            this.tabStudentReport.Location = new System.Drawing.Point(4, 24);
            this.tabStudentReport.Name = "tabStudentReport";
            this.tabStudentReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabStudentReport.Size = new System.Drawing.Size(892, 522);
            this.tabStudentReport.TabIndex = 1;
            this.tabStudentReport.Text = "Kết quả học tập cá nhân";
            this.tabStudentReport.UseVisualStyleBackColor = true;
            // 
            // dgvTranscript
            // 
            this.dgvTranscript.AllowUserToAddRows = false;
            this.dgvTranscript.AllowUserToDeleteRows = false;
            this.dgvTranscript.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTranscript.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvTranscript.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTranscript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTranscript.Location = new System.Drawing.Point(3, 143);
            this.dgvTranscript.Name = "dgvTranscript";
            this.dgvTranscript.ReadOnly = true;
            this.dgvTranscript.RowHeadersVisible = false;
            this.dgvTranscript.RowTemplate.Height = 25;
            this.dgvTranscript.Size = new System.Drawing.Size(886, 376);
            this.dgvTranscript.TabIndex = 2;
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelInfo.Controls.Add(this.lblRank);
            this.panelInfo.Controls.Add(this.lblGPA);
            this.panelInfo.Controls.Add(this.lblStudentName);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfo.Location = new System.Drawing.Point(3, 53);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(886, 90);
            this.panelInfo.TabIndex = 1;
            // 
            // lblRank
            // 
            this.lblRank.AutoSize = true;
            this.lblRank.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRank.ForeColor = System.Drawing.Color.DarkRed;
            this.lblRank.Location = new System.Drawing.Point(20, 55);
            this.lblRank.Name = "lblRank";
            this.lblRank.Size = new System.Drawing.Size(73, 20);
            this.lblRank.TabIndex = 2;
            this.lblRank.Text = "Xếp loại: ";
            // 
            // lblGPA
            // 
            this.lblGPA.AutoSize = true;
            this.lblGPA.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblGPA.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblGPA.Location = new System.Drawing.Point(20, 30);
            this.lblGPA.Name = "lblGPA";
            this.lblGPA.Size = new System.Drawing.Size(126, 20);
            this.lblGPA.TabIndex = 1;
            this.lblGPA.Text = "Điểm tích lũy: ...";
            // 
            // lblStudentName
            // 
            this.lblStudentName.AutoSize = true;
            this.lblStudentName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStudentName.Location = new System.Drawing.Point(20, 5);
            this.lblStudentName.Name = "lblStudentName";
            this.lblStudentName.Size = new System.Drawing.Size(161, 21);
            this.lblStudentName.TabIndex = 0;
            this.lblStudentName.Text = "Sinh viên: (Chưa có)";
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.btnSearchStudent);
            this.panelSearch.Controls.Add(this.txtStudentId);
            this.panelSearch.Controls.Add(this.label1);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(3, 3);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(886, 50);
            this.panelSearch.TabIndex = 0;
            // 
            // btnSearchStudent
            // 
            this.btnSearchStudent.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSearchStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchStudent.ForeColor = System.Drawing.Color.White;
            this.btnSearchStudent.Location = new System.Drawing.Point(250, 10);
            this.btnSearchStudent.Name = "btnSearchStudent";
            this.btnSearchStudent.Size = new System.Drawing.Size(100, 27);
            this.btnSearchStudent.TabIndex = 2;
            this.btnSearchStudent.Text = "Xem kết quả";
            this.btnSearchStudent.UseVisualStyleBackColor = false;
            this.btnSearchStudent.Click += new System.EventHandler(this.btnSearchStudent_Click);
            // 
            // txtStudentId
            // 
            this.txtStudentId.Location = new System.Drawing.Point(100, 12);
            this.txtStudentId.Name = "txtStudentId";
            this.txtStudentId.PlaceholderText = "Nhập ID sinh viên";
            this.txtStudentId.Size = new System.Drawing.Size(140, 23);
            this.txtStudentId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã sinh viên:";
            // 
            // frmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.tabControlReport);
            this.Name = "frmReports";
            this.Text = "Báo cáo thống kê";
            this.Load += new System.EventHandler(this.frmReports_Load);
            this.tabControlReport.ResumeLayout(false);
            this.tabClassStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartClassScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassStats)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabStudentReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTranscript)).EndInit();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlReport;
        private System.Windows.Forms.TabPage tabClassStats;
        private System.Windows.Forms.TabPage tabStudentReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLoadStats;
        private System.Windows.Forms.DataGridView dgvClassStats;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartClassScore;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Button btnSearchStudent;
        private System.Windows.Forms.TextBox txtStudentId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblRank;
        private System.Windows.Forms.Label lblGPA;
        private System.Windows.Forms.Label lblStudentName;
        private System.Windows.Forms.DataGridView dgvTranscript;
    }
}