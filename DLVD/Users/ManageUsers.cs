using Bussiness_Layer;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DLVD.Users
{
    public partial class ManageUsers: Form
    {
        private static DataTable _dtAllUsers;

        public ManageUsers()
        {
            InitializeComponent();
        }

        
        private void _GetAllUsers()
        {
            _dtAllUsers = UsersBussiness.GetAllUsers();
            dataGridView1.DataSource = _dtAllUsers;
            lblRecord.Text = dataGridView1.RowCount.ToString();
        }


        private void ManageUsers_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            comboBox1_SelectedIndexChanged(sender, e);

            _GetAllUsers();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            AddUser adduser = new AddUser(-1);
            adduser.Show();
        }


        private void showToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            UserDetails userdetails = new UserDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            userdetails.Show();
        }


        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUser adduser = new AddUser(-1);
            adduser.Show();
        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUser adduser = new AddUser((int)dataGridView1.CurrentRow.Cells[0].Value);
            adduser.Show();
        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if(MessageBox.Show("هل انت متأكد من حذف هذا المستخدم","تأكيد الحذف",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                UsersBussiness._Delete((int)dataGridView1.CurrentRow.Cells[0].Value);
                MessageBox.Show("تم حذف المستخدم");
            }
            else
            {
                MessageBox.Show("تعذر حذف المستخدم");
            }
        }


        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Soon");
        }


        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Soon");
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePassword changepassword = new ChangePassword((int)dataGridView1.CurrentRow.Cells[0].Value);
            changepassword.Show();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilter.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }

            else

            {

                txtFilterValue.Visible = (cbFilter.Text != "None");
                cbIsActive.Visible = false;

                if (cbFilter.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }


        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
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
                        _dtAllUsers.DefaultView.RowFilter = "";
                        lblRecord.Text = dataGridView1.Rows.Count.ToString();
                        return;
                    }


                    if (FilterColumn != "FullName" && FilterColumn != "UserName")
                        //in this case we deal with numbers not string.
                        _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
                    else
                        _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

                    lblRecord.Text = _dtAllUsers.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

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
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecord.Text = _dtAllUsers.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID" || cbFilter.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }

}

