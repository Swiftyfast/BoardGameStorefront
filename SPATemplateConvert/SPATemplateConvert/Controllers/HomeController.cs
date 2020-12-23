using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPATemplateConvert.Models;
using System.Net.Mail;
using System.Net;

namespace SPATemplateConvert.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ContactAjax(ContactViewModel cvm)
        {
            string body = $"You have reveived an email from {cvm.Name}. The email address given was {cvm.Email}. The following message was sent: {cvm.Message}";

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("no-reply@johndavidswift.com");
            mm.To.Add(new MailAddress("jdavidswift@gmail.com"));
            mm.Subject = cvm.Subject;
            mm.Body = body;
            mm.ReplyToList.Add(cvm.Email);

            SmtpClient smtp = new SmtpClient("mail.johndavidswift.com");
            NetworkCredential cred = new NetworkCredential("no-reply@johndavidswift.com", "NOPE LOL");

            smtp.Credentials = cred;
            smtp.Send(mm);

            return Json(cvm);
        }

    }
}