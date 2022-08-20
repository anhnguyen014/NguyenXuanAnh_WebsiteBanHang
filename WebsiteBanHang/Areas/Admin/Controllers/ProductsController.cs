using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

using static WebsiteBanHang.Common;
using static WebsiteBanHang.Context.WebsiteBanHangEntities;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class ProductsController : Controller

    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Products
        [HttpGet]
        public ActionResult Index(string currentFilter,string SearchText,int? page)
        {
            List<Product> products;
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
                products=objWebsiteBanHangEntities.Products.Where(p=>p.Name.Contains(SearchText)).ToList();
            }        
            else
            {
                products = objWebsiteBanHangEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchText;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            products = products.OrderByDescending(n => n.Id).ToList();
            return View(products.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Details(int Id)
        {
            var objProduct = objWebsiteBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
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

        public ActionResult Create(Product objProduct)
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
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        //tenhinh
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        //png
                        fileName = fileName + extension;
                        //tenhinh.png
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"),fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objWebsiteBanHangEntities.Products.Add(objProduct);
                    objWebsiteBanHangEntities.SaveChanges();
                    TempData["message"] = new XMessage("success", "Thêm Thành Công!");
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objProduct);

        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            this.loadData();
            using(WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities())

            return View(objWebsiteBanHangEntities.Products.Where(n=>n.Id==Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(Product objProduct)
        {
            this.loadData();
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                //tenhinh
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                //png
                fileName = fileName + extension;
                //tenhinh.png
                objProduct.Avatar = fileName;
                objProduct.UpdatedOnUtc = DateTime.Now;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
            }
            objWebsiteBanHangEntities.Entry(objProduct).State = EntityState.Modified;
            objWebsiteBanHangEntities.SaveChanges();
            TempData["message"] = new XMessage("success", "Cập Nhật Thành Công!");
            return RedirectToAction("Index");
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
    }
}

    
