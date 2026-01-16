using Microsoft.EntityFrameworkCore;
using StudentManagement.DAL.EF;
using StudentManagement.DAL.Interfaces;
using StudentManagement.Entities.Models;
using System.Threading.Tasks;

namespace StudentManagement.DAL.Repositories
{
    public class ClassRepository : GenericRepository<Classroom>, IClassRepository
    {
        public ClassRepository(StudentContext context) : base(context)
        {
        }

        public async Task<bool> HasStudentsAsync(int classId)
        {
            // Kiểm tra xem có bất kỳ sinh viên nào thuộc lớp này không
            return await _context.Students.AnyAsync(s => s.ClassId == classId);
        }

        public async Task<Classroom> GetClassWithStudentsAsync(int classId)
        {
            return await _context.Classrooms
                .Include(c => c.Students) // Load kèm danh sách SV
                .FirstOrDefaultAsync(c => c.ClassId == classId);
        }
    }
}