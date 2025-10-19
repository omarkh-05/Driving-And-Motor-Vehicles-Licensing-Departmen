using DataSettings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTypeData
{
    public class ApplicationTypeData
    {
        public static DataTable GetAllApplicationsType()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "Select * from ApplicationTypes";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool GetApplicationInfoByID(int ApplicationID, ref string ApplicationTypeTitle, ref float ApplicationFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;
                    ApplicationTypeTitle = Reader["ApplicationTypeTitle"].ToString();
                    ApplicationFees = Convert.ToSingle(Reader["ApplicationFees"]);
                }
                else
                {
                    IsFound = false;
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool Update(int ApplicationID, string ApplicationTypeTitle, float ApplicationFees)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = @"UPDATE ApplicationTypes 
                     SET ApplicationTypeTitle = @ApplicationTypeTitle, 
                         ApplicationFees = @ApplicationFees 
                     WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                IsUpdated = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during update: " + ex.Message);
                IsUpdated = false;
            }
            finally
            {
                connection.Close();
            }

            return IsUpdated;
        }


    }
}
