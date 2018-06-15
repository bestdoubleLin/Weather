using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class District
    {
        public int DistrictId
        {
            get;
            set;
        }

        public string DisName
        {
            get;
            set;
        }

        public int DisSort
        {
            get;
            set;
        }

        public int CityId
        {
            get;
            set;
        }
    }
}