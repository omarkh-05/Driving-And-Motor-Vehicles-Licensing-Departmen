using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LicenseClassDataLayer;

namespace LicenseClassBussinessLayer
{
    public class LicenseClassBussiness
    {
        public int _LicenseClassID { get; set; }
        public string _ClassName { get; set; }
        public string _ClassDescription { get; set; }
        public byte _MinAllowedAge { get; set; }
        public byte _DefaultValidityLength { get; set; }
        public float _ClassFees { get; set; }


        public LicenseClassBussiness()
        {
            _LicenseClassID = -1;
            _ClassName = "";
            _ClassDescription = "";
            _MinAllowedAge = 0;
            _DefaultValidityLength = 0;
            _ClassFees = 0;
        }
        public LicenseClassBussiness(int licenseClassID, string className, string classDescription,
            byte minAllowedAge, byte defaultValidityLength, float classFees)
        {
            _LicenseClassID = licenseClassID;
            _ClassName = className;
            _ClassDescription = classDescription;
            _MinAllowedAge = minAllowedAge;
            _DefaultValidityLength = defaultValidityLength;
            _ClassFees = classFees;
        }


        public static LicenseClassBussiness Find(int licenseClassID)
        {
            string className = "", classDescription = "";
            byte minAllowedAge = 18, defaultValidityLength = 0;
            float classFees = 0;

            if (LicenseClassData.FindByLicenseClassID(licenseClassID, ref className, ref classDescription,
                ref minAllowedAge, ref defaultValidityLength, ref classFees))
            {
                return new LicenseClassBussiness(licenseClassID, className, classDescription,
                    minAllowedAge, defaultValidityLength, classFees);
            }
            else
                return null;
        }
        public static LicenseClassBussiness Find(string ClassName)
        {
            int LicenseClassID = -1; string ClassDescription = "";
            byte MinimumAllowedAge = 18; byte DefaultValidityLength = 10; float ClassFees = 0;

            if (LicenseClassData.FindByClassName(ClassName, ref LicenseClassID, ref ClassDescription,
                    ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))

                return new LicenseClassBussiness(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;
        }


        public static DataTable GetAllLicenseClasses()
        {
            return LicenseClassData.GetAllLicenseClasses();
        }

    }
}