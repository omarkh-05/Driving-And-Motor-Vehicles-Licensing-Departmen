using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseBussinessLayer;

namespace DLVD.UserControlsUtil
{
    public partial class ctrlDrivingLicenseWithFilterInfo: UserControl
    {
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID); // Raise the event with the parameter
            }
        }
        public ctrlDrivingLicenseWithFilterInfo()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return drivingLicenseInfo1.LicenseID; }
        }

        public LicenseBussiness SelectedLicenseInfo
        { get { return drivingLicenseInfo1.SelectedLicenseInfo; } }


        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            drivingLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = drivingLicenseInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("الرجاء تعبئة الفراغ بالشكل المطلوب", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);
        }


        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnView.PerformClick();
            }

        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }
    }
}
