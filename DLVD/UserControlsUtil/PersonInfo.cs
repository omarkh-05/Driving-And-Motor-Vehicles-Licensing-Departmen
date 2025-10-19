using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLVD.People;
using Bussiness_Layer;
using Countries_Bussiness;
using System.IO;

namespace DLVD.UserControlsUtil
{
    public partial class PersonInfo: UserControl
    {
        //private enum enMode { AddNew = 1, Update = 2 };
        //enMode _mode = enMode.Update;

        //private int _PersonID = -1;
        private Bussiness _BussinessObject;

        public int _PersonID { get; set; }

        public PersonInfo()
        {
            InitializeComponent();
        }

        public bool FillPersonDetailsinfoInUserControl()
        {
            _BussinessObject = Bussiness.Find(_PersonID);

            if (_BussinessObject == null)
               { return false; }

            lblPersonID.Text = _BussinessObject.PersonID.ToString() ;
           
            if (_BussinessObject.ThirdName == null)
            lblName.Text = _BussinessObject.FirstName + " " + _BussinessObject.SecondName + " " + _BussinessObject.LastName;
            else
            lblName.Text = _BussinessObject.FirstName + " " + _BussinessObject.SecondName + " " + _BussinessObject.ThirdName + " " + _BussinessObject.LastName;
            
            lblNationalNo.Text = _BussinessObject.NationalNo;
            
            if(_BussinessObject.Gendor == 0)
            lblGendor.Text = "Male";
            else
            lblGendor.Text = "Female";
            
            if (_BussinessObject.Email == null)
                lblEmail.Text = "";
            else
                lblEmail.Text = _BussinessObject.Email;

            lblAddress.Text = _BussinessObject.Address;
            lblDateOfBirth.Text = _BussinessObject.DateOfBirth.ToShortDateString();
            lblPhone.Text = _BussinessObject.Phone;
            lblCountries.Text = CountriesBussiness.Find(_BussinessObject.NationalityCountryID).CountryName;

            if (!string.IsNullOrWhiteSpace(_BussinessObject.ImagePath) && File.Exists(_BussinessObject.ImagePath))
            {
                pbPhoto.ImageLocation =_BussinessObject.ImagePath;
                pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                if (_BussinessObject.Gendor == 0)
                {
                    pbPhoto.ImageLocation = @"G:\dlvd Project\Icons\Icons\Male 512.png";
                    pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (_BussinessObject.Gendor == 1)
                {
                    pbPhoto.ImageLocation = @"G:\dlvd Project\Icons\Icons\Female 512.png";
                    pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
                return true;
        }


        public bool CheackFillResult()
        {
            return FillPersonDetailsinfoInUserControl();
        }

        private void lleditperson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddNewPerson personUpdate = new AddNewPerson(_PersonID);
            personUpdate.Show();
        }

        private void lblDateOfBirth_Click(object sender, EventArgs e)
        {

        }
    }
}
