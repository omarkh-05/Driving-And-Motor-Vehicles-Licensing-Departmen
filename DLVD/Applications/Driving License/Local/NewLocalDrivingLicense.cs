using Bussiness_Layer;
using DLVD.People;
using LocalDrivingLicenseApplicationBussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DLVD.Session;
using DLVD.UserControlsUtil;
using AppTypeBussiness;
using ApplcationBussinessLayer;
using LicenseClassBussinessLayer;
using LicenseBussinessLayer;


namespace DLVD.Applications
{
    public partial class NewLocalDrivingLicense: Form
    {
        private enum enMode {Add=1,Update=2};
        enMode _Mode;

        ApplicationTypeBussiness _applicationTypeObject;
        ldlApplicationBussiness ldlApplicationObject;
        

        private int _PersonID = -1;
        private int _LocalDrivinLicenseApplicationID = -1;


        public NewLocalDrivingLicense()
        {
            InitializeComponent();
            _Mode = enMode.Add;
        }

        public NewLocalDrivingLicense(int LocalDrivinLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivinLicenseApplicationID = LocalDrivinLicenseApplicationID;
            _Mode = enMode.Update;
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillLicenseClassComboBox()
        {
            DataTable dt =ldlApplicationBussiness.GetAllLicenseClasses();
            cbLicenseClass.SelectedItem = 0;
            cbLicenseClass.DataSource = dt;
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "ClassName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void _LoadData()
        {
            _applicationTypeObject = ApplicationTypeBussiness.Find((int)ApplcationBussiness.enApplicationType.NewDrivingLicense);
            _FillLicenseClassComboBox();

            cbLicenseClass.SelectedIndex = 2;

            if (_Mode == enMode.Add)
            {
                lblTitle.Text = "New Local Driving License Application";
                ldlApplicationObject = new ldlApplicationBussiness();
                lblApplicationFees.Text = _applicationTypeObject._ApplicationFees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedBy.Text = UserSession._UserName;

            }
            if(_Mode == enMode.Update)
            {

            
            ldlApplicationObject = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID(_LocalDrivinLicenseApplicationID);

            if(ldlApplicationObject == null)
            {
                MessageBox.Show("لم يتم العثور على طلب تقديم الرخصة المحلية");
                return;
            }
               
            lblTitle.Text = "Update eLocal Driving License Application";

            personInfoWithFilter1.FillPersonDetailsinfoByPersonID(ldlApplicationObject._ApplicantPersonID);
            lblDLApplicationID.Text = ldlApplicationObject._LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = ldlApplicationObject._ApplicationDate.ToShortDateString();
                lblApplicationFees.Text = _applicationTypeObject._ApplicationFees.ToString();
                cbLicenseClass.SelectedIndex =ldlApplicationObject._LicenseClassID;
            lblCreatedBy.Text = ldlApplicationObject._CreatedByUserID.ToString();
        }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = LicenseClassBussiness.Find(cbLicenseClass.Text)._LicenseClassID;


            int ActiveApplicationID = ApplcationBussiness.GetActiveApplicationIDForLicenseClass(personInfoWithFilter1._PersonID1,ApplcationBussiness.enApplicationType.NewDrivingLicense, LicenseClassID);
            if(ActiveApplicationID != -1)
            {
                MessageBox.Show("هذا الشخص لديه طلب بالفعل من هذه الفئة");
                return;
            }

            int CompletedApplicationID = ApplcationBussiness.DoesPersonHaveCompletedApplication(personInfoWithFilter1._PersonID1, ApplcationBussiness.enApplicationType.NewDrivingLicense, LicenseClassID);
            if (CompletedApplicationID != -1)
            {
                MessageBox.Show("هذا الشخص اكمل هذا الطلب بالفعل من هذه الفئة");
                return;
            }

            if(LicenseBussiness.IsLicenseExistByPersonID(personInfoWithFilter1._PersonID1, LicenseClassID))
            {
                MessageBox.Show("هذا السائف حاصل بالغعل على رخصة قيادة من نفس الفئة المستهدفة", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ldlApplicationObject._ApplicantPersonID = personInfoWithFilter1._PersonID1; ;
            ldlApplicationObject._ApplicationDate = DateTime.Now;
            ldlApplicationObject._ApplicationTypeID = 1;
            ldlApplicationObject._ApplicationStatus = ApplcationBussiness.enApplicationStatus.New;
            ldlApplicationObject._LastStatusDate = DateTime.Now;
            ldlApplicationObject._PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            ldlApplicationObject._CreatedByUserID = UserSession.UserID;
            ldlApplicationObject._LicenseClassID = LicenseClassID;

            if(ldlApplicationObject.Save())
            {
                if(_Mode == enMode.Add)
                MessageBox.Show("تم اضافة طلب الشخص");
             else
                    MessageBox.Show("تم تعديل طلب الشخص");
            }
            else
            {
                if(_Mode == enMode.Add)
                    MessageBox.Show("تعذر اضافة طلب الشخص");
                else
                    MessageBox.Show("تعذر تعديل طلب الشخص");
            }
                

        }

        private void NewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
              _LoadData();
        }

        private void NewLocalDrivingLicense_Activated(object sender, EventArgs e)
        {
            personInfoWithFilter1.FilterFocus();
        }
    }
}
