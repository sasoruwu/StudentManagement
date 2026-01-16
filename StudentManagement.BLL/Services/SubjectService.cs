using StudentManagement.BLL.Interfaces;
using StudentManagement.DAL.Interfaces;
using StudentManagement.Entities.DTOs.Common;
using StudentManagement.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.BLL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DropdownItem>> GetSubjectsForDropdownAsync()
        {
            var subjects = await _unitOfWork.Subjects.GetAllAsync();

            // Chuyển đổi sang DropdownItem để bind vào ComboBox
            return subjects.Select(s => new DropdownItem(s.SubjectId, s.SubjectName)).ToList();
        }

        public async Task<int> GetCreditsAsync(int subjectId)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(subjectId);
            return subject != null ? subject.Credits : 0;
        }
    }
}