using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BrazilSharp
{
    /// <summary>Set of static methods to use in some situations...</summary>
    public static class Utilities {
        
        /// <summary>Brazilian state code by Regional Electoral Court (In Brazil, known as TRE - Tribunal Regional Eleitoral)</summary>
        public static readonly IReadOnlyDictionary<BrazilianStates,short> TREBrazilianStatesCode = new Dictionary<BrazilianStates, short>
        {
            {BrazilianStates.AC, 24},
            {BrazilianStates.AL, 17},
            {BrazilianStates.AM, 22},
            {BrazilianStates.AP, 25},
            {BrazilianStates.BA, 05},
            {BrazilianStates.CE, 07},
            {BrazilianStates.DF, 20},
            {BrazilianStates.ES, 14},
            {BrazilianStates.GO, 10},
            {BrazilianStates.MA, 11},
            {BrazilianStates.MG, 02},
            {BrazilianStates.MS, 19},
            {BrazilianStates.MT, 18},
            {BrazilianStates.PA, 13},
            {BrazilianStates.PB, 12},
            {BrazilianStates.PE, 08},
            {BrazilianStates.PI, 15},
            {BrazilianStates.PR, 06},
            {BrazilianStates.RJ, 03},
            {BrazilianStates.RN, 15},
            {BrazilianStates.RS, 04},
            {BrazilianStates.RO, 23},
            {BrazilianStates.RR, 26},
            {BrazilianStates.SC, 09},
            {BrazilianStates.SE, 21},
            {BrazilianStates.SP, 01},
            {BrazilianStates.TO, 27},
            {BrazilianStates.ZZ, 28},
        };
    
        /// <summary>Gets a issuer Brazilian State of Voter Title.</summary>
        /// <param name="Expression">Voter Title's number to find issuer state</param>
        /// <returns>Returns some <see cref="BrazilianStates"/>' value if <paramref name="Expression"/> is valid Voter Title document. Otherwise, returns <see langword="null"/>.</returns>
        public static BrazilianStates? FindStateVoterTitle(object Expression) {
            string svt;
            switch (Expression)
            {
                case null:
                    svt = string.Empty;
                    break;
                case char[] chArray:
                    svt = new String(chArray);
                    break;
                case string str:
                    svt = str;
                    break;
                case Array arr:
                    svt = String.Join("", arr);
                    break;
                default:
                    svt = Convert.ToString(Expression);
                    break;
            }
            svt = Regex.Replace(svt, @"[^0-9]+", string.Empty);
            if(svt.Length == 0 || svt.Length > 12)
                return null;
            while(svt.Length < 12)
                svt = svt.Insert(0, "0");
            int[] CheckingDigit = new int[2], gotDigit={Convert.ToInt32(svt.Substring(10, 1)),Convert.ToInt32(svt.Substring(11,1))};
            int summation = 0;
            short stateCode = Convert.ToInt16(svt.Substring(8, 2));
            for (int index = 0, weight = 2; index < 8; ++index, ++weight)
                summation += weight * Convert.ToInt32(svt.Substring(index,1));
            CheckingDigit[0] = summation % 11;
            CheckingDigit[0] = CheckingDigit[0] > 9 ? 0 : CheckingDigit[0];
            summation = 0;
            for (int index = 8, weight = 7; index < 11; ++index, ++weight)
                summation += weight * Convert.ToInt32(svt.Substring(index,1));
            CheckingDigit[1] = summation % 11;
            CheckingDigit[1] = CheckingDigit[1] > 9 ? 0 : CheckingDigit[1];
            
            if(!gotDigit.SequenceEqual(CheckingDigit))
                return null;
            else
                return TREBrazilianStatesCode.Where(data => Convert.ToInt16(svt.Substring(8, 2)) == data.Value).FirstOrDefault().Key;
        }

        
    }
}