﻿using Microsoft.AspNetCore.Mvc;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class Donut_ChartsController : Controller
    {
        [Demo]
        public IActionResult Index()
        {
            return View();
        }
    }
}