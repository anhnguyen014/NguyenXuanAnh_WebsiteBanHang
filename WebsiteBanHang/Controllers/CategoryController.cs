using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{
    public class CategoryController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Category
        public ActionResult Index()
        {
            var listCategory = objWebsiteBanHangEntities.Categories.ToList();
            return View(listCategory);
        }
        public ActionResult ProductCategory( int Id)
        {
            var listProduct= objWebsiteBanHangEntities.Products.Where(n=>n.CategoryId== Id).ToList();
            return View(listProduct);
        }
    }
}