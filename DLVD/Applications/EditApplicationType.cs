using AppTypeBussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLVD.Applications
{
    public partial class EditApplicationType: Form
    {
        private int _AppTypeID {get; set;}

        ApplicationTypeBussiness _applicationObject;


        public EditApplicationType(int AppTypeID)
        {
            InitializeComponent();
            _AppTypeID = AppTypeID;
        }


        private void _FillDataToUpdate()
        {
            _applicationObject = ApplicationTypeBussiness.Find(_AppTypeID);

            if(_applicationObject == null)
            {
                MessageBox.Show("تعذر تحدبث بيانات الطلب");
                return;
            }

            lblAppID.Text = _applicationObject._ApplicationTypeID.ToString();
            txtTitle.Text = _applicationObject._ApplicationTypeTitle;
            txtFees.Text = _applicationObject._ApplicationFees.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditApplicationType_Load(object sender, EventArgs e)
        {
            _FillDataToUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _applicationObject = ApplicationTypeBussiness.Find(_AppTypeID);
            if (_applicationObject == null)
            {
                MessageBox.Show("تعذر عرض بيانات الطلب");
                return;
            }

            _applicationObject._ApplicationTypeTitle = txtTitle.Text;
            _applicationObject._ApplicationFees = Convert.ToSingle(txtFees.Text);

            if(_applicationObject.Update() == false)
            {
                MessageBox.Show("تعذر تحدبث بيانات الطلب");
            }else
                MessageBox.Show("تم تحدبث بيانات الطلب");

        }
    }
}
