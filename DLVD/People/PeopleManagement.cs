using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bussiness_Layer;

namespace DLVD.People
{
    public partial class PeopleManagement: Form
    {
        public PeopleManagement()
        {
            InitializeComponent();
        }

        static DataTable _dtAllPeople = Bussiness.GetAllPeople();

        DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                        "FirstName", "SecondName", "ThirdName", "LastName",
                                                        "GendorCaption", "DateOfBirth", "CountryName",
                                                        "Phone", "Email");
        private void _GetAllPeople()
        {
             
        dataGridView1.DataSource = _dtPeople;
            lblRecordsCount.Text = dataGridView1.Rows.Count.ToString();
        }



        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails persondetails = new PersonDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            persondetails.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewPerson addnewperson = new AddNewPerson(-1);
            addnewperson.Show();
            _GetAllPeople();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Soon");
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Soon");
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPerson newPerson = new AddNewPerson(-1);
            newPerson.Show(); 
            _GetAllPeople();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            AddNewPerson newPerson = new AddNewPerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            newPerson.Show();
            _GetAllPeople();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("لم يتم تحديد أي صف للحذف");
                return;
            }

            int personID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (MessageBox.Show($"هل أنت متأكد من حذف العميل بالرقم: {personID}؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Bussiness.Delete(personID))
                {
                    MessageBox.Show("تم حذف العميل");
                    _GetAllPeople();  // تأكد أن هذه الدالة تعيد تحميل البيانات وتحدث الجدول
                }
                else
                {
                    MessageBox.Show("لا يمكن حذف الشخص ");
                }
            }
        }


        private void PeopleManagement_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox1_SelectedIndexChanged(sender, e);
            if (dataGridView1.Rows.Count > 0)
            {

                dataGridView1.Columns[0].HeaderText = "Person ID";
                dataGridView1.Columns[0].Width = 110;
           
                dataGridView1.Columns[1].HeaderText = "National No.";
                dataGridView1.Columns[1].Width = 120;
                
                dataGridView1.Columns[2].HeaderText = "First Name";
                dataGridView1.Columns[2].Width = 120;
           
                dataGridView1.Columns[3].HeaderText = "Second Name";
                dataGridView1.Columns[3].Width = 140;
       
                dataGridView1.Columns[4].HeaderText = "Third Name";
                dataGridView1.Columns[4].Width = 120;
   
                dataGridView1.Columns[5].HeaderText = "Last Name";
                dataGridView1.Columns[5].Width = 120;
     
                dataGridView1.Columns[6].HeaderText = "Gendor";
                dataGridView1.Columns[6].Width = 120;
         
                dataGridView1.Columns[7].HeaderText = "Date Of Birth";
                dataGridView1.Columns[7].Width = 140;
    
                dataGridView1.Columns[8].HeaderText = "Nationality";
                dataGridView1.Columns[8].Width = 120;
        
                dataGridView1.Columns[9].HeaderText = "Phone";
                dataGridView1.Columns[9].Width = 120;


                dataGridView1.Columns[10].HeaderText = "Email";
                dataGridView1.Columns[10].Width = 170;
            }
            _GetAllPeople();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = (comboBox1.Text != "None");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (comboBox1.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (textBox1.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dataGridView1.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.
                try
                {
                    _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, textBox1.Text.Trim());
                }catch
                {
                    MessageBox.Show("لا يمكن كتابة احرف");
                }
                
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, textBox1.Text.Trim());


            lblRecordsCount.Text = dataGridView1.Rows.Count.ToString();
        }
    }
}
