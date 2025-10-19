using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLVD.Drivers
{
    public partial class DriverLicenseHistory: Form
    {
        int _PersonID = -1;
        public DriverLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DriverLicenseHistory_Load(object sender, EventArgs e)
        {
            personInfo1._PersonID = _PersonID;
            personInfo1.FillPersonDetailsinfoInUserControl();
            driverLicenseControl1.LoadLicensesByPersonID(_PersonID);
        }
    }
}
