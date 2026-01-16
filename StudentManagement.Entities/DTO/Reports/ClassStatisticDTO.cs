using System.ComponentModel;

namespace StudentManagement.Entities.DTOs.Reports
{
    public class ClassStatisticDto
    {
        [DisplayName("Tên Lớp")]
        public string ClassName { get; set; }

        [DisplayName("Khoa")]
        public string FacultyName { get; set; }

        [DisplayName("Tổng số SV")]
        public int TotalStudents { get; set; }

        [DisplayName("Nam")]
        public int MaleCount { get; set; }

        [DisplayName("Nữ")]
        public int FemaleCount { get; set; }

        [DisplayName("Điểm TB Lớp")]
        public double AverageClassScore { get; set; }

        [DisplayName("Xếp loại Lớp")]
        public string Classification
        {
            get
            {
                if (AverageClassScore >= 8.0) return "Lớp Tiên tiến";
                if (AverageClassScore >= 6.5) return "Lớp Khá";
                return "Lớp Trung bình";
            }
        }
    }
}