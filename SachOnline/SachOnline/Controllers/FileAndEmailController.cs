using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace SachOnline.Controllers
{
    public class FileAndEmailController : Controller
    {
        // GET: FileAndMail
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMail(Mail model)
        {
            // Cấu hình thông tin gmail (Khai báo thư viện System.Net)
            var mail = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("vk250012@gmail.com", "123456789ab"),
                EnableSsl = true
            };
            //Tao Email
            var message = new MailMessage();
            message.From = new MailAddress(model.From);
            message.ReplyToList.Add(model.From);
            message.To.Add(model.To);
            message.Subject = model.Subject;
            message.Body = model.Notes;

            var f = Request.Files["attachment"];
            var path = Path.Combine(Server.MapPath("~/UploadFile"), f.FileName);
            if (!System.IO.File.Exists(path))
            {
                f.SaveAs(path);
            }

            Attachment data = new Attachment(Server.MapPath("~/uploadFile/" + f.FileName), MediaTypeNames.Application.Octet);
            message.Attachments.Add(data);
            //gui mail
            mail.Send(message);
            return View("SendMail");
        }
    }
}
