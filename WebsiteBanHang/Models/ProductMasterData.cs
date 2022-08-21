using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public partial class ProductMasterData
    {

        public int Id { get; set; }

        [Required]
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }

        [DisplayName("Hình đại diện")]
        public string Avatar { get; set; }

        [DisplayName("Danh mục")]
        public Nullable<int> CategoryId { get; set; }
        [DisplayName("Mô tả ngắn")]
        public string ShortDes { get; set; }
        [DisplayName("Mô tả đầy đủ")]
        public string FullDescription { get; set; }
        [DisplayName("Giá")]
        public Nullable<double> Price { get; set; }
        [DisplayName("Giá khuyến mãi")]
        public Nullable<double> PriceDiscount { get; set; }
        [DisplayName("Loại")]
        public Nullable<int> TypeId { get; set; }
        public string Slug { get; set; }
        [DisplayName("Thương hiệu")]
        public Nullable<int> BrandId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        [DisplayName("Hiển thị trang chủ")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        [DisplayName("Ngày tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [DisplayName("Ngày update")]
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }

        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}