using Microsoft.EntityFrameworkCore;
using StudentManagement.DAL.EF;
using StudentManagement.DAL.Interfaces;
using StudentManagement.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.DAL.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentContext context) : base(context)
        {
        }

        // Lấy danh sách SV kèm tên Lớp (Eager Loading)
        public async Task<IEnumerable<Student>> GetStudentsWithClassAsync()
        {
            return await _context.Students
                .Include(s => s.Classroom) // Tương đương câu lệnh JOIN trong SQL
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> SearchStudentsAsync(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return await GetStudentsWithClassAsync();

            return await _context.Students
                .Include(s => s.Classroom)
                .Where(s => s.FullName.Contains(keyword) ||
                            s.Address.Contains(keyword) ||
                            s.StudentId.ToString().Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByClassIdAsync(int classId)
        {
            return await _context.Students
                .Where(s => s.ClassId == classId)
                .ToListAsync();
        }
    }
}