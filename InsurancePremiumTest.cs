using NUnit.Framework;
using InsurancePremium;

namespace InsurancePremiumTests
{
    public class Tests
    {
        public Program _insurancePremium;

        [Test]
        public void Test_PremiumCalc_ForValidData_Age_18([Values(18)] int age, [Values(25000)] int sumAssured)
        {
            var premium = new DataModel.PremiumDetails{ Age = 18, SumAssured = 40000, Premium = 2.1085748000000004 };
            Assert.AreEqual(premium.Premium, Program.PremiumCalc(age, sumAssured).Premium);
        }

        [Test]
        public void Test_PremiumCalc_ForValidData_Age_30([Values(30)] int age, [Values(50000)] int sumAssured)
        {
            DataModel.PremiumDetails premium = new DataModel.PremiumDetails { Age = 30, SumAssured = 50000, Premium = 2.5917375000000002 };

            Assert.AreEqual(premium.Premium, Program.PremiumCalc(age, sumAssured).Premium);
        }

        [Test]
        public void Test_PremiumCalc_ForValidData_Age_49([Values(49)] int age, [Values(60000)] int sumAssured)
        {
            DataModel.PremiumDetails premium = new DataModel.PremiumDetails { Age = 49, SumAssured = 60000, Premium = 18.5775744 };

            Assert.AreEqual(premium.Premium, Program.PremiumCalc(age, sumAssured).Premium);
        }
    }
}