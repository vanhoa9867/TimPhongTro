using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimPhongTro.Common;
using TimPhongTro.Models;

namespace TimPhongTro.Controllers
{
    public class DangTinController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public DangTinController()
        {
            _dbContext = new DatabaseContext();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult DangTinNgay()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PhongTro()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeAccount]
        public ActionResult PhongTro(PHONGTRO pt, HttpPostedFileBase Anh)
        {
            var result = _dbContext.PHONGTROes.SingleOrDefault(x => x.MaPhong == pt.MaPhong);
            PHONGTRO n = new PHONGTRO();
            int id = Int32.Parse(Session["MaKh"].ToString());

            if (string.IsNullOrEmpty(pt.SoPhong))
            {
                ViewBag.error1 = "Nhập số phòng";
            }
            else if (string.IsNullOrEmpty(pt.DienTich))
            {
                ViewBag.error1 = "Nhập diện tích";
            }
            else if (string.IsNullOrEmpty(pt.DiaChi))
            {
                ViewBag.error1 = "Nhập địa chỉ";
            }
            else if (string.IsNullOrEmpty(pt.GiaThue))
            {
                ViewBag.error1 = "Nhập giá thuê";
            }
            else if (result == null)
            {
                try
                {
                    if (Anh != null && Anh.ContentLength > 0)
                    {
                        var fileName = StringRandom.getRandomString(5) + Path.GetFileName(Anh.FileName);
                        var image_path = "uploads/";
                        var path = Server.MapPath("~/uploads/");
                        Anh.SaveAs(path + fileName);

                        n.SoPhong = pt.SoPhong;
                        n.DienTich = pt.DienTich;
                        n.DiaChi = pt.DiaChi;
                        n.GiaThue = pt.GiaThue;
                        n.TinhTrang = "Chưa duyệt";
                        n.MoTa = pt.MoTa;
                        n.Loai = "Phòng trọ";
                        n.NgayCapNhat = DateTime.Now;
                        n.MaKH = id;
                        n.Anh = image_path + fileName;
                        _dbContext.PHONGTROes.Add(n);
                        ViewBag.error1 = "Tin phòng trọ của bạn đã được gửi thành công";
                        _dbContext.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewBag.error1 = e.Message;
                }
            }
            else
            {
                ViewBag.error1 = "Phòng đã được đăng ký trước đó";
                return View();
            }
            return View();
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult OGhep()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeAccount]
        public ActionResult OGhep(PHONGTRO pt, HttpPostedFileBase Anh)
        {
            var result = _dbContext.PHONGTROes.SingleOrDefault(x => x.MaPhong == pt.MaPhong);
            PHONGTRO n = new PHONGTRO();
            int id = Int32.Parse(Session["MaKh"].ToString());

            if (string.IsNullOrEmpty(pt.SoPhong))
            {
                ViewBag.error1 = "Nhập số phòng";
            }
            else if (string.IsNullOrEmpty(pt.DienTich))
            {
                ViewBag.error1 = "Nhập diện tích";
            }
            else if (string.IsNullOrEmpty(pt.DiaChi))
            {
                ViewBag.error1 = "Nhập địa chỉ";
            }
            else if (string.IsNullOrEmpty(pt.GiaThue))
            {
                ViewBag.error1 = "Nhập giá thuê";
            }
            else if (result == null)
            {
                try
                {

                    if (Anh != null && Anh.ContentLength > 0)
                    {
                        var fileName = StringRandom.getRandomString(5) + Path.GetFileName(Anh.FileName);
                        var image_path = "uploads/";
                        var path = Server.MapPath("~/uploads/");
                        Anh.SaveAs(path + fileName);

                        n.SoPhong = pt.SoPhong;
                        n.DienTich = pt.DienTich;
                        n.DiaChi = pt.DiaChi;
                        n.GiaThue = pt.GiaThue;
                        n.TinhTrang = "Chưa duyệt";
                        n.SoNguoiO = pt.SoNguoiO;
                        n.MoTa = pt.MoTa;
                        n.MaKH = id;
                        n.Loai = "Ở ghép";
                        n.NgayCapNhat = DateTime.Now;
                        n.Anh = pt.Anh;
                        _dbContext.PHONGTROes.Add(n);
                        ViewBag.errorsignup1 = "Tin ở ghép của bạn đã được gửi thành công";
                        _dbContext.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewBag.error1 = e.Message;
                }
            }
            else
            {
                ViewBag.error1 = "Phòng đã được đăng ký trước đó";
                return View();
            }
            ViewBag.error1 = "Đăng tin thành công";
            return View();
        }
    }
}