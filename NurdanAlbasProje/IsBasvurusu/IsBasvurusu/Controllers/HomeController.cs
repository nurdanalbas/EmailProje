using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsBasvurusu.Models;
using System.Web.Helpers;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.IO;

namespace IsBasvurusu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FormPage()
        {

            return View();
        }
        [HttpPost]
        public ActionResult FormPage(IsBasvurusu.Models.FormTab form)
        {

            return View();
        }
        public ActionResult ThankYouPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Form(IsBasvurusu.Models.FormTab objModelMail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string from = objModelMail.adminMail;

                    using (MailMessage mail = new MailMessage(from, objModelMail.adminMail))
                    {
                        mail.Subject = objModelMail.subject;
                        mail.Body = "First Name: " + objModelMail.firstName + "\n"
                                   + "Last Name: " + objModelMail.lastName + "\n"
                                   + "Gender: " + objModelMail.gender + "\n"
                                   + "Phone Number: " + objModelMail.phoneNumber + "\n"
                                   + "School: " + objModelMail.school + "\n"
                                   + "Degree: " + objModelMail.degree + "\n"
                                   + "Message: " + objModelMail.message + "\n";
                        mail.IsBodyHtml = false;
                        if (objModelMail.attachment.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(objModelMail.attachment.FileName);
                            mail.Attachments.Add(new Attachment(objModelMail.attachment.InputStream, fileName));
                        }
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential networkCredential = new NetworkCredential(from, objModelMail.password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCredential;
                        smtp.Port = 587;
                        smtp.Send(mail);


                        return ViewBag("send");

                    }

                    //database kayıt işlemi
                    IsBasvurusu.Models.FormTab.addMail(objModelMail.firstName,
                                            objModelMail.lastName,
                                         objModelMail.birthDay,
                                      objModelMail.gender,
                                      objModelMail.phoneNumber,
                                      objModelMail.school,
                                    objModelMail.degree,
                                  objModelMail.adminMail,
                                objModelMail.password,
                              objModelMail.subject,
                            objModelMail.message);

                    return RedirectToAction("Sent");

                }
                else 
                {
                    return View("FormPage");
                }

            }
            catch (Exception ex)
            {
                Response.Write("FormPage" + ex.Message);
            }
            return View("FormPage");


        }
        /*
        Models.IsBasvurusuEntities4 db;
        // GET: Panel
        public ActionResult Panel()
        {
            try
            {
                // null değeri int e çeviremeyince hata verir o yüzden böyle bakabilirsin ya da if ile null mı değil mi bakabilirsin
                int sID = Convert.ToInt32(Session["userId"].ToString());

                string sName = Session["UserName"].ToString();

                // veritabanından bu sefer verileri alıcaz.
                db = new Models.IsBasvurusuEntities4();

                List<Models.FormTab> formList = new List<Models.FormTab>();

                // db direk veritabanı bağlantısı olduğu için tablo adına ToList() yani select * from Froms yaptık, sondan başa doğru sıralasın diye
                formList = db.FormTabs.OrderByDescending(x => x.userId).ToList();

                // bu listeyi view e dönmemiz gerekiyor ki böylece ön tarafta istediğimiz gibi kullanalım
                // İlişkikli view in de bu listeyi alacağını bilmesi gerekiyor.view kısmında düzeltmeler olacak mı peki ?
                // Bakalım oraya da eğer modeli vermediyseniz modeli göstermemiz gerekiyor
                return View(formList);
            }
            catch (Exception ex)
            {
                ViewData.ModelState.AddModelError("_HATA", ex.Message);
                return RedirectToAction("Account");
            }

        }

        public ActionResult FormDetails(int ID)
        {
            db = new Models.IsBasvurusuEntities4();

            // ID yi action a gönderip o id li veriyi alıp ilişkili view e dönücez. 
            // FirstOrDefault() sonuç olarak 1 tane getirir, eğer o id ile kayıt yoksa null döner

            Models.FormTab form = db.FormTabs.Where(x => x.userId == ID).FirstOrDefault();

            return View(form);
        }

        public ActionResult Yanitla() //bu methodun içersi dolacak
        {
            return View();
        }
   
    
    */
    }
}
