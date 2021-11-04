using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace SachOnline.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();
        // GET: Admin/KhachHang
        public ActionResult Index(int? page)
        {
            int iSize = 20;
            // Tạo biến số trang
            int iPagenum = (page ?? 1);

            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MaKH).ToPagedList(iPagenum, iSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHACHHANG model)
        {
            if (ModelState.IsValid)
            {
                var kh = new KHACHHANG();
                kh.HoTen = model.HoTen;
                kh.TaiKhoan = model.TaiKhoan;
                kh.MatKhau = model.MatKhau;
                kh.Email = model.Email;
                kh.DiaChi = model.DiaChi;
                kh.DienThoai = model.DienThoai;
                kh.NgaySinh = model.NgaySinh;

                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var kh = db.KHACHHANGs.Where(n => n.MaKH == int.Parse(Request.Form["MaKH"])).SingleOrDefault();
                kh.HoTen = f["HoTen"];
                kh.TaiKhoan = f["TaiKhoan"];
                kh.MatKhau = f["MatKhau"];
                kh.Email = f["Email"];
                kh.DiaChi = f["DiaChi"];
                kh.DienThoai = f["DienThoai"];
                kh.NgaySinh = Convert.ToDateTime(f["NgaySinh"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ddh = db.DONDATHANGs.Where(n => n.MaDonHang == id);
            if (ddh.Count() > 0)
            {
                ViewBag.ThongBao = "Khách hàng này đã có đơn hàng trong cửa hàng <br>" +
                " Nếu muốn xóa thì phải xóa hết đơn hàng này trong bảng đơn hàng";

                return RedirectToAction("Index", "DonHang");
            }

            db.KHACHHANGs.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
    }
}