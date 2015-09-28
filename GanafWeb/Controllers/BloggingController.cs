using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GanafWeb.Domain;
using GanafWeb.Logic.Abstract;
using GanafWeb.Logic.Concrete;
using GanafWeb.Logic.Constants;

namespace GanafWeb.Controllers
{
    public class BloggingController : Controller
    {
        readonly IBlogger _blogger = new Blogger();
        IRequest request = new Request();

        public ActionResult Home()
        {
            var result = _blogger.GetPosts();
            TempData["Posts"] = result.ToList();
            TempData.Keep("Posts");
            return View();
        }

        public ActionResult Details(int id)
        {
            var result = _blogger.GetPost(id);
            TempData["Post"] = result;
            TempData.Keep("Post");
            var result2 = _blogger.GetPosts();
            TempData["Posts"] = result2.ToList();
            TempData.Keep("Posts");
            return View("BlogDetails");
        }

        public ActionResult Comment(string message, string postId)
        {
            try
            {
                if (!SessionParameters.GetUserAuthenticated())
                {
                    return RedirectToAction("OtherUser", "Home");
                }
                var result = _blogger.Comment(message, int.Parse(postId));
                if (result.Contains("ERROR"))
                {
                    ViewBag.Status = "ERROR";
                    ViewBag.StatusMesage = result.Substring(0, result.LastIndexOf("?", StringComparison.Ordinal));
                }
                else
                {
                    ViewBag.Status = "SUCCESS";
                    ViewBag.StatusMesage = result;
                }
            }
            catch (Exception)
            {
                //Ignored
            }
            return View("BlogDetails");
        }

        [HttpGet]
        public ActionResult MakePost()
        {
            if (SessionParameters.GetUserAuthenticated() &&
                request.GetUser(SessionParameters.GetCurrentUser()).userType.Trim().Equals(UserTypes.ADMIN))
            {
                var result = _blogger.GetPosts();
                TempData["Posts"] = result.ToList();
                TempData.Keep("Posts");
                return View();
            }
            return RedirectToAction("OtherUser", "Home");
        }

        [HttpPost]
        public ActionResult MakePost(string postedBy, string intro, string topic, string body, HttpPostedFileBase picture)
        {
            if (SessionParameters.GetUserAuthenticated() &&
                request.GetUser(SessionParameters.GetCurrentUser()).userType.Trim().Equals(UserTypes.ADMIN))
            {
                try
                {
                    if (picture != null)
                    {
                        var extension = Path.GetExtension(picture.FileName);

                        var pic = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() +
                                           DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() +
                                           DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                        var path = Path.Combine(Server.MapPath("~/Content/Uploads/Photos/"), pic + extension);
                        picture.SaveAs(path);

                        var scaled = Utils.Scale(Image.FromFile(path), 261, 193);
                        System.IO.File.Delete(path);
                        scaled.Save(path);
                        var picture2 = Utils.Scale(Image.FromFile(path), 75, 75);
                        var path2 = Path.Combine(Server.MapPath("~/Content/Uploads/Photos/Small/"), pic + extension);
                        picture2.Save(path2);

                        var p = new post
                        {
                            postedBy = postedBy,
                            topic = topic,
                            intro = intro,
                            body = body,
                            image = (pic + extension),
                            postDate = DateTime.Now
                        };
                        var r = _blogger.AddPost(p);
                        if (r.Contains("ERROR"))
                        {
                            ViewBag.Status = "ERROR";
                            ViewBag.StatusMessage = r.Substring(0, r.LastIndexOf("?", StringComparison.Ordinal));
                        }
                        else
                        {
                            ViewBag.Status = "SUCCESS";
                            ViewBag.StatusMessage = r;
                        }
                        var result = _blogger.GetPosts();
                        TempData["Posts"] = result.ToList();
                        TempData.Keep("Posts");
                    }
                }
                catch (Exception)
                {
                    //Ignore
                }
                return View();
            }
            return RedirectToAction("OtherUser", "Home");
        }
        
        [HttpGet]
        public ActionResult EditPost(int id)
        {
            if (SessionParameters.GetUserAuthenticated() &&
                request.GetUser(SessionParameters.GetCurrentUser()).userType.Trim().Equals(UserTypes.ADMIN))
            {
                var r = _blogger.GetPosts();
                TempData["Posts"] = r.ToList();
                TempData.Keep("Posts");

                var result = _blogger.GetPost(id);
                TempData["Post"] = result;
                TempData.Keep("Post");
                return View();
            }
            return RedirectToAction("OtherUser", "Home");
        }

        [HttpPost]
        public ActionResult EditPost(string intro, string topic, string body, string postId)
        {
            if (SessionParameters.GetUserAuthenticated() &&
                request.GetUser(SessionParameters.GetCurrentUser()).userType.Trim().Equals(UserTypes.ADMIN))
            {
                var post = _blogger.GetPost(int.Parse(postId));
                post.intro = intro;
                post.topic = topic;
                post.body = body;
                var r = _blogger.ModifyPost(post);
                if (r.Contains("ERROR"))
                {
                    ViewBag.Status = "ERROR";
                    ViewBag.StatusMessage = r.Substring(0, r.LastIndexOf("?", StringComparison.Ordinal));
                }
                else
                {
                    ViewBag.Status = "SUCCESS";
                    ViewBag.StatusMessage = r;
                }
                var result = _blogger.GetPosts();
                TempData["Posts"] = result.ToList();
                TempData.Keep("Posts");
                return View("MakePost");
            }
            return RedirectToAction("OtherUser", "Home");
        }

        public ActionResult DeletePost(int id)
        {
            if (SessionParameters.GetUserAuthenticated() &&
                request.GetUser(SessionParameters.GetCurrentUser()).userType.Trim().Equals(UserTypes.ADMIN))
            {
                var r = _blogger.DeletePost(id);
                if (r.Contains("ERROR"))
                {
                    ViewBag.Status = "ERROR";
                    ViewBag.StatusMessage = r.Substring(0, r.LastIndexOf("?", StringComparison.Ordinal));
                }
                else
                {
                    ViewBag.Status = "SUCCESS";
                    ViewBag.StatusMessage = r;
                }
                var result = _blogger.GetPosts();
                TempData["Posts"] = result.ToList();
                TempData.Keep("Posts");
                return View("MakePost");
            }
            return RedirectToAction("OtherUser", "Home");
        }
    }
}