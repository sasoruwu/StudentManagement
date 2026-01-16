using StudentManagement.Entities.DTOs.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.BLL.Interfaces
{
    public interface IReportingService
    {
        // Báo cáo tình hình các lớp (Sĩ số, Nam/Nữ, Điểm TB)
        Task<List<ClassStatisticDto>> GetClassStatisticsAsync();

        // Báo cáo kết quả học tập của 1 sinh viên (GPA, Xếp loại)
        Task<StudentAcademicReportDto> GetStudentAcademicReportAsync(int studentId);

        // (Mở rộng) Top 5 sinh viên điểm cao nhất trường
        Task<List<StudentAcademicReportDto>> GetTopStudentsAsync(int count);
    }
}