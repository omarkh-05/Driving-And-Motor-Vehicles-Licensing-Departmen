using DLVD.Drivers;
using DLVD.People;
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
using InternationalLicenseBussinessLayer;
using DriversBussinessLayer;

namespace DLVD.Applications.Driving_License.International
{
    public partial class ManageInternationalLicenseApplication: Form
    {
        private DataTable _dtInternationalLicenseApplications;

        public ManageInternationalLicenseApplication()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dataGridView1.CurrentRow.Cells[2].Value;
            int PersonID = DriversBussiness.FindByDriverID(DriverID).PersonID;
            PersonDetails personDetails = new PersonDetails(PersonID);
            personDetails.Show();
        }
        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            InternationalDrivingLicenseInfo showDrivingLicense = new InternationalDrivingLicenseInfo(InternationalLicenseID);
            showDrivingLicense.Show();
        }
        private void showLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dataGridView1.CurrentRow.Cells[2].Value;
            int PersonID = DriversBussiness.FindByDriverID(DriverID).PersonID;
            DriverLicenseHistory driverLicenseHistory = new DriverLicenseHistory(PersonID);
           driverLicenseHistory.Show();
        }


        private void ManageInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenseApplications = InternationalLicenseBussiness.GetAllInternationalLicenses();
            cbFilter.SelectedIndex = 0;

            dataGridView1.DataSource = _dtInternationalLicenseApplications;
            lblRecord.Text = dataGridView1.Rows.Count.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Int.License ID";
                dataGridView1.Columns[0].Width = 160;

                dataGridView1.Columns[1].HeaderText = "Application ID";
                dataGridView1.Columns[1].Width = 150;

                dataGridView1.Columns[2].HeaderText = "Driver ID";
                dataGridView1.Columns[2].Width = 130;

                dataGridView1.Columns[3].HeaderText = "L.License ID";
                dataGridView1.Columns[3].Width = 130;

                dataGridView1.Columns[4].HeaderText = "Issue Date";
                dataGridView1.Columns[4].Width = 180;

                dataGridView1.Columns[5].HeaderText = "Expiration Date";
                dataGridView1.Columns[5].Width = 180;

                dataGridView1.Columns[6].HeaderText = "Is Active";
                dataGridView1.Columns[6].Width = 120;

            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
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
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecord.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Is Active")
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
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                        FilterColumn = "ApplicationID";
                        break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtSearch.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblRecord.Text = dataGridView1.Rows.Count.ToString();
                return;
            }



            _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtSearch.Text.Trim());

            lblRecord.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow numbers only becasue all fiters are numbers.
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InternationalLicenseApplication frm = new InternationalLicenseApplication();
            frm.ShowDialog();
            ManageInternationalLicenseApplication_Load(null, null);
        }
    }
}
