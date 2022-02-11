using DotNetCoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApplication.Controllers
{
    public class LocationController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;  //For Access Global value
        public LocationController(DataContext context, IConfiguration config)  //For Access Global value
        {
            _context = context;
            _config = config;  //For Access Global value
        }

        #region Operation of Dot Net Core
        public IActionResult LocationIndex()
        {
            #region Global value get from Appseting.Json file
            string result = _config.GetValue<string>("Logging:LogLevel:Default");  //Fetch Globalvalue from Appsetting
            string TempDocumentPath = _config.GetValue<string>("TempDocumentPath"); //Fetch Globalvalue from Appsetting
            int noofDays = _config.GetValue<int>("NoOfDays"); //Fetch Globalvalue from Appsetting
            string ManifestAutomationDateString = _config.GetValue<string>("ManifestAutomationDateString"); //Fetch Globalvalue from Appsetting
            string ToMailId = _config.GetValue<string>("ToMail"); //Fetch Globalvalue from Appsetting
            #endregion
            return View();
        }

        [HttpPost]
        public JsonResult SaveLocation(Location model)
        {
            string msg = string.Empty;
            if(model != null)
            {
                if(model.LocationId == 0)
                {
                    _context.Locs.Add(model);
                    _context.SaveChanges();
                    msg = "Location Save Successfully";
                }
                else
                {
                    //_context.Locs.Update(model);  // I can Used this method also
                    _context.Entry(model).State = EntityState.Modified;
                    _context.SaveChanges();
                    msg = "Location has been updated successfully";
                }
                
            }
            else
            {
                msg = "Location Save Not successfully";
            }
            return Json(msg);
        }

        [HttpPost]
        public JsonResult GetAllLocation()
        {
            List<Location> allLocation = new List<Location>();
            allLocation = _context.Locs.ToList();
            return Json(allLocation);
        }

        [HttpPost]
        public JsonResult EditLocation(int locId)
        {
            Location EditLocation = _context.Locs.Where(m => m.LocationId == locId).FirstOrDefault();
            return Json(EditLocation);
        }

        [HttpPost]
        public JsonResult DeleteLocation(long LocationId)
        {
            var msg = "";
                if (LocationId != null)
                {
                    var CheckLocationIdUsed = _context.Locs.Where(m => m.LocationId == LocationId).Count();
                    if (CheckLocationIdUsed != 0)
                    {
                        var getDeleteData = (from t in _context.Locs where t.LocationId == LocationId select t).FirstOrDefault();
                        if (getDeleteData != null)
                        {
                            _context.Locs.Remove(getDeleteData);
                            _context.SaveChanges();
                            msg = "Location is Deleted";
                        }
                        return Json(msg);
                    }
                }
            return Json(msg);
        }

    #endregion
    }
}
