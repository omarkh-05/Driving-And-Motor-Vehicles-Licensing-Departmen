using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InternationalLicenseBussinessLayer;
using DLVD.Properties;
using System.IO;

namespace DLVD.UserControlsUtil
{
    public partial class InternationalDrivingLicensectrl: UserControl
    {
        private int _InternationalLicenseID;
        private InternationalLicenseBussiness _InternationalLicense;

        public InternationalDrivingLicensectrl()
        {
            InitializeComponent();
        }
        public int InternationalLicenseID
        {
            get { return _InternationalLicenseID; }
        }

        private void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.Gendor == 0)
                pbPhoto.Image = Resources.Male_512;
            else
                pbPhoto.Image = Resources.Female_512;

            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPhoto.Load(ImagePath);
        }

        public void LoadInfo(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;
            _InternationalLicense = InternationalLicenseBussiness.Find(_InternationalLicenseID);
            if (_InternationalLicense == null)
            {
                MessageBox.Show("تعذر العثور على هوية دولية ID = " + _InternationalLicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }

            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblApplicationID.Text = _InternationalLicense._ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblDateOfBirth.Text =(_InternationalLicense.DriverInfo.PersonInfo.DateOfBirth).ToShortDateString();

            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblIssueDate.Text =(_InternationalLicense.IssueDate).ToShortDateString();
            lblExDate.Text = (_InternationalLicense.ExpirationDate).ToShortDateString();

            _LoadPersonImage();



        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
