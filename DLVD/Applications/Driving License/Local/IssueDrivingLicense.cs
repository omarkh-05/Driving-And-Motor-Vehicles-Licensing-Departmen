using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLVD.Session;
using LicenseBussinessLayer;
using LocalDrivingLicenseApplicationBussinessLayer;

namespace DLVD.Applications
{
    public partial class IssueDrivingLicense: Form
    {

        private int _LocalDrivingLicenseApplicationID = -1;
        private ldlApplicationBussiness _ldlObject;

        public IssueDrivingLicense(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IssueDrivingLicense_Load(object sender, EventArgs e)
        {
            _ldlObject = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_ldlObject == null)
            {
                MessageBox.Show("تعذر عرض البيانات");
                return;
            }

            if (!_ldlObject.PassedAllTests())
            {

                MessageBox.Show("يجب ان يتخطى هذا الشخص جميع الفحوصات.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _ldlObject.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("هذا الشخص لديه رخصة بالفعل=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }


            applicationInfo1._FillData(_LocalDrivingLicenseApplicationID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //      ممكن احط الكود هاض في بزنس الرخصة واجيب اللوكال درايفينج لايسنز من الفايند واكمل وشكرا بس هيك اسهل 

            int LicenseID = _ldlObject.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(),UserSession.UserID);

            if(LicenseID != -1)
            {
                MessageBox.Show(" تم اصدار رخصة للسائق بنجاح " + LicenseID.ToString(),"Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("تعذر اصدار الرخصة! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
