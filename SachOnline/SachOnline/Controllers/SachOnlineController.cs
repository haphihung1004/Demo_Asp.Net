using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using PagedList;
using PagedList.Mvc;

namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext();
        
        private List<SACH> LaySachMoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        private List<SACH> LaySachBan(int count)
        {
            return data.SACHes.OrderByDescending(a => a.SoLuongBan).Take(count).ToList();
        }

        // GET: SachOnline
        public ActionResult Index()
        {
            var listSachmoi = LaySachMoi(6);
            
            return View(listSachmoi);
        }
        public ActionResult ChuDeSachPartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd;
            return PartialView(listChuDe);
        }
        public ActionResult NhaXuatBanPartial()
        {

            var listXuatBan = from xb in data.NHAXUATBANs select xb;
            return PartialView(listXuatBan);
           
        }
        public ActionResult SachBanNhieuPartial()
        {
            var listSachBan = LaySachBan(6);
            return PartialView(listSachBan);
        }
        public ActionResult SachTheoChuDe(int iMaCD, int ? page)
        {
            ViewBag.MaCD = iMaCD;
            //tạo biến quy định số sản phẩm trên mỗi trang
            int iSize = 3;
            //tạo biến số trang
            int iPageNum = (page ?? 1);
            var sach = from s in data.SACHes where s.MaCD == iMaCD select s;
            return View(sach.ToPagedList(iPageNum,iSize));
        }
        public ActionResult SachTheoNXB(int id, int ? page)
        {
            ViewBag.MaNXB = id;
            //tạo biến quy định số sản phẩm trên mỗi trang
            int iSize = 3;
            //tạo biến số trang 
            int iPageNum = (page ?? 1);
            var sach = from s in data.SACHes where s.MaNXB == id select s;
            return View(sach.ToPagedList(iPageNum,iSize));
        }
        public ActionResult _NavbarPartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd;
            return PartialView(listChuDe);
        }
        public ActionResult NavbarNXB()
        {
            var listNXB = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(listNXB);
        }
        public ActionResult ChiTietSach(int id)
        {
            var sach = from s in data.SACHes where s.MaSach == id select s;
            return View(sach.Single());
        }
        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }
    }
}