using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TempApp.Core.Entities.Concrete;

namespace TempApp.Core.Contexts
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<TempType> TempTypes { get; set; }
    }
}
