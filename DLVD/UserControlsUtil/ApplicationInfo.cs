using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLVD.People;
using DLVD.Session;
using LocalDrivingLicenseApplicationBussinessLayer;
using ApplcationBussinessLayer;
using DLVD.Applications.Driving_License;
using LicenseClassBussinessLayer;
using AppTypeBussiness;
using DLVD.Applications;

namespace DLVD.UserControlsUtil
{
    public partial class ApplicationInfo: UserControl
    {
        ApplcationBussiness _applcationBussinessObject;
        ldlApplicationBussiness _ldlApplicationBussinessObject;

        public int _LDLApplicationID = -1;
        public int _PersonID = -1;
        public int _PassedTest = 0;

        public ApplicationInfo()
        {
            InitializeComponent();
        }

        public bool _FillData(int LDLApplicationID)
        {
            _ldlApplicationBussinessObject = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID(LDLApplicationID);
            _LDLApplicationID = LDLApplicationID;
            _PersonID = _ldlApplicationBussinessObject._ApplicantPersonID;

            if (_ldlApplicationBussinessObject == null)
            {
                MessageBox.Show("تعذر عرض البيانات");
                return false;
            }else
            {
                //  LDLApplication Info
                lblLDLAppID.Text = _ldlApplicationBussinessObject._LocalDrivingLicenseApplicationID.ToString();
                lblLicenseID.Text = _ldlApplicationBussinessObject._LicenseClassName;
                lblPassedTest.Text = _ldlApplicationBussinessObject.GetPassedTestCount().ToString();

                //  Application Info
                lblApplicationID.Text = _ldlApplicationBussinessObject._ApplicationID.ToString();
                lblApplicationStatus.Text = _ldlApplicationBussinessObject._ApplicationStatusText;
                lblAplicationFees.Text = _ldlApplicationBussinessObject._PaidFees.ToString();
                lblApplicationType.Text = ApplicationTypeBussiness.Find(_ldlApplicationBussinessObject._ApplicationTypeID)._ApplicationTypeTitle;
                lblAplicantPersonID.Text = _PersonID.ToString();
                lblApplicationDate.Text = _ldlApplicationBussinessObject._ApplicationDate.ToShortDateString();
                lblStatusDate.Text = _ldlApplicationBussinessObject._LastStatusDate.ToShortDateString();
                lblCreatedBy.Text = UserSession._UserName;

                return true;
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDetails personDetails = new PersonDetails(_PersonID);
            personDetails.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = _LDLApplicationID;

            int LicenseID = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                ShowDrivingLicense internationalDrivingLicenseInfo = new ShowDrivingLicense(LicenseID);
                internationalDrivingLicenseInfo.Show();
            }
            else
            {
                MessageBox.Show("لم يتم العثور ", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void lblAplicationFees_Click(object sender, EventArgs e)
        {

        }

        private void lblPassedTest_Click(object sender, EventArgs e)
        {

        }
    }
}
