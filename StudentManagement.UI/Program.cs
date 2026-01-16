using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.BLL.Interfaces;
using StudentManagement.BLL.Services;
using StudentManagement.DAL.EF;
using StudentManagement.DAL.Interfaces;
using StudentManagement.DAL.Repositories;
using StudentManagement.UI.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace StudentManagement.UI
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static IConfiguration Configuration { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Cấu hình đọc file appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // 2. Setup Dependency Injection (DI)
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            // 3. Chạy Form chính
            // Lưu ý: MainForm cũng phải được lấy từ ServiceProvider để các dependency bên trong nó hoạt động
            try
            {
                var mainForm = ServiceProvider.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi động ứng dụng: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // A. Đăng ký DbContext (Lấy chuỗi kết nối từ appsettings.json)
            services.AddDbContext<StudentContext>(options =>
            {
                // Lấy chuỗi kết nối từ file appsettings.json
                var connectionString = Configuration.GetConnectionString("DefaultConnection");

                options.UseSqlServer(connectionString);
            });

            // B. Đăng ký Repository (DAL)
            // Đăng ký Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Đăng ký các Repo cụ thể
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();

            // Đăng ký UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // C. Đăng ký Services (BLL)
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<ISubjectService, SubjectService>(); // <-- Mới thêm
            services.AddScoped<IReportingService, ReportingService>();

            // D. Đăng ký Forms (UI) - Bắt buộc phải đăng ký tất cả Form có dùng DI
            services.AddTransient<MainForm>();
            services.AddTransient<frmStudentList>();
            services.AddTransient<frmStudentDetail>();
            services.AddTransient<frmClassManagement>();
            services.AddTransient<frmGradeEntry>();
            services.AddTransient<frmReports>();
        }
    }
}