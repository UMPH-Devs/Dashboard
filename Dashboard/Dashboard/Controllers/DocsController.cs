﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class DocsController : Controller
    {
        // GET: Docs
        public ActionResult Index()
        {
            return View();
        }
    }
}