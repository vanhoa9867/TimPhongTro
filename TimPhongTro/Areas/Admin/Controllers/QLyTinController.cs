using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimPhongTro.Models;

namespace TimPhongTro.Areas.Admin.Controllers
{
    public class QLyTinController : ABaseController
    {
        private readonly DatabaseContext _dbContext;

        public QLyTinController()
        {
            _dbContext = new DatabaseContext();
        }
        // GET: Admin/QLyTin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TinPhongTro()
        {
            List<PHONGTRO> lnd = new List<PHONGTRO>();
            lnd = _dbContext.PHONGTROes.ToList();
            return View(lnd);

        }

        public ActionResult TinOGhep()
        {
            List<PHONGTRO> lnd = new List<PHONGTRO>();
            lnd = _dbContext.PHONGTROes.ToList();
            return View(lnd);

        }

        public ActionResult DonThue(string searchString)
        {
            List<DONTHUE> ldt = new List<DONTHUE>();
            if (!string.IsNullOrEmpty(searchString))
            {
                ldt = _dbContext.DONTHUEs.Where(x => x.TinhTrang.Contains(searchString) || x.NgayHen.Contains(searchString) && x.MaPhong != null).ToList();
            }
            else
            {
                ldt = _dbContext.DONTHUEs.Where(x => x.MaPhong != null).ToList();
            }
            ViewBag.searchString = searchString;
            return View(ldt);
        }

        [HttpGet]
        public ActionResult SuaTinPhong(int id)
        {
            PHONGTRO result = _dbContext.PHONGTROes.SingleOrDefault(x => x.MaPhong == id);
            return View(result);
        }

        [HttpPost]
        public ActionResult SuaTinPhong(PHONGTRO nd, int id)
        {
            PHONGTRO result = _dbContext.PHONGTROes.SingleOrDefault(x => x.MaPhong == id);
            result.SoPhong = nd.SoPhong;
            result.DienTich = nd.DienTich;
            result.DiaChi = nd.DiaChi;
            result.GiaThue = nd.GiaThue;
            result.DaNhan = nd.DaNhan;
            if (result.Loai == "Ở ghép")
            {
                result.SoNguoiO = nd.SoNguoiO;
            }
            result.NgayCapNhat = DateTime.Now;
            UpdateModel(result);
            _dbContext.SaveChanges();
            if (result.Loai == "Ở ghép")
            {
                return RedirectToAction("TinOGhep", "QLyTin");
            }
            return RedirectToAction("TinPhongTro", "QLyTin");
        }

        [HttpGet]
        public ActionResult XoaTinPhong(int id)
        {
            PHONGTRO result = _dbContext.PHONGTROes.Find(id);
            if (result == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(result);
        }

        [HttpPost, ActionName("XoaTinPhong")]
        public ActionResult XacNhanXoa(int id)
        {
            PHONGTRO result = _dbContext.PHONGTROes.Find(id);
            if (result == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            _dbContext.PHONGTROes.Remove(result);
            _dbContext.SaveChanges();
            return RedirectToAction("TinPhongTro", "QLyTin");
        }

        [HttpGet]
        public ActionResult ThemMoiPhong()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiPhong(PHONGTRO nd)
        {
            var result = _dbContext.PHONGTROes.SingleOrDefault(a => a.MaPhong == nd.MaPhong);
            PHONGTRO x = new PHONGTRO();
            if (string.IsNullOrEmpty(nd.SoPhong))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (string.IsNullOrEmpty(nd.SoPhong))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (string.IsNullOrEmpty(nd.DienTich))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (string.IsNullOrEmpty(nd.DiaChi))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (string.IsNullOrEmpty(nd.GiaThue))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (string.IsNullOrEmpty(nd.MoTa))
            {
                ViewBag.erorAddUsers1 = "Nhập đầy đủ thông tin!";
            }
            else if (result != null)
            {
                ViewBag.erorAddUsers2 = "Tài khoản đã được sử dụng!";

            }
            else
            {
                x.SoPhong = nd.SoPhong;
                x.DiaChi = nd.DiaChi;
                x.GiaThue = nd.GiaThue;
                x.DaNhan = null;
                x.Loai = nd.Loai;
                x.SoNguoiO = nd.SoNguoiO;
                x.NgayCapNhat = DateTime.Now;
                x.TinhTrang = null;
                _dbContext.PHONGTROes.Add(x);
                _dbContext.SaveChanges();
                return RedirectToAction("TinPhongTro", "QLyTin");
            }
            return this.ThemMoiPhong();
        }

        public ActionResult Duyet (int id)
        {
            var result = _dbContext.PHONGTROes.SingleOrDefault(x => x.MaPhong == id);
            if (result == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            result.TinhTrang = "Đã duyệt";
            UpdateModel(result);
            _dbContext.SaveChanges();
            if(result.Loai == "Ở ghép")
            {
                return RedirectToAction("TinOGhep","QLyTin");

            }
            else if (result.Loai == "Phòng trọ")
            {
                return RedirectToAction("TinPhongTro", "QLyTin");
            }
            return RedirectToAction("Index", "QLyTaiKhoan");
        }
    }
}