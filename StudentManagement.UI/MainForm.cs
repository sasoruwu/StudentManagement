using Microsoft.Extensions.DependencyInjection;
using StudentManagement.UI.Forms; // Namespace chứa các form con
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagement.UI
{
    public partial class MainForm : Form
    {
        // 1. Dùng IServiceProvider để lấy các Form con từ DI Container
        private readonly IServiceProvider _serviceProvider;

        // Form con hiện tại đang hiển thị
        private Form _activeForm;
        private Button _currentButton;

        // Constructor nhận ServiceProvider từ Program.cs
        public MainForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        // --- HÀM HỖ TRỢ ĐỂ MỞ FORM CON ---
        // Sử dụng Generic <T> để biết cần mở form nào
        private void OpenChildForm<T>(object btnSender, string title) where T : Form
        {
            if (_activeForm != null)
            {
                // Nếu đang mở form khác thì đóng nó lại để tránh chồng chéo
                _activeForm.Close();
                _activeForm.Dispose(); // Giải phóng tài nguyên
            }

            ActivateButton(btnSender);

            // TỰ ĐỘNG LẤY FORM TỪ DI CONTAINER
            // Thay vì: T form = new T(); 
            // Ta dùng:
            T childForm = _serviceProvider.GetRequiredService<T>();

            _activeForm = childForm;

            // Cấu hình để form con hiển thị trong Panel
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();

            lblTitle.Text = title;
        }

        // --- XỬ LÝ GIAO DIỆN (Highlight nút đang chọn) ---
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (_currentButton != (Button)btnSender)
                {
                    DisableButton();
                    _currentButton = (Button)btnSender;
                    _currentButton.BackColor = Color.FromArgb(73, 73, 100); // Màu highlight
                    _currentButton.ForeColor = Color.White;
                    _currentButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76); // Màu gốc
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
                }
            }
        }

        // --- SỰ KIỆN CLICK MENU ---

        private void btnStudents_Click(object sender, EventArgs e)
        {
            // Mở form Sinh viên (Giả sử bạn đặt tên là frmStudentList)
            // Lưu ý: Cần đăng ký frmStudentList trong Program.cs
            OpenChildForm<frmStudentList>(sender, "QUẢN LÝ SINH VIÊN");
        }

        private void btnClasses_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmClassManagement>(sender, "QUẢN LÝ LỚP HỌC");
        }

        private void btnGrades_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmGradeEntry>(sender, "QUẢN LÝ ĐIỂM SỐ");
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmReports>(sender, "BÁO CÁO THỐNG KÊ");
        }
    }
}