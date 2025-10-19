using Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Countries_Data;
using System.Xml.Linq;

namespace Countries_Bussiness
{
   public class CountriesBussiness
    {
        public int ID { set; get; }
        public string CountryName { set; get; }
        public CountriesBussiness()
        {
            this.ID = -1;
            this.CountryName = "";
        }

        private CountriesBussiness(int ID, string CountryName)

        {
            this.ID = ID;
            this.CountryName = CountryName;

        }

        public static DataTable GetAllCountries()
        {
            return CountriesData.GetAllCountries();
        }

        public static CountriesBussiness Find(string CountryName)
        {

            int ID = -1;


            if (CountriesData.GetCountryInfoByName(CountryName, ref ID))

                return new CountriesBussiness(ID, CountryName);
            else
                return null;

        }

        public static CountriesBussiness Find(int ID)
        {

            string CountryName = "";

            if (CountriesData.GetCountryInfoByID(ID, ref CountryName))

                return new CountriesBussiness(ID, CountryName);
            else
                return null;

        }

    }
}
