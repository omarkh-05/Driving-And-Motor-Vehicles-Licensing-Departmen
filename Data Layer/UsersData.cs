using DataSettings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;

namespace Data_Layer
{
   public class UsersData
    {
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = @"SELECT  Users.UserID, Users.PersonID,FullName = 
                        People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                        Users.UserName, Users.IsActive FROM  Users INNER JOIN  People ON Users.PersonID = People.PersonID";
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


        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName,
        ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static int AddNewUser(int personID, string userName,string password,bool isActive)
        {
            int PersonID = -1;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = @"INSERT INTO Users (PersonID,UserName,Password,IsActive)
        VALUES
           (@PersonID,@UserName,@Password,@IsActive);SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsActive", isActive);

            try
            {
                connection.Open();
                object Reader = command.ExecuteScalar();
                if (Reader != null && int.TryParse(Reader.ToString(), out int newID))
                {
                    PersonID = newID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
            finally
            {
                connection.Close();
            }
            return PersonID;
        }


        public static bool GetUserInfoByID(int UserID,ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "Select * from Users Where UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)Reader["PersonID"];
                    UserName = (string)Reader["UserName"];
                    Password = (string)Reader["Password"];
                    IsActive = (bool)Reader["IsActive"];
                }
                else
                {
                    IsFound = false;
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        
        }


        public static bool FindUserForLogin(ref int UserID, ref int PersonID, string UserName, string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "Select * from Users Where UserName = @UserName And Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;
                    UserID = (int)Reader["UserID"];
                    PersonID = (int)Reader["PersonID"];
                    UserName = (string)Reader["UserName"];
                    Password = (string)Reader["Password"];
                    IsActive = (bool)Reader["IsActive"];
                }
                else
                {
                    IsFound = false;
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;

        }


        public static bool Update(int UserID, string userName, string password, bool isActive)
        {
            bool isUpdated = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = @"UPDATE Users
                     SET UserName = @UserName,
                         Password = @Password,
                         IsActive = @IsActive
                     WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsActive", isActive);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isUpdated = (rowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during update: " + ex.Message);
                isUpdated = false;
            }
            finally
            {
                connection.Close();
            }

            return isUpdated;
        }

        
        public static bool Delete(int userID)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = "DELETE FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", userID);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isDeleted = (rowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during delete: " + ex.Message);
                isDeleted = false;
            }
            finally
            {
                connection.Close();
            }

            return isDeleted;
        }


        public static bool IsUserExistByPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsUserExist(string UserName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool CheackCurrentPawword(string Password)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE Password = @Password";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }



    }
}
