using System.ComponentModel;

namespace StudentManagement.Entities.DTOs.Reports
{
    public class SubjectAnalysisDto
    {
        [DisplayName("Môn học")]
        public string SubjectName { get; set; }

        [DisplayName("Số SV dự thi")]
        public int TotalExaminees { get; set; }

        [DisplayName("Điểm cao nhất")]
        public double HighestScore { get; set; }

        [DisplayName("Điểm thấp nhất")]
        public double LowestScore { get; set; }

        [DisplayName("Điểm trung bình")]
        public double AverageScore { get; set; }

        [DisplayName("Số lượng Đạt")]
        public int PassedCount { get; set; }

        [DisplayName("Tỷ lệ Đạt (%)")]
        public double PassRate
        {
            get
            {
                if (TotalExaminees == 0) return 0;
                // Làm tròn 2 chữ số thập phân
                return Math.Round(((double)PassedCount / TotalExaminees) * 100, 2);
            }
        }
    }
}