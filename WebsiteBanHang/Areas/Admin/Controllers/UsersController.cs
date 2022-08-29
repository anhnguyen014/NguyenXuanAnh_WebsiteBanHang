using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;
using static WebsiteBanHang.Common;
using static WebsiteBanHang.Context.WebsiteBanHangEntities;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class UsersController : Controller

    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Users
        [HttpGet]
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstUser = new List<User>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstUser = objWebsiteBanHangEntities.Users.Where(n => n.FirstName.Contains(SearchString)).ToList();
            }
            else
            {
                lstUser = objWebsiteBanHangEntities.Users.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstUser = lstUser.OrderByDescending(n => n.Id).ToList();
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Details(int Id)
        {
            var objUser = objWebsiteBanHangEntities.Users.Where(n => n.Id == Id).FirstOrDefault();
            return View(objUser);
        }
        [HttpGet]

        public ActionResult Create()
        {
           //this.loadData();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]

        public ActionResult Create(User objUser)
        {
            //this.loadData();
            if (ModelState.IsValid)
            {
                var result = objWebsiteBanHangEntities.Users.FirstOrDefault(n => n.Email == objUser.Email);
                if (result == null)
                {
                    objUser.Password = GetMD5(objUser.Password);
                    objWebsiteBanHangEntities.Users.Add(objUser);
                    objWebsiteBanHangEntities.SaveChanges();
                    TempData["message"] = new XMessage("success", "Cập Nhật Thành Công!");
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                } 
                
            }
            return View(objUser);

        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        [HttpGet]

        public ActionResult Edit(int Id)
        {
            //this.loadData();
            var objUser = objWebsiteBanHangEntities.Users.Where(n => n.Id == Id).FirstOrDefault();
            return View(objUser);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(User objUser)
        {
            //this.loadData();
            objWebsiteBanHangEntities.Entry(objUser).State = EntityState.Modified;
            objWebsiteBanHangEntities.SaveChanges();
            TempData["message"] = new XMessage("success", "Cập Nhật Thành Công!");
            return RedirectToAction("Index");
        }

        [HttpGet]
       public ActionResult Delete(int Id)
        {
            this.DeleteUser(Id);
            return RedirectToAction("Index");
        }
        //public ActionResult ShowOnPage(int? Id)
        //{
        //    if (Id == null)
        //    {
        //        TempData["message"] = new XMessage("danger", "Mã Nhà Cung Cấp Không Tồn Tại!");
        //        return RedirectToAction("Index", "Brands");
        //    }
        //    Brand brands = objWebsiteBanHangEntities.Brands.Where(n=>n.Id == Id).FirstOrDefault();
        //    if (brands == null)
        //    {
        //        TempData["message"] = new XMessage("danger", "Mẫu Tin Không Tồn Tại!");
        //        return RedirectToAction("Index", "Brands");
        //    }
        //    brands.ShowOnHomePage = (brands.ShowOnHomePage == true) ? false : true;
        //    objWebsiteBanHangEntities.Entry(brands).State = EntityState.Modified;
        //    objWebsiteBanHangEntities.SaveChanges();
        //    TempData["message"] = new XMessage("success", "Thay Đổi Trạng Thái Thành Công!");
        //    return RedirectToAction("Index", "Brands");
        //}

        public bool DeleteUser(int Id)
        {
            using (var db = new WebsiteBanHangEntities())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == Id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

    }
}

    
