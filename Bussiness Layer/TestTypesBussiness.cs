using DataSettings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTypesData;

namespace TestTypesBussiness
{
    public class TestTypeBussiness
    {
        private enum enMode { AddMode = 1, UpdateMode = 2 };
        private enMode _mode = enMode.UpdateMode;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public decimal TestTypeFees { get; set; }
        public string TestTypeDescription { get; set; }

        private TestTypeBussiness(int testTypeID, string testTypeTitle, decimal testTypeFees, string testTypeDescription)
        {
            TestTypeID = testTypeID;
            TestTypeTitle = testTypeTitle;
            TestTypeFees = testTypeFees;
            TestTypeDescription = testTypeDescription;
        }

        public static DataTable GetAllTestTypes()
        {
            return TestData.GetAllTestTypes();
        }

        public static TestTypeBussiness Find(int testTypeID)
        {
            string title = "";
            decimal fees = 0;
            string description = "";

            if (TestData.GetTestTypeByID(testTypeID, ref title, ref fees, ref description))
            {
                return new TestTypeBussiness(testTypeID, title, fees, description);
            }

            return null;
        }

        public bool Update()
        {
            return TestData.Update(TestTypeID, TestTypeTitle, TestTypeFees, TestTypeDescription);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.UpdateMode:
                    return Update();
            }
            return false;
        }
    }
}
