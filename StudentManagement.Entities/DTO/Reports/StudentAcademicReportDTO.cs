using System.ComponentModel;

namespace StudentManagement.Entities.DTOs.Reports
{
    public class StudentAcademicReportDto
    {
        [DisplayName("Mã SV")]
        public int StudentId { get; set; }

        [DisplayName("Họ và Tên")]
        public string FullName { get; set; }

        [DisplayName("Lớp")]
        public string ClassName { get; set; }

        [DisplayName("Số môn đã học")]
        public int TotalSubjects { get; set; }

        [DisplayName("Tổng tín chỉ")]
        public int TotalCredits { get; set; }

        [DisplayName("Điểm TB Tích lũy (GPA)")]
        public double GPA { get; set; }

        // Tự động xếp loại dựa trên GPA
        [DisplayName("Xếp loại học lực")]
        public string AcademicRank
        {
            get
            {
                if (GPA >= 9.0) return "Xuất sắc";
                if (GPA >= 8.0) return "Giỏi";
                if (GPA >= 6.5) return "Khá";
                if (GPA >= 5.0) return "Trung bình";
                return "Yếu";
            }
        }
    }
}