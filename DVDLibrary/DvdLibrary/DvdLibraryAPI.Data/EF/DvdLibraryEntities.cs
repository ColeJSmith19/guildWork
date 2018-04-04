using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DvdLibraryAPI.Data.EF
{
    public class DvdLibraryEntities : DbContext
    {
        public DvdLibraryEntities() : base("DvdDatabaseEF")
        {

        }

        public virtual DbSet<DvdInfo> Dvds { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Release> Releases { get; set; }
    }
}
