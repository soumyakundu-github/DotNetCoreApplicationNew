using DotNetCoreApplication.Models;
using DotNetCoreApplication.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApplication.Controllers
{
    public class LocationDependencyInjectionController : Controller
    {
        #region Implement Dependency Injection in Dot net Core
        private readonly ILocation _locationRepo = null;
        public LocationDependencyInjectionController(ILocation LocaRepo)
        {
            _locationRepo = LocaRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveLocation(Location model)
        {
            string msg = string.Empty;
            if (model != null)
            {
                msg = _locationRepo.SaveLocation(model);
            }
            return Json(msg);
        }

        [HttpPost]
        public JsonResult GetAllLocation()
        {
            List<Location> allLocation = new List<Location>();
            allLocation = _locationRepo.GetAllLocation();
            return Json(allLocation);
        }

        [HttpPost]
        public JsonResult EditLocation(int locId)
        {
            Location EditLocation = _locationRepo.EditLocation(locId);
            return Json(EditLocation);
        }

        [HttpPost]
        public JsonResult DeleteLocation(long LocationId)
        {
            var msg = "";
            if (LocationId != null)
            {
                msg = _locationRepo.DeleteLocation(LocationId);
            }
            return Json(msg);
        }
    }
    #endregion
}
