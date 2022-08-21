using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebsiteBanHang.Context;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Context
{
   [MetadataType (typeof(ProductMasterData))]
   public partial class Product
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

    [MetadataType(typeof(BrandMasterData))]
    public partial class Brand
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}