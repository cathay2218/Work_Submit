using Parser.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Parser.DataBase
{
    public class DBInteractive
    {
        private const string _connect_str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\Visual Studio 2015\Projects\Github_Repository\Work_Submit\3. Web MVC\Parser\AppData\DataBase.mdf;Integrated Security = True";

        public void DataBase_Write(Data_Repository insert)
        {
            SqlConnection connection = new SqlConnection(_connect_str);
            connection.Open();

            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = string.Format(@"INSERT INTO Data(SiteName, UVI, PublishAgency, County, WGS84Lon, WGS84Lat, PublishTime)
                 VALUES (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}')",
                 insert._Parser_SiteName, insert._Parser_UVI, insert._Parser_PublishAgency, insert._Parser_County, insert._Parser_WGS84Lon, insert._Parser_WGS84Lat, insert._Parser_PublishTime);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Data_Repository> DataBase_Read()
        {
            List<Data_Repository> temp = new List<Data_Repository>();

            SqlConnection connection = new SqlConnection(_connect_str);
            connection.Open();

            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = string.Format(@"SELECT * FROM Data");
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Data_Repository stream = new Data_Repository();

                stream._Parser_SiteName = reader["SiteName"].ToString();
                stream._Parser_UVI = reader["UVI"].ToString();
                stream._Parser_PublishAgency = reader["PublishAgency"].ToString();
                stream._Parser_County = reader["County"].ToString();
                stream._Parser_WGS84Lon = reader["WGS84Lon"].ToString();
                stream._Parser_WGS84Lat = reader["WGS84Lat"].ToString();
                stream._Parser_PublishTime = reader["PublishTime"].ToString();

                temp.Add(stream);
            }

            connection.Close();

            return temp;
        }

        public void DataBase_Clear()
        {
            SqlConnection connection = new SqlConnection(_connect_str);
            connection.Open();

            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = string.Format(@"TRUNCATE TABLE Data;");

            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Data_Repository> FindByID(string ID)
        {
            List<Data_Repository> temp = new List<Data_Repository>();
            
            SqlConnection connection = new SqlConnection(_connect_str);
            connection.Open();

            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = string.Format(@"Select * from Data where SiteName = N'{0}'", ID);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Data_Repository stream = new Data_Repository();

                stream._Parser_SiteName = reader["SiteName"].ToString();
                stream._Parser_UVI = reader["UVI"].ToString();
                stream._Parser_PublishAgency = reader["PublishAgency"].ToString();
                stream._Parser_County = reader["County"].ToString();
                stream._Parser_WGS84Lon = reader["WGS84Lon"].ToString();
                stream._Parser_WGS84Lat = reader["WGS84Lat"].ToString();
                stream._Parser_PublishTime = reader["PublishTime"].ToString();

                temp.Add(stream);
            }

            connection.Close();

            return temp;
        }
    }
}
