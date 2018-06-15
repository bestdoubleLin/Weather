using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace WeatherDb
{
    public class Weatherdb
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["weatherdb"].ConnectionString;

        public void Write(string contents)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO weatherdb(contents) VALUES(@contents)", connection);
                command.Parameters.AddWithValue("contents", contents);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public string Read()
        {
            using (var connection=new SqlConnection(_connectionString))
            {
                var contents = new StringBuilder();
                var command = new SqlCommand("SELECT contents FROM weatherdb", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contents.Append(reader.GetString(0));
                    contents.AppendLine();
                }
                return contents.ToString();
            }
               
        }
    }
}
