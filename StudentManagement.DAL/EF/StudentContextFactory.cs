using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StudentManagement.DAL.EF
{
    public class StudentContextFactory : IDesignTimeDbContextFactory<StudentContext>
    {
        public StudentContext CreateDbContext(string[] args)
        {
            // Tạo builder
            var optionsBuilder = new DbContextOptionsBuilder<StudentContext>();

            // Cấu hình chuỗi kết nối tạm thời dùng cho Design-time (lúc chạy lệnh update-database)
            // Lưu ý: Chuỗi kết nối thực tế khi chạy App sẽ lấy từ UI/Program.cs
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=StudentManagementDB;Integrated Security=True;TrustServerCertificate=True");

            return new StudentContext(optionsBuilder.Options);
        }
    }
}