using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace BrazilSharp
{
    internal static class Languages
    {
        internal static LanguageThrowHelper GetDefaultLanguageThrowHelper()
        {
            // TODO: Add support for more languages
            LanguageThrowHelper[] LangResourceForThrowHelper = 
            {
                // ! Portuguese - Only from Brazil
                new LanguageThrowHelper(1046,"PT-BR") /* Brazil */ {
                    MsgErrorNameBrazilianState = "Informe um estado correto ou o código IBGE do estado!",
                    MsgErrorStateRegistration = "Informe um estado correto ou o código IBGE do estado! Não utilize o ZZ ou 99!",
                    MsgErrorThirteenthOutOfRange_Day = "Os dias trabalhados devem estar no intervalo entre 1 e 31!",
                    MsgErrorThirteenthOutOfRange_Month = "Os meses trabalhados devem estar no intervalo entre 1 e 12!"
                },

                // ! Spanish from Latin America
                new LanguageThrowHelper(11274,"ES-AR") /* Argentina */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(2058,"ES-MX") /* Mexico */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(5130,"ES-CR") /* Costa Rica */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(6154,"ES-PA") /* Panama */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(7178,"ES-DO") /* Dominican Republic */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(8202,"ES-VE") /* Venezuela */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(9226,"ES-CO") /* Colombia */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(10250,"ES-PE") /* Peru */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(12298,"ES-EC") /* Ecuador */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(13322,"ES-CL") /* Chile */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(14346,"ES-UY") /* Uruguay */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(15370,"ES-PY") /* Paraguay */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(16394,"ES-BO") /* Bolivia */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(17418,"ES-SV") /* El-Salvador */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(18442,"ES-HN") /* Honduras */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(19466,"ES-NI") /* Nicaragua */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(20490,"ES-PR") /* Puerto Rico */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(21514,"ES-US") /* United States (same as Mexico) */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },
                new LanguageThrowHelper(58378,"ES-419") /* Latin America */ {
                    MsgErrorNameBrazilianState = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño!",
                    MsgErrorStateRegistration = "¡Establezca un estado brasileño correcto o un código de estado IBGE brasileño! ¡No uses ZZ o 99!",
                    MsgErrorThirteenthOutOfRange_Day = "¡Los días trabajados deben estar entre 1 y 31!",
                    MsgErrorThirteenthOutOfRange_Month = "¡Los meses trabajados deben estar entre 1 y 12!"
                },

                // ! English - Only from Americas
                new LanguageThrowHelper(1033,"EN-US") /* United States */ {
                    MsgErrorNameBrazilianState = "Set an correctly Brazilian state or Brazilian IBGE state code!",
                    MsgErrorStateRegistration = "Set an correctly Brazilian state or Brazilian IBGE state code! Don't use ZZ or 99!",
                    MsgErrorThirteenthOutOfRange_Day = "Worked days should be between 1 and 31!",
                    MsgErrorThirteenthOutOfRange_Month = "Worked months should be between 1 and 12!"
                },
                new LanguageThrowHelper(4105,"EN-CA") /* Canada */ {
                    MsgErrorNameBrazilianState = "Set an correctly Brazilian state or Brazilian IBGE state code!",
                    MsgErrorStateRegistration = "Set an correctly Brazilian state or Brazilian IBGE state code! Don't use ZZ or 99!",
                    MsgErrorThirteenthOutOfRange_Day = "Worked days should be between 1 and 31!",
                    MsgErrorThirteenthOutOfRange_Month = "Worked months should be between 1 and 12!"
                },
                new LanguageThrowHelper(9225,"EN-029") /* Caribbean */ {
                    MsgErrorNameBrazilianState = "Set an correctly Brazilian state or Brazilian IBGE state code!",
                    MsgErrorStateRegistration = "Set an correctly Brazilian state or Brazilian IBGE state code! Don't use ZZ or 99!",
                    MsgErrorThirteenthOutOfRange_Day = "Worked days should be between 1 and 31!",
                    MsgErrorThirteenthOutOfRange_Month = "Worked months should be between 1 and 12!"
                },
                new LanguageThrowHelper(10249,"EN-BZ") /* Belize */ {
                    MsgErrorNameBrazilianState = "Set an correctly Brazilian state or Brazilian IBGE state code!",
                    MsgErrorStateRegistration = "Set an correctly Brazilian state or Brazilian IBGE state code! Don't use ZZ or 99!",
                    MsgErrorThirteenthOutOfRange_Day = "Worked days should be between 1 and 31!",
                    MsgErrorThirteenthOutOfRange_Month = "Worked months should be between 1 and 12!"
                },
                new LanguageThrowHelper(11273,"EN-TT") /* Trinidad */ {
                    MsgErrorNameBrazilianState = "Set an correctly Brazilian state or Brazilian IBGE state code!",
                    MsgErrorStateRegistration = "Set an correctly Brazilian state or Brazilian IBGE state code! Don't use ZZ or 99!",
                    MsgErrorThirteenthOutOfRange_Day = "Worked days should be between 1 and 31!",
                    MsgErrorThirteenthOutOfRange_Month = "Worked months should be between 1 and 12!"
                },
            };
            
            LanguageThrowHelper defaultLang = LangResourceForThrowHelper
                .Where(data => data.lpCountryAndLang.Substring(0,2) == CultureInfo.CurrentCulture.Name.Substring(0,2).ToUpper())
                .First();
            
            if(defaultLang == null)
                defaultLang = LangResourceForThrowHelper
                    .Where(data => data.dwRegion == 1033)
                    .First();
            
            LanguageThrowHelper systemDefaultLang = LangResourceForThrowHelper
                .Where(data => String.Equals(
                        data.lpCountryAndLang, 
                        CultureInfo.CurrentCulture.Name, 
                        StringComparison.OrdinalIgnoreCase
                    )
                )
                .DefaultIfEmpty(defaultLang)
                .FirstOrDefault();
            
            return systemDefaultLang;
        }

        private class TupleForGetTranslateOfState
        {
            internal TupleForGetTranslateOfState(int stt, String lang, String translate)
            {
                this.Stt = stt;
                this.Lang = lang;
                this.Translate = Translate;
            }
            internal int Stt { get; set; }
            internal String Lang { get; set; }
            internal String Translate { get; set; }
        }

        internal static string GetTranslateOfState(BrazilianStates state)
        {
            // Uses only for ES, MG, MS, MT, RJ, RN, RS, SC and DF. That then are translatable
            TupleForGetTranslateOfState[] FederalDistrictManyLangs = 
            {
                // TODO: Add Supports for other languages.
                /* Português */
                new TupleForGetTranslateOfState(32, "PT", "Espírito Santo"),
                new TupleForGetTranslateOfState(31, "PT", "Minas Gerais"),
                new TupleForGetTranslateOfState(50, "PT", "Mato Grosso do Sul"),
                new TupleForGetTranslateOfState(51, "PT", "Mato Grosso"),
                new TupleForGetTranslateOfState(33, "PT", "Rio de Janeiro"),
                new TupleForGetTranslateOfState(24, "PT", "Rio Grande do Norte"),
                new TupleForGetTranslateOfState(43, "PT", "Rio Grande do Sul"),
                new TupleForGetTranslateOfState(42, "PT", "Santa Catarina"),
                new TupleForGetTranslateOfState(35, "PT", "São Paulo"),
                new TupleForGetTranslateOfState(53, "PT", "Distrito Federal"),
                /* English */
                new TupleForGetTranslateOfState(32, "EN", "Holy Spirit"),
                new TupleForGetTranslateOfState(31, "EN", "General Mines"),
                new TupleForGetTranslateOfState(50, "EN", "Thick Bushes of the South"),
                new TupleForGetTranslateOfState(51, "EN", "Thick Bushes"),
                new TupleForGetTranslateOfState(33, "EN", "River of January"),
                new TupleForGetTranslateOfState(24, "EN", "Great Northern River"),
                new TupleForGetTranslateOfState(43, "EN", "Great Southern River"),
                new TupleForGetTranslateOfState(42, "EN", "Saint Catherine"),
                new TupleForGetTranslateOfState(35, "EN", "Saint Paul"),
                new TupleForGetTranslateOfState(53, "EN", "Federal District"),
                /* Español */
                new TupleForGetTranslateOfState(32, "ES", "Espíritu Santo"),
                new TupleForGetTranslateOfState(31, "ES", "Minas Generales"),
                new TupleForGetTranslateOfState(50, "ES", "Arbustos Espesos del Sur"),
                new TupleForGetTranslateOfState(51, "ES", "Arbustos Espesos"),
                new TupleForGetTranslateOfState(33, "ES", "Río de Enero"),
                new TupleForGetTranslateOfState(24, "ES", "Gran Río del Norte"),
                new TupleForGetTranslateOfState(43, "ES", "Gran Río del Sul"),
                new TupleForGetTranslateOfState(42, "ES", "Santa Catalina"),
                new TupleForGetTranslateOfState(35, "ES", "San Pablo"),
                new TupleForGetTranslateOfState(53, "ES", "Distrito Federal")
            };
            return FederalDistrictManyLangs
                .Where(
                    data =>
                    data.Lang == 
                        CultureInfo
                        .CurrentCulture
                        .Name
                        .Substring(0, 2)
                        .ToUpper() &&
                    data.Stt == (int)state
                )
                .DefaultIfEmpty(
                    FederalDistrictManyLangs
                    .Where(data => data.Stt == (int)state)
                    .First()
                )
                .FirstOrDefault()
                .Translate;
        }
    }
}