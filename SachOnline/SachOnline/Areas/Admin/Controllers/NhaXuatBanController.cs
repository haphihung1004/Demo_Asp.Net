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
    public class NhaXuatBanController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();
        // GET: Admin/NhaXuatBan
        public ActionResult Index(int? page)
        {
            int iSize = 20;
            int iPageNum = (page ?? 1);
            return View(db.NHAXUATBANs.ToList().OrderBy(n => n.MaNXB).ToPagedList(iPageNum, iSize));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(string TenNXB, string DienThoai, NHAXUATBAN nxb)
        {
            Validate(TenNXB, DienThoai);
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NHAXUATBAN model)
        {

            if (ModelState.IsValid)
            {
                var nxb = new NHAXUATBAN();
                nxb.TenNXB = model.TenNXB;
                nxb.DiaChi = model.DiaChi;
                nxb.DienThoai = model.DienThoai;

                db.NHAXUATBANs.InsertOnSubmit(nxb);
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
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }
        public ActionResult Edit(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var nxb = db.NHAXUATBANs.Where(n => n.MaNXB == int.Parse(Request.Form["MaNXB"])).SingleOrDefault();
                nxb.TenNXB = f["TenNXB"];
                nxb.DiaChi = f["DiaChi"];
                nxb.DienThoai = f["DienThoai"];
                //UpdateModel(nxb);
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
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var sach = db.SACHes.Where(n => n.MaNXB == id);
            if (sach.Count() > 0)
            {
                ViewBag.ThongBao = "Nhà xuất bản này đã có sách trong cửa hàng <br>" +
                " Nếu muốn xóa thì phải xóa hết sách này trong bảng sách";

                return RedirectToAction("Index", "Sach");
            }

            db.NHAXUATBANs.DeleteOnSubmit(nxb);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }
        public ActionResult Validate(String TenNXB, String DienThoai)
        {
            if (String.IsNullOrEmpty(TenNXB))
            {
                ModelState.AddModelError("TenNXB", "Tên không được để trống.");
            }
            else if (db.NHAXUATBANs.Where(n => n.TenNXB == TenNXB).SingleOrDefault() !=
            null)
            {
                ModelState.AddModelError("TenNXB", "Tên nhà xuất bản đã tồn tại.");
            }
            else if (String.IsNullOrEmpty(DienThoai))
            {
                ModelState.AddModelError("DienThoai", "Số điện thoại không được để trống.");
            }
            return View("Create");
        }
    }
}