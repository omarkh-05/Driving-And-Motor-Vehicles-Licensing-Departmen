using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLVD.Applications.Driving_License
{
    public partial class ShowDrivingLicense: Form
    {
        private int _LicenseID;

        public ShowDrivingLicense(int licenseID)
        {
            InitializeComponent();
            _LicenseID = licenseID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowDrivingLicense_Load(object sender, EventArgs e)
        {
            drivingLicenseInfo1.LoadInfo(_LicenseID);
        }
    }
}
