using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimPhongTro.Common;
using TimPhongTro.Models;

namespace TimPhongTro.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public LoginController()
        {
            _dbContext = new DatabaseContext();
        }

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            var session = Session["TBAdmin"];
            if (session != null)
            {
                return RedirectToAction("Index", "QLyTaiKhoan", new { area = "Admin" });
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(TBADMIN ad)
        {
            var userSession = new AdminLogin();
            var result = _dbContext.TBADMINs.SingleOrDefault(x => x.TaiKhoan == ad.TaiKhoan);
            if (string.IsNullOrEmpty(ad.TaiKhoan))
            {
                ViewBag.errorLogin1 = "Vui lòng nhập tài khoản!";
            }
            else if (string.IsNullOrEmpty(ad.Matkhau))
            {
                ViewBag.errorLogin1 = "Vui lòng nhập mật khẩu!";
            }
            else if (string.IsNullOrEmpty(ad.Matkhau) && string.IsNullOrEmpty(ad.TaiKhoan))
            {
                ViewBag.errorLogin1 = "Vui lòng nhập tài khoản và mật khẩu!";
            }
            else if (result == null)
            {
                ViewBag.errorLogin1 = "Tài khoản không tồn tại!";
            }
            else if (result.Matkhau != ad.Matkhau)
            {
                ViewBag.errorLogin1 = "Mật khẩu không đúng!";
            }
            else
            {
                Session["TBAdmin"] = result.Ten;
                ViewBag.tennd = ad.Ten;
                userSession.Ten = result.Ten;
                userSession.UserID = result.ID;
                userSession.UserName = result.TaiKhoan;
                Session.Add(CommonConstants.ADMIN_SESSION, userSession);
                return RedirectToAction("Index", "QLyTaiKhoan", new { area = "Admin" });
            }
            return View();
        }
    }
}