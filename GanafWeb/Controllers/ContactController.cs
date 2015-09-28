using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using GanafWeb.Domain;
using GanafWeb.Logic.Abstract;
using GanafWeb.Logic.Concrete;

namespace GanafWeb.Controllers
{
    public class ContactController : Controller
    {
        IContactUs _iContact = new ContactUs();

        [HttpGet]
        public ActionResult ContactUs()
        {
            return View("ContactForm");
        }



        [HttpPost]
        public ActionResult ContactUs(string name, string mail, string message)
        {
            if (mail == "")
            {
                ViewBag.Status = "ERROR";
                ViewBag.StatusMessage = "Mail address cannot be empty";
                return View("ContactForm");
            }
            else if (message == "")
            {
                ViewBag.Status = "ERROR";
                ViewBag.StatusMessage = "Message cannot be empty";
                return View("ContactForm");
            }
            else
            {
                try
                {
                    string from = System.Configuration.ConfigurationManager.AppSettings.Get("UserID");
                    string Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
                    using (MailMessage email = new MailMessage(from, "ganaftech@gmail.com"))
                    {
                        email.Subject = "Contact Us - " + name;
                        email.Body = name + " sent a message from within the ganaf website. \nMessage Content: \n" + message + "\n\nEmail Address: " + mail;
                        email.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential networkCredential = new NetworkCredential(from, Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCredential;
                        smtp.Port = 587;
                        smtp.Send(email);
                        ViewBag.Status = "SUCCESS";
                        ViewBag.StatusMessage = "Message successfully sent";

                    }
                }
                catch (Exception)
                {
                    ViewBag.Status = "ERROR";
                    ViewBag.StatusMessage = "Something went wrong. Please retry.";
                }
                //var c = new contact()
                //{
                //    firstname = name,
                //    lastname = null,
                //    email = mail,
                //    phone = "",
                //    comment = message
                //};
                //string result = _iContact.Contact(c);
                //if (result.Contains("ERROR"))
                //{
                
                //}
                //else
                //{
                //    ViewBag.Status = "SUCCESS";
                //    ViewBag.StatusMessage = result;
                //}
            }
            return View("ContactForm");
        }
    }
}