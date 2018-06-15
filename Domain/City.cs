using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class City
    {
        public int CityId
        {
            get;
            set;
        }

        public string CityName
        {
            get;
            set;
        }

        public int CitySort
        {
            get;
            set;
        }

        public int ProvinceId
        {
            get;
            set;
        }
    }
}