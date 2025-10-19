using DataSettings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ApplicationDataLayer
{
    public class ApplicationData
    {
       public static bool GetAllApplicationInfo(int applicationID,ref int applicantPersonID, ref DateTime applicationDate,
           ref int applicationTypeID, ref byte applicationStatus, ref DateTime lastStatusDate,
           ref float paidFees, ref int createdByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", applicationID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;
                    applicantPersonID = Convert.ToInt32(Reader["ApplicantPersonID"]);
                    applicationDate = Convert.ToDateTime(Reader["ApplicationDate"]);
                    applicationTypeID = Convert.ToInt32(Reader["ApplicationTypeID"]);
                    applicationStatus = Convert.ToByte(Reader["ApplicationStatus"]);
                    lastStatusDate = Convert.ToDateTime(Reader["LastStatusDate"]);
                    paidFees = Convert.ToSingle(Reader["PaidFees"]);
                    createdByUserID = Convert.ToInt32(Reader["CreatedByUserID"]);

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

        public static int AddNewApplication(int applicantPersonID, DateTime applicationDate,
    int applicationTypeID, byte applicationStatus, DateTime lastStatusDate,
    float paidFees, int createdByUserID)
        {
            int newID = -1;
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                string query = @"INSERT INTO Applications 
                        (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                         VALUES 
                        (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
                         SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicantPersonID", applicantPersonID);
                command.Parameters.AddWithValue("@ApplicationDate", applicationDate);
                command.Parameters.AddWithValue("@ApplicationTypeID", applicationTypeID);
                command.Parameters.AddWithValue("@ApplicationStatus", applicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", lastStatusDate);
                command.Parameters.Add("@PaidFees", System.Data.SqlDbType.Money).Value = paidFees;
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                        newID = Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error (Add): " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return newID;
        }

        public static bool UpdateApplication(int applicationID, int applicantPersonID, DateTime applicationDate,
    int applicationTypeID, byte applicationStatus, DateTime lastStatusDate,
    float paidFees, int createdByUserID)
        {
            bool isUpdated = false;
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                string query = @"UPDATE Applications SET 
                        ApplicantPersonID = @ApplicantPersonID,
                        ApplicationDate = @ApplicationDate,
                        ApplicationTypeID = @ApplicationTypeID,
                        ApplicationStatus = @ApplicationStatus,
                        LastStatusDate = @LastStatusDate,
                        PaidFees = @PaidFees,
                        CreatedByUserID = @CreatedByUserID
                        WHERE ApplicationID = @ApplicationID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicantPersonID", applicantPersonID);
                command.Parameters.AddWithValue("@ApplicationDate", applicationDate);
                command.Parameters.AddWithValue("@ApplicationTypeID", applicationTypeID);
                command.Parameters.AddWithValue("@ApplicationStatus", applicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", lastStatusDate);
                command.Parameters.Add("@PaidFees", System.Data.SqlDbType.SmallMoney).Value = paidFees;
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);

                try
                {
                    connection.Open();
                    isUpdated = command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error (Update): " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return isUpdated;
        }

        public static bool DeleteApplication(int applicationID)
        {
            bool isDeleted = false;
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);

                try
                {
                    connection.Open();
                    isDeleted = command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error (Delete): " + ex.Message);
                }
            }
            return isDeleted;
        }

        public static bool IsApplicationExist(int AppID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = "SELECT Found=1 FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", AppID);

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

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (IsActiveApplication(PersonID, ApplicationTypeID) != -1);
        }

        public static int IsActiveApplication(int ApplicantPersonID , int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = "Select IsActiveApplications = ApplicationID From Applications Where ApplicantPersonID = @ApplicantPersonID And ApplicationTypeID = @ApplicationTypeID And ApplicationStatus = 1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object reader = command.ExecuteScalar();

                if (reader != null && int.TryParse(reader.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }

        public static int  IsActiveApplicationByLicenseClass(int ApplicantPersonID, int ApplicationTypeID,int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = @"Select IsActiveApplications = Applications.ApplicationID From Applications Inner join
                LocalDrivingLicenseApplications On Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                Where ApplicantPersonID = @ApplicantPersonID And
                ApplicationTypeID = @ApplicationTypeID And 
                LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID And
                ApplicationStatus = 1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object reader = command.ExecuteScalar();

                if (reader != null && int.TryParse(reader.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }

        public static int GetCompletedApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=3";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }

        public static bool UpdateStatus(int ApplicationID , short NewStatus)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = @"Update Applications set 
                             ApplicationStatus = @ApplicationStatus,
                             LastStatusDate = @LastStatusDate 
                             Where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

            try
            {
                connection.Open();
                isFound = command.ExecuteNonQuery() > 0;

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
