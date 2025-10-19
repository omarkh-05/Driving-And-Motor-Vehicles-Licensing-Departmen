using Data_Layer;
using System;
using System.Data;

namespace Bussiness_Layer
{
    public class Bussiness
    {
        private enum enMode { AddMode = 1, UpdateMode = 2 };
        enMode _mode = enMode.AddMode;

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        public DateTime DateOfBirth { get; set; }
        public int Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }


        public Bussiness()
        {
            int      PersonID   = -1;
           string    NationalNo = " ";
           string    FirstName  = " ";
            string   SecondName = " ";
            string   ThirdName  = " ";
            string   LastName   = " ";
            DateTime DateOfBirth= DateTime.Now;
           int       Gendor     = 0;
           string    Address    = " ";
            string   Phone      = " ";
            string   Email      = " ";
            int      NationalityCountryID = -1;
           string    ImagePath  = " ";
        }


        public Bussiness(int personID,string nationalNo,string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth
            ,int gendor,string address, string phone, string email, int nationalityCountryID, string imagePath)
        {
            this.PersonID= personID;
            this.NationalNo = nationalNo;
            this.FirstName = firstName;
            this.SecondName= secondName;
            this.ThirdName= thirdName;
            this.LastName= lastName;
            this.DateOfBirth=dateOfBirth;
            this.Gendor= gendor;
            this.Address= address;
            this.Phone= phone;
            this.Email= email;
            this.NationalityCountryID= nationalityCountryID;
            this.ImagePath = imagePath;

            _mode = enMode.UpdateMode;
        }

        public Bussiness( string nationalNo,int personID, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth
            , int gendor, string address, string phone, string email, int nationalityCountryID, string imagePath)
        {
            this.PersonID = personID;
            this.NationalNo = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gendor = gendor;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryID;
            this.ImagePath = imagePath;

            _mode = enMode.UpdateMode;
        }


        public static DataTable GetAllPeople()
        {
          return  PeopleData.GetAllPeople();
        }

        private bool _Add()
        {
            PersonID = PeopleData.AddNewPerson(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor,
                Address, Phone, Email, NationalityCountryID, ImagePath);
            return (PersonID != -1);
        }

        private bool _Update()
        {
            return PeopleData.Update(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor,
                Address, Phone, Email, NationalityCountryID, ImagePath);
        }


        public static bool Delete(int PersonID)
        {
            return PeopleData.Delete(PersonID);
        }


        public static Bussiness Find(int PersonID)
        {
            int gendor=0, nationalityCountryID=-1;
            DateTime dateOfBirth=DateTime.Now;
            string nationalNo=" ", firstName = " ", secondName = " ", thirdName = " ", lastName = " ",
           address = " ", phone = " ", email = " ", imagePath = " ";

            if (PeopleData.GetPersonInfoByID(PersonID, ref nationalNo, ref firstName, ref secondName, ref thirdName,ref lastName,ref dateOfBirth,ref gendor,
               ref address,ref phone,ref email,ref nationalityCountryID, ref imagePath))

                return new Bussiness (PersonID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth
                , gendor, address, phone, email, nationalityCountryID, imagePath);
            else

             return null;
        }
        public static Bussiness FindPersonByNationalNo(string nationalNo)
        {
            int gendor = 0, nationalityCountryID = -1,PersonID=-1;
            DateTime dateOfBirth = DateTime.Now;
            string  firstName = " ", secondName = " ", thirdName = " ", lastName = " ",
           address = " ", phone = " ", email = " ", imagePath = " ";

            if (PeopleData.GetPersonInfoByNationalNo( nationalNo,ref PersonID, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gendor,
               ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath))

                return new Bussiness( nationalNo, PersonID, firstName, secondName, thirdName, lastName, dateOfBirth
                , gendor, address, phone, email, nationalityCountryID, imagePath);
            else

                return null;
        }
        public static bool isPersonExist(string NationlNo)
        {
            return PeopleData.IsPersonExist(NationlNo);
        }


        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddMode:
                    if (_Add())
                    {
                        _mode = enMode.UpdateMode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.UpdateMode:

                    return _Update();
            }
            return false;

        }
    }
}
