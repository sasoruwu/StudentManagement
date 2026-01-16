using Microsoft.EntityFrameworkCore.Storage;
using StudentManagement.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace StudentManagement.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Các Repository thành phần
        IStudentRepository Students { get; }
        IClassRepository Classrooms { get; }
        IGradeRepository Grades { get; }
        // Bạn có thể thêm ISubjectRepository nếu cần CRUD môn học riêng biệt
        IGenericRepository<Entities.Models.Subject> Subjects { get; }

        // Hàm lưu thay đổi xuống Database (Commit)
        Task<int> SaveChangesAsync();

        // --- Hỗ trợ Transaction ---

        // Bắt đầu một transaction
        Task<IDbContextTransaction> BeginTransactionAsync();

        // Commit transaction (nếu manual control)
        Task CommitAsync();

        // Rollback transaction
        Task RollbackAsync();
    }
}