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
    public partial class UserDetails: Form
    {
        private int _UserID { get; set; }
        public UserDetails(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void _LoadData()
        {
            if(!userInfo1.FillUserInfoUserControl(_UserID))
            {
                MessageBox.Show("تعذر عرض المستخدم");
            }
        }

        private void UserDetails_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
