using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DvdLibrary.Models.IdentityEF
{
    public class DvdIdentityDbContext : IdentityDbContext<AppUser>
    {
        public DvdIdentityDbContext() : base("DvdDatabase")
        {

        } 
    }
}