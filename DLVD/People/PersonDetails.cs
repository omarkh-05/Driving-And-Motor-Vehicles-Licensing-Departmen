using Bussiness_Layer;
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

namespace DLVD.People
{
    public partial class PersonDetails: Form
    {

        private int _PersonID = -1;
        public PersonDetails(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        public void FillPersonDetailsinfo()
        {
            personInfo1._PersonID = _PersonID;

            // فحص البيانات من نفس اليوزر كونترول الحقيقي
            if (!personInfo1.CheackFillResult())
            {
                MessageBox.Show("خطأ في عرض البيانات");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PersonDetails_Load(object sender, EventArgs e)
        {
            FillPersonDetailsinfo();
        }

        private void personInfo1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
