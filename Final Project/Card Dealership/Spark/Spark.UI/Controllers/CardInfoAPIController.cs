using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Spark.Data.Factory;
using Spark.Models.Queries;

namespace Spark.UI.Controllers
{
    public class CardInfoAPIController : ApiController
    {
        [Route("Inventory/Cards")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(decimal? minPrice, decimal? maxPrice, string cardName, string setName, string colorName)
        {
            var repo = CardInfoRepositoryFactory.GetRepository();

            try
            {
                var parameters = new CardInfoSearchParameters()
                {
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    CardName = cardName,
                    SetName = setName,
                    ColorName = colorName
                };
                var result = repo.Search(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
