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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLVD.Applications.Detained
{
    public partial class DetainLicense: Form
    {

        private int _DetainID = -1;
        private int _SelectedLicenseID = -1;

        public DetainLicense()
        {
            InitializeComponent();
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
            ShowDrivingLicense showDrivingLicense = new ShowDrivingLicense(_SelectedLicenseID);
            showDrivingLicense.Show();
        }

        private void DetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = (DateTime.Now).ToShortDateString();
            lblCreatedBy.Text = UserSession._UserName;

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متأكد من حجز هذه الرخصة?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            _DetainID = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text), UserSession.UserID);
            if (_DetainID == -1)
            {
                MessageBox.Show("تعذر حجز الرخصة", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show("تم حجز الرخصة ID =" + _DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnDetain.Enabled = false;
            ctrlDrivingLicenseWithFilterInfo1.FilterEnabled = false;
            txtFineFees.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void ctrlDrivingLicenseWithFilterInfo1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)
            {
                return;
            }

            //ToDo: make sure the license is not detained already.
            if (ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("هذه الرخصة محتجزة بالفعل.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }

            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                errorProvider1.SetError(txtFineFees, "الرسوم لايمكن ان تكون فارغة");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            }
            
            if (!IsNumber(txtFineFees.Text))
            {
                errorProvider1.SetError(txtFineFees, "رقم غير صحيح");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            }
            ;
        }

        //    Validate
        public static bool IsNumber(string Number)
        {
            return (ValidateInteger(Number) || ValidateFloat(Number));
        }
        public static bool ValidateInteger(string Number)
        {
            var pattern = @"^[0-9]*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }
        public static bool ValidateFloat(string Number)
        {
            var pattern = @"^[0-9]*(?:\.[0-9]*)?$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }
    }
}
