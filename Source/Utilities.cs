using System;
using System.Collections.Generic;
using System.Linq;

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
                    svt = new string(chArray);
                    break;
                case string str:
                    svt = str;
                    break;
                case Array arr:
                    svt = string.Join("", arr);
                    break;
                default:
                    svt = Convert.ToString(Expression);
                    break;
            }
            if(!Validate.VoterTitle(svt))
                return null;
            else
                return TREBrazilianStatesCode
                    .FirstOrDefault(data => 
                        Convert.ToInt16(
                            svt.Substring(8, 2)
                        ) == data.Value
                    ).Key;
        }

        internal static string TakeOnlyNumbers(string entry)
        {
            string mount = string.Empty;
            char[] chArray = entry.ToCharArray();
            foreach(char chr in chArray)
                if(char.IsNumber(chr))
                    mount += Convert.ToString(chr);
            return mount;
        }
        
        internal static bool IsRepeated(string str)
        {
            int summation = 0;
            char firstChar = Convert.ToChar(str.Substring(0, 1));
            char[] chArray = str.ToCharArray();
            foreach(char chr in chArray)
                if (chr == firstChar)
                    ++summation;
            return summation == str.Length;
        }
    }
}