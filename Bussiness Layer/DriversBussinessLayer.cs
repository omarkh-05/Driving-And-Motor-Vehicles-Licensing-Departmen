using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriversDataLayer;
using Bussiness_Layer;
using System.Data;
using LicenseBussinessLayer;
using InternationalLicenseBussinessLayer;

namespace DriversBussinessLayer
{
   public class DriversBussiness
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public Bussiness PersonInfo;

        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { get; }


        public DriversBussiness()

        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;

        }
        public DriversBussiness(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)

        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = Bussiness.Find(PersonID);

            Mode = enMode.Update;
        }



        private bool _AddNewDriver()
        {
            this.DriverID = DriversData.AddNewDriver(PersonID, CreatedByUserID);
            return (this.DriverID != -1);
        }
        private bool _UpdateDriver()
        {
            return DriversData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }


        public static DriversBussiness FindByDriverID(int DriverID)
        {

            int PersonID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (DriversData.GetDriverInfoByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))

                return new DriversBussiness(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }
        public static DriversBussiness FindByPersonID(int PersonID)
        {

            int DriverID = -1; int CreatedByUserID = -1; DateTime CreatedDate = DateTime.Now;

            if (DriversData.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))

                return new DriversBussiness(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;

        }


        public static DataTable GetAllDrivers()
        {
            return DriversData.GetAllDrivers();
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDriver();

            }

            return false;
        }

        public static DataTable GetLicenses(int DriverID)
        {
            return LicenseBussiness.GetDriverLicenses(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return InternationalLicenseBussiness.GetDriverInternationalLicenses(DriverID);
        }
    }
}
