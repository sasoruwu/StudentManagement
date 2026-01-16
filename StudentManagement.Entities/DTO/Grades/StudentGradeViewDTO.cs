using System.ComponentModel;

namespace StudentManagement.Entities.DTOs.Grades
{
    public class StudentGradeViewDto
    {
        [Browsable(false)] // Ẩn cột này trên DataGridView
        public int SubjectId { get; set; }

        [DisplayName("Môn học")]
        public string SubjectName { get; set; }

        [DisplayName("Số tín chỉ")]
        public int Credits { get; set; }

        [DisplayName("Điểm số (hệ 10)")]
        public double Score { get; set; }

        // Property chỉ đọc: Tự động quy đổi điểm chữ
        [DisplayName("Điểm chữ")]
        public string LetterGrade
        {
            get
            {
                if (Score >= 8.5) return "A";
                if (Score >= 7.0) return "B";
                if (Score >= 5.5) return "C";
                if (Score >= 4.0) return "D";
                return "F";
            }
        }

        // Property chỉ đọc: Trạng thái qua môn
        [DisplayName("Trạng thái")]
        public string Status => Score >= 4.0 ? "Đạt" : "Học lại";
    }
}