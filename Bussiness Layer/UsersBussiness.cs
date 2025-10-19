using Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bussiness_Layer
{
    public class UsersBussiness
    {
        private enum enMode { AddMode = 1, UpdateMode = 2 };
        enMode _mode = enMode.AddMode;

        public int _UserID { get; set; }
        public int _PersonID { get; set; }
        public Bussiness PersonInfo;
        public string _UserName { get; set; }
        public string _Passowrd { get; set; }
        public bool _IsActive { get; set; }


        public UsersBussiness()
        {
            _PersonID = -1;
            _UserID = -1;
            _UserName = "";
            _Passowrd = "";
            _IsActive = false;
        }

        public UsersBussiness(int UserID ,int personID, string UserName, string Password, bool IsActive)
        {
            this._UserID = UserID;

            this._PersonID = personID;
            PersonInfo = Bussiness.Find(personID);

            this._UserName = UserName;
            this._Passowrd = Password;
            this._IsActive = IsActive;

            _mode = enMode.UpdateMode;
        }

        public static UsersBussiness Find(int UserID)
        {
            string UserName = "", Password = "";
            bool IsActive = false;
            int PersonID = -1;

            if (UsersData.GetUserInfoByID(UserID, ref PersonID, ref UserName, ref Password,ref IsActive))

                return new UsersBussiness(UserID, PersonID, UserName, Password, IsActive);
            else

                return null;
        }
        public static UsersBussiness FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            bool IsFound = UsersData.GetUserInfoByPersonID
                                (PersonID, ref UserID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                //we return new object of that User with the right data
                return new UsersBussiness(UserID, UserID, UserName, Password, IsActive);
            else
                return null;
        }
        public static UsersBussiness FindUserForLogin(string UserName,string Password)
        {   
            bool IsActive = false;
            int PersonID = -1, UserID=-1;

            if (UsersData.FindUserForLogin(ref UserID, ref PersonID, UserName, Password, ref IsActive))

                return new UsersBussiness(UserID, PersonID, UserName, Password, IsActive);
            else

                return null;
        }
        public static DataTable GetAllUsers()
        {
            return UsersData.GetAllUsers();
        }



        public static bool IsUserExistByPersonID(int PersonID)
        {
            return UsersData.IsUserExistByPersonID(PersonID);
        }
        public static bool IsUserExistByUserName(string UserName)
        {
            return UsersData.IsUserExist(UserName);
        }



        public bool CheackCurrentPawword(string Password)
        {
            return UsersData.CheackCurrentPawword(Password);
        }

        private bool _Add()
        {
            _UserID = UsersData.AddNewUser(_PersonID, _UserName, _Passowrd, _IsActive);
            return (_UserID != -1);
        }

        private bool _Update()
        {
            return UsersData.Update(_UserID, _UserName, _Passowrd, _IsActive);
        }

        public static bool _Delete(int UserID)
        {
            return UsersData.Delete(UserID);
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
