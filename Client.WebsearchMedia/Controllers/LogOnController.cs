using Client.WebsearchMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.WebsearchMedia.Controllers
{
    public class LogOnController : Controller
    {
        // GET: LogOn
        
        [HttpGet]
        public ActionResult Index()
        {
            LogOnModel model = new LogOnModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Username.ToLower() == "web-search" && model.Password == "web@info")
                {
                    Session["user"] = model;
                    return RedirectToAction("Index", "Home");
                }
                model.IsAuthorized = false;
                return View(model);
            }
            model.IsAuthorized = false;
            return View(model);
        }

    }
}