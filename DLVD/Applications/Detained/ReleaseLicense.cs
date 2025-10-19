using DLVD.Applications.Driving_License;
using DLVD.Drivers;
using DLVD.Session;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppTypeBussiness;
using ApplcationBussinessLayer;

namespace DLVD.Applications.Detained
{
    public partial class ReleaseLicense: Form
    {
        int _SelectLicenseID = -1;

        public ReleaseLicense()
        {
            InitializeComponent();

        }
        public ReleaseLicense(int LicenseID)
        {
            InitializeComponent();

            _SelectLicenseID = LicenseID;

            ctrlDrivingLicenseWithFilterInfo1.LoadLicenseInfo(_SelectLicenseID);
            ctrlDrivingLicenseWithFilterInfo1.FilterEnabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           ShowDrivingLicense showDrivingLicense = new ShowDrivingLicense(_SelectLicenseID);
           showDrivingLicense.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DriverLicenseHistory driverLicenseHistory = new DriverLicenseHistory(ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DriverInfo.PersonID);
            driverLicenseHistory.Show();
        }

        private void ctrlDrivingLicenseWithFilterInfo1_OnLicenseSelected(int obj)
        {
            _SelectLicenseID = obj;

            lblLicenseID.Text = _SelectLicenseID.ToString();

            llShowLicenseHistory.Enabled = (_SelectLicenseID != -1);

            if (_SelectLicenseID == -1)
            {
                MessageBox.Show("تعذر عرض البينات");
                return;
            }

            if(!ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("هذه الرخصة غير محتجزة بالفعل");
                btnRelease.Enabled = false;
                return;
            }

            lblApplicationFees.Text = ApplicationTypeBussiness.Find((int)ApplcationBussiness.enApplicationType.ReleaseDetainedDrivingLicense)._ApplicationFees.ToString();
            lblCreatedByUser.Text = UserSession._UserName;

            lblDetainID.Text =ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblLicenseID.Text = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.LicenseID.ToString();

            lblCreatedByUser.Text = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DetainedInfo.CreatedByUserInfo._UserName;
            lblDetainDate.Text = (ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DetainedInfo.DetainDate).ToShortDateString();
            lblFineFees.Text = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();

            btnRelease.Enabled = true;


        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متأكد من فك حجز هذه الرخصة?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;


            bool IsReleased = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.ReleaseDetainedLicense(UserSession.UserID, ref ApplicationID); ;

            lblApplicationID.Text = ApplicationID.ToString();

            if (!IsReleased)
            {
                MessageBox.Show("تعذر فك حجز الرخصة", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"{_SelectLicenseID.ToString()} تم فك حجز الرخصة رقم الرخصة ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrlDrivingLicenseWithFilterInfo1.FilterEnabled = false;
            llshowLicenseInfo.Enabled = true;
        }
    }
}
