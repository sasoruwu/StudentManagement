using StudentManagement.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.DAL.Interfaces
{
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        // Lấy bảng điểm của một sinh viên cụ thể (kèm thông tin Môn học)
        Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId);

        // Lấy danh sách điểm của một Lớp theo một Môn (Dùng cho màn hình nhập điểm hàng loạt)
        Task<IEnumerable<Grade>> GetGradesByClassAndSubjectAsync(int classId, int subjectId);

        // Tìm điểm số cụ thể của 1 SV trong 1 Môn
        Task<Grade> GetGradeByStudentAndSubjectAsync(int studentId, int subjectId);
    }
}