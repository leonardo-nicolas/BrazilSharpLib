using System.Diagnostics;

namespace BrazilSharp
{
    [DebuggerStepThrough]
    internal class LanguageThrowHelper
    {
        internal readonly int dwRegion;
        internal readonly string lpCountryAndLang;
        internal LanguageThrowHelper(int dwRegion, string lpCountryAndLang) {
            this.dwRegion = dwRegion;
            this.lpCountryAndLang = lpCountryAndLang;
            MsgErrorStateRegistration = string.Empty;
            MsgErrorNameBrazilianState = string.Empty;
            MsgErrorThirteenthOutOfRange_Day = string.Empty;
            MsgErrorThirteenthOutOfRange_Month = string.Empty;
        }
        public string 
            MsgErrorStateRegistration,
            MsgErrorNameBrazilianState,
            MsgErrorThirteenthOutOfRange_Day, 
            MsgErrorThirteenthOutOfRange_Month;
    }
}