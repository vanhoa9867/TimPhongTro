using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimPhongTro.Common;
using TimPhongTro.Models;

namespace TimPhongTro.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public NguoiDungController()
        {
            _dbContext = new DatabaseContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult DangNhap()
        {
            var session = Session["NguoiDung"];
            if (Session == null || session == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DangNhap(NGUOIDUNG nd)
        {
            var userSession = new UserLogin();
            var result = _dbContext.NGUOIDUNGs.SingleOrDefault(x => x.TaiKhoan == nd.TaiKhoan);
            if (string.IsNullOrEmpty(nd.TaiKhoan))
            {
                ViewBag.errorLogin1 = "<div class=\"alert alert-danger\" role=\"alert\">Vui lòng nhập tài khoản!</div>";
            }
            else if (string.IsNullOrEmpty(nd.MatKhau))
            {
                ViewBag.errorLogin1 = "<div class=\"alert alert-danger\" role=\"alert\">Vui lòng nhập mật khẩu!</div>";
            }
            else if (string.IsNullOrEmpty(nd.MatKhau) && string.IsNullOrEmpty(nd.TaiKhoan))
            {
                ViewBag.errorLogin1 = "<div class=\"alert alert-danger\" role=\"alert\">Vui lòng nhập tài khoản và mật khẩu!</div>";
            }
            else if (result == null)
            {
                ViewBag.errorLogin1 = "<div class=\"alert alert-danger\" role=\"alert\">Tài khoản không tồn tại!</div>";
            }
            else if (result.MatKhau != StringHash.crypto(nd.MatKhau))
            {
                ViewBag.errorLogin1 = "<div class=\"alert alert-danger\" role=\"alert\">Mật khẩu không đúng!</div>";
            }
            else
            {
                userSession.UserName = result.TaiKhoan;
                userSession.UserID = result.MaKH;
                userSession.Ten = result.TenKH;
                Session["NguoiDung"] = result.TaiKhoan;
                Session["MaKh"] = result.MaKH;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                ViewBag.tennd = nd.TenKH;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult DangKy()
        {
            var session = Session["NguoiDung"];
            if (Session == null || session == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DangKy(NGUOIDUNG nd)
        {
            var result = _dbContext.NGUOIDUNGs.SingleOrDefault(x => x.TaiKhoan == nd.TaiKhoan);
            NGUOIDUNG n = new NGUOIDUNG();
            if (string.IsNullOrEmpty(nd.TenKH))
            {
                ViewBag.errorsignup1 = "<div class=\"alert alert-danger\" role=\"alert\">Họ tên không được để trống</div>";
            }
            else if (string.IsNullOrEmpty(nd.TaiKhoan))
            {
                ViewBag.errorsignup1 = "<div class=\"alert alert-danger\" role=\"alert\">Nhập tên tài khoản</div>";
            }
            else if (string.IsNullOrEmpty(nd.Email))
            {
                ViewBag.errorsignup1 = "<div class=\"alert alert-danger\" role=\"alert\">Nhập email</div>";
            }
            else if (string.IsNullOrEmpty(nd.Sdt))
            {
                ViewBag.errorsignup1 = "<div class=\"alert alert-danger\" role=\"alert\">Nhập số điện thoại</div>";
            }
            else if (string.IsNullOrEmpty(nd.MatKhau))
            {
                ViewBag.errorsignup1 = "<div class=\"alert alert-danger\" role=\"alert\">Nhập mật khẩu</div>";
            }
            else if (string.IsNullOrEmpty(nd.GioiTinh))
            {
                ViewBag.errorsignup1 = "<div class=\"alert alert-danger\" role=\"alert\">Nhập giới tính</div>";
            }
            else if (result == null)
            {
                n.TenKH = nd.TenKH;
                n.Email = nd.Email;
                n.Sdt = nd.Sdt;
                n.TaiKhoan = nd.TaiKhoan;
                n.MatKhau = StringHash.crypto(nd.MatKhau);
                n.GioiTinh = nd.GioiTinh;
                n.DiaChi = nd.DiaChi;
                _dbContext.NGUOIDUNGs.Add(n);
                _dbContext.SaveChanges();
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            else
            {
                ViewBag.errorsignup1 = "<div class=\"alert alert-danger\" role=\"alert\">Tài khoản đã có người sử dụng!</div>";
            }
            return View();
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private NGUOIDUNG getNguoiDungBySession(string taikhoan)
        {
            return _dbContext.NGUOIDUNGs.SingleOrDefault(x => x.TaiKhoan == taikhoan);
        }

        private List<DONTHUE> listDonThueBySession(int mand)
        {
            return _dbContext.DONTHUEs.OrderByDescending(x => x.NgayNhan).Where(x => x.MaPhong == mand).ToList();
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult ThongTin()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var nd = Session["TenNguoiDung"];
            NGUOIDUNG nguoidung = getNguoiDungBySession(session.UserName);
            List<DONTHUE> donThue = new List<DONTHUE>();
            donThue = listDonThueBySession(session.UserID);
            ViewBag.tennd = nguoidung.TenKH;
            ViewBag.mand = nguoidung.MaKH;
            ViewBag.email = nguoidung.Email;
            ViewBag.diachi = nguoidung.DiaChi;
            ViewBag.sdt = nguoidung.Sdt;
            ViewBag.gioitinh = nguoidung.GioiTinh;
            ViewBag.taikhoan = nguoidung.TaiKhoan;
            ViewBag.anh = nguoidung.Anh;
            return View(donThue);
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult SuaThongTin(int id)
        {
            NGUOIDUNG result = _dbContext.NGUOIDUNGs.SingleOrDefault(x => x.MaKH == id);
            return View(result);
        }

        [HttpPost]
        public ActionResult SuaThongTin(NGUOIDUNG nd, int id)
        {
            NGUOIDUNG result = _dbContext.NGUOIDUNGs.SingleOrDefault(x => x.MaKH == id);
            result.Sdt = nd.Sdt;
            result.DiaChi = nd.DiaChi;
            UpdateModel(result);
            _dbContext.SaveChanges();
            return RedirectToAction("ThongTin", "NguoiDung");
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult TinDaDang()
        {
            int id = Int32.Parse(Session["MaKh"].ToString());
            List<PHONGTRO> lnd = new List<PHONGTRO>();
            lnd = _dbContext.PHONGTROes.Where(x => x.MaKH == id).ToList();
            return View(lnd);

        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult ChinhSua (int id)
        {
            PHONGTRO result = _dbContext.PHONGTROes.SingleOrDefault(x => x.MaPhong == id);
            return View(result);
        }

        [HttpPost]
        public ActionResult ChinhSua (PHONGTRO pt, int id)
        {
            PHONGTRO result = _dbContext.PHONGTROes.SingleOrDefault(x => x.MaPhong == id);
            result.TinhTrang = "Chưa duyệt";
            result.GiaThue = pt.GiaThue;
            result.NgayCapNhat = DateTime.Now;
            result.SoPhong = pt.SoPhong;
            result.SoNguoiO = pt.SoNguoiO;
            result.MoTa = pt.MoTa;
            UpdateModel(result);
            _dbContext.SaveChanges();
            return RedirectToAction("TinDaDang", "NguoiDung");
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult ListDonThue ()
        {
            int id = Int32.Parse(Session["MaKh"].ToString());
            List<DONTHUE> ldt = new List<DONTHUE>();
            ldt = _dbContext.DONTHUEs.Where(x => x.MaKH == id).ToList();
            return View(ldt);
        }

        [HttpGet]
        public ActionResult FacebookDangNhap()
        {
            return View();
        }
    }
}