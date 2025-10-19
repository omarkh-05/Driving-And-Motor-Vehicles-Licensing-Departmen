using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriversBussinessLayer;
using LicenseBussinessLayer;
using DLVD.Applications.Driving_License;
using DLVD.Applications.Driving_License.International;

namespace DLVD.UserControlsUtil
{
    public partial class DriverLicenseControl: UserControl
    {
        int _DriverID = -1;
        DriversBussiness _driversObject;
        DataTable _dtLocalDrivingLicense;
        DataTable _dtInternationalDrivingLicense;


        public DriverLicenseControl()
        {
            InitializeComponent();
        }


        void _LoadLocalDrivingLicenseInDGV()
        {
            _dtLocalDrivingLicense = DriversBussiness.GetLicenses(_DriverID);

            dgvLocalLicensesHistory.DataSource = _dtLocalDrivingLicense;
            lblRecordsCount.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;

            }
        }

        void _LoadInternationalDrivingLicenseInDGV()
        {
            _dtInternationalDrivingLicense = DriversBussiness.GetInternationalLicenses(_DriverID);


            dgvInternationalLicensesHistory.DataSource = _dtInternationalDrivingLicense;
            lblRecordsCount.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();

            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 130;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 130;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 180;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 180;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 120;
            }
        }


        //     غير مستخدمة لكن في حال احتجتها بالمستقبل
        //public void LoadInfo(int DriverID)
        //{
        //    _DriverID = DriverID;
        //    _driversObject = DriversBussiness.FindByDriverID(_DriverID);

        //    _LoadLocalDrivingLicenseInDGV();
        //    _LoadInternationalDrivingLicenseInDGV();

        //}


        public void LoadLicensesByPersonID(int PersonID)
        {
            _driversObject = DriversBussiness.FindByPersonID(PersonID);

            if(_driversObject == null)
            {
                MessageBox.Show("تعذر عرض بيانات السائق");
                return;
            }

            _DriverID = _driversObject.DriverID;

            _LoadLocalDrivingLicenseInDGV();
            _LoadInternationalDrivingLicenseInDGV();
        }

        public void Clear()
        {
            _dtLocalDrivingLicense.Clear();
            _dtInternationalDrivingLicense.Clear();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           ShowDrivingLicense  frmDrivingLIcense = new ShowDrivingLicense((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            frmDrivingLIcense.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InternationalDrivingLicenseInfo frmDrivingLIcense = new InternationalDrivingLicenseInfo((int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value);
            frmDrivingLIcense.ShowDialog();
        }
    }
}
