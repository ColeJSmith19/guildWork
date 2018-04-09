using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Models.Queries
{
    public class CardLongItem
    {
        public int CardId { get; set; }
        public string CardName { get; set; }
        public string CmcAmount { get; set; }
        public string CardArtURL { get; set; }
        public string CardText { get; set; }
        public int? Power { get; set; }
        public int? Toughness { get; set; }
        public string ArtistName { get; set; }
        public string CardNumber { get; set; }
        public int MSRP { get; set; }
        public int SalePrice { get; set; }
        public string SetName { get; set; }
        public string TypeName { get; set; }
        public string ColorName { get; set; }

    }
}
