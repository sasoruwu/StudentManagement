using Microsoft.EntityFrameworkCore;
using StudentManagement.BLL.Interfaces;
using StudentManagement.DAL.EF;
using StudentManagement.DAL.Interfaces;
using StudentManagement.Entities.DTOs.Classes;
using StudentManagement.Entities.DTOs.Common;
using StudentManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.BLL.Services
{
    public class ClassService : IClassService
    {
        //private readonly IUnitOfWork _unitOfWork;

        //public ClassService(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        private readonly StudentContext _context; // Khai báo biến

        public ClassService(StudentContext context) // Constructor để nạp biến vào
        {
            _context = context;
        }

        public async Task<List<ClassViewDto>> GetAllClassesAsync()
        {
            // Lấy tất cả lớp (giả sử repo generic đã include Students hoặc dùng logic riêng)
            // Ở đây gọi hàm riêng để load kèm sinh viên nhằm đếm sĩ số
            var classes = await _context.Classrooms.ToListAsync();

            var result = new List<ClassViewDto>();
            foreach (var c in classes)
            {
                // Gọi count thủ công hoặc load eager nếu cần
                var fullClass = await _context.Classrooms
                         .Include(x => x.Students)
                         .FirstOrDefaultAsync(x => x.ClassId == c.ClassId);
                result.Add(new ClassViewDto
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    FacultyName = c.FacultyName,
                    TotalStudents = fullClass.Students.Count
                });
            }
            return result;
        }

        public async Task<List<DropdownItem>> GetClassesForDropdownAsync()
        {
            var classes = await _context.Classrooms.ToListAsync();
            return classes.Select(c => new DropdownItem(c.ClassId, c.ClassName)).ToList();
        }

        // ... (Các hàm Add, Update tương tự StudentService)

        public async Task<ServiceResult<bool>> DeleteClassAsync(int id)
        {
            // Business Logic: Không được xóa lớp đang có sinh viên
            bool hasStudents = await _context.Students.AnyAsync(s => s.ClassId == id);
            if (hasStudents)
            {
                return ServiceResult<bool>.Failure("Lớp này đang có sinh viên, không thể xóa!");
            }

            try
            {
                var classToDelete = await _context.Classrooms.FindAsync(id);
                if (classToDelete != null)
                {
                    _context.Classrooms.Remove(classToDelete);
                    await _context.SaveChangesAsync(); // Lưu thay đổi
                }
                return ServiceResult<bool>.Success(true, "Xóa lớp thành công");
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure(ex.Message);
            }
        }

        public async Task<ServiceResult<int>> AddClassAsync(ClassCreateDto dto)
        {
            var result = new ServiceResult<int>();

            try
            {
                if (dto == null)
                {
                    result.Message = "Dữ liệu trống";
                    return result;
                }

                var newClass = new Classroom
                {
                    ClassName = dto.ClassName,
                    FacultyName = dto.FacultyName
                };

                _context.Classrooms.Add(newClass);
                await _context.SaveChangesAsync();

                return ServiceResult<int>.Success(newClass.ClassId);
            }
            catch (Exception ex)
            {
                string loiChiTiet = ex.Message;
                if (ex.InnerException != null)
                {
                    loiChiTiet += "\nChi tiết: " + ex.InnerException.Message;
                }

                return ServiceResult<int>.Failure(loiChiTiet);    
            }
        }
        public async Task<ServiceResult<bool>> UpdateClassAsync(ClassUpdateDto dto)
        {
            // 1. Khởi tạo đối tượng kết quả trước
            var result = new ServiceResult<bool>();

            try
            {
                var existingClass = await _context.Classrooms.FindAsync(dto.ClassId);

                if (existingClass == null)
                {
                    // Gán thất bại từng dòng
                    result.IsSuccess = false;
                    result.Message = "Không tìm thấy lớp học";
                    return result;
                }

                // 2. Cập nhật thông tin
                existingClass.ClassName = dto.ClassName;
                // existingClass.FacultyId = dto.FacultyId; // Mở ra nếu cần

                _context.Classrooms.Update(existingClass);
                await _context.SaveChangesAsync();

                // 3. Gán thành công (Theo đúng gợi ý bạn vừa thấy)
                result.IsSuccess = true;
                result.Data = true;
                result.Message = "Cập nhật lớp thành công";

                return result;
            }
            catch (Exception ex)
            {
                // Gán lỗi catch
                result.IsSuccess = false;
                result.Message = ex.Message;
                return result;
            }
        }
        public async Task<ClassDetailDto> GetClassDetailAsync(int classId)
        {
            // 1. Tìm trong DB
            var classroom = await _context.Classrooms.FindAsync(classId);

            if (classroom == null) return null;

            // 2. Chuyển đổi Entity -> DTO (Mapping)
            return new ClassDetailDto
            {
                ClassId = classroom.ClassId,
                ClassName = classroom.ClassName,
                // Gán thêm các trường khác nếu DTO yêu cầu
            };
        }
    }
}