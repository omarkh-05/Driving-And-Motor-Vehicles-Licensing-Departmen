using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDataLayer;
using Bussiness_Layer;
using AppTypeBussiness;
using Data_Layer;
using static System.Net.Mime.MediaTypeNames;


namespace ApplcationBussinessLayer
{
    public class ApplcationBussiness
    {
       public enum enMode { Add = 1, Update = 2 };
       public enMode _Mode = enMode.Add;

        public enum enApplicationType { NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3, ReplaceDamagedDrivingLicense = 4,
            ReleaseDetainedDrivingLicense = 5, NewInternationalLicense = 6, RetakeTest = 7 };

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public int _ApplicationID { get; set; }
        public int _ApplicantPersonID { get; set; }
        public Bussiness PersonInfo { get; set; }
        public string _PersonFullName
        {
            get { return Bussiness.Find(_ApplicantPersonID).FullName; }
        }
        public DateTime _ApplicationDate { get; set; }
        public int _ApplicationTypeID { get; set; }

        public ApplicationTypeBussiness ApplicationTypeInfo;
        public enApplicationStatus _ApplicationStatus { get; set; }
        public string _ApplicationStatusText
        {
            get
            {
                switch(_ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    default:
                        return "Unknown";
                }
                    
            }
            
            
        }
        public DateTime _LastStatusDate { get; set; }
        public float _PaidFees { get; set; }
        public int _CreatedByUserID { get; set; }
        public UsersBussiness CreatedByUserInfo;

        public ApplcationBussiness()
        {
            _ApplicationID = -1;
            _ApplicantPersonID = -1;
            _ApplicationDate = DateTime.Now;
            _ApplicationTypeID = -1;
            _ApplicationStatus = enApplicationStatus.New;
            _LastStatusDate = DateTime.Now;
            _PaidFees = 0;
            _CreatedByUserID = -1;
        }

        public ApplcationBussiness(int applicationID, int applicantPersonID, DateTime applicationDate,
            int applicationTypeID, enApplicationStatus applicationStatus, DateTime lastStatusDate,
            float paidFees, int createdByUserID)
        {
            _ApplicationID = applicationID;
            _ApplicantPersonID = applicantPersonID;
            PersonInfo = Bussiness.Find(_ApplicantPersonID);
            _ApplicationDate = applicationDate;
            _ApplicationTypeID = applicationTypeID;
            ApplicationTypeInfo = ApplicationTypeBussiness.Find(applicationTypeID);
            _ApplicationStatus = applicationStatus;
            _LastStatusDate = lastStatusDate;
            _PaidFees = paidFees;
            _CreatedByUserID = createdByUserID;
            CreatedByUserInfo = UsersBussiness.Find(createdByUserID);

            _Mode = enMode.Update;
        }




        private bool _AddNewApplication()
        {
            _ApplicationID = ApplicationData.AddNewApplication(_ApplicantPersonID, _ApplicationDate, _ApplicationTypeID, (Byte)_ApplicationStatus
                , _LastStatusDate, _PaidFees, _CreatedByUserID);
            return (_ApplicationID > -1);
        }

        private bool _UpdateApplication()
        {
            return ApplicationData.UpdateApplication(_ApplicationID, _ApplicantPersonID, _ApplicationDate, _ApplicationTypeID,(Byte) _ApplicationStatus
                , _LastStatusDate, _PaidFees, _CreatedByUserID);
        }

        public static ApplcationBussiness Find(int ApplicationID)
        {
            int applicantPersonID = -1, applicationTypeID = -1, createdByUserID = -1;
            DateTime applicationDate = DateTime.Now, lastStatusDate = DateTime.Now;
            byte applicationStatus = 1;
            float paidFees = 0;

            if (ApplicationData.GetAllApplicationInfo(ApplicationID, ref applicantPersonID, ref applicationDate,
                ref applicationTypeID, ref applicationStatus, ref lastStatusDate, ref paidFees, ref createdByUserID))
            {
                return new ApplcationBussiness(ApplicationID, applicantPersonID, applicationDate,
                    applicationTypeID, (enApplicationStatus)applicationStatus, lastStatusDate, paidFees, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public bool Cancel()
        {
            return ApplicationData.UpdateStatus(_ApplicationID, 2);
        }

        public bool SetComplete()
        {
            return ApplicationData.UpdateStatus(_ApplicationID,3);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.Add:
                    if (_AddNewApplication())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }else
                    {
                        return false;
                    }
                case enMode.Update:
                        return _UpdateApplication();
                  }
            return false;
        }

        public static bool Delete(int ApplicationID)
        {
            return ApplicationData.DeleteApplication(ApplicationID);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return ApplicationData.IsApplicationExist(ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID,int ApplicationTypeID)
        {
            return ApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return ApplicationData.DoesPersonHaveActiveApplication(_ApplicantPersonID, ApplicationTypeID);
        }

        public static int DoesPersonHaveCompletedApplication(int PersonID, ApplcationBussiness.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return ApplicationData.GetCompletedApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public static int GetActiveApplicationID(int PersonID, ApplcationBussiness.enApplicationType ApplicationTypeID)
        {
            return ApplicationData.IsActiveApplication(PersonID, (int)ApplicationTypeID);
        }

        public int GetActiveApplicationID(ApplcationBussiness.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(_ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, ApplcationBussiness.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return ApplicationData.IsActiveApplicationByLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

    }
}
