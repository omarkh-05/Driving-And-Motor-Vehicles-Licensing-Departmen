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
using DLVD.Session;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DLVD.Users
{
    public partial class LoginScreen: Form
    {
        private UsersBussiness _usersBussinessObject;
        public LoginScreen()
        {
            InitializeComponent();
        }

        private bool _CheackLoginInfo()
        {
            _usersBussinessObject = UsersBussiness.FindUserForLogin(txtUserName.Text,txtPassword.Text);

           

            if (_usersBussinessObject == null || _usersBussinessObject._IsActive == false)
            {
                return false;
            }
            else
                return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                UserSession.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            }
            else
            {
                UserSession.RememberUsernameAndPassword("", "");
            }

            if (_CheackLoginInfo())
            {
                UserSession.SetUser(_usersBussinessObject);
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }else
                MessageBox.Show("UserName/Password Incorrect Or User Not Active");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
            if (UserSession.GetStoredCredential(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                checkBox1.Checked = true;
            }
        }
    }
}
