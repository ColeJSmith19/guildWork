using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Models.Queries
{
    public class CardShortItem
    {
        public int CardId { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string SetName { get; set; }
        public string CardArtURL { get; set; }
        public int MSRP { get; set; }
    }
}
