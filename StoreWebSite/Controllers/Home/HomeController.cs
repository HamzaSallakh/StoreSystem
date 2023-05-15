using Microsoft.AspNetCore.Mvc;
using StoreWebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebSite.Controllers.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (CommonProp.Token == "" || CommonProp.Token == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}
