using Bussiness_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DLVD.Session
{
   
    class UserSession
    {
        public static int UserID { get; private set; }
        public static int PersonID { get; private set; }
        public static string _UserName { get; private set; }
        public static string _Password { get; private set; }
        public static bool IsActive { get; private set; }

        public static void SetUser(UsersBussiness user)
        {
            UserID = user._UserID;
            PersonID = user._PersonID;
            _UserName = user._UserName;
            IsActive = user._IsActive;
        }

        public static void Clear()
        {
            UserID = 0;
            PersonID = 0;
            _UserName = null;
            IsActive = false;
        }

        //  او بدل كل الي فوق بنستخدم   public static UserBussiness CurrentUser;


        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLDAppInfo";
            string valueName = "CurrentUserinDVLDApp";
            string valueData = $"{Username}#{Password}";

            try
            {
                Registry.SetValue(keyPath,valueName,valueData,RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLDAppInfo";
            string valueName = "CurrentUserinDVLDApp";

            try
            {
              string UserInfo =  Registry.GetValue(keyPath, valueName,null).ToString();
                if(UserInfo != null)
                {
                    string[] currentuser = UserInfo.Split('#');
                    Username = currentuser[0];
                    Password = currentuser[1];
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }
    }
}

