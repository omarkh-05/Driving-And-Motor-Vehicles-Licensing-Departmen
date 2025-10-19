using DLVD.Applications.Driving_License;
using DLVD.Drivers;
using DLVD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DetainedLicenseBussinessLayer;
using LicenseBussinessLayer;

namespace DLVD.Applications.Detained
{
    public partial class ManageDetainedLicense : Form
    {
        DataTable _dtDetainedLicenses;
        public ManageDetainedLicense()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            ReleaseLicense releaseLicense = new ReleaseLicense(LicenseID);
            releaseLicense.Show();
            ManageDetainedLicense_Load(null, null);
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            int PersonID = LicenseBussiness.Find(LicenseID).DriverInfo.PersonID;
            PersonDetails personDetails = new PersonDetails(PersonID);
            personDetails.Show();
        }
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            ShowDrivingLicense showDrivingLicense = new ShowDrivingLicense(LicenseID);
            showDrivingLicense.Show();
        }
        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            int PersonID = LicenseBussiness.Find(LicenseID).DriverInfo.PersonID;
            DriverLicenseHistory driverLicenseHistory = new DriverLicenseHistory(PersonID);
            driverLicenseHistory.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DetainLicense detainLicense = new DetainLicense();
            detainLicense.Show();
            ManageDetainedLicense_Load(null, null);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ReleaseLicense releaseLicense = new ReleaseLicense();
            releaseLicense.Show();
            ManageDetainedLicense_Load(null, null);
        }


        private void ManageDetainedLicense_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;

            _dtDetainedLicenses = DetainedLicenseBussiness.GetAllDetainedLicenses();

            dataGridView1.DataSource = _dtDetainedLicenses;
            lblRecord.Text = dataGridView1.Rows.Count.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "D.ID";
                dataGridView1.Columns[0].Width = 90;

                dataGridView1.Columns[1].HeaderText = "L.ID";
                dataGridView1.Columns[1].Width = 90;

                dataGridView1.Columns[2].HeaderText = "D.Date";
                dataGridView1.Columns[2].Width = 160;

                dataGridView1.Columns[3].HeaderText = "Is Released";
                dataGridView1.Columns[3].Width = 110;

                dataGridView1.Columns[4].HeaderText = "Fine Fees";
                dataGridView1.Columns[4].Width = 110;

                dataGridView1.Columns[5].HeaderText = "Release Date";
                dataGridView1.Columns[5].Width = 160;

                dataGridView1.Columns[6].HeaderText = "N.No.";
                dataGridView1.Columns[6].Width = 90;

                dataGridView1.Columns[7].HeaderText = "Full Name";
                dataGridView1.Columns[7].Width = 330;

                dataGridView1.Columns[8].HeaderText = "Rlease App.ID";
                dataGridView1.Columns[8].Width = 150;

            }
        }


        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Is Released")
            {
                txtSearch.Visible = false;
                comboBox1.Visible = true;
                comboBox1.Focus();
                comboBox1.SelectedIndex = 0;
            }

            else

            {

                txtSearch.Visible = (cbFilter.Text != "None");
                comboBox1.Visible = false;

                if (cbFilter.Text == "None")
                {
                    txtSearch.Enabled = false;
                    //_dtDetainedLicenses.DefaultView.RowFilter = "";
                    //lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                }
                else
                    txtSearch.Enabled = true;

                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = comboBox1.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtDetainedLicenses.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecord.Text = _dtDetainedLicenses.Rows.Count.ToString();
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "Is Released":
                    FilterColumn = "IsReleased";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtSearch.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblRecord.Text = dataGridView1.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
                //in this case we deal with numbers not string.
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtSearch.Text.Trim());
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtSearch.Text.Trim());

            lblRecord.Text = _dtDetainedLicenses.Rows.Count.ToString();

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Detain ID" || cbFilter.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            releaseLicenseToolStripMenuItem.Enabled = !(bool)dataGridView1.CurrentRow.Cells[3].Value;
        }
    }
}
