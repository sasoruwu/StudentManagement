using StudentManagement.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.DAL.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        // Lấy danh sách sinh viên kèm theo thông tin Lớp (Eager Loading)
        // Nếu dùng Generic thường sẽ không load được tên lớp
        Task<IEnumerable<Student>> GetStudentsWithClassAsync();

        // Tìm kiếm sinh viên theo tên hoặc mã
        Task<IEnumerable<Student>> SearchStudentsAsync(string keyword);

        // Lấy sinh viên theo lớp
        Task<IEnumerable<Student>> GetStudentsByClassIdAsync(int classId);
    }
}