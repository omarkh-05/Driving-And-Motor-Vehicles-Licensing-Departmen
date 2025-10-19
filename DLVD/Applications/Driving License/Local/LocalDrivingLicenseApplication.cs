using DLVD.Applications.Driving_License;
using DLVD.Applications.Driving_License.International;
using DLVD.Appointments;
using DLVD.Drivers;
using DLVD.Tests;
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
using ApplcationBussinessLayer;
using LicenseBussinessLayer;
using TestTypesBussiness;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DLVD.Applications
{
    public partial class LocalDrivingLicenseApplication: Form
    {
        ApplcationBussiness _applcationObject;
        ldlApplicationBussiness _LocalDrivingLicenseObject;

        public LocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        public DataTable _dtApplications =  ldlApplicationBussiness.GetAllLDLApplications();


        //   Close
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //   Add New
        private void button1_Click(object sender, EventArgs e)
        {
            NewLocalDrivingLicense newLocalDrivingLicense = new NewLocalDrivingLicense();
            newLocalDrivingLicense.Show();
        }



        //   Load Data
        private void _FillData()
        {
            dataGridView1.DataSource = _dtApplications;
           lblRecord.Text = dataGridView1.RowCount.ToString();
        }
        private void LocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillData();
            comboBox1.SelectedIndex = 2;

            //     Edit DataGridView Desigen

            //if (dataGridView1.Rows.Count > 0)
            //{

            //    dataGridView1.Columns[0].HeaderText = "L.D.L.AppID";
            //    dataGridView1.Columns[0].Width = 120;

            //    dataGridView1.Columns[1].HeaderText = "Driving Class";
            //    dataGridView1.Columns[1].Width = 300;

            //    dataGridView1.Columns[2].HeaderText = "National No.";
            //    dataGridView1.Columns[2].Width = 150;

            //    dataGridView1.Columns[3].HeaderText = "Full Name";
            //    dataGridView1.Columns[3].Width = 350;

            //    dataGridView1.Columns[4].HeaderText = "Application Date";
            //    dataGridView1.Columns[4].Width = 170;

            //    dataGridView1.Columns[5].HeaderText = "Passed Tests";
            //    dataGridView1.Columns[5].Width = 150;
            //}
        }



        //   Show Details
        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowApplicationDetails showApplicationDetails = new ShowApplicationDetails((int)dataGridView1.CurrentRow.Cells[0].Value, (int)dataGridView1.CurrentRow.Cells[5].Value);
            showApplicationDetails.Show();
            LocalDrivingLicenseApplication_Load(null,null);
        }



        //   Crud
        private void editToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NewLocalDrivingLicense newLocalDrivingLicense = new NewLocalDrivingLicense((int)dataGridView1.CurrentRow.Cells[0].Value);
            newLocalDrivingLicense.Show();
            LocalDrivingLicenseApplication_Load(null, null);
        }
        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("هل انت متأكد من حذف الطلب","تأكيد الحذف",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ldlApplicationBussiness.Deleteldl((int)dataGridView1.CurrentRow.Cells[0].Value);
                MessageBox.Show("تم حذف الطلب");
            }
            LocalDrivingLicenseApplication_Load(null, null);
        }
        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
             _applcationObject = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID((int)dataGridView1.CurrentRow.Cells[0].Value);
            if(_applcationObject == null)
            {
                MessageBox.Show("NULL");
            }
            if (MessageBox.Show("هل انت متأكد من الغاء الطلب", "تأكيد الغاء", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _applcationObject.Cancel();
                MessageBox.Show("تم الغاء الطلب");
            }
            LocalDrivingLicenseApplication_Load(null, null);
        }



        //   License
        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _LocalDrivingLicenseObject = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID((int)dataGridView1.CurrentRow.Cells[0].Value);
            if(LicenseBussiness.IsLicenseExistByPersonID(_LocalDrivingLicenseObject._ApplicantPersonID, _LocalDrivingLicenseObject._LicenseClassID))
            {
                MessageBox.Show("هذا الشخص لديه رخصة بالفعل");
                return;
            }

            IssueDrivingLicense issueDrivingLicense = new IssueDrivingLicense((int)dataGridView1.CurrentRow.Cells[0].Value);
            issueDrivingLicense.Show();
            LocalDrivingLicenseApplication_Load(null, null);

        }
        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;

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
            LocalDrivingLicenseApplication_Load(null, null);
        }
        private void showPersonLicenseHistoryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _LocalDrivingLicenseObject = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID((int)dataGridView1.CurrentRow.Cells[0].Value);
            DriverLicenseHistory driverLicenseHistory = new DriverLicenseHistory(_LocalDrivingLicenseObject._ApplicantPersonID);
            driverLicenseHistory.Show();
            LocalDrivingLicenseApplication_Load(null, null);
        }
       


        //   Tests
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointment visionTestAppointment = new TestAppointment((int)dataGridView1.CurrentRow.Cells[0].Value,TestTypeBussiness.enTestType.VisionTest);
            visionTestAppointment.Show();
            LocalDrivingLicenseApplication_Load(null, null);
        }
        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointment writtenTestAppointment = new TestAppointment((int)dataGridView1.CurrentRow.Cells[0].Value, TestTypeBussiness.enTestType.WrittenTest);
            writtenTestAppointment.Show();
            LocalDrivingLicenseApplication_Load(null, null);
        }
        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointment streetTestAppointment = new TestAppointment((int)dataGridView1.CurrentRow.Cells[0].Value, TestTypeBussiness.enTestType.StreetTest);
            streetTestAppointment.Show();
            LocalDrivingLicenseApplication_Load(null, null);
        }



        //   Context Menu Settings
        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            ldlApplicationBussiness ldlApplication = ldlApplicationBussiness.FindByLocalDrivingLicenseApplicationID
                ((int)dataGridView1.CurrentRow.Cells[0].Value);

            int TotalPassedTests = (int)dataGridView1.CurrentRow.Cells[5].Value;
            bool LicenseExists = ldlApplication.IsLicenseIssued();

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists && !(ldlApplication._ApplicationStatus == ApplcationBussiness.enApplicationStatus.Completed);

            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editToolStripMenuItem.Enabled = !LicenseExists && (ldlApplication._ApplicationStatus == ApplcationBussiness.enApplicationStatus.New);
            ScheduleTestsMenue.Enabled = !LicenseExists;


            //We only canel the applications with status=new.
            CancelApplicaitonToolStripMenuItem.Enabled = (ldlApplication._ApplicationStatus == ApplcationBussiness.enApplicationStatus.New);


            //We only allow delete incase the application status is new not complete or Cancelled.
            DeleteApplicationToolStripMenuItem.Enabled =
                (ldlApplication._ApplicationStatus == ApplcationBussiness.enApplicationStatus.New);


            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = ldlApplication.DoesPassTestType(TestTypeBussiness.enTestType.VisionTest); ;
            bool PassedWrittenTest = ldlApplication.DoesPassTestType(TestTypeBussiness.enTestType.WrittenTest);
            bool PassedStreetTest = ldlApplication.DoesPassTestType(TestTypeBussiness.enTestType.StreetTest);


            ScheduleTestsMenue.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (ldlApplication._ApplicationStatus == ApplcationBussiness.enApplicationStatus.New);

            if (ScheduleTestsMenue.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }
        }




        //       Text Search Filter
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            int searchNumber;
            string ColoumnName = " ";

            // تحديد اسم العمود بناءً على اختيار الـ ComboBox
            switch (comboBox1.Text)
            {
                case "LDL App ID":
                    ColoumnName = "LocalDrivingLicenseApplicationID";
                    break;
                case "License Class":
                    ColoumnName = "ClassName";
                    break;
                case "National No":
                    ColoumnName = "NationalNo";
                    break;
                case "Full Name":
                    ColoumnName = "FullName";
                    break;
                case "Passed Test":
                    ColoumnName = "PassedTestCount";
                    break;
                case "Status":
                    ColoumnName = "Status";
                    break;
                default:
                    ColoumnName = "None";
                    break;
            }

            // إذا كان النص فارغًا أو لم يتم اختيار عمود مناسب
            if (txtSearch.Text.Trim() == "" || ColoumnName == "None")
            {
                _dtApplications.DefaultView.RowFilter = "";
                lblRecord.Text = dataGridView1.Rows.Count.ToString();
                return;
            }

            // إذا كان العمود هو ID أو Passed Test ونريد مقارنة أرقام
            else if (ColoumnName == "PassedTestCount" || ColoumnName == "LocalDrivingLicenseApplicationID")
            {
                // إذا كان النص المدخل رقمًا صالحًا
                if (int.TryParse(txtSearch.Text.Trim(), out searchNumber))
                {
                    _dtApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColoumnName, searchNumber);
                }
                else
                {
                    MessageBox.Show("الرجاء إدخال رقم صالح.");
                }
            }

            // إذا كانت المدخلات نصًا، نبحث باستخدام LIKE
            else
            {
                _dtApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", ColoumnName, txtSearch.Text.Trim());
            }

            // تحديث عدد السجلات في الـ Label
            lblRecord.Text = dataGridView1.Rows.Count.ToString();
        }




        //    CompoBox Filter
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Visible = (comboBox1.Text != "None");
        }

    }
}

