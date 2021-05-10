using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePremium
{
    public class DataModel
    {
        public class PremiumDetails
        {
            public int Age { get; set; }
            public int SumAssured { get; set; }
            public double? Premium { get; set; }
        }
        public class BandRangeDetails
        {
            public int lowerBandSum { get; set; }
            public double? lowerBandRiskRate { get; set; }
            public int upperBandSum { get; set; }
            public double? upperBandRiskRate { get; set; }
        }

        public class DataDetails
        {
            public int sumAssured { get; set; }
            public double? Age_30 { get; set; }
            public double? Age_31_50 { get; set; }
            public double? Age_50 { get; set; }
        }

    }
}
