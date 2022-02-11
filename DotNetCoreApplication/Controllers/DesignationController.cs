using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApplication.Controllers
{
    public class DesignationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
