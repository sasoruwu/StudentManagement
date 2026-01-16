using StudentManagement.BLL.Interfaces;
using StudentManagement.Entities.DTOs.Classes;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement.UI.Forms
{
    public partial class frmClassManagement : Form
    {
        private readonly IClassService _classService;

        // Biến lưu ID lớp đang chọn (để sửa/xóa)
        private int? _selectedClassId = null;

        public frmClassManagement(IClassService classService)
        {
            InitializeComponent();
            _classService = classService;
        }

        // 1. Load Data
        private async void frmClassManagement_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var classes = await _classService.GetAllClassesAsync();

                dgvClasses.DataSource = classes;

                // Format Grid nếu cần (ẩn cột, đặt tên)
                if (dgvClasses.Columns["ClassId"] != null)
                    dgvClasses.Columns["ClassId"].Visible = false;

                // Reset trạng thái nhập liệu
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ClearInputs()
        {
            txtClassName.Clear();
            txtFaculty.Clear();
            _selectedClassId = null;

            // Điều khiển trạng thái nút bấm
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        // 2. Binding khi Click vào Grid
        private void dgvClasses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy object từ dòng đang chọn
                var selectedRow = dgvClasses.Rows[e.RowIndex];
                var dto = selectedRow.DataBoundItem as ClassViewDto;

                if (dto != null)
                {
                    _selectedClassId = dto.ClassId;
                    txtClassName.Text = dto.ClassName;
                    txtFaculty.Text = dto.FacultyName;

                    // Chuyển sang chế độ Sửa/Xóa
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
        }

        // 3. Nút Thêm
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClassName.Text) || string.IsNullOrWhiteSpace(txtFaculty.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            try
            {
                var dto = new ClassCreateDto
                {
                    ClassName = txtClassName.Text.Trim(),
                    FacultyName = txtFaculty.Text.Trim()
                };

                var result = await _classService.AddClassAsync(dto);

                if (result.IsSuccess)
                {
                    MessageBox.Show("Thêm lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // 4. Nút Sửa
        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedClassId == null) return;

            try
            {
                var dto = new ClassUpdateDto
                {
                    ClassId = _selectedClassId.Value,
                    ClassName = txtClassName.Text.Trim(),
                    FacultyName = txtFaculty.Text.Trim()
                };

                var result = await _classService.UpdateClassAsync(dto);

                if (result.IsSuccess)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // 5. Nút Xóa
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedClassId == null) return;

            var confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa lớp này?\nLưu ý: Không thể xóa lớp nếu đang có sinh viên.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // Logic kiểm tra ràng buộc (có sinh viên không) đã nằm trong ClassService (BLL)
                    var result = await _classService.DeleteClassAsync(_selectedClassId.Value);

                    if (result.IsSuccess)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
        }

        // 6. Nút Làm mới
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearInputs();
            dgvClasses.ClearSelection();
        }
    }
}