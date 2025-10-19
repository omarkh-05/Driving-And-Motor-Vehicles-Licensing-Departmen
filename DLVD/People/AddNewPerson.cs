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
using Countries_Bussiness;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;
using System.Net.Mail;
using System.Diagnostics.Contracts;
using System.IO;
using DLVD.Properties;
using DLVD.Util;

namespace DLVD.People
{
    public partial class AddNewPerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;

        
        private string _OriginalImagePath = null;


        public enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode;


        private int _PersonID = -1;
        Bussiness _BussinessObject;


        public AddNewPerson(int personid)
        {
            InitializeComponent();

            _PersonID = personid;
            if (_PersonID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }
        }

        private void _FillCountriesComboBox()
        {
            DataTable dt = CountriesBussiness.GetAllCountries();
            foreach (DataRow row in dt.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }


        private void _LoadData()
        {
              
            _FillCountriesComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _BussinessObject = new Bussiness();
                return;
            }

            _BussinessObject = Bussiness.Find(_PersonID);

            if (_BussinessObject == null)
            {
                MessageBox.Show("This form will be closed because No Contact with ID = " + _PersonID);
                this.Close();
                return;
            }
            _OriginalImagePath = _BussinessObject.ImagePath;
            lblTitle.Text = "Update Person Info" + _PersonID;
                lblID.Text = _PersonID.ToString();
                txt1Name.Text = _BussinessObject.FirstName;
                txt2Name.Text = _BussinessObject.SecondName;
                txt3Name.Text = _BussinessObject.ThirdName;
                txt4Name.Text = _BussinessObject.LastName;
                txtNationalNo.Text = _BussinessObject.NationalNo;
                dateTimePicker1.Value = _BussinessObject.DateOfBirth;
                
            if (_BussinessObject.Gendor == 0)
                    rbMale.Checked = true;
                else
                    rbFemale.Checked = true;
                
            txtEmail.Text = _BussinessObject.Email;
                txtPhone.Text = _BussinessObject.Phone;
                txtAddress.Text = _BussinessObject.Address;
                cbCountries.SelectedIndex = cbCountries.FindString(CountriesBussiness.Find(_BussinessObject.NationalityCountryID).CountryName);

            if (!string.IsNullOrEmpty(_BussinessObject.ImagePath) && File.Exists(_BussinessObject.ImagePath))
            {
                pbPhoto.ImageLocation = _BussinessObject.ImagePath;
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

            llRemove.Visible = (_BussinessObject.ImagePath != null);
        }



        //   HandleImage
        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_BussinessObject.ImagePath != pbPhoto.ImageLocation)
            {
                if (!string.IsNullOrEmpty(_BussinessObject.ImagePath))
                {
                    try
                    {
                        // محاولة حذف الصورة القديمة
                        File.Delete(_BussinessObject.ImagePath);
                    }
                    catch (IOException ex)
                    {
                        // في حال حدوث خطأ أثناء حذف الملف، نقوم بتسجيله أو إعلام المستخدم.
                        MessageBox.Show($"حدث خطأ أثناء حذف الصورة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (pbPhoto.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPhoto.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPhoto.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }
        


        //   Save btn 
        private void button2_Click(object sender, EventArgs e)
        {
            int CountryID = CountriesBussiness.Find(cbCountries.Text).ID;

            _HandlePersonImage();

            _BussinessObject.NationalNo = txtNationalNo.Text;
            _BussinessObject.FirstName = txt1Name.Text;
            _BussinessObject.SecondName = txt2Name.Text;

            if (string.IsNullOrWhiteSpace(txt3Name.Text))
            {
                _BussinessObject.ThirdName = null;
            }
            else
            {
                _BussinessObject.ThirdName = txt3Name.Text.Trim();
            }

            _BussinessObject.LastName = txt4Name.Text;
            _BussinessObject.DateOfBirth = dateTimePicker1.Value;

            if (rbMale.Checked)
            {
                _BussinessObject.Gendor = 0;
            }
            else
            {
                _BussinessObject.Gendor = 1;
            }

            _BussinessObject.Address = txtAddress.Text;
            _BussinessObject.Phone = txtPhone.Text;

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                _BussinessObject.Email = null;
            }
            else
            {
                _BussinessObject.Email = txtEmail.Text.Trim();
            }

            _BussinessObject.NationalityCountryID = CountryID;

            if (!string.IsNullOrEmpty(pbPhoto.ImageLocation))
            {
                _BussinessObject.ImagePath = pbPhoto.ImageLocation;
            }
            else
            {
                // اذا الصورة الاصلية موجودة، خزنها
                if (!string.IsNullOrEmpty(_OriginalImagePath))
                {
                    _BussinessObject.ImagePath = _OriginalImagePath;
                }
                else
                {
                    // اذا ما في صورة أصلية، اعط صورة افتراضية حسب الجنس
                    if (_BussinessObject.Gendor == 0)
                        _BussinessObject.ImagePath = @"G:\dlvd Project\Icons\Icons\Male 512.png";
                    else
                        _BussinessObject.ImagePath = @"G:\dlvd Project\Icons\Icons\Female 512.png";
                }
            }

            if (_BussinessObject.Save())
            {
                if(_Mode == enMode.AddNew)
                MessageBox.Show($"الى النظام {txt1Name.Text} تم اضافة ");
                else
                    MessageBox.Show($"في النظام {txt1Name.Text} تم تعديل ");
                DataBack?.Invoke(this, _BussinessObject.PersonID);
            }
            else
                if (_Mode == enMode.AddNew)
                MessageBox.Show($"الى النظام {txt1Name.Text} تعذر اضافة ");
            else
                MessageBox.Show($"في النظام {txt1Name.Text} تعذر تعديل ");

            _Mode = enMode.Update;
            lblTitle.Text = "Edit Contact ID = " + _BussinessObject.PersonID;
            lblID.Text = _BussinessObject.PersonID.ToString();

        }
        private void AddNewPerson_Load(object sender, EventArgs e)
        {
            txt1Name.KeyPress += OnlyLetters_KeyPress;
            txt2Name.KeyPress += OnlyLetters_KeyPress;
            txt3Name.KeyPress += OnlyLetters_KeyPress;
            txt4Name.KeyPress += OnlyLetters_KeyPress;


            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);
            _LoadData();
        }



        private void llSetImg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName; 
                pbPhoto.Load(selectedFilePath);
                // pbPhoto.BackgroundImage = Image.FromFile(selectedFilePath);
                llRemove.Visible = true;

            }
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPhoto.ImageLocation = null;



            if (rbMale.Checked)
                pbPhoto.Image = Resources.Male_512;
            else
                pbPhoto.Image = Resources.Female_512;

            llRemove.Visible = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        ///                              Another     





        //private void txt1Name_Validating(object sender, CancelEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txt1Name.Text))
        //    {
        //        txt1Name.Focus();
        //        errorProvider1.SetError(txt1Name, "Field Can not Be Empty");
        //        txt2Name.Focus();
        //        errorProvider1.SetError(txt2Name, "Field Can not Be Empty");
        //        txt3Name.Focus();
        //        errorProvider1.SetError(txt4Name, "Field Can not Be Empty");
        //        txtPhone.Focus();
        //        errorProvider1.SetError(txtPhone, "Field Can not Be Empty");
        //        txtAddress.Focus();
        //        errorProvider1.SetError(txtAddress, "Field Can not Be Empty");
        //        txtNationalNo.Focus();
        //        errorProvider1.SetError(txtNationalNo, "Field Can not Be Empty Or duplicated");
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //        errorProvider1.SetError(txt1Name, "");
        //    }
        //}

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                errorProvider1.SetError(Temp, "مطلوب تعبئة الفراغ");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }



        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                return;
            }
            else
            {
                if (!IsValidEmail(txtEmail.Text))
                {
                    txtEmail.Focus();
                    errorProvider1.SetError(txtEmail, "invalid Email Format");
                }
                else
                {
                    errorProvider1.SetError(txtEmail, "");
                }
            }
        }

        private void OnlyLetters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txt1Name, "Name Field do not take Digits");
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtPhone, "Phone Field do not take Latters");
            }
        }

        private void pbPhoto_Click(object sender, EventArgs e)
        {

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "هذا الفراغ مطلوب");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            //Make sure the national number is not used by another person
            if (txtNationalNo.Text.Trim() != _BussinessObject.NationalNo && Bussiness.isPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "هذا الرقم الوطني مستخدم بالفعل");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

    }
}
