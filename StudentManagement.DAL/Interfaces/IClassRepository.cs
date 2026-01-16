using StudentManagement.Entities.Models;
using System.Threading.Tasks;

namespace StudentManagement.DAL.Interfaces
{
    public interface IClassRepository : IGenericRepository<Classroom>
    {
        // Kiểm tra xem lớp học có sinh viên không (trước khi xóa)
        Task<bool> HasStudentsAsync(int classId);

        // Lấy thông tin lớp kèm danh sách sinh viên bên trong (cho màn hình chi tiết)
        Task<Classroom> GetClassWithStudentsAsync(int classId);
    }
}