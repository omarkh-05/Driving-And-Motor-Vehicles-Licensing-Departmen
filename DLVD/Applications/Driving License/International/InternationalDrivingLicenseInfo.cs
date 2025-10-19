using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLVD.Applications.Driving_License.International
{
    public partial class InternationalDrivingLicenseInfo: Form
    {
        int _InternationalLicenseID = -1;
        public InternationalDrivingLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InternationalDrivingLicenseInfo_Load(object sender, EventArgs e)
        {
            internationalDrivingLicensectrl1.LoadInfo(_InternationalLicenseID);
        }
    }
}
