using StudentManagement.BLL.Interfaces;
using StudentManagement.DAL.Interfaces;
using StudentManagement.Entities.DTOs.Common;
using StudentManagement.Entities.DTOs.Students;
using StudentManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StudentViewDto>> GetAllStudentsAsync()
        {
            // Lấy Entity từ DAL
            var students = await _unitOfWork.Students.GetStudentsWithClassAsync();

            // Mapping thủ công Entity -> DTO
            return students.Select(s => new StudentViewDto
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender, // DTO sẽ tự convert sang "Nam/Nữ"
                ClassName = s.Classroom != null ? s.Classroom.ClassName : "N/A",
                FacultyName = s.Classroom != null ? s.Classroom.FacultyName : "",
                Address = s.Address,
                PhoneNumber = s.PhoneNumber
            }).ToList();
        }

        public async Task<List<StudentViewDto>> SearchStudentsAsync(string keyword)
        {
            var students = await _unitOfWork.Students.SearchStudentsAsync(keyword);

            // Tái sử dụng logic mapping (có thể tách ra hàm riêng hoặc dùng AutoMapper)
            return students.Select(s => new StudentViewDto
            {
                StudentId = s.StudentId,
                FullName = s.FullName,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                ClassName = s.Classroom?.ClassName,
                FacultyName = s.Classroom?.FacultyName,
                Address = s.Address,
                PhoneNumber = s.PhoneNumber
            }).ToList();
        }

        public async Task<StudentUpdateDto> GetStudentForEditAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null) return null;

            return new StudentUpdateDto
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                ClassId = student.ClassId
            };
        }

        public async Task<ServiceResult<int>> AddStudentAsync(StudentCreateDto dto)
        {
            try
            {
                var student = new Student
                {
                    FullName = dto.FullName,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    Address = dto.Address,
                    PhoneNumber = dto.PhoneNumber,
                    ClassId = dto.ClassId
                };

                await _unitOfWork.Students.AddAsync(student);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult<int>.Success(student.StudentId, "Thêm sinh viên thành công!");
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.Failure($"Lỗi khi thêm: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> UpdateStudentAsync(StudentUpdateDto dto)
        {
            try
            {
                var student = await _unitOfWork.Students.GetByIdAsync(dto.StudentId);
                if (student == null)
                    return ServiceResult<bool>.Failure("Không tìm thấy sinh viên!");

                // Cập nhật thông tin
                student.FullName = dto.FullName;
                student.DateOfBirth = dto.DateOfBirth;
                student.Gender = dto.Gender;
                student.Address = dto.Address;
                student.PhoneNumber = dto.PhoneNumber;
                student.ClassId = dto.ClassId;

                _unitOfWork.Students.Update(student);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult<bool>.Success(true, "Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Lỗi cập nhật: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteStudentAsync(int id)
        {
            try
            {
                await _unitOfWork.Students.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
                return ServiceResult<bool>.Success(true, "Xóa thành công!");
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Lỗi xóa: {ex.Message}");
            }
        }
    }
}