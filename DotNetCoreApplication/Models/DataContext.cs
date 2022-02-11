using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApplication.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)  //Data base connection Here
        {

        }
        public DbSet<Location> Locs { get; set; }  //All Dbset Declare Here
    }
}
