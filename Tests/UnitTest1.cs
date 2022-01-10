using BrazilSharp;
using BrazilSharp.Calculations;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("EN-US");
        }

        [Test]
        public void TestUtilities()
        {
            var VoterTitle = 370171000140; // Voter Title is from SP.
            var findStateVoterTitle = Utilities.FindStateVoterTitle(VoterTitle);

            if (findStateVoterTitle == null)
                Assert.IsNull(findStateVoterTitle, "Voter Title Is Null. It means that Voter Title number is invalid or it isn't possible to find.");
            else 
                Assert.IsNotNull(findStateVoterTitle, $"Voter Title isn't null and the result found is {System.Enum.GetName(findStateVoterTitle ?? default)}");
            Assert.Pass($"Voter Title result for '{VoterTitle}' is {(findStateVoterTitle != null ? System.Enum.GetName(findStateVoterTitle ??default) : "null")}");
            System.Diagnostics.Debugger.Break();
        }

        [Test]
        [TestCase(1500.00,true,PeriodType.Month,8)] // Correctly: 8 months with early warn (PT-BR: Aviso Prévio) 
        [TestCase(1500.00,false,PeriodType.Month,5)] // Correctly: 5 months without early EarlyWarn
        [TestCase(1500.00,true,PeriodType.Day,30)] // Correctly: 30 days with early warn (PT-BR: Aviso Prévio) 
        [TestCase(1500.00,false,PeriodType.Day,18)] // Correctly: 18 days without early EarlyWarn
        [TestCase(1500.00,true,PeriodType.Day,12)] //Correctly: 12 days without early warn
        [TestCase(1500.00,false,PeriodType.Day,15)] //Correctly: 15 days without early warn
        [TestCase(1500.00,true,PeriodType.Month,13)] // Wrong (throws exception): 13 month with early warn
        [TestCase(1500.00,false,PeriodType.Month,25)] //Wrong (throws Exception): 25 month without early warn
        [TestCase(1500.00,true,PeriodType.Day,39)] // Wrong (throws exception): 39 days with early warn
        [TestCase(1500.00,false,PeriodType.Day,90)] //Wrong (throws Exception): 90 days without early warn
        public void TestCalculation13th(double BaseCalc, bool EarlyWarn, PeriodType period, short HowLong)
        {
            double result13Th;
            try
            {
                result13Th = WorkersCalculation.Thirteenth(BaseCalc, EarlyWarn, period, HowLong);
                
            }
            catch(System.Exception Ex)
            {
                Assert.Fail($"A fail ocurred: {Ex}");
                return;
            }
            Assert.Pass($"Calculation of 13th: {result13Th}");
        }

        [Test]
        public void TestExtensions()
        {
            BrazilianStates stateWhereAuthorLives = BrazilianStates.ES; //If use ZZ, throws exception
            string fullName = "";
            try
            {
                fullName = stateWhereAuthorLives.GetFullName();
            }
            catch (System.Exception Ex)
            {
                Assert.Fail($"The fail was caused with {System.Enum.GetName(stateWhereAuthorLives)}, with exception {Ex}");
                return;
            }
            Assert.Pass($"The Author of this project lives in {fullName} state");
        }

        [Test]
        [TestCase("CPF", "CPF", "250.135.740-05")]
        [TestCase("CNPJ","CNPJ","55.023.697/0001-08")]
        [TestCase("DriverLicense","CNH","92366290549")]
        [TestCase("Email","E-mail","testcase@dotnet.com")]
        [TestCase("Email","E-mail","testing321@senhordostestes.com.br")]
        [TestCase("DateOrTime", "Date", "16/11/1994")]
        [TestCase("VoterTitle", "Brazilian Voter Title Card", "552667570396")]
        [TestCase("VoterTitle", "Brazilian Voter Title Card", 586612712780)]
        [TestCase("PIS", "PIS or PASEP", "564.28611.47-3")]
        [TestCase("Renavam", "RENAVAM", 69303049313)]
        [TestCase("Numeric", "Any Number", 0xabc5328de64f)]
        public void TestDocuments(string methodName, string type, object document)
        {
            bool isValid = false;
            try
            {
                isValid = methodName switch
                {
                    "CPF" => Validate.CPF(document),
                    "CNPJ" => Validate.CNPJ(document),
                    "DriverLicense" => Validate.DriverLicense(document),
                    "Email" => Validate.Email(document),
                    "DateOrTime" => Validate.DateOrTime(document),
                    "VoterTitle" => Validate.VoterTitle(document),
                    "PIS" => Validate.PIS(document),
                    "Renavam" => Validate.Renavam(document),
                    "Numeric" => Validate.Numeric(document),
                    _ => false
                };
                if(isValid)
                    Assert.IsTrue(isValid,$"test of document {document} as {type.ToUpper()}, passed using method Validate.{methodName}.");
                else
                    Assert.IsFalse(isValid,$"test of document {document} as {type.ToUpper()}, passed using method Validate.{methodName}.");
            }
            catch(System.Exception Ex)
            {
                Assert.Fail($"{Ex}");
                return;
            }
            Assert.Pass($"test of document '{document}' as {type.ToUpper()}, passed as {isValid} using method Validate.{methodName}.");
        }

        [Test]
        [TestCase(BrazilianStates.AC, "01.710.246/011-07")]
        [TestCase(BrazilianStates.AL, "248523996")]
        [TestCase(BrazilianStates.AM, "90.895.295-3")]
        [TestCase(BrazilianStates.AP, "030846056")]
        [TestCase(BrazilianStates.BA, "768866-30")]//Weight 10
        [TestCase(BrazilianStates.BA, "287254-02")]//Weight 11
        [TestCase(BrazilianStates.CE, "07583224-0")]
        [TestCase(BrazilianStates.DF, "07425761001-88")]
        [TestCase(BrazilianStates.ES, "56201110-2")]
        [TestCase(BrazilianStates.GO, "15.189.737-9")]
        [TestCase(BrazilianStates.MA, "12513206-9")]
        [TestCase(BrazilianStates.MG, "178.326.260/1360")]
        [TestCase(BrazilianStates.MS, "28151899-8")]
        [TestCase(BrazilianStates.MT, "7018043344-0")]
        [TestCase(BrazilianStates.PA, "15-502598-8")]
        [TestCase(BrazilianStates.PB, "02044749-3")]
        [TestCase(BrazilianStates.PE, "7361386-09")]
        [TestCase(BrazilianStates.PI, "81622444-7")]
        [TestCase(BrazilianStates.PR, "549.67725-81")]
        [TestCase(BrazilianStates.RJ, "34.792.62-3")]
        [TestCase(BrazilianStates.RJ, "34.350.99-0")] // Invalid state registration number for RJ
        [TestCase(BrazilianStates.RN, "20.225.770-3")]
        [TestCase(BrazilianStates.RO, "8177572143065-0")]
        [TestCase(BrazilianStates.RR, "24394042-2")]
        [TestCase(BrazilianStates.RS, "021/2161180")]
        [TestCase(BrazilianStates.SC, "535.840.012")]
        [TestCase(BrazilianStates.SE, "59772901-8")]
        [TestCase(BrazilianStates.SP, "202.003.550.350")]//Commerce and Industry
        [TestCase(BrazilianStates.SP, "P-011000424.3/002")]//Rural Producers
        [TestCase(BrazilianStates.TO, "9003326815-9")]
        [TestCase(BrazilianStates.ZZ, "123456789")] //Invalid case: ZZ or different value throws exception.
        public void TestStateRegistration(BrazilianStates state, string reg)
        {
            if (state == BrazilianStates.ZZ)
                System.Diagnostics.Debugger.Break();
            string? currentState = System.Enum.GetName(state);
            bool isValid;
            try
            {                    
                isValid = Validate.StateRegistration(reg, state);
                if(isValid)
                    Assert.IsTrue(isValid,$"'{reg}' is {isValid} for {currentState}");
                else
                    Assert.IsFalse(isValid,$"'{reg}' is {isValid} for {currentState}");
            }
            catch (System.Exception Ex)
            {
                Assert.Fail($"State that caused fail: {currentState + System.Environment.NewLine}Who caused fail: {Ex} ");
                return;
            }
            Assert.Pass($"'{reg}' is {isValid} for {currentState}");
        }
    }
}