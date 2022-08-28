using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Models
{
    
    public class CategoryDao
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        public List<Category> getRow(int?Id)
        {
            if (Id == null)
            {
                return null;
            }
            else
            {
                return objWebsiteBanHangEntities.Categories.Where(c => c.Id == Id).ToList();
            }
        }

    }
}