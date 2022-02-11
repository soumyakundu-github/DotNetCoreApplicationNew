using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApplication.Models.Interface
{
    public interface ILocation
    {
        string SaveLocation(Location model);
        List<Location> GetAllLocation();
        Location EditLocation(int LocationId);
        string DeleteLocation(long LocationId);
    }
}
