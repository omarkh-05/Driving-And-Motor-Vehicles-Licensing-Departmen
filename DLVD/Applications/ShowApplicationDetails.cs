using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLVD.Applications
{
    public partial class ShowApplicationDetails: Form
    {
        int _localDrivingLicenseApplicationID = -1;
        int _PassedTest = 0;

        public ShowApplicationDetails(int localDrivingLicenseApplicationID, int passedTest)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _PassedTest = passedTest;
        }




        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadData()
        {
            applicationInfo1._FillData(_localDrivingLicenseApplicationID);
        }

        private void ShowApplicationDetails_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
