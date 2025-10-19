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
    public partial class UserInfo : UserControl
    {
        public int _PersonID { get; set; }
        public int _UserID { get; set; }

        private Bussiness _BussinessObject;

        private UsersBussiness _usersBussinessObject;

        public UserInfo()
        {
            InitializeComponent();
        }

        private void lleditperson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddNewPerson personUpdate = new AddNewPerson(_usersBussinessObject._PersonID);
            personUpdate.Show();
        }

        public bool FillUserInfoUserControl(int UserID)
        {
            _UserID = UserID;
            _usersBussinessObject = UsersBussiness.Find(_UserID);

            if (_usersBussinessObject == null)
            { return false; }

            lblUserID.Text = _usersBussinessObject._UserID.ToString();
            lblUserName.Text = _usersBussinessObject._UserName;
            lblIsActive.Text = _usersBussinessObject._IsActive.ToString();

            FillPersonDetailsinfoInUserControl();
            return true;

           
        }

        public bool FillPersonDetailsinfoInUserControl()
        {
            _BussinessObject = Bussiness.Find(_usersBussinessObject._PersonID);

            if (_BussinessObject == null)
            { return false; }

            lblPersonID.Text = _BussinessObject.PersonID.ToString();

            if (_BussinessObject.ThirdName == null)
                lblName.Text = _BussinessObject.FirstName + " " + _BussinessObject.SecondName + " " + _BussinessObject.LastName;
            else
                lblName.Text = _BussinessObject.FirstName + " " + _BussinessObject.SecondName + " " + _BussinessObject.ThirdName + " " + _BussinessObject.LastName;

            lblNationalNo.Text = _BussinessObject.NationalNo;

            if (_BussinessObject.Gendor == 0)
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
                pbPhoto.Image = Image.FromFile(_BussinessObject.ImagePath);
                pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                if (_BussinessObject.Gendor == 0)
                {
                    pbPhoto.Image = Image.FromFile(@"G:\dlvd Project\Icons\Icons\Male 512.png");
                    pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (_BussinessObject.Gendor == 1)
                {
                    pbPhoto.Image = Image.FromFile(@"G:\dlvd Project\Icons\Icons\Female 512.png");
                    pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            return true;
        }

        public bool CheackFillResult()
        {
            return FillPersonDetailsinfoInUserControl();
        }


    }
}
