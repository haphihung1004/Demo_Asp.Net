using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;

namespace SachOnline.Controllers
{
    public class SearchController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();

        // GET: Search
        public ActionResult Search(string strSearch)
        {
            
            ViewBag.Search = strSearch;
            if(!String.IsNullOrEmpty(strSearch))
            {
                //var kq = from s in db.SACHes select s;
                //var kq = db.SACHes.ToList();
                 var kq = db.SACHes;
                //var kq = db.SACHes.Select(s => s);
                //var kq = from s in db.SACHes where s.SoLuongBan > int.Parse(strSearch) orderby s.SoLuongBan ascending select s;
                return View(kq);
            }
            return View();
        }
        public ActionResult Group()
        {
            //var kq = from s in db.SACHes group s by s.MaCD;
            var kq = db.SACHes.GroupBy(s => s.MaCD);
            return View(kq);
        }
        public ActionResult ThongKe()
        {
            var kq = from s in db.SACHes
                     join cd in db.CHUDEs on s.MaCD equals cd.MaCD
                     group s by new { cd.MaCD, cd.TenChuDe } into g
                     select new ReportInfo
                     {
                         Id = g.Key.MaCD.ToString(),
                         Name = g.Key.TenChuDe,
                         Count = g.Count(),
                         Sum = g.Sum(n => n.SoLuongBan),
                         Max = g.Max(n => n.SoLuongBan),
                         Min = g.Min(n => n.SoLuongBan),
                         Avg = Convert.ToDecimal(g.Average(n => n.SoLuongBan))
                     };
            return View(kq);
        }
    }
}