using Microsoft.EntityFrameworkCore;
using StudentManagement.DAL.EF;
using StudentManagement.DAL.Interfaces;
using StudentManagement.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.DAL.Repositories
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        public GradeRepository(StudentContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId)
        {
            return await _context.Grades
                .Include(g => g.Subject) // Để lấy tên môn học
                .Where(g => g.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Grade>> GetGradesByClassAndSubjectAsync(int classId, int subjectId)
        {
            // Logic: Tìm tất cả điểm của môn X, nhưng chỉ của những SV thuộc lớp Y
            return await _context.Grades
                .Include(g => g.Student)
                .Where(g => g.SubjectId == subjectId && g.Student.ClassId == classId)
                .ToListAsync();
        }

        public async Task<Grade> GetGradeByStudentAndSubjectAsync(int studentId, int subjectId)
        {
            return await _context.Grades
                .FirstOrDefaultAsync(g => g.StudentId == studentId && g.SubjectId == subjectId);
        }
    }
}