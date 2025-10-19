using DLVD.Drivers;
using DLVD.People;
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
using ApplcationBussinessLayer;
using AppTypeBussiness;
using InternationalLicenseBussinessLayer;
using Bussiness_Layer;
using System.Diagnostics;

namespace DLVD.Applications.Driving_License.International
{
    public partial class InternationalLicenseApplication: Form
    {
        int _InternationalLicenseID = -1;
        public InternationalLicenseApplication()
        {
            InitializeComponent();
        }

            private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متأكد من اصدار رخصة دولية?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            InternationalLicenseBussiness InternationalLicense = new InternationalLicenseBussiness();

            // تجهيز البيانات للطلب الدولي
            InternationalLicense._ApplicantPersonID = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense._ApplicationDate = DateTime.Now;
            InternationalLicense._ApplicationStatus = ApplcationBussiness.enApplicationStatus.Completed;
            InternationalLicense._LastStatusDate = DateTime.Now;
            InternationalLicense._PaidFees = ApplicationTypeBussiness.Find((int)ApplcationBussiness.enApplicationType.NewInternationalLicense)._ApplicationFees;
            InternationalLicense._CreatedByUserID = UserSession.UserID;

            // تجهيز بيانات الرخصة الدولية
            InternationalLicense.DriverID = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense._CreatedByUserID = UserSession.UserID;

            // هنا إذا كان ApplicationID غير صالح (أي -1)، نحتاج إلى إنشاء طلب جديد.
            if (InternationalLicense._ApplicationID == -1)
            {
                // إنشاء تطبيق جديد
                ApplcationBussiness application = new ApplcationBussiness();
                application._ApplicantPersonID = ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DriverInfo.PersonID;
                application._ApplicationDate = DateTime.Now;
                application._ApplicationTypeID = (int)ApplcationBussiness.enApplicationType.NewInternationalLicense;
                application._ApplicationStatus = ApplcationBussiness.enApplicationStatus.New;
                application._LastStatusDate = DateTime.Now;
                application._PaidFees = ApplicationTypeBussiness.Find((int)ApplcationBussiness.enApplicationType.NewInternationalLicense)._ApplicationFees;
                application._CreatedByUserID = UserSession.UserID;

                if (!application.Save())
                {
                    MessageBox.Show("تعذر إنشاء الطلب الجديد", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                InternationalLicense._ApplicationID = application._ApplicationID;
            }

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("تعذر اصدار رخصة دولية", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblILApplicationID.Text = InternationalLicense._ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblILLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("تم اصدار رخصة دولية للسائق بنجاح ID =" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            ctrlDrivingLicenseWithFilterInfo1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDetails personDetails = new PersonDetails(ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DriverInfo.PersonID);
            personDetails.Show();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           InternationalDrivingLicenseInfo showDrivingLicense = new InternationalDrivingLicenseInfo(_InternationalLicenseID);
           showDrivingLicense.Show();
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           DriverLicenseHistory driverLicenseHistory = new DriverLicenseHistory(ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DriverInfo.PersonID);
           driverLicenseHistory.Show();
        }


        private void ctrlDrivingLicenseWithFilterInfo1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lblLocalLicenseID.Text = SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)

            {
                return;
            }


            if (ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("لا يمكن اصدار رخصة دولية لفئة غير الثالثة.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check if person already have an active international license
            int ActiveInternaionalLicenseID = InternationalLicenseBussiness.GetActiveInternationalLicenseIDByDriverID(ctrlDrivingLicenseWithFilterInfo1.SelectedLicenseInfo.DriverID);

            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("هذا السائق لديه رخصة دولية بالفعل ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternaionalLicenseID;
                btnIssue.Enabled = false;
                return;
            }

            btnIssue.Enabled = true;
        }

        private void InternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = (DateTime.Now).ToShortDateString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExDate.Text = (DateTime.Now.AddYears(1)).ToShortDateString();//add one year.
            lblApplicationFees.Text = ApplicationTypeBussiness.Find((int)ApplcationBussiness.enApplicationType.NewInternationalLicense)._ApplicationFees.ToString();
            lblCreatedby.Text = UserSession._UserName;
        }
    }
}
