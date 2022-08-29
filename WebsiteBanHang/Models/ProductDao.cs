using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Models
{
   
    public class ProductDao
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        public Product ViewDetail(int Id)
        {
            return objWebsiteBanHangEntities.Products.FirstOrDefault(x=>x.Id==Id);
        }
        public List<Product> SearchByKey(string key)
        {
            return objWebsiteBanHangEntities.Products.SqlQuery("Select * From Product Where Name like N'%"+key+"%'").ToList();
        }
    }
    
}