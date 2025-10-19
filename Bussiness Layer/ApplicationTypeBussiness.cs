using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTypeData;

namespace AppTypeBussiness
{
    public class ApplicationTypeBussiness
    {
        private enum enMode { AddMode = 1, UpdateMode = 2 };
        enMode _mode = enMode.UpdateMode;
        public int _ApplicationTypeID { get; set; }
        public string _ApplicationTypeTitle { get; set; }
        public float _ApplicationFees { get; set; }


        ApplicationTypeBussiness(int ApplicationTypeID, string applicationTypeTitle, float applicationFees)
        {
            _ApplicationTypeID = ApplicationTypeID;
            _ApplicationTypeTitle = applicationTypeTitle;
            _ApplicationFees = applicationFees;
        }

        public static DataTable GetAllAppTypes()
        {
            return ApplicationTypeData.GetAllApplicationsType();
        }

        public static ApplicationTypeBussiness Find(int ApplicationTypeID)
        {
            string Apptype = "";
            float AppFees = 0;

            if (ApplicationTypeData.GetApplicationInfoByID(ApplicationTypeID, ref Apptype, ref AppFees))
                return new ApplicationTypeBussiness(ApplicationTypeID, Apptype, AppFees);
            else
                return null;
        }

        public bool Update()
        {
            return ApplicationTypeData.Update(_ApplicationTypeID, _ApplicationTypeTitle, _ApplicationFees);
        }

        public bool Save()
        {
            switch(_mode)
            {
                case enMode.UpdateMode:
                    return Update();
            }
            return false;
        }
    }
}
