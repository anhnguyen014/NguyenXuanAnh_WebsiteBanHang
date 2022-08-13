using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using static WebsiteBanHang.Common;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class ProductsController : Controller

    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Products
        public ActionResult Index()
        {
            var listProduct = objWebsiteBanHangEntities.Products.ToList();
            return View(listProduct);
        }

        public ActionResult Details(int Id)
        {
            var objProduct = objWebsiteBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]

        public ActionResult Create()
        {
            Common objCommon = new Common();
            //get data category to DB
            var listCat = objWebsiteBanHangEntities.Categories.ToList();
            //convert to select list type value,text

            ViewBag.ListCategory = new SelectList(listCat, "Id", "Name");
            //get data brand to DB
            var listBrand = objWebsiteBanHangEntities.Brands.ToList();
            ViewBag.ListBrand = new SelectList(listBrand, "Id", "Name");

            //convert to select list type value,text
            //ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            ////Loại sản phẩm
            //List<ProductType> listProductType = new List<ProductType>();
            //ProductType objProductType = new ProductType();
            //objProductType.Id = 01;
            //objProductType.Name = "Giảm giá sốc";
            //listProductType.Add(objProductType);

            //objProductType = new ProductType();
            //objProductType.Id = 02;
            //objProductType.Name = "Đề xuất";
            //listProductType.Add(objProductType);

            //get data brand to DB
            // DataTable dtProductType = converter.ToDataTable(listProductType);
            //convert to select list type value,text
           // ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]

        public ActionResult Create(Product objProduct)
        {
            
            if (ModelState.IsValid)
            {
                var listCat = objWebsiteBanHangEntities.Categories.ToList();
                ViewBag.ListCategory=new SelectList(listCat, "Id", "Name");

                var listBrand = objWebsiteBanHangEntities.Brands.ToList();
                ViewBag.ListBrand = new SelectList(listBrand, "Id", "Name");
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
                    
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objProduct);

        }
        //void loadData()
        //{
        //    Common objCommon = new Common();
        //    //get data category to DB
        //    var listCat = objWebsiteBanHangEntities.Categories.ToList();
        //    //convert to select list type value,text
        //    ListtoDataTableConverter converter = new ListtoDataTableConverter();
        //    DataTable dtCategory = converter.ToDataTable(listCat);
        //    ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

        //    //get data brand to DB
        //    var listBrand = objWebsiteBanHangEntities.Brands.ToList();
        //    DataTable dtBrand = converter.ToDataTable(listBrand);
        //    //convert to select list type value,text
        //    ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

        //    //Loại sản phẩm
        //    List<ProductType> listProductType = new List<ProductType>();
        //    ProductType objProductType = new ProductType();
        //    objProductType.Id = 01;
        //    objProductType.Name = "Giảm giá sốc";
        //    listProductType.Add(objProductType);

        //    objProductType = new ProductType();
        //    objProductType.Id = 02;
        //    objProductType.Name = "Đề xuất";
        //    listProductType.Add(objProductType);

        //    //get data brand to DB
        //    DataTable dtProductType = converter.ToDataTable(listProductType);
        //    //convert to select list type value,text
        //    ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        //}
    }
}

    
