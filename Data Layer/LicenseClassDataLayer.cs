using DataSettings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseClassDataLayer
{
    public class LicenseClassData
    {
        public static bool FindByLicenseClassID(int LicenseClassID, ref string ClassName, ref string ClassDescription, ref byte MinAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);
            string query = "Select * from LicenseClasses Where LicenseClassID = @LicenseClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;
                    ClassName = (string)Reader["ClassName"];
                    ClassDescription = (string)Reader["ClassDescription"];
                    MinAllowedAge = (byte)Reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)Reader["DefaultValidityLength"];
                    ClassFees = (float)(decimal)Reader["ClassFees"]; // ✅ هنا التعديل المهم
                }
                else
                {
                    IsFound = false;
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                //SqlError ("خطأ أثناء قراءة بيانات الفئة: " + ex.Message); // ✅ لعرض الخطأ الحقيقي
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }


        public static bool FindByClassName(string ClassName, ref int LicenseClassID,
           ref string ClassDescription, ref byte MinimumAllowedAge,
          ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = "SELECT * FROM LicenseClasses WHERE ClassName = @ClassName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);

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


        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Settings.ConnectionString);

            string query = "SELECT * FROM LicenseClasses order by ClassName";

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
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
    }
}
