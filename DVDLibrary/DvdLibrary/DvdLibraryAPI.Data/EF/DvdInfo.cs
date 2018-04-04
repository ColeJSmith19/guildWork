using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibraryAPI.Data.EF
{
    public class DvdInfo
    {
        [Key]
        public int DvdID { get; set; }
        public string DvdTitle { get; set; }
        public int ReleaseYearID { get; set; }
        public int RatingID { get; set; }
        public int DirectorID { get; set; }
        public string Notes { get; set; }

        public virtual Release Release { get; set; }
        public virtual Rating Rating { get; set; }
        public virtual Director Director { get; set; }
    }
}
