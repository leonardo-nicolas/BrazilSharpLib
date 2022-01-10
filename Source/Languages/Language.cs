using System.Diagnostics;

namespace BrazilSharp
{
    [DebuggerStepThrough]
    internal class LanguageThrowHelper
    {
        internal int DwRegion { get; }
        internal string LpCountryAndLang { get; }
        internal LanguageThrowHelper(int dwRegion, string lpCountryAndLang)
        {
            DwRegion = dwRegion;
            LpCountryAndLang = lpCountryAndLang;
            MsgErrorStateRegistration = string.Empty;
            MsgErrorNameBrazilianState = string.Empty;
            MsgErrorThirteenthOutOfRangeWithDay = string.Empty;
            MsgErrorThirteenthOutOfRangeWithMonth = string.Empty;
        }
        public string MsgErrorStateRegistration { get; internal set; }
        public string MsgErrorNameBrazilianState { get; internal set; }
        public string MsgErrorThirteenthOutOfRangeWithDay { get; internal set; }
        public string MsgErrorThirteenthOutOfRangeWithMonth { get; internal set; }
    }
}