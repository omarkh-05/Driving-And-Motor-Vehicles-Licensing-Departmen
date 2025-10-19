using DLVD.Appointments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestTypesBussiness;

namespace DLVD.Tests
{
    public partial class VisionTest: Form
    {
        int _LocalDrivinLicenseApplicationID = -1;
        int _AppointmentID = -1;
        TestTypeBussiness.enTestType _TestType = TestTypeBussiness.enTestType.VisionTest;


        public VisionTest(int LocalDrvingLicenseID, TestTypeBussiness.enTestType TestType, int AppointmentID=-1)
        {
            InitializeComponent();

            _LocalDrivinLicenseApplicationID = LocalDrvingLicenseID;
            _AppointmentID = AppointmentID;
            _TestType = TestType;
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VisionTest_Load(object sender, EventArgs e)
        {
            schudeldTest1._FillData(_LocalDrivinLicenseApplicationID, _TestType, _AppointmentID);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
