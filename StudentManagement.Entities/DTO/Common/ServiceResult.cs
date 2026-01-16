namespace StudentManagement.Entities.DTOs.Common
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }

        // Thông báo (Lỗi hoặc Thành công) để hiện MessageBox
        public string Message { get; set; }

        // Dữ liệu trả về (nếu có)
        public T Data { get; set; }

        // Helper method cho trường hợp thành công
        public static ServiceResult<T> Success(T data, string message = "Thao tác thành công")
        {
            return new ServiceResult<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }

        // Helper method cho trường hợp thất bại
        public static ServiceResult<T> Failure(string errorMessage)
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
                Data = default(T),
                Message = errorMessage
            };
        }
    }
}