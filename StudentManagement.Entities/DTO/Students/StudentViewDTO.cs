using System;
using System.ComponentModel;

namespace StudentManagement.Entities.DTOs.Students
{
    public class StudentViewDto
    {
        [DisplayName("Mã SV")]
        public int StudentId { get; set; }

        [DisplayName("Họ và Tên")]
        public string FullName { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        // Ẩn cột bool gốc đi nếu không muốn hiện checkbox
        [Browsable(false)]
        public bool Gender { get; set; }

        // Tạo property phụ để hiện chữ "Nam/Nữ" trên Grid
        [DisplayName("Giới tính")]
        public string GenderDisplay => Gender ? "Nam" : "Nữ";

        [DisplayName("Lớp")]
        public string ClassName { get; set; }

        [DisplayName("Khoa")]
        public string FacultyName { get; set; }

        [DisplayName("SĐT")]
        public string PhoneNumber { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
    }
}