using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spark.Models.Queries;
using Spark.Models.Tables;

namespace Spark.Data.Interfaces
{
    public interface ICardInfoRepository
    {
        CardInfo GetById(int CardId);
        void Insert(CardInfo cardInfo);
        void Update(CardInfo cardInfo);
        void Delete(int CardId);
        IEnumerable<CardShortItem> GetMostExpensive();
        CardLongItem GetDetails(int CardId);
        IEnumerable<CardLongItem> Search(CardInfoSearchParameters parameters);
    }
}
