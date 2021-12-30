using System;
using BrazilSharp.Calculations;
using System.Reflection;

namespace BrazilSharp
{
    internal static class ThrowHelper
    {
        internal static string GetMsgErrorStateRegistration() =>
            Convert.ToString(
                typeof(LanguageThrowHelper)
                .GetTypeInfo()
                .GetField(
                    nameof(GetMsgErrorStateRegistration)
                    .Substring(3)
                )
                .GetValue(
                    Languages.GetDefaultLanguageThrowHelper()
                )
            );

        internal static string GetMsgErrorNameBrazilianState() =>
            Convert.ToString(
                typeof(LanguageThrowHelper)
                .GetTypeInfo()
                .GetField(
                    nameof(GetMsgErrorNameBrazilianState)
                    .Substring(3)
                )
                .GetValue(
                    Languages.GetDefaultLanguageThrowHelper()
                )
            );

        internal static string GetMsgErrorThirteenthOutOfRange(PeriodType regime) { 
            switch (regime) {
             case PeriodType.Day:
             case PeriodType.Month:
                    return Convert.ToString(
                        typeof(LanguageThrowHelper)
                        .GetTypeInfo()
                        .GetField(
                            nameof(GetMsgErrorThirteenthOutOfRange)
                            .Substring(3) + "_" + 
                            Extensions.GetEnumName(regime)
                        )
                        .GetValue(
                            Languages
                            .GetDefaultLanguageThrowHelper()
                        )
                    );
            default: return string.Empty;
            }
        }
        
    }
}