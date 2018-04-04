using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DvdLibrary.Models.EF
{
    public class DvdLibraryEntities : DbContext
    {
        public DvdLibraryEntities() : base("DvdDatabaseEF")
        {

        }

        public DbSet<DvdInfo> Dvds { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Release> Releases { get; set; }
    }
}