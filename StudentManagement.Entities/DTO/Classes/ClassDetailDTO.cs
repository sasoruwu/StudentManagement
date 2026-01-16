using StudentManagement.Entities.DTOs.Students; // Cần namespace này
using System.Collections.Generic;

namespace StudentManagement.Entities.DTOs.Classes
{
    public class ClassDetailDto
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string FacultyName { get; set; }

        // Danh sách sinh viên thuộc lớp này (để hiển thị trong Grid chi tiết)
        public List<StudentViewDto> Students { get; set; }

        public ClassDetailDto()
        {
            Students = new List<StudentViewDto>();
        }
    }
}