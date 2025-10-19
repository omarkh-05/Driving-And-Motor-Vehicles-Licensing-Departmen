using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplcationBussinessLayer;
using LocalDrivingLicenseApplicationBussinessLayer;
using AppTypeBussiness;
using DLVD.Session;
using TestAppointmentBussinessLayer;
using LicenseClassBussinessLayer;
using TestTypesBussiness;
using DLVD.Appointments;
using DLVD.Applications;
using System.IO;

namespace DLVD.UserControlsUtil
{
    public partial class SchudeldTest : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;


        private TestTypeBussiness.enTestType _TestTypeID = TestTypeBussiness.enTestType.VisionTest;
        private ldlApplicationBussiness _LDLAppObject;
        private int _LocalDrivingLicenseApplicationID = -1;
        private TestAppointmentBussiness _TestAppointmentObject;
        private int _TestAppointmentID = -1;

        //public TestTypeBussiness.enTestType TestTypeID
        //{
        //    get { return _TestTypeID; }
        //    set
        //    {
        //        _TestTypeID = value;
        //        switch (_TestTypeID)
        //        {
        //            case TestTypeBussiness.enTestType.VisionTest:
        //                _TestTypeID = TestTypeBussiness.enTestType.VisionTest;
        //                gbTestType.Text = "Vision Test";
        //                pbPhoto.ImageLocation = @"G:\dlvd Project\Icons\Icons\Vision 512.png";
        //                break;

        //            case TestTypeBussiness.enTestType.WrittenTest:
        //                _TestTypeID = TestTypeBussiness.enTestType.WrittenTest;
        //                gbTestType.Text = "Written Test";
        //                pbPhoto.ImageLocation = @"G:\dlvd Project\Icons\Icons\Written Test 512.png";
        //                break;

        //            case TestTypeBussiness.enTestType.StreetTest:
        //                _TestTypeID = TestTypeBussiness.enTestType.StreetTest;
        //                gbTestType.Text = "Street Test";
        //                pbPhoto.ImageLocation = @"G:\dlvd Project\Icons\Icons\driving-test 512.png";
        //                break;
        //        }


        //        ;
        //    }
        //}

        public SchudeldTest()
        {
            InitializeComponent();
            

        }

        private void _HandlePhotoAndTitle()
        {
            switch (_TestTypeID)
            {
                case TestTypeBussiness.enTestType.VisionTest:
                    gbTestType.Text = " Vision Test";
                    lblTitle.Text = "   Schedule Vision Test";
                    pbPhoto.ImageLocation = @"G:\dlvd Project\Icons\Icons\Vision 512.png";
                    break;

                case TestTypeBussiness.enTestType.WrittenTest:
                    gbTestType.Text = " Written Test";
                    lblTitle.Text = "   Schedule Written Test";
                    pbPhoto.ImageLocation = @"G:\dlvd Project\Icons\Icons\Schedule Test 512.png";
                    break;

                case TestTypeBussiness.enTestType.StreetTest:
                    gbTestType.Text = " Street Test";
                    lblTitle.Text = "   Schedule Street Test";
                    pbPhoto.ImageLocation = @"G:\dlvd Project\Icons\Icons\driving-test 512.png";
                    break;
            }
        }

        public void _FillData(int LocalDrivingLicenseID, TestTypeBussiness.enTestType TestTypeID, int AppointmentID = -1)
        {
            _TestTypeID = TestTypeID;
            _TestAppointmentID = AppointmentID;
            if (_TestAppointmentID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseID;
            _LDLAppObject = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseID);
            
            if (_LDLAppObject == null)
            {
                MessageBox.Show("تعذر عرض البيانات");
                btnSave.Enabled = false;
                return;
            }
            _HandlePhotoAndTitle();

            if (_LDLAppObject.DoesPersonAttendTestType(_TestTypeID))
               _CreationMode = enCreationMode.RetakeTestSchedule;
            else
               _CreationMode = enCreationMode.FirstTimeSchedule;
            

            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                groupBox1.Enabled = true;
                lblTitle.Text = "Schedule A Retake Test";
                lblRetakeTestFees.Text = ApplicationTypeBussiness.Find((int)ApplcationBussiness.enApplicationType.RetakeTest)._ApplicationFees.ToString();
                lblRetakeTestID.Text = "0";
            }
            else
                groupBox1.Enabled = false;
            


            lblLDLID.Text = _LDLAppObject._LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = LicenseClassBussiness.Find(_LDLAppObject._LicenseClassID)._ClassName;
            lblName.Text = _LDLAppObject._PersonFullName;
            lblTrail.Text = _LDLAppObject.TestTrial(_TestTypeID).ToString();
            lblFees.Text = ApplicationTypeBussiness.Find((int)ApplcationBussiness.enApplicationType.NewDrivingLicense)._ApplicationFees.ToString();

