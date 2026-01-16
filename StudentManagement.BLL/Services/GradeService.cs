using StudentManagement.BLL.Interfaces;
using StudentManagement.DAL.Interfaces;
using StudentManagement.Entities.DTOs.Common;
using StudentManagement.Entities.DTOs.Grades;
using StudentManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.BLL.Services
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StudentGradeViewDto>> GetTranscriptAsync(int studentId)
        {
            var grades = await _unitOfWork.Grades.GetGradesByStudentIdAsync(studentId);
            return grades.Select(g => new StudentGradeViewDto
            {
                SubjectId = g.SubjectId,
                SubjectName = g.Subject.SubjectName,
                Credits = g.Subject.Credits,
                Score = g.Score
                // LetterGrade và Status tự động tính trong DTO
            }).ToList();
        }

        public async Task<List<SubjectGradeEntryDto>> GetGradesForClassInputAsync(int classId, int subjectId)
        {
            // 1. Lấy tất cả sinh viên trong lớp
            var students = await _unitOfWork.Students.GetStudentsByClassIdAsync(classId);

            // 2. Lấy điểm đã có của môn đó thuộc lớp đó
            var existingGrades = await _unitOfWork.Grades.GetGradesByClassAndSubjectAsync(classId, subjectId);

            // 3. Left Join: Ghép sinh viên với điểm (nếu chưa có điểm thì để trống)
            var result = new List<SubjectGradeEntryDto>();
            foreach (var student in students)
            {
                var grade = existingGrades.FirstOrDefault(g => g.StudentId == student.StudentId);
                result.Add(new SubjectGradeEntryDto
                {
                    StudentId = student.StudentId,
                    StudentName = student.FullName,
                    OldScore = grade?.Score,
                    NewScore = grade?.Score // Mặc định hiển thị điểm cũ
                });
            }
            return result;
        }

        // --- XỬ LÝ TRANSACTION ---
        public async Task<ServiceResult<bool>> SaveGradesBatchAsync(int classId, int subjectId, List<SubjectGradeEntryDto> gradeEntries)
        {
            // Bắt đầu Transaction
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    foreach (var entry in gradeEntries)
                    {
                        // Chỉ xử lý những dòng có thay đổi điểm
                        // (Lưu ý: UI cần gán IsChanged = true khi cell value change hoặc so sánh logic ở đây)
                        bool isScoreChanged = entry.NewScore != entry.OldScore;

                        if (isScoreChanged && entry.NewScore.HasValue)
                        {
                            // Kiểm tra xem đã có điểm trong DB chưa
                            var existingGrade = await _unitOfWork.Grades
                                .GetGradeByStudentAndSubjectAsync(entry.StudentId, subjectId);

                            if (existingGrade != null)
                            {
                                // UPDATE
                                existingGrade.Score = entry.NewScore.Value;
                                _unitOfWork.Grades.Update(existingGrade);
                            }
                            else
                            {
                                // INSERT
                                var newGrade = new Grade
                                {
                                    StudentId = entry.StudentId,
                                    SubjectId = subjectId,
                                    Score = entry.NewScore.Value
                                };
                                await _unitOfWork.Grades.AddAsync(newGrade);
                            }
                        }
                    }

                    // Lưu xuống DB (vẫn nằm trong Transaction)
                    await _unitOfWork.SaveChangesAsync();

                    // Nếu không lỗi lầm gì, Commit Transaction
                    await _unitOfWork.CommitAsync();

                    return ServiceResult<bool>.Success(true, "Lưu bảng điểm thành công!");
                }
                catch (Exception ex)
                {
                    // Có lỗi -> Rollback toàn bộ
                    await _unitOfWork.RollbackAsync();
                    return ServiceResult<bool>.Failure($"Lỗi giao dịch: {ex.Message}");
                }
            }
        }
    }
}