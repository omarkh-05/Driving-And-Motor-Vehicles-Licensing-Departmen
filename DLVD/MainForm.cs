using DLVD.Applications;
using DLVD.Applications.Detained;
using DLVD.Applications.Driving_License.International;
using DLVD.Drivers;
using DLVD.People;
using DLVD.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLVD.Session;

namespace DLVD
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PeopleManagement peopleManagement = new PeopleManagement();
            peopleManagement.Show();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageUsers manageusers = new ManageUsers();
            manageusers.Show();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDetails userdetails = new UserDetails(UserSession.UserID);
            userdetails.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ChangePassword changepassword = new ChangePassword(UserSession.UserID);
            changepassword.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
            this.Close();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDrivers managedrivers = new ManageDrivers();
            managedrivers.Show();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageApplications manageApplicationTypes = new ManageApplications();
            manageApplicationTypes.Show();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageTestTypes manageTestTypes = new ManageTestTypes();
            manageTestTypes.Show();
        }

        private void localDrinvingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLocalDrivingLicense newLocalDrivingLicense = new NewLocalDrivingLicense();
            newLocalDrivingLicense.Show();
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplication localDrivingLicense = new LocalDrivingLicenseApplication();
            localDrivingLicense.Show();
        }

        private void internationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InternationalLicenseApplication internationalLicenseApplication = new InternationalLicenseApplication();
            internationalLicenseApplication.Show();
        }

        private void internationalDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageInternationalLicenseApplication manageInternationalLicenseApplication = new ManageInternationalLicenseApplication();
            manageInternationalLicenseApplication.Show();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewDrivingLicenseApplication renewDrivingLicenseApplication = new RenewDrivingLicenseApplication();
            renewDrivingLicenseApplication.Show();
        }

        private void replacementForLostOrDamagendLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplacementDamagedLicense replacementDamagedLicense = new ReplacementDamagedLicense();
            replacementDamagedLicense.Show();
        }

        private void detainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetainLicense detainLicense = new DetainLicense();
            detainLicense.Show();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseLicense releaseLicense = new ReleaseLicense();
            releaseLicense.Show();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDetainedLicense manageDetainedLicense = new ManageDetainedLicense();
            manageDetainedLicense.Show();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplication localDrivingLicenseApplication = new LocalDrivingLicenseApplication();
            localDrivingLicenseApplication.Show();
        }

        private void releaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseLicense releaseLicense = new ReleaseLicense();
            releaseLicense.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); 
        }
    }
}
