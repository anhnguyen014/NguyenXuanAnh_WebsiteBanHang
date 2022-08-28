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
    public class BrandsController : Controller

    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Brands
        [HttpGet]
        public ActionResult Index(string currentFilter,string SearchText,int? page)
        {
            List<Brand> brands;
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
                brands = objWebsiteBanHangEntities.Brands.Where(p=>p.Name.Contains(SearchText)).ToList();
            }        
            else
            {
                brands = objWebsiteBanHangEntities.Brands.ToList();
            }
            ViewBag.CurrentFilter = SearchText;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            brands = brands.OrderByDescending(n => n.Id).ToList();
            return View(brands.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Details(int Id)
        {
            var objProduct = objWebsiteBanHangEntities.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]

        public ActionResult Create()
        {
           this.loadData();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]

        public ActionResult Create(Brand objBrand)
        {
            this.loadData();
            if (ModelState.IsValid)
            {
                
                //var listCat = objWebsiteBanHangEntities.Categories.ToList();
                //ViewBag.ListCategory=new SelectList(listCat, "Id", "Name");

                //var listBrand = objWebsiteBanHangEntities.Brands.ToList();
                //ViewBag.ListBrand = new SelectList(listBrand, "Id", "Name");
                try
                {
                   // this.loadData();
                    if (objBrand.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                        //tenhinh
                        string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                        //png
                        fileName = fileName + extension;
                        //tenhinh.png
                        objBrand.Avatar = fileName;
                        objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brand"),fileName));
                    }
                    objBrand.CreatedOnUtc = DateTime.Now;
                    objWebsiteBanHangEntities.Brands.Add(objBrand);
                    objWebsiteBanHangEntities.SaveChanges();
                    TempData["message"] = new XMessage("success", "Thêm Thành Công!");
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objBrand);

        }

        [HttpGet]

        public ActionResult Edit(int Id)
        {
            this.loadData();
            var objBrands = objWebsiteBanHangEntities.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objBrands);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Brand objBrands)
        {
            //this.loadData();
            if (objBrands.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrands.ImageUpload.FileName);
                //tenhinh
                string extension = Path.GetExtension(objBrands.ImageUpload.FileName);
                //png
                fileName = fileName + extension;
                //tenhinh.png
                objBrands.Avatar = fileName;
                objBrands.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brand"), fileName));
            }
           
            objBrands.UpdatedOnUtc = DateTime.Now;
           
            objWebsiteBanHangEntities.Entry(objBrands).State = EntityState.Modified;
            objWebsiteBanHangEntities.SaveChanges();
            TempData["message"] = new XMessage("success", "Cập Nhật Thành Công!");
            return RedirectToAction("Index");
        }

        [HttpGet]
       public ActionResult Delete(int Id)
        {
            this.DeleteBrand(Id);
            return RedirectToAction("Index");
        }
        public ActionResult ShowOnPage(int? Id)
        {
            if (Id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã Nhà Cung Cấp Không Tồn Tại!");
                return RedirectToAction("Index", "Brands");
            }
            Brand brands = objWebsiteBanHangEntities.Brands.Where(n=>n.Id == Id).FirstOrDefault();
            if (brands == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
                return RedirectToAction("Index", "Brands");
            }
            brands.ShowOnHomePage = (brands.ShowOnHomePage == true) ? false : true;
            objWebsiteBanHangEntities.Entry(brands).State = EntityState.Modified;
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
        public bool DeleteBrand(int Id)
        {
            using (var db = new WebsiteBanHangEntities())
            {
                var brands = db.Brands.FirstOrDefault(x => x.Id == Id);
                if (brands != null)
                {
                    db.Brands.Remove(brands);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

    }
}

    
