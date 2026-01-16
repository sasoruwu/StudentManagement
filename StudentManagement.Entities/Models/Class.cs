using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Entities.Models
{
    [Table("Classrooms")] // Đặt tên bảng trong SQL là Classrooms
    public class Classroom
    {
        // Constructor để khởi tạo list, tránh lỗi NullReference khi add
        public Classroom()
        {
            Students = new HashSet<Student>();
        }

        [Key] // Khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự tăng
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Tên lớp không được để trống")]
        [StringLength(20)] // Varchar(20)
        public string ClassName { get; set; }

        [StringLength(50)]
        public string FacultyName { get; set; } // Tên khoa

        // Navigation Property: Một lớp có nhiều sinh viên
        public virtual ICollection<Student> Students { get; set; }
    }
}