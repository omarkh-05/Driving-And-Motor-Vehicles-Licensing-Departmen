using DLVD.Applications.Driving_License;
using DLVD.Drivers;
using DLVD.Session;
using DLVD.UserControlsUtil;
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
using LicenseBussinessLayer;
using static LicenseBussinessLayer.LicenseBussiness;

namespace DLVD.Applications
{
    public partial class ReplacementDamagedLicense: Form
    {
        int _NewLicenseID = -1;

        public ReplacementDamagedLicense()
        {
            InitializeComponent();
        }

         private int _GetApplicationTypeID()
        {
            if (rbDamagedLicense.Checked)

                return (int)ApplcationBussiness.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)ApplcationBussiness.enApplicationType.ReplaceLostDrivingLicense;
        }

        private enIssueReason _GetIssueReason()
        {
            //this will decide which reason to issue a replacement for

            if (rbDamagedLicense.Checked)

                return enIssueReason.DamagedReplacement;
            else
                return enIssueReason.LostReplacement;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DriverLicenseHistory driverLicenseHistory = new DriverLicenseHistory(ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DriverInfo.PersonID);
            driverLicenseHistory.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDrivingLicense showDrivingLicense = new ShowDrivingLicense(_NewLicenseID);
            showDrivingLicense.Show();
        }

        private void ctrlDrivingLicenseWithFilterInfo1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            linkLabel1.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                return;
            }

            //dont allow a replacement if is Active .
            if (!ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReplace.Enabled = false;
                return;
            }

            btnReplace.Enabled = true;
        }

        private void ReplacementDamagedLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = UserSession._UserName;
            rbDamagedLicense.Checked = true;
            ctrlDrivingLicenseWithFilterInfo1.txtLicenseIDFocus();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Damaged License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = ApplicationTypeBussiness.Find(_GetApplicationTypeID())._ApplicationFees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = ApplicationTypeBussiness.Find(_GetApplicationTypeID())._ApplicationFees.ToString();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متأكد من اصدار هذه الرخصة?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            LicenseBussiness NewLicense =ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.Replace(_GetIssueReason(),UserSession.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("تعذر تبديل الرخصة", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;

            lblRreplacedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("تم تبديل الرخصة بنجاح ID =" + _NewLicenseID.ToString(), "Replacement", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnReplace.Enabled = false;
            gbReplacement.Enabled = false;
            ctrlDrivingLicenseWithFilterInfo1.FilterEnabled = false;
            linkLabel2.Enabled = true;


        }
    }
}
