using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Models.Tables
{
    public class CardInfo
    {
        public int CardId { get; set; }
        public string CardName { get; set; }
        public int Cmc { get; set; }
        public int CmcId { get; set; }
        public string Colors { get; set; }
        public string CardArtURL { get; set; }
        public string CardText { get; set; }
        public int? Power { get; set; }
        public int? Toughness { get; set; }
        public string SetId { get; set; }
        public string SetName { get; set; }
        public string ArtistName { get; set; }
        public int ArtistId { get; set; }
        public string CardNumber { get; set; }
        public int MSRP { get; set; }
        public int SalePrice { get; set; }
    }
}
