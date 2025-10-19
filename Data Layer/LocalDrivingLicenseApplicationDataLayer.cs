using DataSettings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LocalDrivingLicenseApplicationDataLayer
{
    public class ldlApplicationData
    {
        public static DataTable GetAllLDLApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "Select * from LocalDrivingLicenseApplications_View";
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
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "SELECT * FROM LicenseClasses";
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


        public static bool GetInfoByLocalDrivingLicenseApplicationID(int localDrivingLicenseApplicationID,
        ref int applicationID, ref int licenseClassID)
        {
            bool isFound = false;

            string query = @"SELECT * FROM LocalDrivingLicenseApplications 
                     WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        applicationID = (int)reader["ApplicationID"];
                        licenseClassID = (int)reader["LicenseClassID"];
                        isFound = true;
                    }

                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }
        public static bool GetInfoByApplicationID(int applicationID,
        ref int localDrivingLicenseApplicationID, ref int licenseClassID)
        {
            bool isFound = false;

            string query = @"SELECT LocalDrivingLicenseApplicationID, LicenseClassID 
                     FROM LocalDrivingLicenseApplications 
                     WHERE ApplicationID = @ApplicationID";

            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        localDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                        licenseClassID = (int)reader["LicenseClassID"];
                        isFound = true;
                    }

                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }


        public static int AddNewLDLApplication(int ApplicationID, int LicenseClassID)
        {
            int rowsAffected = -1;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
                string query = @"INSERT INTO LocalDrivingLicenseApplications
                        ( ApplicationID,LicenseClassID)
                         VALUES ( @ApplicationID, @LicenseClassID); SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
                
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                    try
                    {
                        connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    rowsAffected = Convert.ToInt32(result); // بدل TryParse
                }

            }
            catch (Exception ex)
                    {
                        Console.WriteLine("Insert Error: " + ex.Message);
                rowsAffected = -1;
                    }
                    finally
                    {
                connection.Close();
                    }

                        return rowsAffected;
                    }
        public static bool UpdateLDLApplication(int LocalDrivingLicenseApplicationID,int ApplicationID, int LicenseClassID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = @"Update  LocalDrivingLicenseApplications  
                            set ApplicationID = @ApplicationID, 
                                LicenseClassID = @LicenseClassID
                                where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
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
        public static bool DeleteLDLApplication(int LocalDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = @"Delete  LocalDrivingLicenseApplications  
                                where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
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


        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = @" SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                {
                    Result = returnedResult;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return Result;

        }
        public static bool DoesPersonAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return IsFound;
        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {

            bool Result = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                            AND(TestAppointments.TestTypeID = @TestTypeID) and isLocked=0
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null)
                {
                    Result = true;
                }

            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return Result;

        }

        public static int TestTrial(int LocalDrivingLicenseApplicationID, int TestTypeID)
        { int Trails = 0;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = @"  SELECT TotalTrialsPerTest = count(TestID)
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    Trails = Convert.ToInt32(result);
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }
            return Trails;
        }
    }
}
