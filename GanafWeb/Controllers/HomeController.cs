using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GanafWeb.Domain;
using GanafWeb.Logic.Abstract;
using GanafWeb.Logic.Concrete;
using GanafWeb.Logic.Constants;
using GanafWeb.Models;
using request = GanafWeb.Models.request;

namespace GanafWeb.Controllers
{
    public class HomeController : Controller
    {
        readonly IRequest _request = new Request();
        readonly IContactUs _iContactUs = new ContactUs();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        [HttpGet]
        public ActionResult MyLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MyLogin(users user)
        {
            GanafDBBEntities1 entity = new GanafDBBEntities1();
            entity.users.Add(user);
            entity.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ErrorPage()
        {
            return View("ErrorPage");
        }

        [HttpPost]
        public ActionResult ContactUs(request request)
        {
            
            using (GanafDBBEntities1  entity = new GanafDBBEntities1 ())
            {
                entity.request.Add(request);
                entity.SaveChanges();
            }
          
           
            return View();
        }

        public ActionResult Authenticate(string username, string password)
        {
            try
            {
                var validUsername = System.Configuration.ConfigurationManager.AppSettings.Get("AuthenticationUsername");
                var validPassword = System.Configuration.ConfigurationManager.AppSettings.Get("AuthenticationPassword");
                if (!username.Equals(validUsername) || !password.Equals(validPassword))
                {
                    var result = _request.Authenticate(username, password);
                    if (result.Contains("?ERROR"))
                    {
                        ViewBag.Status = "ERROR";
                        ViewBag.StatusMessage = result.Substring(0, result.LastIndexOf("?", StringComparison.Ordinal));
                    }
                    else
                    {
                        return View("Index");
                    }
                    return View("OtherUsers");
                }
                _request.AuthenticateAdmin(validUsername, validPassword);
                return View("Index");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ActionResult Register(string firstname, string lastname, string email, string phone, string username, string password)
        {
            try
            {
                var user = new user
                {
                    firstname = firstname,
                    lastname = lastname,
                    email = email,
                    phone = phone,
                    username = username,
                    password = password,
                    userType = UserTypes.OTHER
                };
                _iContactUs.Register(user);
                ViewBag.Status = "SUCCESS";
                ViewBag.StatusMessage = "Registration successful";
            }
            catch (Exception)
            {
                ViewBag.Status = "ERROR";
                ViewBag.StatusMessage = "Something went wrong. Please retry";
            }
            return View("OtherUsers");
        }

        [HttpGet]
        public ActionResult OtherUser()
        {
            return View("OtherUsers");
        }
    }
}
