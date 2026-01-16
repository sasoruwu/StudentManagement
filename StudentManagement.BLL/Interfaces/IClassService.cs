using StudentManagement.Entities.DTOs.Classes;
using StudentManagement.Entities.DTOs.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.BLL.Interfaces
{
    public interface IClassService
    {
        // Lấy danh sách lớp kèm sĩ số (đã tính toán)
        Task<List<ClassViewDto>> GetAllClassesAsync();

        // Lấy danh sách lớp cho ComboBox (chỉ cần ID và Name)
        Task<List<DropdownItem>> GetClassesForDropdownAsync();

        // Lấy chi tiết lớp kèm danh sách sinh viên bên trong
        Task<ClassDetailDto> GetClassDetailAsync(int classId);

        // CRUD
        Task<ServiceResult<int>> AddClassAsync(ClassCreateDto dto);
        Task<ServiceResult<bool>> UpdateClassAsync(ClassUpdateDto dto);

        // Logic xóa lớp: Phải kiểm tra xem lớp có sinh viên không mới cho xóa
        Task<ServiceResult<bool>> DeleteClassAsync(int id);
    }
}