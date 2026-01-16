using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Entities.DTOs.Students
{
    public class StudentUpdateDto
    {
        [Required]
        public int StudentId { get; set; } // Khóa chính (Hidden trên UI hoặc Readonly)

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên không quá 100 ký tự")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool Gender { get; set; }

        [StringLength(200, ErrorMessage = "Địa chỉ không quá 200 ký tự")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
        [StringLength(15, ErrorMessage = "Số điện thoại quá dài")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Lớp học")]
        public int ClassId { get; set; }
    }
}