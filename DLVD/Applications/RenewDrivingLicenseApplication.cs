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

namespace DLVD.Applications
{
    public partial class RenewDrivingLicenseApplication: Form
    {
        int _NewLicenseID = -1;
        public RenewDrivingLicenseApplication()
        {
            InitializeComponent();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RenewDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseWithFilterInfo1.txtLicenseIDFocus();


            lblRLAppID.Text = (DateTime.Now).ToShortDateString();
            lblIssueDate.Text = lblRLAppID.Text;

            lblExpirationDate.Text = "???";
            lblApplicationFees.Text = ApplicationTypeBussiness.Find((int)ApplcationBussiness.enApplicationType.RenewDrivingLicense)._ApplicationFees.ToString();
            lblUserCreated.Text = UserSession._UserName ;
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

            int DefaultValidityLength = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.LicenseClassIfo._DefaultValidityLength;
            lblExpirationDate.Text = (DateTime.Now.AddYears(DefaultValidityLength)).ToShortDateString();
            lblLicenseFees.Text = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.LicenseClassIfo._ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNote.Text = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.Notes;


            //check the license is not Expired.
            if (!ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("لا يمكن تجديد الرخصة الرخصة هذه غير منتهية بعد: " + (ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.ExpirationDate).ToShortDateString()
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }

            //check the license is not Expired.
            if (!ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("هذه الرخصة غير فعالة الرجاء التأكد من فعالية الرخصة."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }



            btnRenew.Enabled = true;

        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متأكد من تجديد هذه الرخصة?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            LicenseBussiness NewLicense = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.RenewLicense(txtNote.Text.Trim(),UserSession.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("تعذر تجديد الرخصة", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblRLAppID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblRLID.Text = _NewLicenseID.ToString();
            MessageBox.Show("تم تجديد الرخصة بنجاح ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenew.Enabled = false;
            ctrlDrivingLicenseWithFilterInfo1.FilterEnabled = false;
            linkLabel2.Enabled = true;

        }
    }
}
