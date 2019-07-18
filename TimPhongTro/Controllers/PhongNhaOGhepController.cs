using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TimPhongTro.Common;
using TimPhongTro.Models;

namespace TimPhongTro.Controllers
{
    public class PhongNhaOGhepController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public PhongNhaOGhepController()
        {
            _dbContext = new DatabaseContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        private List<PHONGTRO> getPhongTro()
        {
            return _dbContext.PHONGTROes.OrderByDescending(x => x.NgayCapNhat).Where(x => x.DaNhan != 1).Where(x => x.TinhTrang == "Đã duyệt").ToList();
        }

        public ActionResult TinPhongTro()
        {
            List<PHONGTRO> listPhongTro = new List<PHONGTRO>();
            listPhongTro = getPhongTro();
            return View(listPhongTro);
        }

        public ActionResult ChiTiet(int id)
        {
            PHONGTRO item = _dbContext.PHONGTROes.SingleOrDefault(x => x.MaPhong == id);
            NGUOIDUNG nd = _dbContext.NGUOIDUNGs.SingleOrDefault(x => x.MaKH == item.MaKH);
            ViewBag.tenkh = nd.TenKH;
            ViewBag.sdt = nd.Sdt;
            ViewBag.email = nd.Email;
            ViewBag.anh = nd.Anh;
            return View(item);
        }

        public ActionResult ChiTietPhong(int id)
        {
            PHONGTRO item = _dbContext.PHONGTROes.SingleOrDefault(x => x.MaPhong == id);
            NGUOIDUNG nd = _dbContext.NGUOIDUNGs.SingleOrDefault(x => x.MaKH == item.MaKH);

            ViewBag.SoPhong = item.SoPhong;
            ViewBag.DienTich = item.DienTich;
            ViewBag.DiaChi = item.DiaChi;
            ViewBag.Gia = item.GiaThue;
            ViewBag.TenKH = nd.TenKH;
            ViewBag.Sdt = nd.Sdt;
            ViewBag.Email = nd.Email;
            return View(item);
        }

        [HttpGet]
        [AuthorizeAccount]
        public ActionResult Thue(int id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var phong = _dbContext.PHONGTROes.SingleOrDefault(x => x.MaPhong == id);
            if (session == null)
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            else if (phong.Loai == "Phòng trọ")
            {
                phong.DaNhan = 1;
                DONTHUE dn = new DONTHUE();
                dn.MaKH = session.UserID;
                dn.MaPhong = phong.MaPhong;
                dn.NgayNhan = DateTime.Now;
                dn.TinhTrang = "Đã chờ duyệt";
                _dbContext.DONTHUEs.Add(dn);
                _dbContext.SaveChanges();

                var kh = _dbContext.NGUOIDUNGs.SingleOrDefault(k => k.MaKH == session.UserID); // lay nguoi thue phong
                var chu = _dbContext.NGUOIDUNGs.SingleOrDefault(y => y.MaKH == phong.MaKH); // lay chu phong
                GuiEmail("Phòng đã được thuê", chu.Email, "phanquoclam9867@gmail.com", "zewang.help", "abcd");

                return RedirectToAction("Index", "Home");
            }
            else if (phong.Loai == "Ở ghép")
            {
                if (phong.SoNguoiO == 1)
                {
                    phong.DaNhan = 1;
                    DONTHUE dn = new DONTHUE();
                    dn.MaKH = session.UserID;
                    dn.MaPhong = phong.MaPhong;
                    dn.NgayNhan = DateTime.Now;
                    dn.TinhTrang = "Đã chờ duyệt";
                    _dbContext.DONTHUEs.Add(dn);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else if (phong.SoNguoiO > 1)
                {
                    DONTHUE dn = new DONTHUE();
                    dn.MaKH = session.UserID;
                    dn.MaPhong = phong.MaPhong;
                    dn.NgayNhan = DateTime.Now;
                    phong.SoNguoiO = phong.SoNguoiO - 1;
                    _dbContext.DONTHUEs.Add(dn);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public void GuiEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            // goi email
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail); // Địa chỉ nhận
            mail.From = new MailAddress(ToEmail); // Địa chửi gửi
            mail.Subject = Title;  // tiêu đề gửi
            mail.Body = Content;                 // Nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // host gửi của Gmail
            smtp.Port = 587;               //port của Gmail
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential
            ("phanquoclam9867@gmail.com", "Inspiron15");//Tài khoản/password người gửi
            smtp.EnableSsl = true;   //kích hoạt giao tiếp an toàn SSL
            smtp.Send(message: mail);   //Gửi mail đi
        }
        [HttpPost]
        public JsonResult ListName(string search)
        {
            if (search == "" || search == null)
            {
                return Json(new { data = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var lt = _dbContext.PHONGTROes.Where(n => n.DiaChi.Contains(search)).ToList();
                List<TimKiem> result = new List<TimKiem>();
                foreach (var i in lt)
                {
                    result.Add(new TimKiem(int.Parse(i.MaPhong.ToString()), i.DiaChi.ToString()));
                }
                return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}