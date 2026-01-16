using Microsoft.EntityFrameworkCore.Storage;
using StudentManagement.DAL.EF;
using StudentManagement.DAL.Interfaces;
using StudentManagement.Entities.Models;
using System.Threading.Tasks;

namespace StudentManagement.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentContext _context;

        // Khai báo private fields
        private IStudentRepository _students;
        private IClassRepository _classrooms;
        private IGradeRepository _grades;
        private IGenericRepository<Subject> _subjects;

        public UnitOfWork(StudentContext context)
        {
            _context = context;
        }

        // Triển khai Singleton (cho mỗi Scope request) để đảm bảo chỉ tạo Repo khi cần dùng
        public IStudentRepository Students => _students ??= new StudentRepository(_context);

        public IClassRepository Classrooms => _classrooms ??= new ClassRepository(_context);

        public IGradeRepository Grades => _grades ??= new GradeRepository(_context);

        public IGenericRepository<Subject> Subjects => _subjects ??= new GenericRepository<Subject>(_context);

        // --- Transaction Management ---

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}