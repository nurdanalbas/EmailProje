using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsBasvurusu.Models;
using System.Web.Helpers;

namespace IsBasvurusu.Controllers
{
    public class PanelController : Controller
    {
        Models.IsBasvurusuEntities4 db;
        // GET: Panel
        [HttpPost]
        public ActionResult Panel()
        {
            try
            {
               
                int sID = Convert.ToInt32(Session["userId"].ToString());
               
                string sName = Session["UserName"].ToString();

                
                db = new Models.IsBasvurusuEntities4();

                List<Models.FormTab> formList = new List<Models.FormTab>();

                
                formList = db.FormTabs.OrderByDescending(x => x.userId).ToList();

                return View(formList);
            }
            catch (Exception ex)
            {
                ViewData.ModelState.AddModelError("_HATA", ex.Message);
                return RedirectToAction("Login");
            }

        }

        public ActionResult FormDetails(int ID)
        {
            db = new Models.IsBasvurusuEntities4();

            

            Models.FormTab form = db.FormTabs.Where(x => x.userId == ID).FirstOrDefault();

            return View(form);
        }

        public ActionResult Reply() 
        {
            return View();
        }
    }
}