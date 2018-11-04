using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication9.Controllers
{
    public class HomeController : Controller
    {
        GhoomoDbContext db = new GhoomoDbContext();
        public ActionResult Index()
        {
            return View(db);
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

        public ActionResult Userpage()
        {
            ViewBag.Message = "This is userpage";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "This is userpage";

            return View();
        }

        [HttpPost]
        public ActionResult Login(String username, String password)
        {
            var user = db.users.Where(x => x.userName.Equals(username) && x.password.Equals(password)).ToList();
            if (user.Count == 1)
            {
                Session["userID"] = user.First().userID;
                var user2 = user.Single();
                if (user2.roleID == 1)
                    return Redirect(Url.Action("Index", "Home"));
                else
                    return Redirect(Url.Action("Userpage", "Home"));

            }
            else
            {
                ViewBag.msg = "Login Failed";
                return View();
            }

        }

        public ActionResult Scrapbook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Scrapbook(HttpPostedFileBase picture)
        {
            if (picture != null && picture.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(picture.FileName));
                    
                    scrabbook model = new scrabbook();
                    model.userid = int.Parse(Session["userID"].ToString());
                    model.image = path;
                    model.tag = "";
                    if (ModelState.IsValid)
                    {
                        db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    picture.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }
    }
}