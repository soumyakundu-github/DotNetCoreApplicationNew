using DotNetCoreApplication.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApplication.Models.Repository
{
    public class LocationRepository : ILocation
    {
        private readonly DataContext _context;
        public LocationRepository(DataContext context)
        {
            _context = context;
        }
        public string SaveLocation(Location model)
        {
            string msg = string.Empty;
            if (model.LocationId == 0)
            {
                _context.Locs.Add(model);
                _context.SaveChanges();
                msg = "Location Save Successfully";
            }
            else
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                msg = "Location has been updated successfully";
            }
            return msg;
        }

        public string DeleteLocation(long LocationId)
        {
            string msg = string.Empty;
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
                }
            }
            return msg;
        }

        public Location EditLocation(int LocationId)
        {
            Location EditLocation = _context.Locs.Where(m => m.LocationId == LocationId).FirstOrDefault();
            return EditLocation;
        }

        public List<Location> GetAllLocation()
        {
            List<Location> allLocation = new List<Location>();
            allLocation = _context.Locs.ToList();
            return allLocation;
        }
    }
}
