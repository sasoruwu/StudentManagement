using System.ComponentModel;

namespace StudentManagement.Entities.DTOs.Classes
{
    public class ClassViewDto
    {
        [DisplayName("ID")]
        public int ClassId { get; set; }

        [DisplayName("Tên Lớp")]
        public string ClassName { get; set; }

        [DisplayName("Khoa")]
        public string FacultyName { get; set; }

        [DisplayName("Sĩ số")]
        public int TotalStudents { get; set; } // Thuộc tính này sẽ được tính toán (Count) từ BLL
    }
}