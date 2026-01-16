using StudentManagement.BLL.Interfaces;
using StudentManagement.Entities.DTOs.Common;
using StudentManagement.Entities.DTOs.Students;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement.UI.Forms
{
    public partial class frmStudentDetail : Form
    {
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;

        // Property này được set từ frmStudentList trước khi ShowDialog
        // Null -> Thêm mới, Có giá trị -> Sửa
        public int? StudentId { get; set; }

        public frmStudentDetail(IStudentService studentService, IClassService classService)
        {
            InitializeComponent();
            _studentService = studentService;
            _classService = classService;
        }

        private async void frmStudentDetail_Load(object sender, EventArgs e)
        {
            await LoadClassesAsync();

            if (StudentId.HasValue)
            {
                // Chế độ Sửa
                this.Text = "Cập nhật sinh viên";
                btnSave.Text = "Cập nhật";
                await LoadStudentDataAsync(StudentId.Value);
            }
            else
            {
                // Chế độ Thêm
                this.Text = "Thêm mới sinh viên";
                btnSave.Text = "Thêm mới";
                radMale.Checked = true; // Mặc định Nam
                dtpDob.Value = DateTime.Now.AddYears(-18); // Mặc định 18 tuổi
            }
        }

        private async Task LoadClassesAsync()
        {
            try
            {
                // Lấy danh sách lớp cho ComboBox từ Service
                var classes = await _classService.GetClassesForDropdownAsync();

                cboClass.DataSource = classes;
                cboClass.DisplayMember = "Text"; // Hiển thị tên lớp
                cboClass.ValueMember = "Value";  // Giá trị là ID lớp
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp: " + ex.Message);
            }
        }

        private async Task LoadStudentDataAsync(int id)
        {
            try
            {
                var dto = await _studentService.GetStudentForEditAsync(id);
                if (dto != null)
                {
                    txtFullName.Text = dto.FullName;
                    cboClass.SelectedValue = dto.ClassId;
                    dtpDob.Value = dto.DateOfBirth;
                    txtAddress.Text = dto.Address;
                    txtPhone.Text = dto.PhoneNumber;

                    if (dto.Gender) radMale.Checked = true;
                    else radFemale.Checked = true;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu sinh viên: " + ex.Message);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            // Disable nút để tránh click đúp
            btnSave.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (StudentId.HasValue)
                {
                    // --- LOGIC CẬP NHẬT ---
                    var dto = new StudentUpdateDto
                    {
                        StudentId = StudentId.Value,
                        FullName = txtFullName.Text.Trim(),
                        ClassId = (int)cboClass.SelectedValue,
                        DateOfBirth = dtpDob.Value,
                        Gender = radMale.Checked,
                        Address = txtAddress.Text.Trim(),
                        PhoneNumber = txtPhone.Text.Trim()
                    };

                    var result = await _studentService.UpdateStudentAsync(dto);
                    ProcessResult(result);
                }
                else
                {
                    // --- LOGIC THÊM MỚI ---
                    var dto = new StudentCreateDto
                    {
                        FullName = txtFullName.Text.Trim(),
                        ClassId = (int)cboClass.SelectedValue,
                        DateOfBirth = dtpDob.Value,
                        Gender = radMale.Checked,
                        Address = txtAddress.Text.Trim(),
                        PhoneNumber = txtPhone.Text.Trim()
                    };

                    var result = await _studentService.AddStudentAsync(dto);
                    ProcessResult(result); // result ở đây là ServiceResult<int>
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        // Hàm xử lý kết quả chung
        private void ProcessResult<T>(ServiceResult<T> result)
        {
            if (result.IsSuccess)
            {
                MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Đóng form và báo cho form cha biết là thành công
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên sinh viên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn lớp học.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboClass.Focus();
                return false;
            }

            // Có thể thêm validate cho ngày sinh (ví dụ: phải > 18 tuổi)
            if (DateTime.Now.Year - dtpDob.Value.Year < 17)
            {
                MessageBox.Show("Sinh viên phải từ 17 tuổi trở lên.", "Cảnh báo");
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}