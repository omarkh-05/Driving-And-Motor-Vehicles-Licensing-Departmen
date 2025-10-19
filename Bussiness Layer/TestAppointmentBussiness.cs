using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAppointmentDataLayer;
using LocalDrivingLicenseApplicationBussinessLayer;
using System.Data;
using TestTypesBussiness;
using ApplcationBussinessLayer;

namespace TestAppointmentBussinessLayer
{
   public class TestAppointmentBussiness
    {
        private enum enMode { AddMode = 1, UpdateMode = 2 };
        enMode _mode = enMode.AddMode;

        public int      _TestAppointmentID { get; set; }
        public TestTypeBussiness.enTestType _TestTypeID { get; set; }
        public int      _LocalDrivingLicenseApplicationID { get; set; }
        public ldlApplicationBussiness ldlApplicationInfo;
        public DateTime _AppointmentDate { get; set; }
        public float    _PaidFees { get; set; }
        public int      _CreatedByUserID { get; set; }
        public bool     _IsLocked { get; set; }
        public int      _RetakeTestApplicationID { get; set; }
        public ApplcationBussiness  RetakeTestAppInfo { set; get; }
        public int _TestID
        {
            get { return _GetTestID(); }

        }


        public TestAppointmentBussiness()
        {
            _TestAppointmentID = -1;
            _TestTypeID = TestTypeBussiness.enTestType.VisionTest;
            _LocalDrivingLicenseApplicationID = -1;
            _AppointmentDate = DateTime.MinValue;
            _PaidFees = 0;
            _CreatedByUserID = -1;
            _IsLocked = false;
            _RetakeTestApplicationID = -1;
        }
        public TestAppointmentBussiness(int testAppointmentID, TestTypeBussiness.enTestType testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate, float paidFees, int createdByUserID, bool isLocked, int retakeTestApplicationID)
        {
            this._TestAppointmentID = testAppointmentID;
            this._TestTypeID = testTypeID;
            this._LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            ldlApplicationInfo = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID(localDrivingLicenseApplicationID);
            this._AppointmentDate = appointmentDate;
            this._PaidFees = paidFees;
            this._CreatedByUserID = createdByUserID;
            this._IsLocked = isLocked;
            this._RetakeTestApplicationID = retakeTestApplicationID;
            RetakeTestAppInfo = ApplcationBussiness.Find(_RetakeTestApplicationID);
            
            _mode = enMode.UpdateMode;
        }



        //    Find
        public static TestAppointmentBussiness Find(int TestAppointmentID)
        {
            int TestTypeID = 1; int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (TestAppointmentData.Find(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
            ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new TestAppointmentBussiness(TestAppointmentID, (TestTypeBussiness.enTestType)TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        
        }
        public static TestAppointmentBussiness FindLastTestAppointment(int LocalDrivingLicenseApplicationID,TestTypeBussiness.enTestType TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            if (TestAppointmentData.FindLastTestAppointmentByLDLApplicationID(LocalDrivingLicenseApplicationID,(int) TestTypeID ,
                ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new TestAppointmentBussiness(
                    TestAppointmentID,
                    TestTypeID,
                    LocalDrivingLicenseApplicationID,
                    AppointmentDate,
                    PaidFees,
                    CreatedByUserID,
                    IsLocked,
                    RetakeTestApplicationID);
            }
            else
            {
                return null;
            }
        }
       
        

        //    DataTable
      //  public DataTable GetApplicationTestAppointmentsPerTestType( TestBussiness.enTestType TestTypeID)
       //{
       //    return TestAppointmentData.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, (int)TestTypeID);
       //
       //}
        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, TestTypeBussiness.enTestType TestTypeID)
        {
            return TestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }
        public DataTable GetAllTestAppointments()
        {
            return TestAppointmentData.GetAllTestAppointments();
        }



        //    Crud
        private bool _Add()
        {
            _TestAppointmentID = TestAppointmentData.AddNewTestAppointment((int)_TestTypeID, _LocalDrivingLicenseApplicationID,
                _AppointmentDate, _PaidFees, _CreatedByUserID, _IsLocked, _RetakeTestApplicationID);

            return (_TestAppointmentID != -1);
        }
        private bool _Update()
        {
            return TestAppointmentData.Update(_TestAppointmentID, (int)_TestTypeID,_LocalDrivingLicenseApplicationID,
            _AppointmentDate, _PaidFees,_CreatedByUserID, _IsLocked, _RetakeTestApplicationID );
        }



        //    Save
        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddMode:
                    if (_Add())
                    {
                        _mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.UpdateMode:
                    return _Update();
            }
            return false;
        }


       public int _GetTestID()
        {
          return TestAppointmentData.GetTestID(_TestAppointmentID);
        }

    }
}