            if (_Mode == enMode.AddNew)
            {
                dateTimePicker1.MinDate = DateTime.Now;
                dateTimePicker1.MaxDate = DateTime.Now.AddYears(1);
                _TestAppointmentObject = new TestAppointmentBussiness();
            }
            else
            {
                if (!LoadTestAppointmentDate())
                    return;
            }

            lblTotalFees.Text = (Convert.ToSingle(lblRetakeTestFees.Text) + Convert.ToSingle(lblFees.Text)).ToString();

            if (!_HandleActiveTestAppointment())
                return;
            if (!_HandleAppointmentLocked())
                return;
            if (!_HandlePreviousTest())
                return;
        }


        private bool LoadTestAppointmentDate()
        {
            _TestAppointmentObject = TestAppointmentBussiness.Find(_TestAppointmentID);

            if (_TestAppointmentObject == null)
            {
                MessageBox.Show("تعذر عرض البيانات");
                btnSave.Enabled = false;
                return false;
            }

            if (DateTime.Compare(DateTime.Now, _TestAppointmentObject._AppointmentDate) < 0)
            {
                dateTimePicker1.MinDate = DateTime.Now;
            }
            else
                dateTimePicker1.MinDate = _TestAppointmentObject._AppointmentDate;


            dateTimePicker1.Value = _TestAppointmentObject._AppointmentDate;


            if(_TestAppointmentObject._RetakeTestApplicationID == -1)
            {
                lblRetakeTestID.Text = "N/A";
                lblRetakeTestFees.Text = "0";
                groupBox1.Enabled = false;
            }else
            {
                groupBox1.Enabled = true;
                lblRetakeTestID.Text = _TestAppointmentObject._RetakeTestApplicationID.ToString();
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestFees.Text = _TestAppointmentObject.RetakeTestAppInfo._PaidFees.ToString();
            }
            return true;
        }


        //     Handle
       private bool _HandleActiveTestAppointment()
        {
            if(_Mode == enMode.AddNew && ldlApplicationBussiness.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID,_TestTypeID))
            {
                MessageBox.Show("هذا الشخص لديه موعد بالفعل");
                dateTimePicker1.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
            return true;
        }
        private bool _HandleAppointmentLocked()
        {
            if(_TestAppointmentObject._IsLocked)
            {
                MessageBox.Show("هذا الشخص تقدم للامتحان بالفعل");
                btnSave.Enabled = false;
                return false;
            }
            return true;
        }
        private bool _HandlePreviousTest()
        {
            switch (_TestTypeID)
            {
                case TestTypeBussiness.enTestType.VisionTest:

                    return true;

                case TestTypeBussiness.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.
                    if (!_LDLAppObject.DoesPassTestType(TestTypeBussiness.enTestType.VisionTest))
                    {
                        MessageBox.Show("هذا الشخص لم ينجح بالمتطلب السابق");
                        btnSave.Enabled = false;
                        return false;
                    }

                    return true;

                case TestTypeBussiness.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    if (!_LDLAppObject.DoesPassTestType(TestTypeBussiness.enTestType.WrittenTest))
                    {
                        MessageBox.Show("هذا الشخص لم ينجح بالمتطلب السابق");
                        btnSave.Enabled = false;
                        return false;
                    }
                    return true;
            }
            return true;

        }
        private bool _HandleRetakeTestApplication()
        {
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                //incase the mode is add new and creation mode is retake test we should create a seperate application for it.
                //then we linke it with the appointment.

                //First Create Applicaiton 
                ApplcationBussiness Application = new ApplcationBussiness();

                Application._ApplicantPersonID = _LDLAppObject._ApplicantPersonID;
                Application._ApplicationDate = DateTime.Now;
                Application._ApplicationTypeID = (int)ApplcationBussiness.enApplicationType.RetakeTest;
                Application._ApplicationStatus = ApplcationBussiness.enApplicationStatus.Completed;
                Application._LastStatusDate = DateTime.Now;
                Application._PaidFees = Convert.ToSingle(ApplicationTypeBussiness.Find((int)ApplcationBussiness.enApplicationType.RetakeTest)._ApplicationFees);
                Application._CreatedByUserID = UserSession.UserID;

                if (!Application.Save())
                {
                    _TestAppointmentObject._RetakeTestApplicationID = -1;
                    MessageBox.Show("تعذر خلق طلب اعادة تقديم فحص جديد", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointmentObject._RetakeTestApplicationID = Application._ApplicationID;

            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeTestApplication())
                return;

            _TestAppointmentObject._TestTypeID = _TestTypeID;
            _TestAppointmentObject._LocalDrivingLicenseApplicationID = _LDLAppObject._LocalDrivingLicenseApplicationID;
            _TestAppointmentObject._AppointmentDate = dateTimePicker1.Value;
            _TestAppointmentObject._PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointmentObject._CreatedByUserID = UserSession.UserID;

            if (_TestAppointmentObject.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("تم حغظ البانات بنجاح", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("تعذر حفظ البيانات", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }
    }
}
