using Bussiness_Layer;
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
    public partial class ChangePassword: Form
    {
        private int _UserID { get; set; }

        private UsersBussiness _usersBussinessObject;

        public ChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _usersBussinessObject = UsersBussiness.Find(_UserID);

            _usersBussinessObject._Passowrd = txtConfirmPass.Text;

            if (_usersBussinessObject.Save())
            {
                MessageBox.Show($"في النظام {_usersBussinessObject._UserName} تم تحديث بيانات");
            }
            else
                MessageBox.Show($"في النظام {_usersBussinessObject._UserName} حدث خطأ في تحديث بيانات");
        }
        private void _LoadData()
        {
            if (!userInfo1.FillUserInfoUserControl(_UserID))
            {
                MessageBox.Show("تعذر عرض المستخدم");
            }
        }
        private void ChangePassword_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void txtConfirmPass_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPass.Text != txtNewPass.Text)
            {
                txtConfirmPass.Focus();
                errorProvider1.SetError(txtConfirmPass, "Confirm Password And Password Does Not Match");
            }
        }

        private void txtCurrentPass_Validating(object sender, CancelEventArgs e)
        {
            _usersBussinessObject = UsersBussiness.Find(_UserID);
            if (!_usersBussinessObject.CheackCurrentPawword(txtCurrentPass.Text))
            {
                txtCurrentPass.Focus();
                errorProvider1.SetError(txtCurrentPass, "Password Does Not Match");
            }
        }

        private void txtNewPass_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPass.Text == txtCurrentPass.Text)
            {
                txtNewPass.Focus();
                errorProvider1.SetError(txtNewPass, "New Password Can Not Match With Current Password");
            }
        }
    }
}
