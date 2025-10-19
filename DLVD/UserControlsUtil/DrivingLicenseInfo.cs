using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseBussinessLayer;
using DLVD.Properties;
using System.IO;

namespace DLVD.UserControlsUtil
{
    public partial class DrivingLicenseInfo: UserControl
    {
        private int _LicenseID;
        private LicenseBussiness _License;

        public DrivingLicenseInfo()
        {
            InitializeComponent();
        }
        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public LicenseBussiness SelectedLicenseInfo
        { get { return _License; } }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gendor == 0)
                pbPhoto.Image = Resources.Male_512;
            else
                pbPhoto.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPhoto.Load(ImagePath);

        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = LicenseBussiness.Find(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("تعذر عرض البيانات = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblClassType.Text = _License.LicenseClassIfo._ClassName;
            lblName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _License.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = (_License.DriverInfo.PersonInfo.DateOfBirth).ToShortDateString();

            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = (_License.IssueDate).ToShortDateString();
            lblExDate.Text = (_License.ExpirationDate).ToShortDateString();
            lblIssueReason.Text = _License.IssueReasonText;
            lblNote.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            _LoadPersonImage();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}