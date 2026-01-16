using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudentManagement.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // Lấy tất cả bản ghi
        Task<IEnumerable<T>> GetAllAsync();

        // Lấy 1 bản ghi theo ID
        Task<T> GetByIdAsync(object id);

        // Tìm kiếm theo điều kiện (LINQ Expression)
        // Ví dụ: repository.Find(s => s.Name.Contains("A"))
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Thêm mới
        Task AddAsync(T entity);

        // Sửa (EF Core chỉ cần đánh dấu trạng thái, việc lưu xuống DB do UnitOfWork lo)
        void Update(T entity);

        // Xóa
        void Delete(T entity);

        // Xóa theo ID
        Task DeleteAsync(object id);
    }
}