using System.Web.Mvc;
using System;
using StoreFront.UI.MVC.Models;
using System.Net.Mail;
using System.Net;

namespace StoreFront.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            string emailBody = $"You have received an email from {cvm.Name} with a subject of {cvm.Subject} from your STOREFRONT page. Please respond to {cvm.Email} with your response the following message: <br /><br /> {cvm.Message}";

            MailMessage msg = new MailMessage(
                "no-reply@johndavidswift.com",
                "jdavidswift@gmail.com",
                "Email from johndavidswift.com",
                emailBody
                );

            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("mail.johndavidswift.com");

            client.Credentials = new NetworkCredential("no-reply@johndavidswift.com", "LOL NOPE");
            //client.port = 8889;

            try
            {
                client.Send(msg);
            } catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Sorry, something went wrong. Error message: {ex.Message}<br />{ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);
        }
    }
}
