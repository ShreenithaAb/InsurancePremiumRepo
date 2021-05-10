using System;
using System.Collections.Generic;
using System.Linq;
using static InsurancePremium.DataModel;

namespace InsurancePremium
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Life Insuarance Premium Calculator");


            int age, sumAssured = 0;
        Age:
            Console.WriteLine("Enter the age between 18 - 65 years ?");
            age = Convert.ToInt32(Console.ReadLine());
            if (!(age >= 18 && age <= 65))
            {
                goto Age;
            }
        SumAssured:
            Console.WriteLine("Enter total sum to be assured in the range 25000 - 500000 ?");
            sumAssured = Convert.ToInt32(Console.ReadLine());
            if (!(sumAssured >= 25000 && sumAssured <= 500000))
            {
                goto SumAssured;
            }

            PremiumDetails premium = PremiumCalc(age, sumAssured);
            Console.WriteLine("Age : " + premium.Age);
            Console.WriteLine("SumAssured : " + premium.SumAssured);
            Console.WriteLine("Gross Premium : " + (premium.Premium != null ? premium.Premium  : "Not Available"));
        }
            
        public static PremiumDetails PremiumCalc(int age, int sumAssured)
        {
            PremiumDetails premium = new PremiumDetails();
            try
            {

            Caln:

                BandRangeDetails rangeDetails = BandRangeValues(age, sumAssured);
                double? grossPremium = null;

                if (rangeDetails.lowerBandRiskRate != null && rangeDetails.upperBandRiskRate != null)
                {
                    double? riskRate = ((double)(sumAssured - rangeDetails.lowerBandSum) / (double)(rangeDetails.upperBandSum - rangeDetails.lowerBandSum) * rangeDetails.upperBandRiskRate + (double)(rangeDetails.upperBandSum - sumAssured) / (double)(rangeDetails.upperBandSum - rangeDetails.lowerBandSum) * rangeDetails.lowerBandRiskRate);
                
                    double? riskPremium = riskRate * (sumAssured / 1000);
                    double? renewalCommission = 0.03 * riskPremium;
                    double? netPremium = riskPremium + renewalCommission;
                    double? initialCommission = netPremium * 2.05;
                    grossPremium = netPremium + initialCommission;
                    if (grossPremium < 2)
                    {
                        sumAssured += 5000;
                        goto Caln;
                    }

                    
                }
                premium.Age = age;
                premium.SumAssured = sumAssured;
                premium.Premium = grossPremium;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return premium;

        }

        public static BandRangeDetails BandRangeValues(int age, int sumAssured)
        {
            BandRangeDetails rangeDetails = new BandRangeDetails();

            List<DataDetails> data = Data();

            rangeDetails.lowerBandSum = data.Where(a => a.sumAssured <= sumAssured).Select(b => b.sumAssured).LastOrDefault();
            rangeDetails.lowerBandRiskRate = data.Where(a => a.sumAssured <= sumAssured).Select(
                b => ((age >= 18 && age <= 30) ? b.Age_30 :
                        ((age >= 31 && age <= 50) ? b.Age_31_50 : b.Age_50))
                ).LastOrDefault();

            rangeDetails.upperBandSum = data.Where(a => a.sumAssured > sumAssured).Select(b => b.sumAssured).FirstOrDefault();
            rangeDetails.upperBandRiskRate = data.Where(a => a.sumAssured > sumAssured).Select(
                b => ((age >= 18 && age <= 30) ? b.Age_30 :
                        ((age >= 31 && age <= 50) ? b.Age_31_50 : b.Age_50))
                ).FirstOrDefault();

            return rangeDetails;
        }

        public static List<DataDetails> Data()
        {
            List<DataDetails> data = new List<DataDetails>();
            data.Add(new DataDetails
            {
                sumAssured = 25000,
                Age_30 = 0.0172,
                Age_31_50 = 0.1043,
                Age_50 = 0.2677
            });
            data.Add(new DataDetails
            {
                sumAssured = 50000,
                Age_30 = 0.0165,
                Age_31_50 = 0.0999,
                Age_50 = 0.2525
            });
            data.Add(new DataDetails
            {
                sumAssured = 100000,
                Age_30 = 0.0154,
                Age_31_50 = 0.0932,
                Age_50 = 0.2393
            });
            data.Add(new DataDetails
            {
                sumAssured = 200000,
                Age_30 = 0.0147,
                Age_31_50 = 0.0887,
                Age_50 = 0.2285
            });
            data.Add(new DataDetails
            {
                sumAssured = 300000,
                Age_30 = 0.0144,
                Age_31_50 = 0.0872,
                Age_50 = null
            });
            data.Add(new DataDetails
            {
                sumAssured = 500000,
                Age_30 = 0.0146,
                Age_31_50 = null,
                Age_50 = null
            });

            return data;
        }

    }
}
