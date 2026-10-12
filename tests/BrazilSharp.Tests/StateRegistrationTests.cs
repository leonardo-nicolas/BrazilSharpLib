using System;
using BrazilSharp;
using BrazilSharp.Exceptions;
using Xunit;

namespace BrazilSharp.Tests
{
    public sealed class StateRegistrationTests
    {
        // Reference vectors: SINTEGRA/SEFAZ cross-references, indexed in
        // https://github.com/open-data-brazil/br-validators/blob/main/docs/OFFICIAL-SOURCES.md
        [Theory]
        [InlineData(BrazilianStates.AC, "0113253877910")]
        [InlineData(BrazilianStates.AL, "248682954")]
        [InlineData(BrazilianStates.AM, "917050150")]
        [InlineData(BrazilianStates.AP, "039045820")]
        [InlineData(BrazilianStates.BA, "63984300")]
        [InlineData(BrazilianStates.CE, "836182316")]
        [InlineData(BrazilianStates.DF, "0730000100109")]
        [InlineData(BrazilianStates.ES, "463921810")]
        [InlineData(BrazilianStates.GO, "112237118")]
        [InlineData(BrazilianStates.MA, "123517680")]
        [InlineData(BrazilianStates.MG, "2490944173923")]
        [InlineData(BrazilianStates.MS, "282570926")]
        [InlineData(BrazilianStates.MT, "130000019")]
        [InlineData(BrazilianStates.PA, "153662476")]
        [InlineData(BrazilianStates.PB, "312029063")]
        [InlineData(BrazilianStates.PE, "064970639")]
        [InlineData(BrazilianStates.PI, "465180426")]
        [InlineData(BrazilianStates.PR, "0031595584")]
        [InlineData(BrazilianStates.RJ, "06540481")]
        [InlineData(BrazilianStates.RN, "204502292")]
        [InlineData(BrazilianStates.RO, "39206839474860")]
        [InlineData(BrazilianStates.RR, "247681047")]
        [InlineData(BrazilianStates.RS, "3288345503")]
        [InlineData(BrazilianStates.SC, "632480718")]
        [InlineData(BrazilianStates.SE, "826594042")]
        [InlineData(BrazilianStates.SP, "110042490114")]
        [InlineData(BrazilianStates.TO, "27035910938")]
        public void AcceptsReferenceChecksums(BrazilianStates state, string number)
        {
            Assert.True(Validate.StateRegistration(number, state));
            Assert.True(Validate.StateRegistration(number.AsSpan(), state));

            char[] characters = number.ToCharArray();
            Assert.True(Validate.StateRegistration((object)characters, state));

            char[] invalid = number.ToCharArray();
            invalid[invalid.Length - 1] = invalid[invalid.Length - 1] == '0' ? '1' : '0';
            Assert.False(Validate.StateRegistration(new string(invalid), state));
        }

        [Theory]
        [InlineData(BrazilianStates.SP, "P-01100424.3/002")]
        [InlineData(BrazilianStates.BA, "123456748")]
        [InlineData(BrazilianStates.MT, "00130000019")]
        [InlineData(BrazilianStates.TO, "290319986")]
        [InlineData(BrazilianStates.PE, "032141840")]
        public void AcceptsImportantStateSpecificVariants(BrazilianStates state, string number)
        {
            Assert.True(Validate.StateRegistration(number, state));
        }

        [Theory]
        [InlineData(BrazilianStates.SP, "110.042.490.114")]
        [InlineData(BrazilianStates.RJ, "06.540.48-1")]
        [InlineData(BrazilianStates.SP, "P-01100424.3/002")]
        public void AcceptsOrdinaryFormatting(BrazilianStates state, string number)
        {
            Assert.True(Validate.StateRegistration(number, state));
        }

        [Theory]
        [InlineData("ISENTO", BrazilianStates.SP)]
        [InlineData("abcdefghijklmn", BrazilianStates.SP)]
        [InlineData("110042490114Z", BrazilianStates.SP)]
        [InlineData("110042490114/100", BrazilianStates.SP)]
        [InlineData("999999999", BrazilianStates.AL)]
        [InlineData("P-01100424.3/002", BrazilianStates.RJ)]
        [InlineData("000000000", BrazilianStates.PB)]
        [InlineData("", BrazilianStates.AC)]
        public void RejectsInvalidInput(string number, BrazilianStates state)
        {
            Assert.False(Validate.StateRegistration(number, state));
        }

        [Fact]
        public void RejectsNullViaLegacyAndStringOverloads()
        {
            Assert.False(Validate.StateRegistration((object)null!, BrazilianStates.SP));
            Assert.False(Validate.StateRegistration((string)null!, BrazilianStates.SP));
        }

        [Fact]
        public void UnknownUfUsesOriginalExceptionContract()
        {
            Assert.Throws<StateNotFoundException>(
                () => Validate.StateRegistration("110042490114", (BrazilianStates)99));
        }
    }
}
