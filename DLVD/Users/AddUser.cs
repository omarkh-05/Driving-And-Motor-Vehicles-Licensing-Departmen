using Bussiness_Layer;
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

namespace DLVD.Users
{
    public partial class AddUser: Form
    {
        UsersBussiness _usersBussinessObject;
        private int _UserID { get; set; }
        public enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode;


        public AddUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            if (_UserID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddNewPerson addnewperson = new AddNewPerson(-1);
            addnewperson.Show();
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "كلمة المرور غير متطابقة مع كلمة مرور التأكيد");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
            ;
        }

        private void _LoadData()
        {
            if (_Mode == enMode.AddNew)
            {
                label1.Text = "Add New Person";
                _usersBussinessObject = new UsersBussiness();
                tpLoginInfo.Enabled = false;
                return;
            }

            _usersBussinessObject = UsersBussiness.Find(_UserID);

            if (_usersBussinessObject == null)
            {
                MessageBox.Show("This form will be closed because No Contact with ID = " + _UserID);
                this.Close();
                return;
            }
            tpLoginInfo.Enabled = true;
            label1.Text = "Update Person Info" + _UserID;
            personInfoWithFilter1.FillPersonDetailsinfoByPersonID(_usersBussinessObject._PersonID);
            lblUserID.Text = _UserID.ToString();
            txtUserName.Text = _usersBussinessObject._UserName;
            txtPassword.Text = _usersBussinessObject._Passowrd;
            txtConfirmPassword.Text = _usersBussinessObject._Passowrd;
            cbIsActive.Checked = _usersBussinessObject._IsActive;

        }


        //      Save
        private void button3_Click(object sender, EventArgs e)
        {
          
            if (!string.IsNullOrWhiteSpace(txtUserName.Text))
                _usersBussinessObject._UserName = txtUserName.Text;
            else
                MessageBox.Show("الرجاء تعبئة جميع الفراغات المطلوبة");

            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                _usersBussinessObject._Passowrd = txtConfirmPassword.Text;
            else
                MessageBox.Show("الرجاء تعبئة جميع الفراغات المطلوبة");

            _usersBussinessObject._IsActive =Convert.ToBoolean( cbIsActive.CheckState);

            _usersBussinessObject._PersonID = personInfoWithFilter1._PersonID1;


            if (_usersBussinessObject.Save())
            {
                MessageBox.Show($"الى النظام {txtUserName.Text} تم اضافة ");
            }
            else
                MessageBox.Show($"الى النظام {txtUserName.Text} حدث خطأ في اضافة");

            _Mode = enMode.Update;
            label1.Text = "Edit Contact ID = " + _usersBussinessObject._UserID;
               
        }

        
        //      Next
        private void button1_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update)
            {
                tpLoginInfo.Enabled = true;
                button1.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }else
            {
                if(personInfoWithFilter1._PersonID1 != -1)
                {
                    if(UsersBussiness.IsUserExistByPersonID(personInfoWithFilter1._PersonID1))
                    {
                        MessageBox.Show("هذا الشخص هو مستخدم بالفعل");
                        return;
                    }else
                    {
                        tpLoginInfo.Enabled = true;
                        button1.Enabled = true;
                        tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                    }
                }else
                {
                    MessageBox.Show("الرجاء اختيار شخص");
                }
            }
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "الرجاء تعبئة اسم المستخدم");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
            ;


            if (_Mode == enMode.AddNew)
            {

                if (UsersBussiness.IsUserExistByUserName(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "اسم المستخدم هذا مستخدم من قبل");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
                ;
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_usersBussinessObject._UserName != txtUserName.Text.Trim())
                {
                    if (UsersBussiness.IsUserExistByUserName(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "اسم المستخدم هذا مستخدم من قبل");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                    ;
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "الرجاء تعبئة كلمة المرور");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
            ;
        }
    }
}
