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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DLVD.UserControlsUtil
{
    public partial class PersonInfoWithFilter: UserControl
    {

        private Bussiness _BussinessObject;

        public int _PersonID1 { get; set; }

        public string _NationalNo { get; set; }


        public PersonInfoWithFilter()
        {
            InitializeComponent();
        }

        private void lleditperson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddNewPerson personUpdate = new AddNewPerson(_BussinessObject.PersonID);
            personUpdate.Show();
            FillPersonDetailsinfoByPersonIDInUserControl(_PersonID1);
        }


        public bool FillPersonDetailsinfoByNationalNoInUserControl()
        {
            _BussinessObject = Bussiness.FindPersonByNationalNo(_NationalNo);
            _PersonID1 = _BussinessObject.PersonID;

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
                pbPhoto.ImageLocation = (_BussinessObject.ImagePath);
                pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                if (_BussinessObject.Gendor == 0)
                {
                    pbPhoto.ImageLocation = (@"G:\dlvd Project\Icons\Icons\Male 512.png");
                    pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (_BussinessObject.Gendor == 1)
                {
                    pbPhoto.ImageLocation = (@"G:\dlvd Project\Icons\Icons\Female 512.png");
                    pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            return true;
        }

        
        public void FillPersonDetailsinfoByNationalNo(string PersonID)
        {
            _NationalNo = PersonID;
            
            
            if (!FillPersonDetailsinfoByNationalNoInUserControl())
            {
                MessageBox.Show("المستخدم غير متوفر!");
                _BussinessObject = null; 
                _PersonID1 = -1;
                return;
            }
        }


        public bool FillPersonDetailsinfoByPersonIDInUserControl(int PersonID)
        {
            _BussinessObject = Bussiness.Find(PersonID);

            if (_BussinessObject == null)
            { return false; }

            lblPersonID.Text = _BussinessObject.PersonID.ToString();
            _PersonID1 = _BussinessObject.PersonID;

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
                pbPhoto.ImageLocation = (_BussinessObject.ImagePath);
                pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                if (_BussinessObject.Gendor == 0)
                {
                    pbPhoto.ImageLocation = (@"G:\dlvd Project\Icons\Icons\Male 512.png");
                    pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (_BussinessObject.Gendor == 1)
                {
                    pbPhoto.ImageLocation = (@"G:\dlvd Project\Icons\Icons\Female 512.png");
                    pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            return true;
        }


        public void FillPersonDetailsinfoByPersonID(int PersonID)
        {


            if (!FillPersonDetailsinfoByPersonIDInUserControl(PersonID))
            {
                MessageBox.Show("المستخدم غير متوفر!");
                _BussinessObject = null;
                _PersonID1 = -1;
                return;
            }
            else
                groupBox2.Enabled = false;
        }



        private void button5_Click(object sender, EventArgs e)
        {
            switch(comboBox1.Text)
            {
                case "Person ID":
                    FillPersonDetailsinfoByPersonID(int.Parse(textBox1.Text.Trim()));
                    break;
                case "National No":
                    FillPersonDetailsinfoByNationalNo(textBox1.Text.Trim());
                    break;
                default:
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddNewPerson addNewPerson = new AddNewPerson(-1);
            addNewPerson.DataBack += DataBackEvent;
            addNewPerson.Show();
        }

        private void DataBackEvent(object sender,int PersonID)
        {
            comboBox1.SelectedIndex = 2;
            textBox1.Text = PersonID.ToString();
            FillPersonDetailsinfoByPersonID(PersonID);
        }

        private void PersonInfoWithFilter_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
        }

        public void FilterFocus()
        {
            textBox1.Focus();
        }
    }
}
