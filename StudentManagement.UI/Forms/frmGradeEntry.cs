using StudentManagement.BLL.Interfaces;
using StudentManagement.Entities.DTOs.Grades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement.UI.Forms
{
    public partial class frmGradeEntry : Form
    {
        private readonly IGradeService _gradeService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;

        // Dùng BindingSource để dễ quản lý dữ liệu trên Grid
        private BindingSource _bindingSource = new BindingSource();

        public frmGradeEntry(IGradeService gradeService, IClassService classService, ISubjectService subjectService)
        {
            InitializeComponent();
            _gradeService = gradeService;
            _classService = classService;
            _subjectService = subjectService;
        }

        private async void frmGradeEntry_Load(object sender, EventArgs e)
        {
            await LoadCombosAsync();
            btnSave.Enabled = false; // Chỉ cho lưu khi đã load dữ liệu
        }

        private async Task LoadCombosAsync()
        {
            try
            {
                var classes = await _classService.GetClassesForDropdownAsync();
                cboClass.DataSource = classes;
                cboClass.DisplayMember = "Text";
                cboClass.ValueMember = "Value";

                var subjects = await _subjectService.GetSubjectsForDropdownAsync();
                cboSubject.DataSource = subjects;
                cboSubject.DisplayMember = "Text";
                cboSubject.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            if (cboClass.SelectedValue == null || cboSubject.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Lớp và Môn học.");
                return;
            }

            int classId = (int)cboClass.SelectedValue;
            int subjectId = (int)cboSubject.SelectedValue;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Lấy danh sách DTO từ BLL
                var gradeList = await _gradeService.GetGradesForClassInputAsync(classId, subjectId);

                // Gán vào BindingSource -> Grid
                _bindingSource.DataSource = gradeList;
                dgvGrades.DataSource = _bindingSource;

                FormatGrid();
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách sinh viên: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FormatGrid()
        {
            // Ẩn các cột không cần thiết
            if (dgvGrades.Columns["StudentId"] != null) dgvGrades.Columns["StudentId"].Visible = false;
            if (dgvGrades.Columns["IsChanged"] != null) dgvGrades.Columns["IsChanged"].Visible = false;

            // Đặt tên tiếng Việt (nếu DTO chưa có DisplayName)
            if (dgvGrades.Columns["StudentIdDisplay"] != null)
            {
                dgvGrades.Columns["StudentIdDisplay"].HeaderText = "Mã SV";
                dgvGrades.Columns["StudentIdDisplay"].ReadOnly = true;
            }

            if (dgvGrades.Columns["StudentName"] != null)
            {
                dgvGrades.Columns["StudentName"].HeaderText = "Họ và Tên";
                dgvGrades.Columns["StudentName"].ReadOnly = true;
                dgvGrades.Columns["StudentName"].Width = 200;
            }

            if (dgvGrades.Columns["OldScore"] != null)
            {
                dgvGrades.Columns["OldScore"].HeaderText = "Điểm cũ";
                dgvGrades.Columns["OldScore"].ReadOnly = true;
                dgvGrades.Columns["OldScore"].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            }

            if (dgvGrades.Columns["NewScore"] != null)
            {
                dgvGrades.Columns["NewScore"].HeaderText = "Điểm mới (Nhập tại đây)";
                dgvGrades.Columns["NewScore"].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
                dgvGrades.Columns["NewScore"].DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            }
        }

        // Validate: Chỉ cho nhập số từ 0-10
        private void dgvGrades_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Chỉ kiểm tra cột "NewScore"
            if (dgvGrades.Columns[e.ColumnIndex].Name == "NewScore")
            {
                string input = e.FormattedValue.ToString();

                // Cho phép để trống (nếu chưa muốn nhập điểm)
                if (string.IsNullOrWhiteSpace(input)) return;

                if (!double.TryParse(input, out double score))
                {
                    dgvGrades.Rows[e.RowIndex].ErrorText = "Điểm phải là số.";
                    e.Cancel = true;
                    return;
                }

                if (score < 0 || score > 10)
                {
                    dgvGrades.Rows[e.RowIndex].ErrorText = "Điểm phải từ 0 đến 10.";
                    e.Cancel = true;
                    return;
                }

                // Xóa thông báo lỗi nếu hợp lệ
                dgvGrades.Rows[e.RowIndex].ErrorText = string.Empty;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // EndEdit để đảm bảo giá trị cell đang gõ được commit vào DataSource
            dgvGrades.EndEdit();

            // Lấy danh sách từ BindingSource
            var gradeList = _bindingSource.DataSource as List<SubjectGradeEntryDto>;

            if (gradeList == null || gradeList.Count == 0) return;

            int classId = (int)cboClass.SelectedValue;
            int subjectId = (int)cboSubject.SelectedValue;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                // GỌI SERVICE TRANSACTION
                var result = await _gradeService.SaveGradesBatchAsync(classId, subjectId, gradeList);

                if (result.IsSuccess)
                {
                    MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Load lại dữ liệu để cập nhật cột Điểm cũ = Điểm mới vừa nhập
                    btnLoad_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra: " + result.Message, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}