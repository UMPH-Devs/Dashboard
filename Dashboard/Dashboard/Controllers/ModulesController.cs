using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dashboard.Models;
using Dashboard.Entities;

namespace Dashboard.Controllers
{
    public class ModulesController : Controller
    {

        // GET: Module/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.AppId = id;
            return View();
        }

        public ActionResult Status(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.moduleId = id;
            return View();
        }

       
    }
}
