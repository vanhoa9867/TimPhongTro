using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimPhongTro.Models;

namespace TimPhongTro.Controllers
{
    public class TimKiemController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public TimKiemController()
        {
            _dbContext = new DatabaseContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KQTimKiem(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<PHONGTRO> listKQ = _dbContext.PHONGTROes.Where(n => n.DiaChi.Contains(sTuKhoa)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (listKQ.Count == 0)
            {
                ViewBag.ThongBao1 = "Không tìm thấy kết quả nào phù hợp.";
                return View(_dbContext.PHONGTROes.OrderBy(n => n.DienTich).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao1 = "Đã tìm thấy " + listKQ.Count + " kết quả!";
            return View(listKQ.OrderBy(n => n.DiaChi).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult KQTimKiem(int? page, string sTuKhoa)
        {
            ViewBag.TuKhoa = sTuKhoa;
            List<PHONGTRO> listKQ = _dbContext.PHONGTROes.Where(n => n.DiaChi.Contains(sTuKhoa)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (listKQ.Count == 0)
            {
                ViewBag.ThongBao1 = "Không tìm thấy kết quả nào phù hợp.";
                ViewBag.ThongBao2 = "Thử xem một số nơi ở khác.";
                return View(_dbContext.PHONGTROes.OrderBy(n => n.DienTich).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao1 = "Đã tìm thấy " + listKQ.Count + " kết quả!";
            return View(listKQ.OrderBy(n => n.DienTich).ToPagedList(pageNumber, pageSize));
        }

        public List<PHONGTRO> SearchInDB(string sTuKhoa)
        {
            List<PHONGTRO> listKQ = _dbContext.PHONGTROes.Where(n => n.DiaChi.Contains(sTuKhoa)).ToList();
            return listKQ;
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