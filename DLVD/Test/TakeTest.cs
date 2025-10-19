using DLVD.Session;
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
using TestBussinessLayer;
using TestTypesBussiness;

namespace DLVD.Tests
{
    public partial class TakeTest : Form
    {
        private int _AppointmentID;
        private TestTypeBussiness.enTestType _TestType;

        private int _TestID = -1;
        private TestBussiness _TestObject;

        public TakeTest(int AppointmentID, TestTypeBussiness.enTestType TestType)
        {
            InitializeComponent();

            _AppointmentID = AppointmentID;
            _TestType = TestType;
        }

        private void TakeTest_Load(object sender, EventArgs e)
        {
           takeTest1.TestTypeID = _TestType;

            takeTest1.LoadInfo(_AppointmentID);

           if (takeTest1.TestAppointmentID == -1)
               btnSave.Enabled = false;
           else
               btnSave.Enabled = true;


           int _TestID = takeTest1.TestID;
           if (_TestID != -1)
           {
               _TestObject = TestBussiness.Find(_TestID);

               if (_TestObject.TestResult)
                   rbPass.Checked = true;
               else
                   rbFail.Checked = true;

               txtNotes.Text = _TestObject.Notes;

               lblUserMessage.Visible = true;
               rbFail.Enabled = false;
               rbPass.Enabled = false;
                btnSave.Enabled = false;
           }

           else
                _TestObject = new TestBussiness();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متأكد من تسجيل هذه النتيجة","Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            _TestObject.TestAppointmentID = _AppointmentID;
            _TestObject.TestResult = rbPass.Checked;
            _TestObject.Notes = txtNotes.Text.Trim();
            _TestObject.CreatedByUserID = UserSession.UserID;

            if (_TestObject.Save())
            {
                MessageBox.Show("تم تخزين البيانات بنجاح.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                
            }
            else
                MessageBox.Show("تعذر تخزين البيانات", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}