using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spark.Data.Interfaces;
using Spark.Models.Queries;
using Spark.Models.Tables;

namespace Spark.Data.ADO
{
    public class CardInfoRepositoryADO : ICardInfoRepository
    {
        public void Delete(int CardId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CardInfoDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CardId", CardId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
        public CardInfo GetById(int CardId)
        {
            CardInfo cardInfo = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CardInfoSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CardId", CardId);

                cn.Open();
                //SELECT CardId, CardName, CmcId, CardArtURL, CardText, Power, Toughness, ArtistId, CardNumber, MSRP, SalePrice
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        cardInfo = new CardInfo();
                        cardInfo.CardId = (int)dr["CardId"];
                        cardInfo.CardName = dr["CardName"].ToString();
                        cardInfo.CmcId = (int)dr["CmcId"];
                        cardInfo.CardArtURL = dr["CardArtURL"].ToString();
                        cardInfo.ArtistId = (int)dr["ArtistId"];
                        cardInfo.CardNumber = dr["CardNumber"].ToString();
                        cardInfo.MSRP = (int)dr["MSRP"];
                        cardInfo.SalePrice = (int)dr["SalePrice"];

                        if (dr["CardText"] != DBNull.Value)
                            cardInfo.CardText = dr["CardText"].ToString();

                        if (dr["Power"] != DBNull.Value)
                            cardInfo.Power = (int)dr["Power"];

                        if (dr["Toughness"] != DBNull.Value)
                            cardInfo.Toughness = (int)dr["Toughness"];

                    }

                }
            }

