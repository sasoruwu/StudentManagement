using Microsoft.EntityFrameworkCore;
using StudentManagement.Entities.Models;
using System.Diagnostics;

namespace StudentManagement.DAL.EF
{
    public class StudentContext : DbContext
    {
        // Constructor nhận options từ Dependency Injection (bên UI)
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
        }

        // Khai báo các bảng
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }

        // Cấu hình Fluent API và Seed Data (Dữ liệu mẫu)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- 1. Cấu hình Fluent API (Bổ sung cho Data Annotations) ---

            // Đảm bảo quan hệ 1-N: Lớp - Sinh viên
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Classroom)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Lớp -> Xóa luôn Sinh viên

            // Đảm bảo quan hệ: Sinh viên - Điểm (Xóa SV -> Xóa điểm)
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Đảm bảo quan hệ: Môn học - Điểm (Xóa Môn -> Xóa điểm)
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- 2. Data Seeding (Tạo dữ liệu mẫu khi chạy lần đầu) ---

            // Tạo Môn học mẫu
            modelBuilder.Entity<Subject>().HasData(
                new Subject { SubjectId = 1, SubjectName = "Lập trình C#", Credits = 4 },
                new Subject { SubjectId = 2, SubjectName = "Cơ sở dữ liệu", Credits = 3 },
                new Subject { SubjectId = 3, SubjectName = "Tiếng Anh chuyên ngành", Credits = 2 }
            );

            // Tạo Lớp học mẫu
            modelBuilder.Entity<Classroom>().HasData(
                new Classroom { ClassId = 1, ClassName = "CNTT_K15", FacultyName = "Công nghệ thông tin" },
                new Classroom { ClassId = 2, ClassName = "QTKD_K15", FacultyName = "Quản trị kinh doanh" }
            );

            // Tạo Sinh viên mẫu
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    FullName = "Nguyễn Văn A",
                    DateOfBirth = new DateTime(2003, 5, 20),
                    Gender = true,
                    Address = "Hà Nội",
                    PhoneNumber = "0987654321",
                    ClassId = 1
                },
                new Student
                {
                    StudentId = 2,
                    FullName = "Trần Thị B",
                    DateOfBirth = new DateTime(2003, 8, 15),
                    Gender = false,
                    Address = "Đà Nẵng",
                    PhoneNumber = "0912345678",
                    ClassId = 1
                }
            );

            // Tạo Điểm mẫu
            modelBuilder.Entity<Grade>().HasData(
                new Grade { GradeId = 1, StudentId = 1, SubjectId = 1, Score = 8.5 }, // A giỏi C#
                new Grade { GradeId = 2, StudentId = 1, SubjectId = 2, Score = 7.0 },
                new Grade { GradeId = 3, StudentId = 2, SubjectId = 1, Score = 9.0 }
            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Đây là cách ép buộc code phải dùng LocalDB, bất chấp appsettings.json nói gì
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
    @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=StudentManagementDB;Integrated Security=True;TrustServerCertificate=True");
            }
        }
    }
}