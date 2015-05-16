using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInformationApp
{
    class City
    {
        public int id;
        public string name;
        public string about;
        public string country;

        SqlConnection aConnection = new SqlConnection();

        string connectionString = ConfigurationManager.ConnectionStrings["CityInfoDBConnectionString"].ConnectionString;
        public string SaveCityInfornamtion(City aCity)
        {
            if (aCity.name.Length>=4)
            {
                if (HasthisCityAlreadyinSystem(aCity.name))
                {
                    return "This City Already in System!!! Try another one";
                }
                else
                {
                    aConnection.ConnectionString = connectionString;
                    aConnection.Open();
                    string query = string.Format("INSERT INTO t_city VALUES('{0}','{1}','{2}')", aCity.name, aCity.about, aCity.country);

                    SqlCommand aCommand = new SqlCommand(query, aConnection);
                    int rowAffected = aCommand.ExecuteNonQuery();
                    aConnection.Close();
                    if (rowAffected > 0)
                    {
                        return "Save Successful";
                    }
                    else
                    {
                        return "Failed";
                    } 
                }
                
            }
            else
            {
                return "City Name Must be at least 4 Characters Long";
            }
            
        }

        private bool HasthisCityAlreadyinSystem(string name)
        {
            aConnection.ConnectionString = connectionString;
            aConnection.Open();
            string query = string.Format("SELECT * FROM t_city WHERE name='{0}'", name);

            SqlCommand aCommand = new SqlCommand(query, aConnection);
            SqlDataReader aReader = aCommand.ExecuteReader();
            bool message = aReader.HasRows;
            aConnection.Close();
            return message;
        }

        public List<City> GetAllCityInformation()
        {
            aConnection.ConnectionString = connectionString;
            aConnection.Open();
            string query = string.Format("SELECT * FROM t_city");

            SqlCommand aCommand = new SqlCommand(query, aConnection);
            SqlDataReader aReader = aCommand.ExecuteReader();

            List<City> cities = new List<City>();
            if (aReader.HasRows)
            {
                while (aReader.Read())
                {
                    City aCity = new City();
                    aCity.id = Convert.ToInt32(aReader[0].ToString());
                    aCity.name= aReader[1].ToString();
                    aCity.about= aReader[2].ToString();
                    aCity.country = aReader[3].ToString();

                    cities.Add(aCity);
                }
            }
            aConnection.Close();
            return cities;
        }

        public City GetCityById(int id)
        {
            aConnection.ConnectionString = connectionString;
            aConnection.Open();
            string query = string.Format("SELECT * FROM t_city WHERE id='{0}'", id);

            SqlCommand aCommand = new SqlCommand(query, aConnection);
            SqlDataReader aReader = aCommand.ExecuteReader();

            City aCity = new City();
            while (aReader.Read())
            {

                aCity.id = (int)aReader[0];
                aCity.name = aReader[1].ToString();
                aCity.about = aReader[2].ToString();
                aCity.country = aReader[3].ToString();
                
            }
            aReader.Close();
            aConnection.Close();

            return aCity;
        }

        public string UpdateCity(City aCity, int cityId)
        {
            aConnection.ConnectionString = connectionString;
            aConnection.Open();
            string query = string.Format("UPDATE t_city SET about='{0}',country='{1}' WHERE id ={2}", aCity.about, aCity.country, cityId);

            SqlCommand aCommand = new SqlCommand(query, aConnection);
            int rowAffected = aCommand.ExecuteNonQuery();
            aConnection.Close();
            if (rowAffected > 0)
            {
                return "Update Successful";

            }
            else
            {
                return "Update Failed";
            }
        }

        public List<City> GetAllSearechInformationByCountry(string searchText)
        {
            aConnection.ConnectionString = connectionString;
            aConnection.Open();
            string query = string.Format("SELECT * FROM t_city WHERE country='{0}' ", searchText);

            SqlCommand aCommand = new SqlCommand(query, aConnection);
            SqlDataReader aReader = aCommand.ExecuteReader();

            List<City> cities = new List<City>();
            if (aReader.HasRows)
            {
                while (aReader.Read())
                {
                    City aCity = new City();
                    aCity.id = Convert.ToInt32(aReader[0].ToString());
                    aCity.name = aReader[1].ToString();
                    aCity.about = aReader[2].ToString();
                    aCity.country = aReader[3].ToString();

                    cities.Add(aCity);
                }
            }
            aConnection.Close();
            return cities;
        }

        public List<City> GetAllSearechInformationByCity(string searchText)
        {
            aConnection.ConnectionString = connectionString;
            aConnection.Open();
            string query = string.Format("SELECT * FROM t_city WHERE name='{0}' ", searchText);

            SqlCommand aCommand = new SqlCommand(query, aConnection);
            SqlDataReader aReader = aCommand.ExecuteReader();

            List<City> cities = new List<City>();
            if (aReader.HasRows)
            {
                while (aReader.Read())
                {
                    City aCity = new City();
                    aCity.id = Convert.ToInt32(aReader[0].ToString());
                    aCity.name = aReader[1].ToString();
                    aCity.about = aReader[2].ToString();
                    aCity.country = aReader[3].ToString();

                    cities.Add(aCity);
                }
            }
            aConnection.Close();
            return cities;
        }
    }
}
