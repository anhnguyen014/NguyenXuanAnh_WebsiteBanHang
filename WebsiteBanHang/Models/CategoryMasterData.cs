using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class CategoryMasterData
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Tên Danh Mục")]
        public string Name { get; set; }
        [DisplayName("Hình Đại Diện")]
        public string Avatar { get; set; }
        [DisplayName("Slug")]
        public string Slug { get; set; }
        [DisplayName("Hiển Thị Trang Chủ")]
        public Nullable<bool> ShowOnHomPage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<bool> Deleted { get; set; }
        [DisplayName("Ngày Tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [DisplayName("Ngày Cập Nhật")]
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }

        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}