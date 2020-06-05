using Kurumsal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Kurumsal.Controllers
{

    public class HomeController : Controller
    {
        KurumsalDB db = new KurumsalDB();
        // GET: Home
        [Route("")]
        [Route("Anasayfa")]
        [HttpGet]
        public ActionResult Index(int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hizmet.Include("HizmetKategori").OrderByDescending(x => x.HizmetId).ToPagedList(Sayfa, 9));

        }


        public ActionResult İletişimPartial()
        {
            return View(db.Iletisim.ToList());
        }



        [HttpGet]
        [Route("Iletisim")]
        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Iletisim.ToList());
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Iletisim(string adsoyad = null, string email = null, string konu = null, string mesaj = null)
        {

            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "kurumsalsite6161@gmail.com";
                WebMail.Password = "Furkan123";
                WebMail.SmtpPort = 587;
                WebMail.Send("kurumsalsite6161@gmail.com", konu, "<p class=p-5 bg-primary>" + email + "</p>" + "<p>" + mesaj + "</p>");
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";
            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu.Tekrar deneyiniz";
            }
            return RedirectToAction("Iletisim", "Home");
        }







            //Ürün Detay
        [Route("UrunPost/{UrunKodu}-{id:int}")]
        public ActionResult HizmetDetay(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var u = db.Hizmet.Include("HizmetKategori").Where(x => x.HizmetId == id).SingleOrDefault();
            return View(u);
        }


        //Menüde ki Kategori Listelemesi
        public ActionResult HizmetKategoriPartial()
        {

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(db.HizmetKategori.Include("Hizmet").ToList().OrderBy(x => x.HizmetKategoriAdi));
        }





        //Kategoriye Ait Hizmetler
        [Route("UrunPost/{HizmetKategoriAdi}/{id:int}")]
        public ActionResult KategoriHizmet(int id, int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var u = db.Hizmet.Include("HizmetKategori").OrderByDescending(x => x.HizmetId).Where(x => x.HizmetKategori.HizmetKategoriId == id).ToPagedList(Sayfa, 9);
            return View(u);
        }



      


        public ActionResult BayrampasaSube()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }

        public ActionResult MerterSube()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }

        public ActionResult LaleliSube()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }

        public ActionResult BoyTablosu()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }

        public ActionResult AramaYap(string aranan)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            if (!string.IsNullOrEmpty(aranan))
            {
                var mkl = db.Hizmet.Where(x => x.Baslik.Contains(aranan)).ToList();
                if (mkl.Count == 0)
                {
                    ViewBag.NotFound = "Aramanızla Eşleşen Hiçbir Kayıt Bulunamadı!";
                    return View(mkl);
                }
                return View(mkl);
            }
            return View();
        }
    }
}