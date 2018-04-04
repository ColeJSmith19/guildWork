using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdLibrary.Models.EF
{
    public class Director
    {
        [Key]
        public int DirectorID { get; set; }
        public string DirectorName { get; set; }
    }
}