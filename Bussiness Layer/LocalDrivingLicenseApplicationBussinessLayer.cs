using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Countries_Data;
using LocalDrivingLicenseApplicationDataLayer;
using ApplcationBussinessLayer;
using AppTypeBussiness;
using Bussiness_Layer;
using LicenseClassBussinessLayer;
using static System.Net.Mime.MediaTypeNames;
using TestTypesBussiness;
using ApplicationDataLayer;
using TestBussinessLayer;
using TestDataLayer;
using LicenseBussinessLayer;
using DriversBussinessLayer;

namespace LocalDrivingLicenseApplicationBussinessLayer
{
   public class ldlApplicationBussiness : ApplcationBussiness
    {
        enum enMode {Add=1,Update=2 }
        enMode _Mode = enMode.Add;

        public int _LocalDrivingLicenseApplicationID { get; set; }
        public int _LicenseClassID { get; set; }
        public string _PersonFullName { get { return base.PersonInfo.FullName;}}
        public string _LicenseClassName { get{return LicenseClassBussiness.Find(_LicenseClassID)._ClassName;}}
        LicenseClassBussiness LicenseClassInfo;

        public ldlApplicationBussiness()
        {
            _LocalDrivingLicenseApplicationID = -1;
            _LicenseClassID = -1;
            _Mode = enMode.Add;
        }

        public ldlApplicationBussiness(int LocalDrivingLicenseApplicationID,int applicationID, int applicantPersonID, DateTime applicationDate,
            int applicationTypeID, enApplicationStatus applicationStatus, DateTime lastStatusDate,
            float paidFees, int createdByUserID,int LicenseClassID)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _ApplicationID = applicationID;
            _ApplicantPersonID = applicantPersonID;
            this.PersonInfo = Bussiness.Find(_ApplicantPersonID);
            _ApplicationDate = applicationDate;
            _ApplicationTypeID = applicationTypeID;
            _ApplicationStatus = applicationStatus;
            _LastStatusDate = lastStatusDate;
            _PaidFees = paidFees;
            _CreatedByUserID = createdByUserID;
            _LicenseClassID = LicenseClassID;
            LicenseClassInfo = LicenseClassBussiness.Find(LicenseClassID);
            _Mode = enMode.Update;
        }



        //Find
        public static ldlApplicationBussiness FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int applicationID = -1, licenseClassID = -1;

            if (ldlApplicationData.GetInfoByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID, ref applicationID, ref licenseClassID))
            {
                ApplcationBussiness applcation = ApplcationBussiness.Find(applicationID);

                return new ldlApplicationBussiness(LocalDrivingLicenseApplicationID, applcation._ApplicationID, applcation._ApplicantPersonID
                    ,applcation._ApplicationDate,applcation._ApplicationTypeID, (enApplicationStatus)applcation._ApplicationStatus,applcation._LastStatusDate,
                    applcation._PaidFees,applcation._CreatedByUserID, licenseClassID);
            }
            return null;
        }
        public static ldlApplicationBussiness FindByApplicationID(int applicationID)
        {
            int localDrivingLicenseApplicationID = -1, licenseClassID = -1;

            if (ldlApplicationData.GetInfoByApplicationID(applicationID, ref localDrivingLicenseApplicationID, ref licenseClassID))
            {
                ApplcationBussiness applcation = ApplcationBussiness.Find(applicationID);

                return new ldlApplicationBussiness(localDrivingLicenseApplicationID, applcation._ApplicationID, applcation._ApplicantPersonID
                , applcation._ApplicationDate, applcation._ApplicationTypeID, (enApplicationStatus)applcation._ApplicationStatus, applcation._LastStatusDate,
                applcation._PaidFees, applcation._CreatedByUserID, licenseClassID);
            }
            return null;
        }



        //Fill DataGridView And Compobox
        public static DataTable GetAllLDLApplications()
        {
            return ldlApplicationData.GetAllLDLApplications();
        }
        public static DataTable GetAllLicenseClasses()
        {
            return ldlApplicationData.GetAllLicenseClasses();
        }



        //Crud Op
        public bool Add()
        {
            this._LocalDrivingLicenseApplicationID = ldlApplicationData.AddNewLDLApplication(this._ApplicationID,this._LicenseClassID);
            return (this._LocalDrivingLicenseApplicationID > -1);
        }
        public bool Update()
        {
            return ldlApplicationData.UpdateLDLApplication(this._LocalDrivingLicenseApplicationID,this._ApplicationID,this._LicenseClassID);
        }
        public static bool Deleteldl(int ldlAppID)
        {
            return ldlApplicationData.DeleteLDLApplication(ldlAppID);
        }



        //Save
        public bool Save()
        {
            base._Mode = (ApplcationBussiness.enMode)_Mode;
            if(!base.Save())
            {
                return false;
            }

            switch(_Mode)
            {
                case enMode.Add:
                    if (Add())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return Update();
             }
            return false;
        }



        //  Tests
        public bool DoesPassTestType(TestTypeBussiness.enTestType TestTypeID)
        {
            return ldlApplicationData.DoesPassTestType(this._LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool DoesPersonAttendTestType(TestTypeBussiness.enTestType TestTypeID)
        {
            return ldlApplicationData.DoesPersonAttendTestType(this._LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, TestTypeBussiness.enTestType TestTypeID)
        {
            return ldlApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool IsThereAnActiveScheduledTest(TestTypeBussiness.enTestType TestTypeID)
        {
            return ldlApplicationData.IsThereAnActiveScheduledTest(this._LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public TestBussiness GetLastTestPerTestType(TestTypeBussiness.enTestType TestTypeID)
        {
            return TestBussiness.FindLastTestPerPersonAndLicenseClass(this._ApplicantPersonID, this._LicenseClassID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return TestBussiness.GetPassedTestCount(this._LocalDrivingLicenseApplicationID);
        }
        public bool PassedAllTests()
        {
            return TestBussiness.PassedAllTests(this._LocalDrivingLicenseApplicationID);
        }
        public int TestTrial(TestTypeBussiness.enTestType TestTypeID)
        {
            return ldlApplicationData.TestTrial(this._LocalDrivingLicenseApplicationID,(int) TestTypeID);
        }



        //   License
        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            DriversBussiness Driver = DriversBussiness.FindByPersonID(this._ApplicantPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new DriversBussiness();

                Driver.PersonID = this._ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse

            LicenseBussiness License = new LicenseBussiness();
            License.ApplicationID = this._ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = this._LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo._DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo._ClassFees;
            License.IsActive = true;
            License.IssueReason = LicenseBussiness.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetComplete();

                return License.LicenseID;
            }

            else
                return -1;
        }
        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }
        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return LicenseBussiness.GetActiveLicenseIDByPersonID(this._ApplicantPersonID, this._LicenseClassID);
        }
    }
}
