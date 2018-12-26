using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        GhoomoDbContext db = new GhoomoDbContext();
        user user2;
        [HttpGet]
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(user usr, vendor vndr)
        {
            if (vndr.companyName.Equals(""))
            {
                db.users.Add(usr);
                db.SaveChanges();
            }
            else
            {

            }
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
                Session["userName"] = user.First().userName;
                user2 = user.Single();
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

        public ActionResult Logout()
        {
            Session["userName"] = null;
            return RedirectToAction("Index");
        }
        public ActionResult Scrapbook()
        {

            return View(db);
        }
        [HttpPost]
        public ActionResult Scrapbook(HttpPostedFileBase picture, String tag)
        {
            if (picture != null && picture.ContentLength > 0)
                try
                {

                    string fileName = System.IO.Path.GetFileName(picture.FileName);
                    //Set the Image File Path.
                    string path = "~/Images/" + fileName;
                    //Save the Image File in Folder.
                    picture.SaveAs(Server.MapPath(path));

                    db.scrabbooks.Add(new scrabbook
                    {
                        userid = 1,
                        image = path,
                        tag = tag
                    });
                    db.SaveChanges();
                    
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
            return View(db);
        }

        public ActionResult pDetails(int pkgid)
        {

            ViewBag.id = pkgid;
            List<image> imgs = db.images.Where(x => x.packageID == pkgid).ToList();
            Console.WriteLine(pkgid);
            return View(imgs);
        }
        
        public ActionResult vendorPanel()
        {
            return View();
        }
        public ActionResult addPackage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult addPackage(package pkg)
        {
           
                db.packages.Add(pkg);
                db.SaveChanges();
           
            return View();
        }
        public ActionResult managePackage()
        {
            return View();
        }
    }
}