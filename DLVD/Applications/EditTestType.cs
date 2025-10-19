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
using TestTypesBussiness;

namespace DLVD.Applications
{
    public partial class EditTestType : Form
    {

        private int _TestTypeID { get; set; }

        TestTypeBussiness _testObject;


        public EditTestType(int TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void _FillDataToUpdate()
        {
            _testObject = TestTypeBussiness.Find(_TestTypeID);

            if (_testObject == null)
            {
                MessageBox.Show("تعذر عرض بيانات الفحص");
                return;
            }

            lblTestID.Text = _testObject.TestTypeID.ToString();
            txtTitle.Text = _testObject.TestTypeTitle;
            txtFees.Text = _testObject.TestTypeFees.ToString();
            txtDescription.Text = _testObject.TestTypeDescription;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditTestType_Load(object sender, EventArgs e)
        {
            _FillDataToUpdate();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            _testObject = TestTypeBussiness.Find(_TestTypeID);

            if (_testObject == null)
            {
                MessageBox.Show("تعذر عرض بيانات الفحص");
                return;
            }

            if (!decimal.TryParse(txtFees.Text, out decimal fees))
            {
                MessageBox.Show("يرجى إدخال رسوم صالحة");
                return;
            }

            _testObject.TestTypeTitle = txtTitle.Text;
            _testObject.TestTypeFees = fees;
            _testObject.TestTypeDescription = txtDescription.Text;

            if (_testObject.Update())
            {
                MessageBox.Show("تم تحديث بيانات الفحص بنجاح");
                this.Close();
            }
            else
            {
                MessageBox.Show("فشل في تحديث بيانات الفحص");
            }
        }
    }
}
