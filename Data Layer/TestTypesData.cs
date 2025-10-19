using DataSettings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTypesData
{
    public class TestData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "SELECT * FROM TestTypes";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading test types: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool GetTestTypeByID(int TestTypeID, ref string TestTypeTitle, ref decimal TestTypeFees, ref string TestTypeDescription)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    TestTypeTitle = reader["TestTypeTitle"].ToString();
                    TestTypeFees = Convert.ToDecimal(reader["TestTypeFees"]);
                    TestTypeDescription = reader["TestTypeDescription"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting test type: " + ex.Message);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool Update(int TestTypeID, string TestTypeTitle, decimal TestTypeFees, string TestTypeDescription)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = @"UPDATE TestTypes 
                             SET TestTypeTitle = @TestTypeTitle,
                                 TestTypeFees = @TestTypeFees,
                                 TestTypeDescription = @TestTypeDescription
                             WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                IsUpdated = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating test type: " + ex.Message);
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
