using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Kurumsal.Models;

namespace Kurumsal.Controllers
{
    public class BannerController : Controller
    {
        private KurumsalDB db = new KurumsalDB();

        public object LogoURL { get; private set; }

        // GET: Banner
        public ActionResult Index()
        {
            return View(db.Banner.ToList());
        }

        // GET: Banner/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banner.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Banner1,ResimURL1,Banner2,ResimURL2,Banner3,ResimURL3")] Banner banner, HttpPostedFileBase ResimURL1, HttpPostedFileBase ResimURL2, HttpPostedFileBase ResimURL3, int id)
        {
            if (ModelState.IsValid)
            {
                var k = db.Banner.Where(x => x.ID == id).SingleOrDefault();

                if (ResimURL1 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.ResimURL1)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.ResimURL1));
                    }
                    WebImage img = new WebImage(ResimURL1.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL1.FileName);

                    string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Save("~/Uploads/Banner/" + logoname);
                    k.ResimURL1 = "/Uploads/Banner/" + logoname;

                    k.ResimURL1 = "/Uploads/Banner/" + logoname;
                }
                if (ResimURL2 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.ResimURL2)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.ResimURL2));
                    }
                    WebImage img = new WebImage(ResimURL2.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL2.FileName);

                    string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Save("~/Uploads/Banner/" + logoname);
                    k.ResimURL2 = "/Uploads/Banner/" + logoname;

                    k.ResimURL2 = "/Uploads/Banner/" + logoname;
                }
                if (ResimURL3 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.ResimURL3)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.ResimURL3));
                    }
                    WebImage img = new WebImage(ResimURL3.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL3.FileName);

                    string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Save("~/Uploads/Banner/" + logoname);
                    k.ResimURL3 = "/Uploads/Banner/" + logoname;

                    k.ResimURL3 = "/Uploads/Banner/" + logoname;
                }
                k.Banner1 = banner.Banner1;
                k.Banner2 = banner.Banner2;
                k.Banner3 = banner.Banner3;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }
    }
}

