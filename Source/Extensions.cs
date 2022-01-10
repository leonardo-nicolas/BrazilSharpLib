using System;
using BrazilSharp.Exceptions;

namespace BrazilSharp
{
    /// <summary>Set of static methods to extends some classes.</summary>
    public static class Extensions 
    {
        /// <summary>Gets Full name of this state</summary>
        /// <param name="bs">Source of <see cref="BrazilianStates"/>' object.</param>
        /// <returns>A <see cref="String"/> containing full name of brazilian state. E.G: <see cref="BrazilianStates.RJ"/> => "Rio de Janeiro". </returns>
        /// <exception cref="StateNotFoundException">Value in <paramref name="bs"/> is invalid value...</exception>
        public static string GetFullName(this BrazilianStates bs) {
            switch (bs)
            {
                case BrazilianStates.AC: return "Acre";
                case BrazilianStates.AL: return "Alagoas";
                case BrazilianStates.AM: return "Amazonas";
                case BrazilianStates.AP: return "Amapá";
                case BrazilianStates.BA: return "Bahia";
                case BrazilianStates.CE: return "Ceará";
                case BrazilianStates.ES: return Languages.GetTranslateOfState(bs);
                case BrazilianStates.GO: return "Goiás";
                case BrazilianStates.MA: return "Maranhão";
                case BrazilianStates.MG: return Languages.GetTranslateOfState(bs);
                case BrazilianStates.MS: return Languages.GetTranslateOfState(bs);
                case BrazilianStates.MT: return Languages.GetTranslateOfState(bs);
                case BrazilianStates.PA: return "Pará";
                case BrazilianStates.PB: return "Paraíba";
                case BrazilianStates.PR: return "Paraná";
                case BrazilianStates.PE: return "Pernambuco";
                case BrazilianStates.PI: return "Piauí";
                case BrazilianStates.RJ: return Languages.GetTranslateOfState(bs);
                case BrazilianStates.RN: return Languages.GetTranslateOfState(bs);
                case BrazilianStates.RS: return Languages.GetTranslateOfState(bs);
                case BrazilianStates.RO: return "Rondônia";
                case BrazilianStates.RR: return "Roraima";
                case BrazilianStates.SC: return Languages.GetTranslateOfState(bs);
                case BrazilianStates.SP: return Languages.GetTranslateOfState(bs);
                case BrazilianStates.SE: return "Sergipe";
                case BrazilianStates.TO: return "Tocantins";
                case BrazilianStates.DF: return Languages.GetTranslateOfState(bs);
                default: throw new StateNotFoundException(ThrowHelper.GetMsgErrorNameBrazilianState());
            }
        }


       }
}