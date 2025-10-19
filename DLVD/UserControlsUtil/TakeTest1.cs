using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestTypesBussiness;
using LocalDrivingLicenseApplicationBussinessLayer;
using DLVD.Properties;
using TestAppointmentBussinessLayer;
using DLVD.Appointments;
using DLVD.Applications;

namespace DLVD.UserControlsUtil
{
    public partial class TakeTest1: UserControl
    {
        private TestTypeBussiness.enTestType _TestTypeID;
        private int _TestID = -1;
        private ldlApplicationBussiness _LDLObject;

        public TestTypeBussiness.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case TestTypeBussiness.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case TestTypeBussiness.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case TestTypeBussiness.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }

        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }

        public int TestID
        {
            get
            {
                return _TestID;
            }
        }

        private int _TestAppointmentID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        private TestAppointmentBussiness _TestAppointment;


        public void LoadInfo(int TestAppointmentID)
        {

            _TestAppointmentID = TestAppointmentID;


            _TestAppointment = TestAppointmentBussiness.Find(_TestAppointmentID);

            //incase we did not find any appointment .
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointment._TestID;

            _LocalDrivingLicenseApplicationID = _TestAppointment._LocalDrivingLicenseApplicationID;
            _LDLObject = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LDLObject == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _LDLObject._LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LDLObject._LicenseClassName;
            lblFullName.Text = _LDLObject._PersonFullName;


            //this will show the trials for this test before 
            lblTrial.Text = _LDLObject.TestTrial(_TestTypeID).ToString();



            lblDate.Text = _TestAppointment._AppointmentDate.ToShortDateString();
            lblFees.Text = _TestAppointment._PaidFees.ToString();
            lblTestID.Text = (_TestAppointment._TestID == -1) ? "Not Taken Yet" : _TestAppointment._TestID.ToString();



        }

        public TakeTest1()
        {
            InitializeComponent();
        }

        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }
    }
}
