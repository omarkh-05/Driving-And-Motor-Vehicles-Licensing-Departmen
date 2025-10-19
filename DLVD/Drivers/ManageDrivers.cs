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
using DriversBussinessLayer;

namespace DLVD.Drivers
{
    public partial class ManageDrivers: Form
    {
        private DataTable _dtAllDrivers;

        public ManageDrivers()
        {
            InitializeComponent();
        }


        private void personDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails personinfo = new PersonDetails((int)dataGridView1.CurrentRow.Cells[1].Value);
            personinfo.Show();
        }

        private void showDriverLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DriverLicenseHistory driverLicenseHistory = new DriverLicenseHistory((int)dataGridView1.CurrentRow.Cells[1].Value);
            driverLicenseHistory.Show();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ManageDrivers_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            _dtAllDrivers = DriversBussiness.GetAllDrivers();
            dataGridView1.DataSource = _dtAllDrivers;
            lblRecord.Text = dataGridView1.Rows.Count.ToString();
           
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Driver ID";
                dataGridView1.Columns[0].Width = 120;

                dataGridView1.Columns[1].HeaderText = "Person ID";
                dataGridView1.Columns[1].Width = 120;

                dataGridView1.Columns[2].HeaderText = "National No.";
                dataGridView1.Columns[2].Width = 140;

                dataGridView1.Columns[3].HeaderText = "Full Name";
                dataGridView1.Columns[3].Width = 320;

                dataGridView1.Columns[4].HeaderText = "Date";
                dataGridView1.Columns[4].Width = 170;

                dataGridView1.Columns[5].HeaderText = "Active Licenses";
                dataGridView1.Columns[5].Width = 150;
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterValue.Visible = (comboBox1.Text != "None");


            if (comboBox1.Text == "None")
            {
                txtFilterValue.Enabled = false;
            }
            else
                txtFilterValue.Enabled = true;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (comboBox1.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecord.Text = dataGridView1.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
                //in this case we deal with numbers not string.
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecord.Text = _dtAllDrivers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (comboBox1.Text == "Driver ID" || comboBox1.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
