﻿using Kendo.Mvc.Examples.Models;
using Microsoft.AspNet.Mvc;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class Area_ChartsController : Controller
    {
        public IActionResult Date_Axis()
        {
            return View(ChartDataRepository.DatePoints());
        }
    }
}