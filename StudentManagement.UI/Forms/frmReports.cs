using StudentManagement.BLL.Interfaces;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Cần namespace này cho biểu đồ

namespace StudentManagement.UI.Forms
{
    public partial class frmReports : Form
    {
        private readonly IReportingService _reportingService;
        private readonly IGradeService _gradeService;

        // Constructor Injection
        public frmReports(IReportingService reportingService, IGradeService gradeService)
        {
            InitializeComponent();
            _reportingService = reportingService;
            _gradeService = gradeService;
        }

        private async void frmReports_Load(object sender, EventArgs e)
        {
            // Tự động tải thống kê lớp khi mở form
            await LoadClassStatisticsAsync();
        }

        // --- TAB 1: THỐNG KÊ LỚP HỌC ---

        private async void btnLoadStats_Click(object sender, EventArgs e)
        {
            await LoadClassStatisticsAsync();
        }

        private async Task LoadClassStatisticsAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // 1. Lấy dữ liệu từ BLL
                var stats = await _reportingService.GetClassStatisticsAsync();

                // 2. Hiển thị lên Grid
                dgvClassStats.DataSource = stats;

                // 3. Vẽ biểu đồ (Chart) - So sánh điểm TB các lớp
                SetupChart(stats);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SetupChart(System.Collections.Generic.List<Entities.DTOs.Reports.ClassStatisticDto> stats)
        {
            chartClassScore.Series.Clear();
            chartClassScore.Titles.Clear();

            // Thêm tiêu đồ
            chartClassScore.Titles.Add("Biểu đồ so sánh điểm trung bình giữa các lớp");

            // Tạo Series mới (dạng cột)
            Series series = new Series("Điểm TB");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true; // Hiện số trên cột

            foreach (var item in stats)
            {
                // Thêm DataPoint: X = Tên lớp, Y = Điểm TB
                DataPoint point = new DataPoint();
                point.AxisLabel = item.ClassName;
                point.YValues = new double[] { item.AverageClassScore };

                // Tô màu cột dựa trên điểm (Ví dụ: > 8 màu xanh, < 5 màu đỏ)
                if (item.AverageClassScore >= 8.0) point.Color = Color.SeaGreen;
                else if (item.AverageClassScore < 5.0) point.Color = Color.IndianRed;
                else point.Color = Color.SteelBlue;

                series.Points.Add(point);
            }

            chartClassScore.Series.Add(series);
            chartClassScore.ChartAreas[0].AxisX.Interval = 1; // Hiện đủ tên lớp
            chartClassScore.ChartAreas[0].AxisY.Maximum = 10; // Thang điểm 10
        }

        // --- TAB 2: KẾT QUẢ HỌC TẬP CÁ NHÂN ---

        private async void btnSearchStudent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                MessageBox.Show("Vui lòng nhập ID sinh viên.");
                return;
            }

            if (!int.TryParse(txtStudentId.Text, out int studentId))
            {
                MessageBox.Show("ID sinh viên phải là số.");
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                // 1. Lấy thông tin tổng hợp (GPA, Xếp loại)
                var report = await _reportingService.GetStudentAcademicReportAsync(studentId);

                if (report == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên hoặc sinh viên chưa có điểm.");
                    ClearStudentInfo();
                    return;
                }

                // Hiển thị thông tin
                lblStudentName.Text = $"Sinh viên: {report.FullName} - Lớp: {report.ClassName}";
                lblGPA.Text = $"Điểm tích lũy (GPA): {report.GPA} / 10";
                lblRank.Text = $"Xếp loại học lực: {report.AcademicRank}";

                // 2. Lấy bảng điểm chi tiết (Transcript) từ GradeService
                var transcript = await _gradeService.GetTranscriptAsync(studentId);
                dgvTranscript.DataSource = transcript;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tra cứu: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ClearStudentInfo()
        {
            lblStudentName.Text = "Sinh viên: (Chưa có)";
            lblGPA.Text = "Điểm tích lũy: ...";
            lblRank.Text = "Xếp loại: ...";
            dgvTranscript.DataSource = null;
        }
    }
}