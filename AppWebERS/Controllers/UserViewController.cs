﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebERS.Controllers
{
    public class UserViewController : Controller
    {
        // GET:  UserView
        public ActionResult Index()
        {
            return View("Principal");
        }

        // GET: UserView
        public ActionResult Principal(String name)
        {
            if (name == null)
            {
                ViewBag.name = " UserView - Test";
            }
            else
            {
                ViewBag.name = name;
            }
            return View();
        }
    }
}