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
    public class SetsRepositoryADO : ISetsRepository
    {
        public List<Set> GetAll()
        {
            List<Set> sets = new List<Set>();

            using(var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SetsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Set currentRow = new Set();
                        currentRow.SetId = dr["SetId"].ToString();
                        currentRow.SetName = dr["SetName"].ToString();

                        sets.Add(currentRow);
                    }
                   
                }
            }

            return sets;
        }
    }
}
