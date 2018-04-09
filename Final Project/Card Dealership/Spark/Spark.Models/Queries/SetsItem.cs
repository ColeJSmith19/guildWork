using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Models.Queries
{
    public class SetsItem
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public string SetName { get; set; }
        public string Email { get; set; }
    }
}
