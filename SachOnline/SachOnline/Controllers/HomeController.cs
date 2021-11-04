using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using System.Security.Cryptography;
using System.Text;

namespace SachOnline.Controllers
{
    public class HomeController : Controller
    {
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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            dbSachOnlineDataContext db = new dbSachOnlineDataContext();
            //Gán giá trị người dung nhập liệu cho các biến 
            var sTenDN = f["UserName"];
            var sMatKhau = f["Password"];
            // gán giá trị cho đối tượng được tạo mới (ad)
            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == GetMD5(sMatKhau));
            if (ad!= null)
            {
               Session["Admin"] = ad;
               return RedirectToAction("Index", "Home");
            }
            else
            {
              ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
             }
            return View();
        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }


    }
}