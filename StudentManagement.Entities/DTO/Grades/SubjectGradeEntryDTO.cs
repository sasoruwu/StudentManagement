using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Entities.DTOs.Grades
{
    public class SubjectGradeEntryDto
    {
        // Thông tin định danh (Read-only trên UI)
        [Browsable(false)]
        public int StudentId { get; set; }

        [DisplayName("Mã SV")]
        [ReadOnly(true)] // Không cho sửa trên Grid
        public string StudentIdDisplay => StudentId.ToString(); // Giả lập mã SV nếu không có cột Code riêng

        [DisplayName("Họ và tên")]
        [ReadOnly(true)]
        public string StudentName { get; set; }

        // Điểm hiện tại trong DB (để tham chiếu)
        [DisplayName("Điểm cũ")]
        [ReadOnly(true)]
        public double? OldScore { get; set; }

        // Điểm mới nhập vào (Cho phép sửa trên Grid)
        [DisplayName("Điểm mới")]
        [Range(0, 10, ErrorMessage = "Điểm từ 0-10")]
        public double? NewScore { get; set; }

        // Cờ đánh dấu xem dòng này có bị thay đổi không
        // Giúp BLL chỉ update những dòng có thay đổi, tối ưu hiệu năng
        [Browsable(false)]
        public bool IsChanged { get; set; } = false;
    }
}