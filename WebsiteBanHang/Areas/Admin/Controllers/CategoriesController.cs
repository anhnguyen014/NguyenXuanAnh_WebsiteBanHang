using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using static WebsiteBanHang.Common;
using static WebsiteBanHang.Context.WebsiteBanHangEntities;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class CategoriesController : Controller

    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Categories
        [HttpGet]
        public ActionResult Index(string currentFilter,string SearchText,int? page)
        {
            List<Category> categories;
            if(SearchText!=null)
            {
                page = 1;

            }
            else
            {
                SearchText = currentFilter;
            }
            if(!string.IsNullOrEmpty(SearchText))

            {
                categories = objWebsiteBanHangEntities.Categories.Where(p=>p.Name.Contains(SearchText)).ToList();
            }        
            else
            {
                categories = objWebsiteBanHangEntities.Categories.ToList();
            }
            ViewBag.CurrentFilter = SearchText;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            categories = categories.OrderByDescending(n => n.Id).ToList();
            return View(categories.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Details(int Id)
        {
            var objCate = objWebsiteBanHangEntities.Categories.Where(n => n.Id == Id).FirstOrDefault();
            return View(objCate);
        }
        [HttpGet]

        public ActionResult Create()
        {
           //this.loadData();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]

        public ActionResult Create(Category objCate)
        {
           // this.loadData();
            if (ModelState.IsValid)
            {
                
                //var listCat = objWebsiteBanHangEntities.Categories.ToList();
                //ViewBag.ListCategory=new SelectList(listCat, "Id", "Name");

                //var listBrand = objWebsiteBanHangEntities.Brands.ToList();
                //ViewBag.ListBrand = new SelectList(listBrand, "Id", "Name");
                try
                {
                   // this.loadData();
                    if (objCate.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCate.ImageUpload.FileName);
                        //tenhinh
                        string extension = Path.GetExtension(objCate.ImageUpload.FileName);
                        //png
                        fileName = fileName + extension;
                        //tenhinh.png
                        objCate.Avatar = fileName;
                        objCate.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category"),fileName));
                    }
                    objCate.CreatedOnUtc = DateTime.Now;
                    objWebsiteBanHangEntities.Categories.Add(objCate);
                    objWebsiteBanHangEntities.SaveChanges();
                    TempData["message"] = new XMessage("success", "Thêm Thành Công!");
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objCate);

        }

        [HttpGet]

        public ActionResult Edit(int Id)
        {
            //this.loadData();
            var objCate = objWebsiteBanHangEntities.Categories.Where(n => n.Id == Id).FirstOrDefault();
            return View(objCate);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            //this.loadData();
            if (category.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(category.ImageUpload.FileName);
                //tenhinh
                string extension = Path.GetExtension(category.ImageUpload.FileName);
                //png
                fileName = fileName + extension;
                //tenhinh.png
                category.Avatar = fileName;
                category.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category"), fileName));
            }

            category.UpdatedOnUtc = DateTime.Now;
            objWebsiteBanHangEntities.Entry(category).State = EntityState.Modified;
            objWebsiteBanHangEntities.SaveChanges();
            TempData["message"] = new XMessage("success", "Cập Nhật Thành Công!");
            return RedirectToAction("Index");
        }

        [HttpGet]
       public ActionResult Delete(int Id)
        {
            this.DeleteCate(Id);
            return RedirectToAction("Index");
        }
        public ActionResult ShowOnPage(int? Id)
        {
            if (Id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Nhà Cung Cấp Không Tồn Tại!");
                return RedirectToAction("Index", "Brands");
            }
            Category category = objWebsiteBanHangEntities.Categories.Where(n=>n.Id == Id).FirstOrDefault();
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Brands");
            }
            category.ShowOnHomPage = (category.ShowOnHomPage == true) ? false : true;
            objWebsiteBanHangEntities.Entry(category).State = EntityState.Modified;
            objWebsiteBanHangEntities.SaveChanges();
            TempData["message"] = new XMessage("success", "Thay Đổi Trạng Thái Thành Công!");
            return RedirectToAction("Index", "Brands");
        }

        void loadData()
        {
            Common objCommon = new Common();
            //get data category to DB
            var listCat = objWebsiteBanHangEntities.Categories.ToList();
            //convert to select list type value,text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(listCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            //get data brand to DB
            var listBrand = objWebsiteBanHangEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(listBrand);
            //convert to select list type value,text
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            //Loại sản phẩm
            List<ProductType> listProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            listProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            listProductType.Add(objProductType);

            //get data brand to DB
            DataTable dtProductType = converter.ToDataTable(listProductType);
            //convert to select list type value,text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }
        public bool DeleteCate(int Id)
        {
            using (var db = new WebsiteBanHangEntities())
            {
                var category = db.Categories.FirstOrDefault(x => x.Id == Id);
                if (category != null)
                {
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

    }
}

    
