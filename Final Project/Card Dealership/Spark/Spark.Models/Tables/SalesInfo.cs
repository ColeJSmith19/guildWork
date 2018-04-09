using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Models.Tables
{
    public class SalesInfo
    {
        public int SalesId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public int CityId { get; set; }
        public string StateId { get; set; }
        public string Zipcode { get; set; }
        public int PurchasePrice { get; set; }
        public int CardId { get; set; }
    }
}
