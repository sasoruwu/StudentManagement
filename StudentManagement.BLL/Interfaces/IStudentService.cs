using StudentManagement.Entities.DTOs.Common;
using StudentManagement.Entities.DTOs.Students;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.BLL.Interfaces
{
    public interface IStudentService
    {
        // Lấy danh sách hiển thị lên Grid (Join sẵn tên lớp)
        Task<List<StudentViewDto>> GetAllStudentsAsync();

        // Tìm kiếm sinh viên
        Task<List<StudentViewDto>> SearchStudentsAsync(string keyword);

        // Lấy thông tin chi tiết để fill vào Form Sửa
        Task<StudentUpdateDto> GetStudentForEditAsync(int id);

        // Thêm mới (trả về ID hoặc lỗi)
        Task<ServiceResult<int>> AddStudentAsync(StudentCreateDto dto);

        // Cập nhật
        Task<ServiceResult<bool>> UpdateStudentAsync(StudentUpdateDto dto);

        // Xóa
        Task<ServiceResult<bool>> DeleteStudentAsync(int id);
    }
}