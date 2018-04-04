﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibraryAPI.Data.EF
{
    public class Rating
    {
        [Key]
        public int RatingID { get; set; }
        public string RatingName { get; set; }
    }
}
