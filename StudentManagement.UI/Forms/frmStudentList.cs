using Microsoft.Extensions.DependencyInjection;
using StudentManagement.BLL.Interfaces;
using StudentManagement.Entities.DTOs.Students;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement.UI.Forms
{
    public partial class frmStudentList : Form
    {
        private readonly IStudentService _studentService;
        private readonly IServiceProvider _serviceProvider;

        // Constructor Injection
        public frmStudentList(IStudentService studentService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _studentService = studentService;
            _serviceProvider = serviceProvider;
        }

        // 1. Load Data (Async)
        private async void frmStudentList_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // Hiển thị trạng thái đang tải (có thể dùng ProgressBar nếu muốn)
                this.Cursor = Cursors.WaitCursor;

                var studentList = await _studentService.GetAllStudentsAsync();

                // Binding vào Grid
                dgvStudents.DataSource = studentList;

                // Tinh chỉnh hiển thị cột nếu DTO chưa chuẩn
                if (dgvStudents.Columns["StudentId"] != null)
                {
                    dgvStudents.Columns["StudentId"].Visible = false; // Ẩn cột ID
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // 2. Tìm kiếm
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                await LoadDataAsync();
                return;
            }

            try
            {
                var results = await _studentService.SearchStudentsAsync(keyword);
                dgvStudents.DataSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // 3. Nút Refresh
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            await LoadDataAsync();
        }

        // 4. Thêm mới
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            // Sử dụng DI để tạo Form chi tiết, đảm bảo Form chi tiết cũng nhận được các Service nó cần
            // Giả sử tên form chi tiết là frmStudentDetail
            var frmDetail = _serviceProvider.GetRequiredService<frmStudentDetail>();

            // Thiết lập trạng thái là Thêm mới (nếu form chi tiết có property này)
            frmDetail.StudentId = null;

            if (frmDetail.ShowDialog() == DialogResult.OK)
            {
                await LoadDataAsync(); // Tải lại dữ liệu sau khi thêm thành công
            }
        }

        // 5. Sửa
        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy ID từ dòng đang chọn
            // DataBoundItem trả về đối tượng StudentViewDto
            var selectedDto = (StudentViewDto)dgvStudents.SelectedRows[0].DataBoundItem;
            int studentId = selectedDto.StudentId;

            var frmDetail = _serviceProvider.GetRequiredService<frmStudentDetail>();

            // Truyền ID sang form chi tiết để nó load dữ liệu lên
            frmDetail.StudentId = studentId;

            if (frmDetail.ShowDialog() == DialogResult.OK)
            {
                await LoadDataAsync();
            }
        }

        // 6. Xóa
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedDto = (StudentViewDto)dgvStudents.SelectedRows[0].DataBoundItem;

            var confirmResult = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sinh viên: {selectedDto.FullName}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var result = await _studentService.DeleteStudentAsync(selectedDto.StudentId);
                    if (result.IsSuccess)
                    {
                        MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
        }
    }
}