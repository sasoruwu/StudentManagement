using StudentManagement.Entities.DTOs.Common;
using StudentManagement.Entities.Models; // Có thể dùng Model nếu Subject đơn giản
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.BLL.Interfaces
{
    public interface ISubjectService
    {
        // Lấy danh sách môn học cho ComboBox
        Task<List<DropdownItem>> GetSubjectsForDropdownAsync();

        // Lấy số tín chỉ của môn học
        Task<int> GetCreditsAsync(int subjectId);
    }
}