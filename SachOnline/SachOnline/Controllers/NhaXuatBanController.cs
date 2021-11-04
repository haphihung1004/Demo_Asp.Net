using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SachOnline.Controllers
{
    public class NhaXuatBanController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext();
        // GET: NhaXuatBan
        public ActionResult Index()
        {
            return View(from nxb in data.NHAXUATBANs select nxb);
        }


        public ActionResult Details()
        {
            //sử dụng request để lấy giá trị của tham số id
            int manxb = int.Parse(Request.QueryString["id"]);
            //int manxb = int.Parse(Request["id"]);
            //int manxb = int.Parse(Request..Params["id"]);
            var result = data.NHAXUATBANs.Where(nxb => nxb.MaNXB == manxb).SingleOrDefault();
            return View(result);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            NHAXUATBAN nxb = data.NHAXUATBANs.Where(n => n.MaNXB == id).SingleOrDefault();
            return View(nxb);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                
                //nxb.TenNXB = model.TenNXB;
                //nxb.DiaChi = model.DiaChi;
                //nxb.DienThoai = model.DienThoai;

                data.SubmitChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(NHAXUATBAN model)
        {
            if (ModelState.IsValid)
            {
                var nxb = new NHAXUATBAN();
                nxb.TenNXB = model.TenNXB;
                nxb.DiaChi = model.DiaChi;
                nxb.DienThoai = model.DienThoai;
                
                data.NHAXUATBANs.InsertOnSubmit(nxb);
                data.SubmitChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Add");
            }
        }
    }
}