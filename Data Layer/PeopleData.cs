using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSettings;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Net;
using System.Security.Policy;

namespace Data_Layer
{
    public class PeopleData
    {
        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = @"SELECT PersonID, NationalNo,
                            FirstName,SecondName,ThirdName, LastName,
                                DateOfBirth,Gendor,CASE WHEN Gendor = 0 THEN 'Male' 
                                ELSE 'Female' End As GendorCaption , Address, Phone, Email, 
                                NationalityCountryID, CountryName, ImagePath      FROM  
                                People INNER JOIN  Countries ON NationalityCountryID = CountryID
                                ORDER BY FirstName";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if(Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
               
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }


        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName, string ThirdName,string LastName
         ,DateTime DateOfBirth,int Gendor,string Address,string Phone,string Email,int NationalityCountryID,string ImagePath)
        {
            int PersonID = -1;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = @"INSERT INTO People (NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor,Address,Phone,Email,NationalityCountryID,ImagePath)
        VALUES
           (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName, @DateOfBirth,@Gendor,@Address,@Phone, @Email, @NationalityCountryID,@ImagePath);SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName",string.IsNullOrWhiteSpace(ThirdName)? (object)DBNull.Value : ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(Email) ? (object)DBNull.Value : Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            command.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(ImagePath) ? (object)DBNull.Value : ImagePath);
           
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
    

        public static bool Update(int PersonID ,string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName
         , DateTime DateOfBirth, int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = @"Update  People  
                            set NationalNo = @NationalNo, 
                                FirstName = @FirstName, 
                                SecondName = @SecondName, 
                                ThirdName = @ThirdName, 
                                LastName = @LastName, 
                                DateOfBirth = @DateOfBirth,
                                Gendor = @Gendor,
                                Address =@Address,
                                Phone =@Phone,
                                Email =@Email,
                                NationalityCountryID = @NationalityCountryID,
                                ImagePath = @ImagePath
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
             catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally 
             {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


        public static bool GetPersonInfoByID( int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref int Gendor,
        ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "Select * from People Where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;
                    NationalNo = (string)Reader["NationalNo"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    if (Reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)Reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)Reader["LastName"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    if (Reader["Gendor"] != DBNull.Value)
                        Gendor = Convert.ToInt32(Reader["Gendor"]);
                    Address = (string)Reader["Address"];
                    Phone = (string)Reader["Phone"];
                    if (Reader["Email"] != DBNull.Value)
                    {
                        Email = (string)Reader["Email"];
                    }else
                    {
                    Email = "";
                    }
                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    if (Reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)Reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
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



        public static bool GetPersonInfoByNationalNo( string NationalNo,ref int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref int Gendor,
        ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "Select * from People Where NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)Reader["PersonID"];
                    NationalNo = (string)Reader["NationalNo"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    if (Reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)Reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)Reader["LastName"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    if (Reader["Gendor"] != DBNull.Value)
                        Gendor = Convert.ToInt32(Reader["Gendor"]);
                    Address = (string)Reader["Address"];
                    Phone = (string)Reader["Phone"];
                    if (Reader["Email"] != DBNull.Value)
                    {
                        Email = (string)Reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    if (Reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)Reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
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


        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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


        public static bool Delete(int PersonID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = @"DELETE FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
    }
}                                          