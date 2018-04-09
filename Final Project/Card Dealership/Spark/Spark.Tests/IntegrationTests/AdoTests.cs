using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Spark.Data.ADO;
using Spark.Models.Queries;
using Spark.Models.Tables;

namespace Spark.Tests.IntegrationTests
{
    [TestFixture]
    public class AdoTests
    {
        [SetUp]
        public void Init()
        {
            using(var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
        [Test]
        public void CanLoadSets()
        {
            var repo = new SetsRepositoryADO();

            var sets = repo.GetAll();

            Assert.AreEqual(5, sets.Count);

            Assert.AreEqual("CMD", sets[0].SetId);
            Assert.AreEqual("Commander", sets[0].SetName);
        }

        [Test]
        public void CanLoadColors()
        {
            var repo = new ColorRepositoryADO();

            var colors = repo.GetAll();

            Assert.AreEqual(6, colors.Count);

            Assert.AreEqual(1, colors[0].ColorId);
            Assert.AreEqual("White", colors[0].ColorName);
        }

        [Test]
        public void CanLoadCardInfo()
        {
            var repo = new CardInfoRepositoryADO();
            var cardInfo = repo.GetById(1);

            Assert.IsNotNull(cardInfo);

            Assert.AreEqual(1, cardInfo.CardId);
            Assert.AreEqual("Animar, Soul of the Elements", cardInfo.CardName);
            Assert.AreEqual(3, cardInfo.CmcId);
            Assert.AreEqual("animar.jpg", cardInfo.CardArtURL);
            Assert.AreEqual("Protection from white and from black.", cardInfo.CardText);
            Assert.AreEqual(1, cardInfo.Power);
            Assert.AreEqual(1, cardInfo.Toughness);
            Assert.AreEqual(1, cardInfo.ArtistId);
            Assert.AreEqual("186", cardInfo.CardNumber);
            Assert.AreEqual(25, cardInfo.MSRP);
            Assert.AreEqual(20, cardInfo.SalePrice);
        }

        [Test]
        public void NotFoundCardInfoReturnsNull()
        {
            var repo = new CardInfoRepositoryADO();
            var cardInfo = repo.GetById(1000213);

            Assert.IsNull(cardInfo);
        }

        [Test]
        public void CanAddCardInfo()
        {
            CardInfo cardInfoToAdd = new CardInfo();
            var repo = new CardInfoRepositoryADO();

            cardInfoToAdd.CardId = 7;
            cardInfoToAdd.CardName = "Ponder";
            cardInfoToAdd.CmcId = 1;
            cardInfoToAdd.CardArtURL = "ponder.jpg";
            cardInfoToAdd.CardText = "Look at the top 3 cards.";
            cardInfoToAdd.SetName = "Kamigawa";
            cardInfoToAdd.SetId = "KMG";
            cardInfoToAdd.Power = null;
            cardInfoToAdd.Toughness = null;
            cardInfoToAdd.ArtistId = 1;
            cardInfoToAdd.CardNumber = "37";
            cardInfoToAdd.MSRP = 20;
            cardInfoToAdd.SalePrice = 15;

            repo.Insert(cardInfoToAdd);

            Assert.AreEqual(7, cardInfoToAdd.CardId);
        }

        [Test]
        public void CanUpdateCardInfo()
        {
            CardInfo cardInfoToAdd = new CardInfo();
            var repo = new CardInfoRepositoryADO();

            cardInfoToAdd.CardId = 7;
            cardInfoToAdd.CardName = "Ponder";
            cardInfoToAdd.CmcId = 1;
            cardInfoToAdd.CardArtURL = "ponder.jpg";
            cardInfoToAdd.CardText = "Look at the top 3 cards.";
            cardInfoToAdd.Power = 1;
            cardInfoToAdd.Toughness = 1;
            cardInfoToAdd.ArtistId = 1;
            cardInfoToAdd.CardNumber = "37";
            cardInfoToAdd.MSRP = 20;
            cardInfoToAdd.SalePrice = 15;

            repo.Insert(cardInfoToAdd);

            cardInfoToAdd.CardId = 8;
            cardInfoToAdd.CardName = "Brainstorm";
            cardInfoToAdd.CmcId = 1;
            cardInfoToAdd.CardArtURL = "Brainstorm.jpg";
            cardInfoToAdd.CardText = "Draw 3.";
            cardInfoToAdd.Power = null;
            cardInfoToAdd.Toughness = null;
            cardInfoToAdd.ArtistId = 1;
            cardInfoToAdd.CardNumber = "30";
            cardInfoToAdd.MSRP = 25;
            cardInfoToAdd.SalePrice = 10;

            repo.Update(cardInfoToAdd);

            var updatedCardInfo = repo.GetById(8);

            Assert.AreEqual(8, cardInfoToAdd.CardId);
            Assert.AreEqual("Brainstorm", cardInfoToAdd.CardName);
            Assert.AreEqual(1, cardInfoToAdd.CmcId);
            Assert.AreEqual("Brainstorm.jpg", cardInfoToAdd.CardArtURL);
            Assert.AreEqual("Draw 3.", cardInfoToAdd.CardText);
            Assert.AreEqual(null, cardInfoToAdd.Power);
            Assert.AreEqual(null, cardInfoToAdd.Toughness);
            Assert.AreEqual(1, cardInfoToAdd.ArtistId);
            Assert.AreEqual("30", cardInfoToAdd.CardNumber);
            Assert.AreEqual(25, cardInfoToAdd.MSRP);
            Assert.AreEqual(10, cardInfoToAdd.SalePrice);

        }

        [Test]
        public void CanDeleteCardInfo()
        {
            CardInfo cardInfoToAdd = new CardInfo();
            var repo = new CardInfoRepositoryADO();

            cardInfoToAdd.CardId = 7;
            cardInfoToAdd.CardName = "Ponder";
            cardInfoToAdd.CmcId = 1;
            cardInfoToAdd.CardArtURL = "ponder.jpg";
            cardInfoToAdd.CardText = "Look at the top 3 cards.";
            cardInfoToAdd.Power = 1;
            cardInfoToAdd.Toughness = 1;
            cardInfoToAdd.ArtistId = 1;
            cardInfoToAdd.CardNumber = "37";
            cardInfoToAdd.MSRP = 20;
            cardInfoToAdd.SalePrice = 15;

            repo.Insert(cardInfoToAdd);

            var loaded = repo.GetById(7);
            Assert.IsNotNull(loaded);

            repo.Delete(7);
            loaded = repo.GetById(7);

            Assert.IsNull(loaded);
        }

        [Test]
        public void CanLoadMostExpensiveCards()
        {
            var repo = new CardInfoRepositoryADO();
            List<CardShortItem> cardInfo = repo.GetMostExpensive().ToList();

            Assert.AreEqual(6, cardInfo.Count());

            Assert.AreEqual(1, cardInfo[3].CardId);
            Assert.AreEqual("Animar, Soul of the Elements", cardInfo[3].CardName);
            Assert.AreEqual("186", cardInfo[3].CardNumber);
            Assert.AreEqual("Commander", cardInfo[3].SetName);
            Assert.AreEqual("animar.jpg", cardInfo[3].CardArtURL);
            Assert.AreEqual(25, cardInfo[3].MSRP);
        }

        [Test]
        public void CanLoadCardDetails()
        {
            var repo = new CardInfoRepositoryADO();
            var cardInfo = repo.GetDetails(1);

            Assert.IsNotNull(cardInfo);

            Assert.AreEqual(1, cardInfo.CardId);
            Assert.AreEqual("Animar, Soul of the Elements", cardInfo.CardName);
            Assert.AreEqual("3", cardInfo.CmcAmount);
            Assert.AreEqual("animar.jpg", cardInfo.CardArtURL);
            Assert.AreEqual("Protection from white and from black.", cardInfo.CardText);
            Assert.AreEqual(1, cardInfo.Power);
            Assert.AreEqual(1, cardInfo.Toughness);
            Assert.AreEqual("Peter Morbacher", cardInfo.ArtistName);
            Assert.AreEqual("186", cardInfo.CardNumber);
            Assert.AreEqual(25, cardInfo.MSRP);
            Assert.AreEqual(20, cardInfo.SalePrice);
            Assert.AreEqual("Commander", cardInfo.SetName);
            Assert.AreEqual("Legendary Creature", cardInfo.TypeName);
            Assert.AreEqual("Blue, Red, Green", cardInfo.ColorName);
            //Assert.AreEqual("Red", cardInfo.ColorName);
            //Assert.AreEqual("Green", cardInfo.ColorName);
        }

        [Test]
        public void CanSearchOnMinPrice()
        {
            var repo = new CardInfoRepositoryADO();

            var found = repo.Search(new CardInfoSearchParameters { MinPrice = 100M });

            Assert.AreEqual(3, found.Count());
        }

        [Test]
        public void CanSearchOnMaxPrice()
        {
            var repo = new CardInfoRepositoryADO();

            var found = repo.Search(new CardInfoSearchParameters { MaxPrice = 100M });

            Assert.AreEqual(3, found.Count());
        }

        [Test]
        public void CanSearchOnPriceRange()
        {
            var repo = new CardInfoRepositoryADO();

            var found = repo.Search(new CardInfoSearchParameters { MinPrice = 7M, MaxPrice = 250M });

            Assert.AreEqual(4, found.Count());
        }
    }
}
