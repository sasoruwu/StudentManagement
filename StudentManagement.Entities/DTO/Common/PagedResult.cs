using System;
using System.Collections.Generic;

namespace StudentManagement.Entities.DTOs.Common
{
    // Dùng Generic <T> để có thể áp dụng cho cả Sinh viên, Lớp học, Môn học...
    public class PagedResult<T>
    {
        // Danh sách dữ liệu của trang hiện tại
        public List<T> Items { get; set; }

        // Tổng số bản ghi trong Database (để tính số trang)
        public int TotalRecords { get; set; }

        // Trang hiện tại (0 hoặc 1 based)
        public int PageIndex { get; set; }

        // Kích thước trang (số dòng mỗi trang)
        public int PageSize { get; set; }

        // Tính toán tổng số trang
        public int TotalPages
        {
            get
            {
                if (PageSize == 0) return 0;
                return (int)Math.Ceiling((double)TotalRecords / PageSize);
            }
        }

        // Kiểm tra xem có trang trước/sau không (để enable/disable nút Next/Prev)
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PagedResult()
        {
            Items = new List<T>();
        }
    }
}