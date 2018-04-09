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
    public class ColorRepositoryADO : IColorRepository
    {
        public List<Colors> GetAll()
        {
            List<Colors> colors = new List<Colors>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ColorsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Colors currentRow = new Colors();
                        currentRow.ColorId = (int)dr["ColorId"];
                        currentRow.ColorName = dr["ColorName"].ToString();

                        colors.Add(currentRow);
                    }

                }
            }

            return colors;
        }
    }
}

