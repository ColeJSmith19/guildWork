using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Models.Queries
{
    public class CardInfoSearchParameters
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string CardName { get; set; }
        public string SetName { get; set; }
        public string ColorName { get; set; }

    }
}
