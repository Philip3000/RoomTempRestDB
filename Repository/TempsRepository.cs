using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;

namespace RoomTempRestDB.Repository
{
    public class TempsRepository
    {
        //Bruger connectionString, slet evt. "keys" den ikke kan finde - Data mellemrum Source med @ foran ""
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=tempMeasurement;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        
        //Laver en ny liste baseret på databasen og returnerer den nye liste
        public List<RoomTemp> GetAll()
        {
            List<RoomTemp> roomTemps = new List<RoomTemp>();
            //Using(sql conn = new sql )
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //åbner forbindelsen
                connection.Open();
                string query = "SELECT * FROM RoomTemp";

                //Laver en ny sql commando som bliver eksekveret ved sqldatareader reader = cmd.executeReader()
                SqlCommand cmd = new SqlCommand(query, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RoomTemp roomTemp = new RoomTemp()
                        {
                            Id = (int)reader["Id"],
                            RoomNo = (string)reader["RoomNo"],
                            Temp_C = (int)reader["Temp_C"],
                            Day = (string)reader["Day"]
                        };
                        roomTemps.Add(roomTemp);
                    }
                }
            }
            return roomTemps;
        } 
        public RoomTemp GetById(int id) 
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM RoomTemp where id = {id}";

                SqlCommand cmd = new SqlCommand(query, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    //If i stedet for while da det kun er et enkelt objekt der returneres
                    if (reader.Read())
                    {
                        RoomTemp roomTemp2 = new RoomTemp()
                        {
                            Id = (int)reader["Id"],
                            RoomNo = (string)reader["RoomNo"],
                            Temp_C = (int)reader["Temp_C"],
                            Day = (string)reader["Day"]
                        };
                        return roomTemp2;
                    }
                }
            }
            return null;
        }
        public RoomTemp Add(RoomTemp roomTemp)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO RoomTemp (RoomNo, Temp_C, Day) VALUES ('{roomTemp.RoomNo}', '{roomTemp.Temp_C}', '{roomTemp.Day}')";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            return roomTemp;
        }

        ///Passer på sql injection angreb, så er en mere sikker ADD metode
        
        //public RoomTemp Add(RoomTemp roomTemp)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string query = "INSERT INTO RoomTemp (RoomNo, Temp_C, Day) VALUES (@RoomNo, @Temp_C, @Day)";
        //        SqlCommand command = new SqlCommand(query, connection);

        //        command.Parameters.AddWithValue("@RoomNo", roomTemp.RoomNo);
        //        command.Parameters.AddWithValue("@Temp_C", roomTemp.Temp_C);
        //        command.Parameters.AddWithValue("@Day", roomTemp.Day);
        //        command.ExecuteNonQuery();
        //    }
        //    return roomTemp;
        //}     
    }
}
