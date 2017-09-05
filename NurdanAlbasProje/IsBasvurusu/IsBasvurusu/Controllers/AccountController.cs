using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsBasvurusu.Models;

namespace IsBasvurusu.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(IsBasvurusu.Models.LoginTable avm)
        {
            using (Models.IsBasvurusuEntities4 db = new Models.IsBasvurusuEntities4())
            {
                var userDetails = db.LoginTables.Where(x => x.userName == avm.userName
                                                  &&
                                                  x.password == avm.password).FirstOrDefault();

                if (userDetails == null)
                {
                   
                    return View("Login", avm);
                }
                else
                {
                    Session["userId"] = userDetails.kID;
                    Session["UserName"] = userDetails.userName;
                    return RedirectToAction("Panel", "Panel");
                }
            }
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }
    }
}