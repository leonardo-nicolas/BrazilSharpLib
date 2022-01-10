using System;
using System.Globalization;
using System.Linq;

namespace BrazilSharp
{
    internal static class Languages
    {
        internal static LanguageThrowHelper GetDefaultLanguageThrowHelper()
        {
            // TODO: Add support for more languages
            LanguageThrowHelper[] langResForThrowHelper = 
            {
                // ! Portuguese - Only from Brazil
                new LanguageThrowHelper(1046,"PT-BR") /* Brazil */ {
                    MsgErrorNameBrazilianState = "Informe um estado correto ou o código IBGE do estado!",
                    MsgErrorStateRegistration = "Informe um estado correto ou o código IBGE do estado! Não utilize o ZZ ou 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "Os dias trabalhados devem estar no intervalo entre 1 e 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "Os meses trabalhados devem estar no intervalo entre 1 e 12!"
                },

                // ! Spanish from Latin America
                new LanguageThrowHelper(11274,"ES-AR") /* Argentina */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(2058,"ES-MX") /* Mexico */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(5130,"ES-CR") /* Costa Rica */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(6154,"ES-PA") /* Panama */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(7178,"ES-DO") /* Dominican Republic */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(8202,"ES-VE") /* Venezuela */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(9226,"ES-CO") /* Colombia */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(10250,"ES-PE") /* Peru */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(12298,"ES-EC") /* Ecuador */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(13322,"ES-CL") /* Chile */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(14346,"ES-UY") /* Uruguay */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(15370,"ES-PY") /* Paraguay */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(16394,"ES-BO") /* Bolivia */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(17418,"ES-SV") /* El-Salvador */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(18442,"ES-HN") /* Honduras */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(19466,"ES-NI") /* Nicaragua */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(20490,"ES-PR") /* Puerto Rico */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(21514,"ES-US") /* United States (same as Mexico) */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(58378,"ES-419") /* Latin America */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "¡Los meses trabajados deben estar entre 1 y 12!"
                },

                // ! English - Only from Americas
                new LanguageThrowHelper(1033,"EN-US") /* United States */ {
                    MsgErrorNameBrazilianState = "Set an correctly Brazilian state or Brazilian IBGE state code!",
                    MsgErrorStateRegistration = "Set an correctly Brazilian state or Brazilian IBGE state code! Don't use ZZ or 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "Worked days should be between 1 and 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "Worked months should be between 1 and 12!"
                },
                new LanguageThrowHelper(4105,"EN-CA") /* Canada */ {
                    MsgErrorNameBrazilianState = "Set an correctly Brazilian state or Brazilian IBGE state code!",
                    MsgErrorStateRegistration = "Set an correctly Brazilian state or Brazilian IBGE state code! Don't use ZZ or 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "Worked days should be between 1 and 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "Worked months should be between 1 and 12!"
                },
                new LanguageThrowHelper(9225,"EN-029") /* Caribbean */ {
                    MsgErrorNameBrazilianState = "Set an correctly Brazilian state or Brazilian IBGE state code!",
                    MsgErrorStateRegistration = "Set an correctly Brazilian state or Brazilian IBGE state code! Don't use ZZ or 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "Worked days should be between 1 and 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "Worked months should be between 1 and 12!"
                },
                new LanguageThrowHelper(10249,"EN-BZ") /* Belize */ {
                    MsgErrorNameBrazilianState = "Set an correctly Brazilian state or Brazilian IBGE state code!",
                    MsgErrorStateRegistration = "Set an correctly Brazilian state or Brazilian IBGE state code! Don't use ZZ or 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "Worked days should be between 1 and 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "Worked months should be between 1 and 12!"
                },
                new LanguageThrowHelper(11273,"EN-TT") /* Trinidad */ {
                    MsgErrorNameBrazilianState = "Set an correctly Brazilian state or Brazilian IBGE state code!",
                    MsgErrorStateRegistration = "Set an correctly Brazilian state or Brazilian IBGE state code! Don't use ZZ or 99!",
                    MsgErrorThirteenthOutOfRangeWithDay = "Worked days should be between 1 and 31!",
                    MsgErrorThirteenthOutOfRangeWithMonth = "Worked months should be between 1 and 12!"
                },
            };            
            LanguageThrowHelper defaultLang = langResForThrowHelper.FirstOrDefault(data => data.DwRegion == CultureInfo.CurrentCulture.LCID);
            defaultLang = defaultLang ?? langResForThrowHelper.First(data => data.DwRegion == 1033);
            LanguageThrowHelper systemDefaultLang = langResForThrowHelper
                .Where(data => string.Equals(
                        data.LpCountryAndLang, 
                        CultureInfo.CurrentCulture.Name, 
                        StringComparison.OrdinalIgnoreCase
                    )
                )
                .DefaultIfEmpty(defaultLang)
                .FirstOrDefault();
            
            return systemDefaultLang;
        }

        internal static string GetTranslateOfState(BrazilianStates state)
        {
            // Uses only for ES, MG, MS, MT, RJ, RN, RS, SC and DF. That then are translatable
            (int Stt, string Lang, string Translate)[] statesInManyLangs = 
            {
                // TODO: Add Supports for other languages.
                /* Português */
                (32, "PT", "Espírito Santo"),
                (31, "PT", "Minas Gerais"),
                (50, "PT", "Mato Grosso do Sul"),
                (51, "PT", "Mato Grosso"),
                (33, "PT", "Rio de Janeiro"),
                (24, "PT", "Rio Grande do Norte"),
                (43, "PT", "Rio Grande do Sul"),
                (42, "PT", "Santa Catarina"),
                (35, "PT", "São Paulo"),
                (53, "PT", "Distrito Federal"),
                /* English */
                (32, "EN", "Holy Spirit"),
                (31, "EN", "General Mines"),
                (50, "EN", "Thick Bushes of the South"),
                (51, "EN", "Thick Bushes"),
                (33, "EN", "River of January"),
                (24, "EN", "Great Northern River"),
                (43, "EN", "Great Southern River"),
                (42, "EN", "Saint Catherine"),
                (35, "EN", "Saint Paul"),
                (53, "EN", "Federal District"),
                /* Español */
                (32, "ES", "Espíritu Santo"),
                (31, "ES", "Minas Generales"),
                (50, "ES", "Arbustos Espesos del Sur"),
                (51, "ES", "Arbustos Espesos"),
                (33, "ES", "Río de Enero"),
                (24, "ES", "Gran Río del Norte"),
                (43, "ES", "Gran Río del Sul"),
                (42, "ES", "Santa Catalina"),
                (35, "ES", "San Pablo"),
                (53, "ES", "Distrito Federal")
            };

            string currentLang = string.IsNullOrEmpty(CultureInfo.CurrentCulture.Name) ? "EN" : CultureInfo.CurrentCulture.Name;
            currentLang = currentLang.Length >= 2 ? currentLang.Substring(0, 2) : currentLang;
            var (_, _, translate) = statesInManyLangs
                .Where(data =>
                    string.Equals(
                        data.Lang,
                        currentLang,
                        StringComparison.InvariantCultureIgnoreCase
                    ) &&
                    data.Stt == (int)state
                )
                .DefaultIfEmpty(
                    statesInManyLangs
                    .First(data =>
                        data.Stt == (int)state &&
                        string.Equals(
                            data.Lang,
                            "PT",
                            StringComparison.OrdinalIgnoreCase
                        )
                    )
                )
                .FirstOrDefault();
            return translate;
        }
    }
}