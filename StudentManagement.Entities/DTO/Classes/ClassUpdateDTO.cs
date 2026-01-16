using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Entities.DTOs.Classes
{
    public class ClassUpdateDto
    {
        [Required]
        public int ClassId { get; set; } // Khóa chính để định danh bản ghi cần sửa

        [Required(ErrorMessage = "Tên lớp không được để trống")]
        [StringLength(20, ErrorMessage = "Tên lớp không quá 20 ký tự")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Tên khoa không được để trống")]
        [StringLength(50, ErrorMessage = "Tên khoa không quá 50 ký tự")]
        public string FacultyName { get; set; }
    }
}