            return cardInfo;
        }

        public CardLongItem GetDetails(int CardId)
        {
            CardLongItem cardInfo = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CardInfoSelectDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CardId", CardId);

                cn.Open();
                //SELECT CardId, CardName, CmcId, CardArtURL, CardText, Power, Toughness, ArtistId, CardNumber, MSRP, SalePrice
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        cardInfo = new CardLongItem();
                        cardInfo.CardId = (int)dr["CardId"];
                        cardInfo.CardName = dr["CardName"].ToString();
                        cardInfo.CmcAmount = dr["CmcAmount"].ToString();
                        cardInfo.CardArtURL = dr["CardArtURL"].ToString();
                        cardInfo.ArtistName = dr["ArtistName"].ToString();
                        cardInfo.CardNumber = dr["CardNumber"].ToString();
                        cardInfo.MSRP = (int)dr["MSRP"];
                        cardInfo.SalePrice = (int)dr["SalePrice"];
                        cardInfo.SetName = dr["SetName"].ToString();
                        cardInfo.TypeName = dr["TypeName"].ToString();
                        cardInfo.ColorName = dr["ColorName"].ToString();

                        if (dr["CardText"] != DBNull.Value)
                            cardInfo.CardText = dr["CardText"].ToString();

                        if (dr["Power"] != DBNull.Value)
                            cardInfo.Power = (int)dr["Power"];

                        if (dr["Toughness"] != DBNull.Value)
                            cardInfo.Toughness = (int)dr["Toughness"];

                    }

                }
            }

            return cardInfo;
        }


        public IEnumerable<CardShortItem> GetMostExpensive()
        {
            List<CardShortItem> cardInfo = new List<CardShortItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CardInfoSelectMSRP", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CardShortItem row = new CardShortItem();

                        row.CardId = (int)dr["CardId"];
                        row.CardName = dr["CardName"].ToString();
                        row.CardNumber = dr["CardNumber"].ToString();
                        row.SetName = dr["SetName"].ToString();
                        row.CardArtURL = dr["CardArtURL"].ToString();
                        row.MSRP = (int)dr["MSRP"];

                        cardInfo.Add(row);
                    }

                }
            }

            return cardInfo;
        }


        public void Insert(CardInfo cardInfo)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CardInfoInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@CardId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);


                cmd.Parameters.AddWithValue("@CardName", cardInfo.CardName);
                cmd.Parameters.AddWithValue("@CmcId", cardInfo.CmcId);


                cmd.Parameters.AddWithValue("@CardArtURL", cardInfo.CardArtURL);


                if (string.IsNullOrEmpty(cardInfo.CardText))
                {
                    cmd.Parameters.AddWithValue("@CardText", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CardText", cardInfo.CardText);
                }
                if (cardInfo.Power == null)
                    cmd.Parameters.AddWithValue("@Power", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Power", cardInfo.Power);

                if (cardInfo.Toughness == null)
                    cmd.Parameters.AddWithValue("@Toughness", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Toughness", cardInfo.Toughness);

                cmd.Parameters.AddWithValue("@SetName", cardInfo.SetName);
                cmd.Parameters.AddWithValue("@SetId", cardInfo.SetId);
                cmd.Parameters.AddWithValue("@ArtistId", cardInfo.ArtistId);
                cmd.Parameters.AddWithValue("@CardNumber", cardInfo.CardNumber);
                cmd.Parameters.AddWithValue("@MSRP", cardInfo.MSRP);
                cmd.Parameters.AddWithValue("@SalePrice", cardInfo.SalePrice);
                cn.Open();

                cmd.ExecuteNonQuery();

                cardInfo.CardId = (int)param.Value;
            }
        }

        public IEnumerable<CardLongItem> Search(CardInfoSearchParameters parameters)
        {
            List<CardLongItem> cardInfo = new List<CardLongItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "CREATE TABLE #Temp (CardID int, ColorName varchar(50)) INSERT INTO #Temp SELECT i.CardId, c.ColorName FROM CardInfo i " +
                    "JOIN CardColors cc ON cc.CardId = i.CardId JOIN Colors c ON c.ColorId = cc.ColorId CREATE TABLE #ColorString(CardID int, ColorName varchar(50)) " +
                    "INSERT INTO #ColorString Select Main.CardID, Left(Main.Colors, Len(Main.Colors) - 1) As 'Students' From(Select distinct T2.CardId," +
                    "(Select T1.ColorName + ', ' AS[text()] From #Temp T1 Where T1.CardId = T2.CardID ORDER BY T1.CardID For XML PATH(''))[Colors] From #Temp T2)" +
                    " [Main] SELECT DISTINCT CardInfo.CardId, CardName, CmcAmount, CardArtURL, ArtistName, CardNumber, MSRP, SalePrice, SetName, TypeName, #ColorString.ColorName, CardText," +
                    " [Power], Toughness FROM CardInfo INNER JOIN CardSet ON CardInfo.CardId = CardSet.CardId INNER JOIN[Set] ON CardSet.SetId = [Set].SetId " +
                    "INNER JOIN CMC ON CardInfo.CmcId = CMC.CmcId INNER JOIN Artist ON CardInfo.ArtistId = Artist.ArtistId INNER JOIN CardType ON " +
                    "CardInfo.CardId = CardType.CardId INNER JOIN[Type] ON CardType.TypeId = [Type].TypeId INNER JOIN CardColors ON CardInfo.CardId = " +
                    "CardColors.CardId INNER JOIN Colors ON CardColors.ColorId = Colors.ColorId INNER JOIN #ColorString ON CardInfo.CardId = #ColorString.CardID WHERE 1 = 1 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND SalePrice >= @MinPrice ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND SalePrice <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (!string.IsNullOrEmpty(parameters.CardName))
                {
                    query += "AND CardName LIKE @CardName ";
                    cmd.Parameters.AddWithValue("@CardName", '%' + parameters.CardName + '%');
                }

                if (!string.IsNullOrEmpty(parameters.SetName))
                {
                    query += "AND SetName LIKE @SetName ";
                    cmd.Parameters.AddWithValue("@SetName", parameters.SetName + '%');
                }

                if (!string.IsNullOrEmpty(parameters.ColorName))
                {
                    query += "AND #ColorString.ColorName LIKE @ColorName ";
                    cmd.Parameters.AddWithValue("@ColorName", '%' + parameters.ColorName + '%');
                }

                query += "ORDER BY MSRP DESC";

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CardLongItem row = new CardLongItem();
                        row.CardId = (int)dr["CardId"];
                        row.CardName = dr["CardName"].ToString();
                        row.CmcAmount = dr["CmcAmount"].ToString();
                        row.CardArtURL = dr["CardArtURL"].ToString();
                        row.ArtistName = dr["ArtistName"].ToString();
                        row.CardNumber = dr["CardNumber"].ToString();
                        row.MSRP = (int)dr["MSRP"];
                        row.SalePrice = (int)dr["SalePrice"];
                        row.SetName = dr["SetName"].ToString();
                        row.TypeName = dr["TypeName"].ToString();
                        row.ColorName = dr["ColorName"].ToString();

                        if (dr["CardText"] != DBNull.Value)
                            row.CardText = dr["CardText"].ToString();

                        if (dr["Power"] != DBNull.Value)
                            row.Power = (int)dr["Power"];

                        if (dr["Toughness"] != DBNull.Value)
                            row.Toughness = (int)dr["Toughness"];

                        cardInfo.Add(row);
                    }

                }
            }

            return cardInfo;
        }


        public void Update(CardInfo cardInfo)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CardInfoUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CardId", cardInfo.CardId);
                cmd.Parameters.AddWithValue("@CardName", cardInfo.CardName);
                cmd.Parameters.AddWithValue("@CmcId", cardInfo.CmcId);
                cmd.Parameters.AddWithValue("@CardArtURL", cardInfo.CardArtURL);
                cmd.Parameters.AddWithValue("@CardText", cardInfo.CardText);
                if (cardInfo.Power == null)
                    cmd.Parameters.AddWithValue("@Power", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Power", cardInfo.Power);

                if (cardInfo.Toughness == null)
                    cmd.Parameters.AddWithValue("@Toughness", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Toughness", cardInfo.Toughness);

                cmd.Parameters.AddWithValue("@ArtistId", cardInfo.ArtistId);
                cmd.Parameters.AddWithValue("@CardNumber", cardInfo.CardNumber);
                cmd.Parameters.AddWithValue("@MSRP", cardInfo.MSRP);
                cmd.Parameters.AddWithValue("@SalePrice", cardInfo.SalePrice);
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
