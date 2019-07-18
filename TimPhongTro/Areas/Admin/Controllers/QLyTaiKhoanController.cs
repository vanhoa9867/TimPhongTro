using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimPhongTro.Models;

namespace TimPhongTro.Areas.Admin.Controllers
{
    public class QLyTaiKhoanController : ABaseController
    {
        private readonly DatabaseContext _dbContext;

        public QLyTaiKhoanController()
        {
            _dbContext = new DatabaseContext();
        }
        // GET: Admin/QLyTaiKhoan
        public ActionResult Index()
        {
            PHONGTRO pt = new PHONGTRO();
            ViewBag.resultND = _dbContext.NGUOIDUNGs.Count();
            ViewBag.total = _dbContext.PHONGTROes.Count();
            ViewBag.result = _dbContext.PHONGTROes.Where(x => x.TinhTrang == "Chưa duyệt").Count();
            return View();
        }
        public ActionResult NguoiDung()
        {
            List<NGUOIDUNG> lnd = new List<NGUOIDUNG>();
            lnd = _dbContext.NGUOIDUNGs.ToList();
            return View(lnd);

        }
        [HttpGet]
        public ActionResult ThemMoiNguoiDung()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiNguoiDung(NGUOIDUNG nd)
        {
            var result = _dbContext.NGUOIDUNGs.SingleOrDefault(a => a.TaiKhoan == nd.TaiKhoan);
            NGUOIDUNG x = new NGUOIDUNG();
            if (string.IsNullOrEmpty(nd.TenKH))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (string.IsNullOrEmpty(nd.TaiKhoan))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (string.IsNullOrEmpty(nd.MatKhau))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (string.IsNullOrEmpty(nd.Sdt))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (string.IsNullOrEmpty(nd.GioiTinh))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (string.IsNullOrEmpty(nd.DiaChi))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (string.IsNullOrEmpty(nd.Email))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (result != null)
            {
                ViewBag.erorAddUsers2 = "Tài khoản đã được sử dụng!";

            }
            else
            {
                x.TenKH = nd.TenKH;
                x.TaiKhoan = nd.TaiKhoan;
                x.MatKhau = nd.MatKhau;
                x.GioiTinh = nd.GioiTinh;
                x.DiaChi = nd.DiaChi;
                x.Email = nd.Email;
                x.Sdt = nd.Sdt;
                _dbContext.NGUOIDUNGs.Add(x);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "QLyTaiKhoan");
            }
            return this.ThemMoiNguoiDung();
        }

        [HttpGet]
        public ActionResult XoaNguoiDung(int id)
        {
            NGUOIDUNG result = _dbContext.NGUOIDUNGs.Find(id);
            if (result == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(result);
        }

        [HttpPost, ActionName("XoaNguoiDung")]
        public ActionResult XacNhanXoa(int id)
        {
            NGUOIDUNG result = _dbContext.NGUOIDUNGs.Find(id);
            if (result == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            _dbContext.NGUOIDUNGs.Remove(result);
            _dbContext.SaveChanges();
            return RedirectToAction("Nguoidung", "QLyTaiKhoan");
        }

        [HttpGet]
        public ActionResult SuaNguoiDung(int id)
        {
            NGUOIDUNG result = _dbContext.NGUOIDUNGs.SingleOrDefault(x => x.MaKH == id);
            return View(result);
        }

        [HttpPost]
        public ActionResult SuaNguoiDung(NGUOIDUNG nd, int id)
        {
            NGUOIDUNG result = _dbContext.NGUOIDUNGs.SingleOrDefault(x => x.MaKH == id);
            result.TenKH = nd.TenKH;
            result.TaiKhoan = nd.TaiKhoan;
            result.Sdt = nd.Sdt;
            result.Email = nd.Email;
            result.DiaChi = nd.DiaChi;
            result.GioiTinh = nd.GioiTinh;
            UpdateModel(result);
            _dbContext.SaveChanges();
            return RedirectToAction("NguoiDung", "QLyTaiKhoan");
        }
    }
}