using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spark.Data.ADO;
using Spark.Data.Interfaces;

namespace Spark.Data.Factory
{
    public static class CardInfoRepositoryFactory
    {
        public static ICardInfoRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "ADO":
                    return new CardInfoRepositoryADO();
                default:
                    throw new Exception("Could not find valid Repository type configuration value.");
            }
        }
    }
}
