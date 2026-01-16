using StudentManagement.BLL.Interfaces;
using StudentManagement.DAL.Interfaces;
using StudentManagement.Entities.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.BLL.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClassStatisticDto>> GetClassStatisticsAsync()
        {
            var classes = await _unitOfWork.Classrooms.GetAllAsync();
            var reportList = new List<ClassStatisticDto>();

            foreach (var c in classes)
            {
                // Lấy chi tiết để tính toán
                var fullClass = await _unitOfWork.Classrooms.GetClassWithStudentsAsync(c.ClassId);
                var students = fullClass.Students;

                // Tính điểm TB của lớp (Cần load Grades của từng SV - Logic này nên tối ưu bằng Query trực tiếp ở Repo thì tốt hơn)
                // Ở đây demo logic in-memory đơn giản:
                double totalScore = 0;
                int countScore = 0;

                // Lưu ý: Logic lấy điểm này hơi nặng nếu data lớn, 
                // thực tế nên viết 1 SQL Query hoặc LINQ Project trong Repo để DB tự tính.

                var dto = new ClassStatisticDto
                {
                    ClassName = c.ClassName,
                    FacultyName = c.FacultyName,
                    TotalStudents = students.Count,
                    MaleCount = students.Count(s => s.Gender),
                    FemaleCount = students.Count(s => !s.Gender),
                    AverageClassScore = 0 // Tạm để 0 hoặc tính toán phức tạp hơn
                };
                reportList.Add(dto);
            }
            return reportList;
        }

        public async Task<StudentAcademicReportDto> GetStudentAcademicReportAsync(int studentId)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            var grades = await _unitOfWork.Grades.GetGradesByStudentIdAsync(studentId);

            if (student == null) return null;

            double gpa = 0;
            if (grades.Any())
            {
                // Tính GPA theo tín chỉ: Sum(Điểm * Tín chỉ) / Sum(Tín chỉ)
                double totalPoints = grades.Sum(g => g.Score * g.Subject.Credits);
                int totalCredits = grades.Sum(g => g.Subject.Credits);
                if (totalCredits > 0)
                    gpa = Math.Round(totalPoints / totalCredits, 2);
            }

            return new StudentAcademicReportDto
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                ClassName = student.Classroom?.ClassName,
                TotalSubjects = grades.Count(),
                TotalCredits = grades.Sum(g => g.Subject.Credits),
                GPA = gpa
                // AcademicRank tự động tính trong DTO
            };
        }

        public Task<List<StudentAcademicReportDto>> GetTopStudentsAsync(int count) => throw new NotImplementedException();
    }
}