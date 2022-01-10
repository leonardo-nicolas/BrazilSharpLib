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
                .GetTypeInfo()?
                .GetProperty(
                    nameof(GetMsgErrorStateRegistration)
                    .Substring(3)
                )?
                .GetValue(
                    Languages.GetDefaultLanguageThrowHelper()
                ) ?? string.Empty
            );

        internal static string GetMsgErrorNameBrazilianState() =>
            Convert.ToString(
                typeof(LanguageThrowHelper)
                .GetTypeInfo()?
                .GetProperty(
                    nameof(GetMsgErrorNameBrazilianState)
                    .Substring(3)
                )?
                .GetValue(
                    Languages.GetDefaultLanguageThrowHelper()
                ) ?? string.Empty
            );

        internal static string GetMsgErrorThirteenthOutOfRange(PeriodType regime) { 
            switch (regime) {
             case PeriodType.Day:
             case PeriodType.Month:
                    return Convert.ToString(
                        typeof(LanguageThrowHelper)
                        .GetTypeInfo()?
                        .GetProperty(
                            nameof(GetMsgErrorThirteenthOutOfRange)
                            .Substring(3) + "With" + 
                            Enum.GetName(typeof(PeriodType), regime)
                        )?
                        .GetValue(
                            Languages
                            .GetDefaultLanguageThrowHelper()
                        ) ?? string.Empty
                    );
            default: return string.Empty;
            }
        }
        
    }
}