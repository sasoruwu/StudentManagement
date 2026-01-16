namespace StudentManagement.Entities.DTOs.Common
{
    public class DropdownItem
    {
        // Giá trị ẩn (thường là ID: ClassId, SubjectId)
        // Dùng kiểu object để linh hoạt (có thể là int, string, hoặc Guid)
        public object Value { get; set; }

        // Giá trị hiển thị (thường là Name: ClassName, SubjectName)
        public string Text { get; set; }

        // Constructor tiện lợi
        public DropdownItem(object value, string text)
        {
            Value = value;
            Text = text;
        }

        // WinForms đôi khi gọi ToString() để hiển thị nếu không set DisplayMember
        public override string ToString()
        {
            return Text;
        }
    }
}