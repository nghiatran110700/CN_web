using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Entity;

namespace Web.Controllers
{
   // [AllowAnonymous]
    public class LoginController : Controller
    {
        ShopModel db = new ShopModel();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string username = form["username"];
            string password = form["password"];
            foreach(var item in db.Accounts)
            {
                if(item.username.Equals(username) && item.password.Equals(password))
                {
                    if(item.status == true)
                    {
                        Session["fullname"] = item.fullname;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        Session["fullname"] = item.fullname;
                        return RedirectToAction("Index", "Customer");
                    }
                }
                else
                {
                    ViewBag.eror = "Thông Tin mật Khẩu Hoặc Tài Khoản Không Chính Xác";
                }
            }
                    return View();
            
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            Account acc = new Account();
            string FirtName = form["FirtName"];
            string LastName = form["LastName"];
            string Email = form["Email"];
            string password = form["password"];
            string RepeatPassword = form["RepeatPassword"];
            var result = db.Accounts.Where(s => s.username == Email).SingleOrDefault();
            if(!string.IsNullOrEmpty(Email) &&!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(RepeatPassword) && password== RepeatPassword)
            {
                if (result != null)
                {
                    ViewBag.user = "Email Đã Được Đăng Ký";
                    return View();
                }
                else
                {
                    acc.fullname = FirtName +" "+ LastName;
                    acc.username = Email;
                    acc.password = password;
                    acc.status = false;
                    db.Accounts.Add(acc);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Login");

                }
            }
            else
            {
                ViewBag.eror = "Đăng Ký Thất Bại Mời Bạn Nhập Lại!!!";
                return View();
            }
        }
    }
}