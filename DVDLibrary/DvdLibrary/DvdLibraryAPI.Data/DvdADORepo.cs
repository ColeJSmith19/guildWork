using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibraryAPI.Data
{
    class DvdADORepo : IDvdRepo
    {
        public void Add(Dvd dvd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdDatabase"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "AddDvd";

                cmd.Parameters.AddWithValue("@DvdTitle", dvd.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", dvd.ReleaseYear);
                cmd.Parameters.AddWithValue("@DirectorName", dvd.Director);
                cmd.Parameters.AddWithValue("@RatingName", dvd.Rating);
                cmd.Parameters.AddWithValue("@DvdNotes", dvd.Notes);
                cmd.Parameters.AddWithValue("@DvdReleaseYearID", SqlDbType.Int);
                cmd.Parameters.AddWithValue("@DvdDirectorID", SqlDbType.Int);
                cmd.Parameters.AddWithValue("@DvdRatingID", SqlDbType.Int);
                //cmd.Parameters.AddWithValue("@DvdID", SqlDbType.Int);


                conn.Open();
                cmd.ExecuteNonQuery();
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        Dvd currentRow = new Dvd();

                //        currentRow.DvdID = (int)dr["DvdID"];
                //        currentRow.Title = dr["DvdTitle"].ToString();
                //        currentRow.ReleaseYear = (short)dr["ReleaseYear"];
                //        currentRow.Director = dr["DirectorName"].ToString();
                //        currentRow.Rating = dr["RatingName"].ToString();

                //        if (dr["Notes"] != DBNull.Value)
                //        {
                //            currentRow.Notes = dr["Notes"].ToString();
                //        }

                //        dvds.Add(currentRow);
                //    }
                //}
            }
        }

        public void Delete(int dvdID)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdDatabase"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "DeleteDvd";

                cmd.Parameters.AddWithValue("@DvdID", dvdID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Edit(Dvd dvd)
        {
            
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdDatabase"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "UpdateDvd";

                cmd.Parameters.AddWithValue("@DvdId", dvd.DvdID);
                cmd.Parameters.AddWithValue("@DvdTitle", dvd.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", dvd.ReleaseYear);
                cmd.Parameters.AddWithValue("@DirectorName", dvd.Director);
                cmd.Parameters.AddWithValue("@RatingName", dvd.Rating);
                cmd.Parameters.AddWithValue("@DvdNotes", dvd.Notes);
                cmd.Parameters.AddWithValue("@DvdReleaseYearID", SqlDbType.Int);
                cmd.Parameters.AddWithValue("@DvdDirectorID", SqlDbType.Int);
                cmd.Parameters.AddWithValue("@DvdRatingID", SqlDbType.Int);


                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Dvd> GetAll()
        {
            List<Dvd> dvds = new List<Dvd>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdDatabase"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "GetAllDvds";

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();

                        currentRow.DvdID = (int)dr["DvdID"];
                        currentRow.Title = dr["DvdTitle"].ToString();
                        currentRow.ReleaseYear = (short)dr["ReleaseYear"];
                        currentRow.Director = dr["DirectorName"].ToString();
                        currentRow.Rating = dr["RatingName"].ToString();

                        if(dr["Notes"] != DBNull.Value)
                        {
                            currentRow.Notes = dr["Notes"].ToString();
                        }

                        dvds.Add(currentRow);
                    }
                }
            }
            return dvds;
            //throw new NotImplementedException();
        }

        public List<Dvd> GetByDirector(string dvdDirector)
        {
            List<Dvd> dvds = new List<Dvd>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdDatabase"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "GetDvdByDirector";
                cmd.Parameters.AddWithValue("@DvdDirector", dvdDirector);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();

                        currentRow.DvdID = (int)dr["DvdID"];
                        currentRow.Title = dr["DvdTitle"].ToString();
                        currentRow.ReleaseYear = (short)dr["ReleaseYear"];
                        currentRow.Director = dr["DirectorName"].ToString();
                        currentRow.Rating = dr["RatingName"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                        {
                            currentRow.Notes = dr["Notes"].ToString();
                        }

                        dvds.Add(currentRow);
                    }
                }
            }
            return dvds;
        }
        public Dvd GetByID(int dvdID)
        {
            Dvd dvds = new Dvd();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdDatabase"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "GetDvdByID";
                cmd.Parameters.AddWithValue("@DvdID", dvdID);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();

                        currentRow.DvdID = (int)dr["DvdID"];
                        currentRow.Title = dr["DvdTitle"].ToString();
                        currentRow.ReleaseYear = (short)dr["ReleaseYear"];
                        currentRow.Director = dr["DirectorName"].ToString();
                        currentRow.Rating = dr["RatingName"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                        {
                            currentRow.Notes = dr["Notes"].ToString();
                        }

                        dvds = currentRow;
                    }
                }
            }
            return dvds;
        }

        public List<Dvd> GetByRating(string dvdRating)
        {
            List<Dvd> dvds = new List<Dvd>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdDatabase"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "GetDvdByRating";
                cmd.Parameters.AddWithValue("@DvdRating", dvdRating);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();

                        currentRow.DvdID = (int)dr["DvdID"];
                        currentRow.Title = dr["DvdTitle"].ToString();
                        currentRow.ReleaseYear = (short)dr["ReleaseYear"];
                        currentRow.Director = dr["DirectorName"].ToString();
                        currentRow.Rating = dr["RatingName"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                        {
                            currentRow.Notes = dr["Notes"].ToString();
                        }

                        dvds.Add(currentRow);
                    }
                }
            }
            return dvds;
        }

        public List<Dvd> GetByTitle(string dvdTitle)
        {
            List<Dvd> dvds = new List<Dvd>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdDatabase"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "GetDvdByTitle";
                cmd.Parameters.AddWithValue("@DvdTitle", dvdTitle);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();

                        currentRow.DvdID = (int)dr["DvdID"];
                        currentRow.Title = dr["DvdTitle"].ToString();
                        currentRow.ReleaseYear = (short)dr["ReleaseYear"];
                        currentRow.Director = dr["DirectorName"].ToString();
                        currentRow.Rating = dr["RatingName"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                        {
                            currentRow.Notes = dr["Notes"].ToString();
                        }

                        dvds.Add(currentRow);
                    }
                }
            }
            return dvds;
        }

        public List<Dvd> GetByYear(int dvdYear)
        {
            List<Dvd> dvds = new List<Dvd>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdDatabase"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "GetDvdByReleaseYear";
                cmd.Parameters.AddWithValue("@DvdReleaseYear", dvdYear);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();

                        currentRow.DvdID = (int)dr["DvdID"];
                        currentRow.Title = dr["DvdTitle"].ToString();
                        currentRow.ReleaseYear = (short)dr["ReleaseYear"];
                        currentRow.Director = dr["DirectorName"].ToString();
                        currentRow.Rating = dr["RatingName"].ToString();

                        if (dr["Notes"] != DBNull.Value)
                        {
                            currentRow.Notes = dr["Notes"].ToString();
                        }

                        dvds.Add(currentRow);
                    }
                }
            }
            return dvds;
        }
    }
}
