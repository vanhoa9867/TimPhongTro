using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimPhongTro.Models;

namespace TimPhongTro.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public HomeController()
        {
            _dbContext = new DatabaseContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //Tim kiem
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