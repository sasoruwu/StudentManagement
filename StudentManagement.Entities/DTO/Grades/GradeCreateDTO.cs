using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Entities.DTOs.Grades
{
    public class GradeCreateDto
    {
        [Required(ErrorMessage = "Vui lòng chọn sinh viên")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn môn học")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Điểm số không được để trống")]
        [Range(0, 10, ErrorMessage = "Điểm số phải nằm trong khoảng từ 0 đến 10")]
        public double Score { get; set; }
    }
}