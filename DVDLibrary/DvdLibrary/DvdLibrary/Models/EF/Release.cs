using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdLibrary.Models.EF
{
    public class Release
    {
        [Key]
        public int ReleaseYearID { get; set; }
        public int ReleaseYear { get; set; }
    }
}