using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spark.Data.Interfaces;
using Spark.Models.Tables;

namespace Spark.Data.ADO
{
    class ArtistsRepositoryADO : IArtistRepository
    {
        public List<Artist> GetAll()
        {
            List<Artist> artists = new List<Artist>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ArtistsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Artist currentRow = new Artist();
                        currentRow.ArtistId = (int)dr["ArtistId"];
                        currentRow.ArtistName = dr["ArtistName"].ToString();

                        artists.Add(currentRow);
                    }

                }
            }

            return artists;
        }
    }
}
