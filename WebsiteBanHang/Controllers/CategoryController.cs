﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using PagedList;

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
        public ActionResult ProductCategory( int Id, int? page)
        {
            if(page == null)
                page = 1;
            var listProduct = (from l in objWebsiteBanHangEntities.Products.Where(n => n.CategoryId == Id).OrderBy(n => n.Id) select l);
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(listProduct.ToPagedList(pageNumber,pageSize));
        }
    }
}