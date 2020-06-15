using KutuphaneProgrami.Data.HelperClass;
using KutuphaneProgrami.Data.Model;
using KutuphaneProgrami.Data.UnitOfWork;
using System.Web.Mvc;

namespace KutuphaneProgrami.Controllers
{
    public class UyelikController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public UyelikController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            var uyeler = unitOfWork.GetRepository<Uye>().GetAll(x => x.Yetki != null);
            return View(uyeler);
        }

        public ActionResult Ekle()
        {
            var uyeler = unitOfWork.GetRepository<Uye>().GetAll(x => x.Yetki == null);
            return View(uyeler);
        }

        [HttpPost]
        public JsonResult EkleJson(int uyeId, string mail, string parola, string parolaTekrar)
        {
            if (!string.IsNullOrEmpty(mail) && !string.IsNullOrEmpty(parola) && !string.IsNullOrEmpty(parolaTekrar))
            {
                if (parola == parolaTekrar)
                {
                    parola = Sifreleme.Sifrele(parola);
                    var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
                    uye.Mail = mail;
                    uye.Sifre = parola;
                    // 1 = admin , 2 = moderator, 3 = izleyici
                    uye.Yetki = "3";

                    unitOfWork.GetRepository<Uye>().Update(uye);
                    unitOfWork.SaveChanges();
                    return Json("1");
                }
                else return Json("parolaUyusmazligi");
            }
            else return Json("bosOlamaz");
        }

        public ActionResult Guncelle(int uyeId)
        {
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
            return View(uye);
        }

        [HttpPost]
        public JsonResult GuncelleJson(int uyeId, string mail, string parola, string parolaTekrar)
        {
            if (!string.IsNullOrEmpty(mail))
            {
                if (parola == parolaTekrar)
                {
                    parola = Sifreleme.Sifrele(parola);
                    var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
                    uye.Mail = mail;

                    if (!string.IsNullOrEmpty(parola))
                        uye.Sifre = parola;

                    unitOfWork.GetRepository<Uye>().Update(uye);
                    unitOfWork.SaveChanges();
                    return Json("1");
                }
                else return Json("parolaUyusmazligi");
            }
            else return Json("mailBosOlamaz");
        }

        [HttpPost]
        public JsonResult SilJson(int uyeId)
        {
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
            unitOfWork.GetRepository<Uye>().Delete(uye);
            var durum = unitOfWork.SaveChanges();
            if (durum > 0)
                return Json("1");

            return Json("0");
        }

        [HttpPost]
        public JsonResult YetkiAtaJson(int uyeId, string yetkiId)
        {
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
            uye.Yetki = yetkiId;
            unitOfWork.GetRepository<Uye>().Update(uye);
            var durum = unitOfWork.SaveChanges();
            if (durum > 0) return Json("1");

            return Json("0");
        }
    }
}