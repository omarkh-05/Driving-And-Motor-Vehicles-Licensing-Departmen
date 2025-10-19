using DLVD.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LocalDrivingLicenseApplicationBussinessLayer;
using TestAppointmentBussinessLayer;
using TestTypesBussiness;
using TestBussinessLayer;


namespace DLVD.Appointments
{
    public partial class TestAppointment: Form
    {

       public int _LDLAppID = -1;
         ldlApplicationBussiness _ldlAppObject;
        private DataTable _dtLicenseTestAppointments;
        TestTypeBussiness.enTestType _TestType = TestTypeBussiness.enTestType.VisionTest;

        public TestAppointment(int LDLAppID, TestTypeBussiness.enTestType TestType)
        {
            InitializeComponent();

            _LDLAppID = LDLAppID;

            _TestType = TestType;

        }


       
        ///   Close
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //    Shcedule Test
        private void button5_Click(object sender, EventArgs e)
        {
            ldlApplicationBussiness localDrivingLicenseApplication = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID(_LDLAppID);


            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("هذا الشخص لديه موعد فحص فعال", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            //---
            TestBussiness LastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_TestType);

            if (LastTest == null)
            {
                VisionTest visionTest = new VisionTest(_LDLAppID, _TestType);
                visionTest.Show();
                _FillDataTable();
                return;
            }

            //if person already passed the test s/he cannot retak it.
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("هذا الشخص اجتاز هذا الفحص بالفعل", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            VisionTest frm2 = new VisionTest(LastTest.TestAppointmentInfo._LocalDrivingLicenseApplicationID, _TestType);
            frm2.ShowDialog();
            _FillDataTable();
        }



        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTest visionTest = new TakeTest((int)dataGridView1.CurrentRow.Cells[0].Value , _TestType);
            visionTest.Show();
            _FillDataTable();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VisionTest visionTest = new VisionTest(_LDLAppID, _TestType, (int)dataGridView1.CurrentRow.Cells[0].Value );
            visionTest.Show();
            _FillDataTable();
        }

        private void _FillDataTable()
        {
            _ldlAppObject = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID(_LDLAppID);
            _dtLicenseTestAppointments = TestAppointmentBussiness.GetApplicationTestAppointmentsPerTestType(_LDLAppID, _TestType);

            dataGridView1.DataSource = _dtLicenseTestAppointments;
            lblRecord.Text = dataGridView1.Rows.Count.ToString();
        }

        private void _LoadData()
        {

            _FillDataTable();
            if (_ldlAppObject == null)
            {
                MessageBox.Show("تعذر عرض بيانات الطلب");
                return;
            }

            switch (_TestType)
            {
                case TestTypeBussiness.enTestType.VisionTest:

                    applicationInfo1._FillData(_LDLAppID);
                    lblTitle.Text = "Vision Test Appointment";
                    pbPhoto.ImageLocation = @"G:\dlvd Project\Icons\Icons\Vision 512.png";
                    break;

                case TestTypeBussiness.enTestType.WrittenTest:

                    applicationInfo1._FillData(_LDLAppID);
                    lblTitle.Text = "Written Test Appointment";
                    pbPhoto.ImageLocation = @"G:\dlvd Project\Icons\Icons\Written Test 512.png";
                    break;

                case TestTypeBussiness.enTestType.StreetTest:

                    applicationInfo1._FillData(_LDLAppID);
                    lblTitle.Text = "Street Test Appointment";
                    pbPhoto.ImageLocation = @"G:\dlvd Project\Icons\Icons\driving-test 512.png";
                    break;
            }
            
        }

        private void TestAppointment_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

    }
}
