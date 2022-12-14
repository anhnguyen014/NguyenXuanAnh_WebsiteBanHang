using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class BrandMasterData
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Tên Thương Hiệu")]
        public string Name { get; set; }
        [DisplayName("Hình Đại Diện")]
        public string Avatar { get; set; }
        [DisplayName("Slug")]
        public string Slug { get; set; }
        [DisplayName("Hiển Thị Trang Chủ")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        [DisplayName("Ngày Tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [DisplayName("Ngày Cập Nhật")]
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
        public Nullable<bool> Deleted { get; set; }
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}
