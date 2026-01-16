using StudentManagement.Entities.DTOs.Common;
using StudentManagement.Entities.DTOs.Grades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.BLL.Interfaces
{
    public interface IGradeService
    {
        // Xem bảng điểm cá nhân của 1 sinh viên (Transcript)
        Task<List<StudentGradeViewDto>> GetTranscriptAsync(int studentId);

        // Lấy danh sách điểm của cả lớp theo môn học (để nhập điểm hàng loạt)
        Task<List<SubjectGradeEntryDto>> GetGradesForClassInputAsync(int classId, int subjectId);

        // LƯU ĐIỂM HÀNG LOẠT (Sử dụng Transaction)
        // Nhận vào danh sách các dòng điểm đã sửa từ Grid
        Task<ServiceResult<bool>> SaveGradesBatchAsync(int classId, int subjectId, List<SubjectGradeEntryDto> grades);
    }
}