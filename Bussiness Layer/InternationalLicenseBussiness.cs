using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternationalLicenseDataLayer;
using ApplcationBussinessLayer;
using DriversBussinessLayer;
using AppTypeBussiness;
using System.Diagnostics;
using ApplicationDataLayer;

namespace InternationalLicenseBussinessLayer
{
   public class InternationalLicenseBussiness : ApplcationBussiness
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public DriversBussiness DriverInfo;
        public int InternationalLicenseID { set; get; }
        public int DriverID { set; get; }
        public int IssuedUsingLocalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }


        public InternationalLicenseBussiness()

        {
            //here we set the applicaiton type to New International License.
            this._ApplicationTypeID = (int)ApplcationBussiness.enApplicationType.NewInternationalLicense;

            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;

            this.IsActive = true;


            Mode = enMode.AddNew;

        }

        public InternationalLicenseBussiness(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID,
             int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)

        {
            //this is for the base clase
            base._ApplicationID = ApplicationID;
            base._ApplicantPersonID = ApplicantPersonID;
            base._ApplicationDate = ApplicationDate;
            base._ApplicationTypeID = (int)ApplcationBussiness.enApplicationType.NewInternationalLicense;
            base._ApplicationStatus = ApplicationStatus;
            base._LastStatusDate = LastStatusDate;
            base._PaidFees = PaidFees;
            base._CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this._ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this._CreatedByUserID = CreatedByUserID;

            this.DriverInfo = DriversBussiness.FindByDriverID(this.DriverID);

            Mode = enMode.Update;
        }



        private bool _AddNewInternationalLicense()
        {
            //call DataAccess Layer 

            this.InternationalLicenseID =
                InternationalLicenseData.AddNewInternationalLicense(this._ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
               this.IssueDate, this.ExpirationDate,
               this.IsActive, this._CreatedByUserID);


            return (this.InternationalLicenseID != -1);
        }
        private bool _UpdateInternationalLicense()
        {
            //call DataAccess Layer 

            return InternationalLicenseData.UpdateInternationalLicense(
                this.InternationalLicenseID, this._ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
               this.IssueDate, this.ExpirationDate,
               this.IsActive, this._CreatedByUserID);
        }


        public static InternationalLicenseBussiness Find(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1; int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true; int CreatedByUserID = 1;

            if (InternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID,
                ref IssuedUsingLocalLicenseID,
            ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                //now we find the base application
                ApplcationBussiness Application = ApplcationBussiness.Find(ApplicationID);


                return new InternationalLicenseBussiness(Application._ApplicationID,
                    Application._ApplicantPersonID,
                                     Application._ApplicationDate,
                                    (enApplicationStatus)Application._ApplicationStatus, Application._LastStatusDate,
                                     Application._PaidFees, Application._CreatedByUserID,
                                     InternationalLicenseID, DriverID, IssuedUsingLocalLicenseID,
                                         IssueDate, ExpirationDate, IsActive);

            }

            else
                return null;

        }
        public static DataTable GetAllInternationalLicenses()
        {
            return InternationalLicenseData.GetAllInternationalLicenses();

        }


        public bool Save()
        {
            if (_AddNewInternationalLicense())
            {
                return true;
            }
            return false;
        }




        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return InternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return InternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
    }
}
