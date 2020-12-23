using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntroToMVC.UI.MVC.Models;
using System.Net.Mail; //Added for most email functionality
using System.Net; //added for network credentials

namespace IntroToMVC.UI.MVC.Controllers
{
    public class StronglyTypedDataController : Controller
    {
        //Collection initialization syntax
        List<FamilyMemberViewModel> fam = new List<FamilyMemberViewModel>()
        {
            new FamilyMemberViewModel(){Name="Scott", Relation="Father", Birthday= new DateTime(1952, 12, 26)},
            new FamilyMemberViewModel(){Name="Sheryl", Relation="Mother", Birthday= new DateTime(1951, 01,02)},
            new FamilyMemberViewModel(){Name="Nathan", Relation="Cousin", Birthday= new DateTime(1975, 08,02)},
            new FamilyMemberViewModel(){Name="Maurice", Relation="Nephew", Birthday= new DateTime(2013, 07,07)},
            new FamilyMemberViewModel(){Name="Abigail", Relation="Cousin", Birthday= new DateTime(1980, 11,11)}
        };

        // GET: StronglyTypedData
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SingleFamilyMember()
        {
            FamilyMemberViewModel fm = new FamilyMemberViewModel(111, "Scott", "Father", new DateTime(1952, 12, 27));
            ViewBag.Hello = "This is a family member";
            //Pass the objects (fm) to the strongly typed view as part of the views parameter set.

            //If you omit passing in the object and you have @Model calls in the view you will receive an exception at run for Object reference not set to an instance of the object. The object and proptery requested were not sent to the view.
            return View(fm);
        }

        public ActionResult All()
        {
            return View(fam.ToList());
        }

        //Add a family member (At least in the current memory)
        //GET request to render the form
        public ActionResult Add()
        {
            return View();
        }

        //Process the Add Form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FamilyMemberViewModel fmvm)
        {
            //Add the new family member to the list- send the user to the "All" view to see all family members, including the new one.
            fam.Add(fmvm);

            // View Name, Object to be sent to the view
            return View("All", fam);
        }

        //************** C O N T A C T   F O R M S ******************
        //Contact forms in our MVC apps always required a GET and POST action. The GET action returns the view with the form to the user. The POST action processes the form data (sends email)

        //GET
        public ActionResult Contact()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            //When a class has validation attributes, that validation should be checked before attempting to process the data.
            if (!ModelState.IsValid)
            {
                //Send them back to the form and pass the object(cvm) they created to the view.
                //This will restore the info they typed in the textboxes, so they don't have to type it again.
                return View(cvm);
            }

            //Steps to send an email:
            //1) Create a string fot he message
            string emailBody = $"You have received an email from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response the following message: <br /><br /> {cvm.Message}";
            //2) Create the MailMessage object - System.Net.Mail
            MailMessage msg = new MailMessage
            (
                //From
                "no-reply@johndavidswift.com",
                //To(where the actual message is sent)
                "jdavidswift@gmail.com",
                //Subject
                "Email from johndavidswift.com",
                //Body
                emailBody
            );

            //3) (optional) Customize the MailMessage Object
            msg.IsBodyHtml = true; //Allow HTML formatting in the email
            //msg.CC.Add("anotherEmail@email.com");
            //msg.ReplyToList.Add(cvm.Email); //Response to the sender's email instead of our smarterasp.net email
            //msg.Priority = MailPriority.High;

            //4) Create the SmptpClient - This is the information from the HOST (smarterasp.net)
            //This allows the email to actually be sent
            SmtpClient client = new SmtpClient("mail.johndavidswift.com");

            //Client Credentials (SmarterASP requires your username and password)
            client.Credentials = new NetworkCredential("no-reply@johndavidswift.com", "Lol NOPE");
            //client.port = 8889;

            //5) Attempt to send the email
            //It is always possible the mailserver will be down or may have a configuration issue.
            //So we want to wrap our code with a try/catch
            try
            {
                //atempt to send the email
                client.Send(msg);
            } catch(Exception ex)
            {
                ViewBag.ErrorMessage = $"Sorry, something went wrong. Error message: {ex.Message}<br />{ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);
        }



    }   
}