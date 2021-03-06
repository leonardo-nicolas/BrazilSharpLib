using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BrazilSharp
{
    public static partial class Validate
    {
        private static class _InternalMethodsValidateIE
        {
            /*
             * Instructions in http://www.sintegra.gov.br/insc_est.html 
             * Instructions is in Portuguese.
             *
             * Cities code got in https://www.ibge.gov.br/explica/codigos-dos-municipios.php
             * Cities code page is in Portuguese.
             */

            public static bool ValidateIE_AC(string IE)
            {
                // Acre
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 13)
                    return false;
                while (IE.Length < 13)
                    IE = IE.Insert(0, "0");
                int summation;
                summation = 0;
                for (int index = 10, weight = 2; index >= 0; --index, weight = weight < 9 ? weight + 1 : 2)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int remainder = summation % 11;
                int[] CheckingDigit = new int[2];
                CheckingDigit[0] = (11 - remainder >= 10) ? 0 : 11 - remainder;
                summation = 0;
                for (int index = 11, weight = 2; index >= 0; --index, weight = weight < 9 ? weight + 1 : 2)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                remainder = summation % 11;
                CheckingDigit[1] = (11 - remainder >= 10) ? 0 : 11 - remainder;
                return CheckingDigit[0] >= 0 ? CheckingDigit[0] == Convert.ToInt32(IE.Substring(11, 1)) && CheckingDigit[1] == Convert.ToInt32(IE.Substring(12, 1)) : false;
            }

            public static bool ValidateIE_AL(string IE)
            {
                //Alagoas
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length != 9)
                    return false;
                if (IE.Substring(0, 2) != "24")
                    return false;
                if (!(new char[] { '0', '3', '5', '7', '8' }).Contains(IE[2]))
                    return false;
                int summation = 0;
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation * 10;
                CheckingDigit = CheckingDigit - (Convert.ToInt32(CheckingDigit / 11D) * 11);
                return CheckingDigit == Convert.ToInt32(IE.Substring(8, 1));
            }

            public static bool ValidateIE_AP(string IE)
            {
                //Amap??
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length != 9)
                    return false;
                if (IE.Substring(0, 2) != "03")
                    return false;
                int d, summation;
                if (Convert.ToInt32(IE) >= 3000001 && Convert.ToInt32(IE) <= 3017000)
                {
                    summation = 5;
                    d = 0;
                }
                else if (Convert.ToInt32(IE) >= 03017001 && Convert.ToInt32(IE) <= 03019022)
                {
                    summation = 9;
                    d = 1;
                }
                else
                {
                    summation = 0;
                    d = 0;
                }
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = 11 - CheckingDigit;
                return !(CheckingDigit != Convert.ToInt32(IE.Substring(8, 1)) || (CheckingDigit == 10 && IE[8] != '0') || (CheckingDigit == 11 && Convert.ToInt32(IE.Substring(8, 1)) != d));
            }

            public static bool ValidateIE_AM(string IE)
            {
                //Amazonas
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 9)
                    return false;
                while (IE.Length < 9)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                if (summation < 11)
                    return Convert.ToInt32(IE.Substring(8, 1)) == (11 - summation);
                else
                {
                    int CheckingDigit = summation % 11;
                    CheckingDigit = CheckingDigit >= 2 ? 11 - CheckingDigit : 0;
                    return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit;
                }
            }

            public static bool ValidateIE_BA(string IE)
            {
                //Bahia
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                int summation, divider, weight, current;
                int[] CheckingDigit = new int[2], GotDigits = { Convert.ToInt32(IE.Substring(IE.Length - 2, 1)), Convert.ToInt32(IE.Substring(IE.Length - 1, 1)) };                
                if(!(new[]{ 8, 9 }).Contains(IE.Length))
                    return false;
                int SecondDigit = Convert.ToInt32(IE.Substring(1, 1));
                if((new[]{0,1,2,3,4,5,8}).Contains(SecondDigit))
                    divider = 10;
                else if ((new[]{6,7,9}).Contains(SecondDigit))
                    divider = 11;
                else // Useless else context, but necessary!
                    divider = 1;

                //Second Digit
                summation = 0;
                weight = IE.Length-1;
                for (int index = 0; index < IE.Length - 2; ++index)
                {
                    current = Convert.ToInt32(IE.Substring(index, 1));
                    summation += weight * current;
                    --weight;
                }
                CheckingDigit[1] = summation % divider;
                CheckingDigit[1] = CheckingDigit[1] == 0 ? 0 : divider - CheckingDigit[1];
                
                //First Digit
                summation = 0;
                weight = IE.Length;
                for (int index = 0; index < IE.Length-2; ++index)
                {
                    current = Convert.ToInt32(IE.Substring(index, 1));
                    summation += weight * current;
                    --weight;
                }
                current = Convert.ToInt32(IE.Substring(IE.Length-1, 1));
                summation += weight * current;
                CheckingDigit[0] = summation % divider;
                CheckingDigit[0] = CheckingDigit[0] == 0 ? 0 : divider - CheckingDigit[0];

                bool result = GotDigits.SequenceEqual(CheckingDigit);
                
                return result;
            }

            public static bool ValidateIE_CE(string IE)
            {
                // Cear??
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 9)
                    return false;
                while (IE.Length < 9)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = 11 - CheckingDigit;
                CheckingDigit = CheckingDigit > 9 ? 0 : CheckingDigit;
                return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_DF(string IE)
            {
                // Federal District / Distrito Federal
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 13)
                    return false;
                while (IE.Length < 13)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 10, weight = 2; index >= 0; --index, weight = weight < 9 ? weight + 1 : 2)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int[] CheckingDigit = new int[2];
                CheckingDigit[0] = summation % 11;
                CheckingDigit[0] = (11 - CheckingDigit[0]) >= 10 ? 0 : 11 - CheckingDigit[0];
                summation = 0;
                for (int index = 11, weight = 2; index >= 0; --index, weight = weight < 9 ? weight + 1 : 2)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                CheckingDigit[1] = summation % 11;
                CheckingDigit[1] = (11 - CheckingDigit[1]) >= 10 ? 0 : 11 - CheckingDigit[1];
                return CheckingDigit[0] == Convert.ToInt32(IE.Substring(11, 1)) && CheckingDigit[1] == Convert.ToInt32(IE.Substring(12, 1));
            }

            public static bool ValidateIE_ES(string IE)
            {
                //Esp??rito Santo
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 9)
                    return false;
                while (IE.Length < 9)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = 11 - CheckingDigit < 2 ? 0 : 11 - CheckingDigit;
                return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_GO(string IE)
            {
                //Goi??s
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if(IE.Length != 9)
                    return false;
                if (!(new[] { "10", "11", "15" }).Contains(IE.Substring(0, 2)))
                    if (IE.Substring(0, 8) == "11094402")
                        return (new[] { "0", "1" }).Contains(IE.Substring(8, 1));
                int summation = 0;
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                
                if (CheckingDigit == 0 || (CheckingDigit == 1 && !(Convert.ToInt32(IE.Substring(0, 8)) >= 10103105 && Convert.ToInt32(IE.Substring(0, 8)) <= 10119997)))
                    CheckingDigit = 0;
                else if (CheckingDigit == 1 && (Convert.ToInt32(IE.Substring(0, 8)) >= 10103105 && Convert.ToInt32(IE.Substring(0, 8)) <= 10119997))
                    CheckingDigit = 1;
                else
                    CheckingDigit = 11 - CheckingDigit;
                
                return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_MA(string IE)
            {
                //Maranh??o
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length != 9 || IE.Substring(0, 2) != "12")
                    return false;
                int summation = 0;
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = (new[] { 0, 1 }).Contains(CheckingDigit) ? 0 : 11 - CheckingDigit;
                return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_MT(string IE)
            {
                //Mato Grosso
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 11)
                    return false;
                while (IE.Length < 11)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 9, weight = 2; index >= 0; --index, weight = weight < 9 ? weight + 1 : 2)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = CheckingDigit == 0 || CheckingDigit == 1 ? 0 : 11 - CheckingDigit;
                return Convert.ToInt32(IE.Substring(10, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_MS(string IE)
            {
                //Mato Grosso do Sul
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length != 9 || IE.Substring(0, 2) != "28")
                    return false;
                int summation = 0;
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                if (CheckingDigit == 0)
                    CheckingDigit = 0;
                else if (CheckingDigit < 0)
                    return false;
                else
                    CheckingDigit = (11 - CheckingDigit > 9) ? 0 : 11 - CheckingDigit;
                return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_MG(string IE)
            {
                //Minas Gerais
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 13 || !IsCityValid(Convert.ToInt32(IE.Substring(0, 3)), "31"))
                    return false;
                while (IE.Length < 13)
                    IE = IE.Insert(0, "0");
                IE = IE.Insert(3, "0");
                bool controller = true;
                string strCalculated = string.Empty;
                for (int indexA = 0; indexA < IE.Length - 2; ++indexA, controller = !controller)
                    strCalculated += Convert.ToString((controller ? 1 : 2) * Convert.ToInt32(IE.Substring(indexA, 1)));
                int summation = 0;
                for (int indexB = 0; indexB < strCalculated.Length; ++indexB)
                    summation += Convert.ToInt32(strCalculated.Substring(indexB, 1));
                int[] CheckingDigit = new int[2];
                for (int weightX = 0; weightX < 100; weightX += 10)
                    if (summation >= weightX && summation < (weightX + 10))
                    {
                        CheckingDigit[0] = weightX + 10 - summation;
                        break;
                    }
                IE = IE.Remove(3, 1);
                summation = 0;
                for (int indexC = 11, weight = 2; indexC >= 0; indexC--, weight = weight < 11 ? weight + 1 : 2)
                    summation += Convert.ToInt32(IE.Substring(indexC, 1)) * weight;
                CheckingDigit[1] = summation % 11;
                CheckingDigit[1] = CheckingDigit[1] < 2 ? 0 : 11 - CheckingDigit[1];
                return CheckingDigit[0] == Convert.ToInt32(IE.Substring(11, 1)) && CheckingDigit[1] == Convert.ToInt32(IE.Substring(12, 1));
            }

            public static bool ValidateIE_PA(string IE)
            {
                //Par??
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length != 9 || !IE.StartsWith("15"))
                    return false;
                int summation = 0;
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = CheckingDigit < 2 ? 0 : 11 - CheckingDigit;
                return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_PB(string IE)
            {
                //Para??ba
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 9)
                    return false;
                while (IE.Length < 9)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = 11 - CheckingDigit > 9 ? 0 : 11 - CheckingDigit;
                return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_PR(string IE)
            {
                //Paran??
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 10)
                    return false;
                while (IE.Length < 10)
                    IE = IE.Insert(0, "0");
                int summation;
                int[] CheckingDigit = new int[2], gotDigit = { Convert.ToInt32(IE.Substring(8, 1)), Convert.ToInt32(IE.Substring(9, 1)) };
                summation = 0;
                for (int index = 7, weight = 2; index >= 0; --index, weight = weight < 7 ? weight + 1 : 2)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                CheckingDigit[0] = summation % 11;
                CheckingDigit[0] = 11 - CheckingDigit[0];
                CheckingDigit[0] = CheckingDigit[0] > 9 ? 0 : CheckingDigit[0];
                summation = 0;
                for (int index = 8, weight = 2; index >= 0; --index, weight = weight < 7 ? weight + 1 : 2)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                CheckingDigit[1] = summation % 11;
                CheckingDigit[1] = 11 - CheckingDigit[1];
                CheckingDigit[1] = CheckingDigit[1] > 9 ? 0 : CheckingDigit[1];
                return CheckingDigit.SequenceEqual(gotDigit);
            }

            public static bool ValidateIE_PE(string IE)
            {
                //Pernambuco                    
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                bool result;
                int summation;
                int[] CheckingDigit;
                if (IE.Length <= 9) // e-Fisco
                { 
                    while (IE.Length < 9)
                        IE = IE.Insert(0, "0");
                    summation = 0;
                    for (int index = 0, weight = 8; index < 7; ++index, --weight)
                        summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                    CheckingDigit = new int[2];
                    CheckingDigit[0] = summation % 11;
                    CheckingDigit[0] = CheckingDigit[0] < 2 ? 0 : 11 - CheckingDigit[0];
                    summation = 0;
                    for (int index = 0, weight = 9; index < 8; ++index, --weight)
                        summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                    CheckingDigit[1] = summation % 11;
                    CheckingDigit[1] = CheckingDigit[1] < 2 ? 0 : 11 - CheckingDigit[1];
                    result = CheckingDigit[0] == Convert.ToInt32(IE.Substring(7, 1)) && CheckingDigit[1] == Convert.ToInt32(IE.Substring(8, 1));
                }
                else if (IE.Length > 9 && IE.Length <= 14) //Older
                { 
                    while (IE.Length < 14)
                        IE = IE.Insert(0, "0");
                    summation = 0;
                    for (int index = 12, weight = 2; index >= 0; --index, weight = weight < 9 ? weight + 1 : 1)
                        summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                    CheckingDigit = new int[1];
                    CheckingDigit[0] = summation % 11;
                    CheckingDigit[0] = 11 - CheckingDigit[0] > 9 ? 11 - CheckingDigit[0] - 10 : 11 - CheckingDigit[0];
                    result = Convert.ToInt32(IE.Substring(13, 1)) == CheckingDigit[0];
                }
                else
                    result = false;
                return result;
            }

            public static bool ValidateIE_PI(string IE)
            {
                //Piau??
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 9)
                    return false;
                while (IE.Length < 9)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = 11 - CheckingDigit > 9 ? 0 : 11 - CheckingDigit;
                return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_RJ(string IE)
            {
                // Rio de Janeiro
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 8)
                    return false;
                while (IE.Length < 8)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 6, weight = 2; index >= 0; --index, weight = weight < 7 ? weight + 1 : 2)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = CheckingDigit > 1 ? 11 - CheckingDigit : 0;
                bool result = Convert.ToInt32(IE.Substring(7, 1)) == CheckingDigit;
                return result;
            }

            public static bool ValidateIE_RN(string IE)
            {
                //Rio Grande do Norte
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                bool result;
                if (IE.StartsWith("20") && (new[] { 9, 10 }).Contains(IE.Length))
                {
                    int summation = 0;
                    for (int index = IE.Length - 2, weight = 2; index >= 0; --index, ++weight)
                        summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                    int CheckingDigit = summation * 10;
                    CheckingDigit = CheckingDigit % 11;
                    CheckingDigit = CheckingDigit > 9 ? 0 : CheckingDigit;
                    result = Convert.ToInt32(IE.Substring(IE.Length - 1, 1)) == CheckingDigit;
                }
                else
                    result = false;
                return result;
            }

            public static bool ValidateIE_RS(string IE)
            {
                //Rio Grande do Sul
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 10)
                    return false;
                while (IE.Length < 10)
                    IE = IE.Insert(0, "0");
                if (!IsCityValid(Convert.ToInt32(IE.Substring(0, 3)), "43"))
                    return false;
                int summation = 0;
                for (int index = 8, weight = 2; index >= 0; --index, weight = weight < 9 ? weight + 1 : 2)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = 11 - CheckingDigit;
                CheckingDigit = CheckingDigit > 9 ? 0 : CheckingDigit;
                return Convert.ToInt32(IE.Substring(9, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_RO(string IE)
            {
                //Rond??nia
                IE = Regex.Replace(IE, @"[^0-9]+", String.Empty);
                if (IE.Length == 0 || IE.Length > 14)
                    return false;
                while (IE.Length < 14)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 12, weight = 2; index >= 0; --index, weight = weight < 9 ? weight + 1 : 2)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = 11 - CheckingDigit;
                CheckingDigit = CheckingDigit > 9 ? 10 - CheckingDigit : CheckingDigit;
                return Convert.ToInt32(IE.Substring(13, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_SC(string IE)
            {
                //Santa Catarina
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 9)
                    return false;
                while (IE.Length < 9)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 7, weight = 2; index >= 0; --index, ++weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = 11 - CheckingDigit;
                CheckingDigit = CheckingDigit < 2 ? 0 : CheckingDigit;
                return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_SP(string IE)
            {
                //S??o Paulo
                bool hasP = IE.StartsWith("P");
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 13)
                    return false;
                while (IE.Length < 12)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 7, weight = 10; index >= 0; --index)
                {
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                    if (index == 1)
                        weight = 1;
                    else if (index == 7)
                        weight = 8;
                    else
                        --weight;
                }
                int remainder = summation % 11;
                string strRem = remainder.ToString();
                int[] CheckingDigit = new int[(hasP ? 1 : 2)];
                CheckingDigit[0] = Convert.ToInt32(strRem.Substring(strRem.Length - 1, 1));
                if (hasP) //If the state registration in S??o Paulo begins with "P", it's because it belongs to rural producers and doesn't have a second checking digit.
                    return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit[0];//For rural producers, ends here!
                
                //For industry and commerce, there is still the validation of the second Checking Digit, which is the routine below:
                summation = 0;
                for (int index = 10, weight = 2; index >= 0; --index, weight = weight < 10 ? weight + 1 : 2)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                remainder = summation % 11;
                strRem = remainder.ToString();
                CheckingDigit[1] = Convert.ToInt32(strRem.Substring(strRem.Length - 1, 1));
                return Convert.ToInt32(IE.Substring(8, 1)) == CheckingDigit[0] && Convert.ToInt32(IE.Substring(11, 1)) == CheckingDigit[1];
            }

            public static bool ValidateIE_SE(string IE)
            {
                //Sergipe
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length == 0 || IE.Length > 9)
                    return false;
                while (IE.Length < 9)
                    IE = IE.Insert(0, "0");
                int summation = 0;
                for (int index = 0, weight = 9; index < 8; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = 11 - CheckingDigit > 9 ? 0 : 11 - CheckingDigit;
                return Convert.ToInt32(IE.Substring(8, 1)).Equals(CheckingDigit);
            }

            public static bool ValidateIE_TO(string IE)
            {
                //Tocantins
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if (IE.Length != 11)
                    return false;
                if (!(new[] { "01", "02", "03", "99" }).Contains(IE.Substring(2, 2)))
                    return false;
                int summation = 0;
                for (int index = 0, weight = 9; index < 2; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                for (int index = 4, weight = 7; index < 10; ++index, --weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 11;
                CheckingDigit = CheckingDigit < 2 ? 0 : 11 - CheckingDigit;
                return Convert.ToInt32(IE.Substring(10, 1)) == CheckingDigit;
            }

            public static bool ValidateIE_RR(string IE) 
            {
                IE = Regex.Replace(IE, @"[^0-9]+", string.Empty);
                if(IE.Length != 9 || IE.Substring(0, 2) != "24")
                    return false;
                int summation = 0;
                for (int index = 0, weight = 1; index < 8; ++index, ++weight)
                    summation += weight * Convert.ToInt32(IE.Substring(index, 1));
                int CheckingDigit = summation % 9;
                int digit = Convert.ToInt32(IE.Substring(8, 1));
                return digit == CheckingDigit;
            }

            private static bool IsCityValid(int cityCode, string stateCode)
            {
                Dictionary<int, string> Cities = new Dictionary<int, string>() 
                {   
                    // For more details, see more on https://ibge.gov.br

                    /* Minas Gerais */
                    //Code  ,   City Name
                    {3100104,"Abadia dos Dourados"},
                    {3100203,"Abaet??"},
                    {3100302,"Abre Campo"},
                    {3100401,"Acaiaca"},
                    {3100500,"A??ucena"},
                    {3100609,"??gua Boa"},
                    {3100708,"??gua Comprida"},
                    {3100807,"Aguanil"},
                    {3100906,"??guas Formosas"},
                    {3101003,"??guas Vermelhas"},
                    {3101102,"Aimor??s"},
                    {3101201,"Aiuruoca"},
                    {3101300,"Alagoa"},
                    {3101409,"Albertina"},
                    {3101508,"Al??m Para??ba"},
                    {3101607,"Alfenas"},
                    {3101631,"Alfredo Vasconcelos"},
                    {3101706,"Almenara"},
                    {3101805,"Alpercata"},
                    {3101904,"Alpin??polis"},
                    {3102001,"Alterosa"},
                    {3102050,"Alto Capara??"},
                    {3153509,"Alto Jequitib??"},
                    {3102100,"Alto Rio Doce"},
                    {3102209,"Alvarenga"},
                    {3102308,"Alvin??polis"},
                    {3102407,"Alvorada de Minas"},
                    {3102506,"Amparo do Serra"},
                    {3102605,"Andradas"},
                    {3102803,"Andrel??ndia"},
                    {3102852,"Angel??ndia"},
                    {3102902,"Ant??nio Carlos"},
                    {3103009,"Ant??nio Dias"},
                    {3103108,"Ant??nio Prado de Minas"},
                    {3103207,"Ara??a??"},
                    {3103306,"Aracitaba"},
                    {3103405,"Ara??ua??"},
                    {3103504,"Araguari"},
                    {3103603,"Arantina"},
                    {3103702,"Araponga"},
                    {3103751,"Arapor??"},
                    {3103801,"Arapu??"},
                    {3103900,"Ara??jos"},
                    {3104007,"Arax??"},
                    {3104106,"Arceburgo"},
                    {3104205,"Arcos"},
                    {3104304,"Areado"},
                    {3104403,"Argirita"},
                    {3104452,"Aricanduva"},
                    {3104502,"Arinos"},
                    {3104601,"Astolfo Dutra"},
                    {3104700,"Atal??ia"},
                    {3104809,"Augusto de Lima"},
                    {3104908,"Baependi"},
                    {3105004,"Baldim"},
                    {3105103,"Bambu??"},
                    {3105202,"Bandeira"},
                    {3105301,"Bandeira do Sul"},
                    {3105400,"Bar??o de Cocais"},
                    {3105509,"Bar??o de Monte Alto"},
                    {3105608,"Barbacena"},
                    {3105707,"Barra Longa"},
                    {3105905,"Barroso"},
                    {3106002,"Bela Vista de Minas"},
                    {3106101,"Belmiro Braga"},
                    {3106200,"Belo Horizonte"},
                    {3106309,"Belo Oriente"},
                    {3106408,"Belo Vale"},
                    {3106507,"Berilo"},
                    {3106655,"Berizal"},
                    {3106606,"Bert??polis"},
                    {3106705,"Betim"},
                    {3106804,"Bias Fortes"},
                    {3106903,"Bicas"},
                    {3107000,"Biquinhas"},
                    {3107109,"Boa Esperan??a"},
                    {3107208,"Bocaina de Minas"},
                    {3107307,"Bocai??va"},
                    {3107406,"Bom Despacho"},
                    {3107505,"Bom Jardim de Minas"},
                    {3107604,"Bom Jesus da Penha"},
                    {3107703,"Bom Jesus do Amparo"},
                    {3107802,"Bom Jesus do Galho"},
                    {3107901,"Bom Repouso"},
                    {3108008,"Bom Sucesso"},
                    {3108107,"Bonfim"},
                    {3108206,"Bonfin??polis de Minas"},
                    {3108255,"Bonito de Minas"},
                    {3108305,"Borda da Mata"},
                    {3108404,"Botelhos"},
                    {3108503,"Botumirim"},
                    {3108701,"Br??s Pires"},
                    {3108552,"Brasil??ndia de Minas"},
                    {3108602,"Bras??lia de Minas"},
                    {3108800,"Bra??nas"},
                    {3108909,"Braz??polis"},
                    {3109006,"Brumadinho"},
                    {3109105,"Bueno Brand??o"},
                    {3109204,"Buen??polis"},
                    {3109253,"Bugre"},
                    {3109303,"Buritis"},
                    {3109402,"Buritizeiro"},
                    {3109451,"Cabeceira Grande"},
                    {3109501,"Cabo Verde"},
                    {3109600,"Cachoeira da Prata"},
                    {3109709,"Cachoeira de Minas"},
                    {3102704,"Cachoeira de Paje??"},
                    {3109808,"Cachoeira Dourada"},
                    {3109907,"Caetan??polis"},
                    {3110004,"Caet??"},
                    {3110103,"Caiana"},
                    {3110202,"Cajuri"},
                    {3110301,"Caldas"},
                    {3110400,"Camacho"},
                    {3110509,"Camanducaia"},
                    {3110608,"Cambu??"},
                    {3110707,"Cambuquira"},
                    {3110806,"Campan??rio"},
                    {3110905,"Campanha"},
                    {3111002,"Campestre"},
                    {3111101,"Campina Verde"},
                    {3111150,"Campo Azul"},
                    {3111200,"Campo Belo"},
                    {3111309,"Campo do Meio"},
                    {3111408,"Campo Florido"},
                    {3111507,"Campos Altos"},
                    {3111606,"Campos Gerais"},
                    {3111903,"Cana Verde"},
                    {3111705,"Cana??"},
                    {3111804,"Can??polis"},
                    {3112000,"Candeias"},
                    {3112059,"Cantagalo"},
                    {3112109,"Capara??"},
                    {3112208,"Capela Nova"},
                    {3112307,"Capelinha"},
                    {3112406,"Capetinga"},
                    {3112505,"Capim Branco"},
                    {3112604,"Capin??polis"},
                    {3112653,"Capit??o Andrade"},
                    {3112703,"Capit??o En??as"},
                    {3112802,"Capit??lio"},
                    {3112901,"Caputira"},
                    {3113008,"Cara??"},
                    {3113107,"Carana??ba"},
                    {3113206,"Caranda??"},
                    {3113305,"Carangola"},
                    {3113404,"Caratinga"},
                    {3113503,"Carbonita"},
                    {3113602,"Carea??u"},
                    {3113701,"Carlos Chagas"},
                    {3113800,"Carm??sia"},
                    {3113909,"Carmo da Cachoeira"},
                    {3114006,"Carmo da Mata"},
                    {3114105,"Carmo de Minas"},
                    {3114204,"Carmo do Cajuru"},
                    {3114303,"Carmo do Parana??ba"},
                    {3114402,"Carmo do Rio Claro"},
                    {3114501,"Carm??polis de Minas"},
                    {3114550,"Carneirinho"},
                    {3114600,"Carrancas"},
                    {3114709,"Carvalh??polis"},
                    {3114808,"Carvalhos"},
                    {3114907,"Casa Grande"},
                    {3115003,"Cascalho Rico"},
                    {3115102,"C??ssia"},
                    {3115300,"Cataguases"},
                    {3115359,"Catas Altas"},
                    {3115409,"Catas Altas da Noruega"},
                    {3115458,"Catuji"},
                    {3115474,"Catuti"},
                    {3115508,"Caxambu"},
                    {3115607,"Cedro do Abaet??"},
                    {3115706,"Central de Minas"},
                    {3115805,"Centralina"},
                    {3115904,"Ch??cara"},
                    {3116001,"Chal??"},
                    {3116100,"Chapada do Norte"},
                    {3116159,"Chapada Ga??cha"},
                    {3116209,"Chiador"},
                    {3116308,"Cipot??nea"},
                    {3116407,"Claraval"},
                    {3116506,"Claro dos Po????es"},
                    {3116605,"Cl??udio"},
                    {3116704,"Coimbra"},
                    {3116803,"Coluna"},
                    {3116902,"Comendador Gomes"},
                    {3117009,"Comercinho"},
                    {3117108,"Concei????o da Aparecida"},
                    {3115201,"Concei????o da Barra de Minas"},
                    {3117306,"Concei????o das Alagoas"},
                    {3117207,"Concei????o das Pedras"},
                    {3117405,"Concei????o de Ipanema"},
                    {3117504,"Concei????o do Mato Dentro"},
                    {3117603,"Concei????o do Par??"},
                    {3117702,"Concei????o do Rio Verde"},
                    {3117801,"Concei????o dos Ouros"},
                    {3117836,"C??nego Marinho"},
                    {3117876,"Confins"},
                    {3117900,"Congonhal"},
                    {3118007,"Congonhas"},
                    {3118106,"Congonhas do Norte"},
                    {3118205,"Conquista"},
                    {3118304,"Conselheiro Lafaiete"},
                    {3118403,"Conselheiro Pena"},
                    {3118502,"Consola????o"},
                    {3118601,"Contagem"},
                    {3118700,"Coqueiral"},
                    {3118809,"Cora????o de Jesus"},
                    {3118908,"Cordisburgo"},
                    {3119005,"Cordisl??ndia"},
                    {3119104,"Corinto"},
                    {3119203,"Coroaci"},
                    {3119302,"Coromandel"},
                    {3119401,"Coronel Fabriciano"},
                    {3119500,"Coronel Murta"},
                    {3119609,"Coronel Pacheco"},
                    {3119708,"Coronel Xavier Chaves"},
                    {3119807,"C??rrego Danta"},
                    {3119906,"C??rrego do Bom Jesus"},
                    {3119955,"C??rrego Fundo"},
                    {3120003,"C??rrego Novo"},
                    {3120102,"Couto de Magalh??es de Minas"},
                    {3120151,"Cris??lita"},
                    {3120201,"Cristais"},
                    {3120300,"Crist??lia"},
                    {3120409,"Cristiano Otoni"},
                    {3120508,"Cristina"},
                    {3120607,"Crucil??ndia"},
                    {3120706,"Cruzeiro da Fortaleza"},
                    {3120805,"Cruz??lia"},
                    {3120839,"Cuparaque"},
                    {3120870,"Curral de Dentro"},
                    {3120904,"Curvelo"},
                    {3121001,"Datas"},
                    {3121100,"Delfim Moreira"},
                    {3121209,"Delfin??polis"},
                    {3121258,"Delta"},
                    {3121308,"Descoberto"},
                    {3121407,"Desterro de Entre Rios"},
                    {3121506,"Desterro do Melo"},
                    {3121605,"Diamantina"},
                    {3121704,"Diogo de Vasconcelos"},
                    {3121803,"Dion??sio"},
                    {3121902,"Divin??sia"},
                    {3122009,"Divino"},
                    {3122108,"Divino das Laranjeiras"},
                    {3122207,"Divinol??ndia de Minas"},
                    {3122306,"Divin??polis"},
                    {3122355,"Divisa Alegre"},
                    {3122405,"Divisa Nova"},
                    {3122454,"Divis??polis"},
                    {3122470,"Dom Bosco"},
                    {3122504,"Dom Cavati"},
                    {3122603,"Dom Joaquim"},
                    {3122702,"Dom Silv??rio"},
                    {3122801,"Dom Vi??oso"},
                    {3122900,"Dona Euz??bia"},
                    {3123007,"Dores de Campos"},
                    {3123106,"Dores de Guanh??es"},
                    {3123205,"Dores do Indai??"},
                    {3123304,"Dores do Turvo"},
                    {3123403,"Dores??polis"},
                    {3123502,"Douradoquara"},
                    {3123528,"Durand??"},
                    {3123601,"El??i Mendes"},
                    {3123700,"Engenheiro Caldas"},
                    {3123809,"Engenheiro Navarro"},
                    {3123858,"Entre Folhas"},
                    {3123908,"Entre Rios de Minas"},
                    {3124005,"Erv??lia"},
                    {3124104,"Esmeraldas"},
                    {3124203,"Espera Feliz"},
                    {3124302,"Espinosa"},
                    {3124401,"Esp??rito Santo do Dourado"},
                    {3124500,"Estiva"},
                    {3124609,"Estrela Dalva"},
                    {3124708,"Estrela do Indai??"},
                    {3124807,"Estrela do Sul"},
                    {3124906,"Eugen??polis"},
                    {3125002,"Ewbank da C??mara"},
                    {3125101,"Extrema"},
                    {3125200,"Fama"},
                    {3125309,"Faria Lemos"},
                    {3125408,"Fel??cio dos Santos"},
                    {3125606,"Felisburgo"},
                    {3125705,"Felixl??ndia"},
                    {3125804,"Fernandes Tourinho"},
                    {3125903,"Ferros"},
                    {3125952,"Fervedouro"},
                    {3126000,"Florestal"},
                    {3126109,"Formiga"},
                    {3126208,"Formoso"},
                    {3126307,"Fortaleza de Minas"},
                    {3126406,"Fortuna de Minas"},
                    {3126505,"Francisco Badar??"},
                    {3126604,"Francisco Dumont"},
                    {3126703,"Francisco S??"},
                    {3126752,"Francisc??polis"},
                    {3126802,"Frei Gaspar"},
                    {3126901,"Frei Inoc??ncio"},
                    {3126950,"Frei Lagonegro"},
                    {3127008,"Fronteira"},
                    {3127057,"Fronteira dos Vales"},
                    {3127073,"Fruta de Leite"},
                    {3127107,"Frutal"},
                    {3127206,"Funil??ndia"},
                    {3127305,"Galil??ia"},
                    {3127339,"Gameleiras"},
                    {3127354,"Glaucil??ndia"},
                    {3127370,"Goiabeira"},
                    {3127388,"Goian??"},
                    {3127404,"Gon??alves"},
                    {3127503,"Gonzaga"},
                    {3127602,"Gouveia"},
                    {3127701,"Governador Valadares"},
                    {3127800,"Gr??o Mogol"},
                    {3127909,"Grupiara"},
                    {3128006,"Guanh??es"},
                    {3128105,"Guap??"},
                    {3128204,"Guaraciaba"},
                    {3128253,"Guaraciama"},
                    {3128303,"Guaran??sia"},
                    {3128402,"Guarani"},
                    {3128501,"Guarar??"},
                    {3128600,"Guarda-Mor"},
                    {3128709,"Guaxup??"},
                    {3128808,"Guidoval"},
                    {3128907,"Guimar??nia"},
                    {3129004,"Guiricema"},
                    {3129103,"Gurinhat??"},
                    {3129202,"Heliodora"},
                    {3129301,"Iapu"},
                    {3129400,"Ibertioga"},
                    {3129509,"Ibi??"},
                    {3129608,"Ibia??"},
                    {3129657,"Ibiracatu"},
                    {3129707,"Ibiraci"},
                    {3129806,"Ibirit??"},
                    {3129905,"Ibiti??ra de Minas"},
                    {3130002,"Ibituruna"},
                    {3130051,"Icara?? de Minas"},
                    {3130101,"Igarap??"},
                    {3130200,"Igaratinga"},
                    {3130309,"Iguatama"},
                    {3130408,"Ijaci"},
                    {3130507,"Ilic??nea"},
                    {3130556,"Imb?? de Minas"},
                    {3130606,"Inconfidentes"},
                    {3130655,"Indaiabira"},
                    {3130705,"Indian??polis"},
                    {3130804,"Inga??"},
                    {3130903,"Inhapim"},
                    {3131000,"Inha??ma"},
                    {3131109,"Inimutaba"},
                    {3131158,"Ipaba"},
                    {3131208,"Ipanema"},
                    {3131307,"Ipatinga"},
                    {3131406,"Ipia??u"},
                    {3131505,"Ipui??na"},
                    {3131604,"Ira?? de Minas"},
                    {3131703,"Itabira"},
                    {3131802,"Itabirinha"},
                    {3131901,"Itabirito"},
                    {3132008,"Itacambira"},
                    {3132107,"Itacarambi"},
                    {3132206,"Itaguara"},
                    {3132305,"Itaip??"},
                    {3132404,"Itajub??"},
                    {3132503,"Itamarandiba"},
                    {3132602,"Itamarati de Minas"},
                    {3132701,"Itambacuri"},
                    {3132800,"Itamb?? do Mato Dentro"},
                    {3132909,"Itamogi"},
                    {3133006,"Itamonte"},
                    {3133105,"Itanhandu"},
                    {3133204,"Itanhomi"},
                    {3133303,"Itaobim"},
                    {3133402,"Itapagipe"},
                    {3133501,"Itapecerica"},
                    {3133600,"Itapeva"},
                    {3133709,"Itatiaiu??u"},
                    {3133758,"Ita?? de Minas"},
                    {3133808,"Ita??na"},
                    {3133907,"Itaverava"},
                    {3134004,"Itinga"},
                    {3134103,"Itueta"},
                    {3134202,"Ituiutaba"},
                    {3134301,"Itumirim"},
                    {3134400,"Iturama"},
                    {3134509,"Itutinga"},
                    {3134608,"Jaboticatubas"},
                    {3134707,"Jacinto"},
                    {3134806,"Jacu??"},
                    {3134905,"Jacutinga"},
                    {3135001,"Jaguara??u"},
                    {3135050,"Ja??ba"},
                    {3135076,"Jampruca"},
                    {3135100,"Jana??ba"},
                    {3135209,"Janu??ria"},
                    {3135308,"Japara??ba"},
                    {3135357,"Japonvar"},
                    {3135407,"Jeceaba"},
                    {3135456,"Jenipapo de Minas"},
                    {3135506,"Jequeri"},
                    {3135605,"Jequita??"},
                    {3135704,"Jequitib??"},
                    {3135803,"Jequitinhonha"},
                    {3135902,"Jesu??nia"},
                    {3136009,"Joa??ma"},
                    {3136108,"Joan??sia"},
                    {3136207,"Jo??o Monlevade"},
                    {3136306,"Jo??o Pinheiro"},
                    {3136405,"Joaquim Fel??cio"},
                    {3136504,"Jord??nia"},
                    {3136520,"Jos?? Gon??alves de Minas"},
                    {3136553,"Jos?? Raydan"},
                    {3136579,"Josen??polis"},
                    {3136652,"Juatuba"},
                    {3136702,"Juiz de Fora"},
                    {3136801,"Juramento"},
                    {3136900,"Juruaia"},
                    {3136959,"Juven??lia"},
                    {3137007,"Ladainha"},
                    {3137106,"Lagamar"},
                    {3137205,"Lagoa da Prata"},
                    {3137304,"Lagoa dos Patos"},
                    {3137403,"Lagoa Dourada"},
                    {3137502,"Lagoa Formosa"},
                    {3137536,"Lagoa Grande"},
                    {3137601,"Lagoa Santa"},
                    {3137700,"Lajinha"},
                    {3137809,"Lambari"},
                    {3137908,"Lamim"},
                    {3138005,"Laranjal"},
                    {3138104,"Lassance"},
                    {3138203,"Lavras"},
                    {3138302,"Leandro Ferreira"},
                    {3138351,"Leme do Prado"},
                    {3138401,"Leopoldina"},
                    {3138500,"Liberdade"},
                    {3138609,"Lima Duarte"},
                    {3138625,"Limeira do Oeste"},
                    {3138658,"Lontra"},
                    {3138674,"Luisburgo"},
                    {3138682,"Luisl??ndia"},
                    {3138708,"Lumin??rias"},
                    {3138807,"Luz"},
                    {3138906,"Machacalis"},
                    {3139003,"Machado"},
                    {3139102,"Madre de Deus de Minas"},
                    {3139201,"Malacacheta"},
                    {3139250,"Mamonas"},
                    {3139300,"Manga"},
                    {3139409,"Manhua??u"},
                    {3139508,"Manhumirim"},
                    {3139607,"Mantena"},
                    {3139805,"Mar de Espanha"},
                    {3139706,"Maravilhas"},
                    {3139904,"Maria da F??"},
                    {3140001,"Mariana"},
                    {3140100,"Marilac"},
                    {3140159,"M??rio Campos"},
                    {3140209,"Marip?? de Minas"},
                    {3140308,"Marli??ria"},
                    {3140407,"Marmel??polis"},
                    {3140506,"Martinho Campos"},
                    {3140530,"Martins Soares"},
                    {3140555,"Mata Verde"},
                    {3140605,"Materl??ndia"},
                    {3140704,"Mateus Leme"},
                    {3171501,"Mathias Lobato"},
                    {3140803,"Matias Barbosa"},
                    {3140852,"Matias Cardoso"},
                    {3140902,"Matip??"},
                    {3141009,"Mato Verde"},
                    {3141108,"Matozinhos"},
                    {3141207,"Matutina"},
                    {3141306,"Medeiros"},
                    {3141405,"Medina"},
                    {3141504,"Mendes Pimentel"},
                    {3141603,"Merc??s"},
                    {3141702,"Mesquita"},
                    {3141801,"Minas Novas"},
                    {3141900,"Minduri"},
                    {3142007,"Mirabela"},
                    {3142106,"Miradouro"},
                    {3142205,"Mira??"},
                    {3142254,"Mirav??nia"},
                    {3142304,"Moeda"},
                    {3142403,"Moema"},
                    {3142502,"Monjolos"},
                    {3142601,"Monsenhor Paulo"},
                    {3142700,"Montalv??nia"},
                    {3142809,"Monte Alegre de Minas"},
                    {3142908,"Monte Azul"},
                    {3143005,"Monte Belo"},
                    {3143104,"Monte Carmelo"},
                    {3143153,"Monte Formoso"},
                    {3143203,"Monte Santo de Minas"},
                    {3143401,"Monte Si??o"},
                    {3143302,"Montes Claros"},
                    {3143450,"Montezuma"},
                    {3143500,"Morada Nova de Minas"},
                    {3143609,"Morro da Gar??a"},
                    {3143708,"Morro do Pilar"},
                    {3143807,"Munhoz"},
                    {3143906,"Muria??"},
                    {3144003,"Mutum"},
                    {3144102,"Muzambinho"},
                    {3144201,"Nacip Raydan"},
                    {3144300,"Nanuque"},
                    {3144359,"Naque"},
                    {3144375,"Natal??ndia"},
                    {3144409,"Nat??rcia"},
                    {3144508,"Nazareno"},
                    {3144607,"Nepomuceno"},
                    {3144656,"Ninheira"},
                    {3144672,"Nova Bel??m"},
                    {3144706,"Nova Era"},
                    {3144805,"Nova Lima"},
                    {3144904,"Nova M??dica"},
                    {3145000,"Nova Ponte"},
                    {3145059,"Nova Porteirinha"},
                    {3145109,"Nova Resende"},
                    {3145208,"Nova Serrana"},
                    {3136603,"Nova Uni??o"},
                    {3145307,"Novo Cruzeiro"},
                    {3145356,"Novo Oriente de Minas"},
                    {3145372,"Novorizonte"},
                    {3145406,"Olaria"},
                    {3145455,"Olhos-d'??gua"},
                    {3145505,"Ol??mpio Noronha"},
                    {3145604,"Oliveira"},
                    {3145703,"Oliveira Fortes"},
                    {3145802,"On??a de Pitangui"},
                    {3145851,"Orat??rios"},
                    {3145877,"Oriz??nia"},
                    {3145901,"Ouro Branco"},
                    {3146008,"Ouro Fino"},
                    {3146107,"Ouro Preto"},
                    {3146206,"Ouro Verde de Minas"},
                    {3146255,"Padre Carvalho"},
                    {3146305,"Padre Para??so"},
                    {3146552,"Pai Pedro"},
                    {3146404,"Paineiras"},
                    {3146503,"Pains"},
                    {3146602,"Paiva"},
                    {3146701,"Palma"},
                    {3146750,"Palm??polis"},
                    {3146909,"Papagaios"},
                    {3147105,"Par?? de Minas"},
                    {3147006,"Paracatu"},
                    {3147204,"Paragua??u"},
                    {3147303,"Parais??polis"},
                    {3147402,"Paraopeba"},
                    {3147600,"Passa Quatro"},
                    {3147709,"Passa Tempo"},
                    {3147808,"Passa Vinte"},
                    {3147501,"Passab??m"},
                    {3147907,"Passos"},
                    {3147956,"Patis"},
                    {3148004,"Patos de Minas"},
                    {3148103,"Patroc??nio"},
                    {3148202,"Patroc??nio do Muria??"},
                    {3148301,"Paula C??ndido"},
                    {3148400,"Paulistas"},
                    {3148509,"Pav??o"},
                    {3148608,"Pe??anha"},
                    {3148707,"Pedra Azul"},
                    {3148756,"Pedra Bonita"},
                    {3148806,"Pedra do Anta"},
                    {3148905,"Pedra do Indai??"},
                    {3149002,"Pedra Dourada"},
                    {3149101,"Pedralva"},
                    {3149150,"Pedras de Maria da Cruz"},
                    {3149200,"Pedrin??polis"},
                    {3149309,"Pedro Leopoldo"},
                    {3149408,"Pedro Teixeira"},
                    {3149507,"Pequeri"},
                    {3149606,"Pequi"},
                    {3149705,"Perdig??o"},
                    {3149804,"Perdizes"},
                    {3149903,"Perd??es"},
                    {3149952,"Periquito"},
                    {3150000,"Pescador"},
                    {3150109,"Piau"},
                    {3150158,"Piedade de Caratinga"},
                    {3150208,"Piedade de Ponte Nova"},
                    {3150307,"Piedade do Rio Grande"},
                    {3150406,"Piedade dos Gerais"},
                    {3150505,"Pimenta"},
                    {3150539,"Pingo-d'??gua"},
                    {3150570,"Pint??polis"},
                    {3150604,"Piracema"},
                    {3150703,"Pirajuba"},
                    {3150802,"Piranga"},
                    {3150901,"Pirangu??u"},
                    {3151008,"Piranguinho"},
                    {3151107,"Pirapetinga"},
                    {3151206,"Pirapora"},
                    {3151305,"Pira??ba"},
                    {3151404,"Pitangui"},
                    {3151503,"Piumhi"},
                    {3151602,"Planura"},
                    {3151701,"Po??o Fundo"},
                    {3151800,"Po??os de Caldas"},
                    {3151909,"Pocrane"},
                    {3152006,"Pomp??u"},
                    {3152105,"Ponte Nova"},
                    {3152131,"Ponto Chique"},
                    {3152170,"Ponto dos Volantes"},
                    {3152204,"Porteirinha"},
                    {3152303,"Porto Firme"},
                    {3152402,"Pot??"},
                    {3152501,"Pouso Alegre"},
                    {3152600,"Pouso Alto"},
                    {3152709,"Prados"},
                    {3152808,"Prata"},
                    {3152907,"Prat??polis"},
                    {3153004,"Pratinha"},
                    {3153103,"Presidente Bernardes"},
                    {3153202,"Presidente Juscelino"},
                    {3153301,"Presidente Kubitschek"},
                    {3153400,"Presidente Oleg??rio"},
                    {3153608,"Prudente de Morais"},
                    {3153707,"Quartel Geral"},
                    {3153806,"Queluzito"},
                    {3153905,"Raposos"},
                    {3154002,"Raul Soares"},
                    {3154101,"Recreio"},
                    {3154150,"Reduto"},
                    {3154200,"Resende Costa"},
                    {3154309,"Resplendor"},
                    {3154408,"Ressaquinha"},
                    {3154457,"Riachinho"},
                    {3154507,"Riacho dos Machados"},
                    {3154606,"Ribeir??o das Neves"},
                    {3154705,"Ribeir??o Vermelho"},
                    {3154804,"Rio Acima"},
                    {3154903,"Rio Casca"},
                    {3155108,"Rio do Prado"},
                    {3155009,"Rio Doce"},
                    {3155207,"Rio Espera"},
                    {3155306,"Rio Manso"},
                    {3155405,"Rio Novo"},
                    {3155504,"Rio Parana??ba"},
                    {3155603,"Rio Pardo de Minas"},
                    {3155702,"Rio Piracicaba"},
                    {3155801,"Rio Pomba"},
                    {3155900,"Rio Preto"},
                    {3156007,"Rio Vermelho"},
                    {3156106,"Rit??polis"},
                    {3156205,"Rochedo de Minas"},
                    {3156304,"Rodeiro"},
                    {3156403,"Romaria"},
                    {3156452,"Ros??rio da Limeira"},
                    {3156502,"Rubelita"},
                    {3156601,"Rubim"},
                    {3156700,"Sabar??"},
                    {3156809,"Sabin??polis"},
                    {3156908,"Sacramento"},
                    {3157005,"Salinas"},
                    {3157104,"Salto da Divisa"},
                    {3157203,"Santa B??rbara"},
                    {3157252,"Santa B??rbara do Leste"},
                    {3157278,"Santa B??rbara do Monte Verde"},
                    {3157302,"Santa B??rbara do Tug??rio"},
                    {3157336,"Santa Cruz de Minas"},
                    {3157377,"Santa Cruz de Salinas"},
                    {3157401,"Santa Cruz do Escalvado"},
                    {3157500,"Santa Efig??nia de Minas"},
                    {3157609,"Santa F?? de Minas"},
                    {3157658,"Santa Helena de Minas"},
                    {3157708,"Santa Juliana"},
                    {3157807,"Santa Luzia"},
                    {3157906,"Santa Margarida"},
                    {3158003,"Santa Maria de Itabira"},
                    {3158102,"Santa Maria do Salto"},
                    {3158201,"Santa Maria do Sua??u??"},
                    {3159209,"Santa Rita de Caldas"},
                    {3159407,"Santa Rita de Ibitipoca"},
                    {3159308,"Santa Rita de Jacutinga"},
                    {3159357,"Santa Rita de Minas"},
                    {3159506,"Santa Rita do Itueto"},
                    {3159605,"Santa Rita do Sapuca??"},
                    {3159704,"Santa Rosa da Serra"},
                    {3159803,"Santa Vit??ria"},
                    {3158300,"Santana da Vargem"},
                    {3158409,"Santana de Cataguases"},
                    {3158508,"Santana de Pirapama"},
                    {3158607,"Santana do Deserto"},
                    {3158706,"Santana do Garamb??u"},
                    {3158805,"Santana do Jacar??"},
                    {3158904,"Santana do Manhua??u"},
                    {3158953,"Santana do Para??so"},
                    {3159001,"Santana do Riacho"},
                    {3159100,"Santana dos Montes"},
                    {3159902,"Santo Ant??nio do Amparo"},
                    {3160009,"Santo Ant??nio do Aventureiro"},
                    {3160108,"Santo Ant??nio do Grama"},
                    {3160207,"Santo Ant??nio do Itamb??"},
                    {3160306,"Santo Ant??nio do Jacinto"},
                    {3160405,"Santo Ant??nio do Monte"},
                    {3160454,"Santo Ant??nio do Retiro"},
                    {3160504,"Santo Ant??nio do Rio Abaixo"},
                    {3160603,"Santo Hip??lito"},
                    {3160702,"Santos Dumont"},
                    {3160801,"S??o Bento Abade"},
                    {3160900,"S??o Br??s do Sua??u??"},
                    {3160959,"S??o Domingos das Dores"},
                    {3161007,"S??o Domingos do Prata"},
                    {3161056,"S??o F??lix de Minas"},
                    {3161106,"S??o Francisco"},
                    {3161205,"S??o Francisco de Paula"},
                    {3161304,"S??o Francisco de Sales"},
                    {3161403,"S??o Francisco do Gl??ria"},
                    {3161502,"S??o Geraldo"},
                    {3161601,"S??o Geraldo da Piedade"},
                    {3161650,"S??o Geraldo do Baixio"},
                    {3161700,"S??o Gon??alo do Abaet??"},
                    {3161809,"S??o Gon??alo do Par??"},
                    {3161908,"S??o Gon??alo do Rio Abaixo"},
                    {3125507,"S??o Gon??alo do Rio Preto"},
                    {3162005,"S??o Gon??alo do Sapuca??"},
                    {3162104,"S??o Gotardo"},
                    {3162203,"S??o Jo??o Batista do Gl??ria"},
                    {3162252,"S??o Jo??o da Lagoa"},
                    {3162302,"S??o Jo??o da Mata"},
                    {3162401,"S??o Jo??o da Ponte"},
                    {3162450,"S??o Jo??o das Miss??es"},
                    {3162500,"S??o Jo??o del Rei"},
                    {3162559,"S??o Jo??o do Manhua??u"},
                    {3162575,"S??o Jo??o do Manteninha"},
                    {3162609,"S??o Jo??o do Oriente"},
                    {3162658,"S??o Jo??o do Pacu??"},
                    {3162708,"S??o Jo??o do Para??so"},
                    {3162807,"S??o Jo??o Evangelista"},
                    {3162906,"S??o Jo??o Nepomuceno"},
                    {3162922,"S??o Joaquim de Bicas"},
                    {3162948,"S??o Jos?? da Barra"},
                    {3162955,"S??o Jos?? da Lapa"},
                    {3163003,"S??o Jos?? da Safira"},
                    {3163102,"S??o Jos?? da Varginha"},
                    {3163201,"S??o Jos?? do Alegre"},
                    {3163300,"S??o Jos?? do Divino"},
                    {3163409,"S??o Jos?? do Goiabal"},
                    {3163508,"S??o Jos?? do Jacuri"},
                    {3163607,"S??o Jos?? do Mantimento"},
                    {3163706,"S??o Louren??o"},
                    {3163805,"S??o Miguel do Anta"},
                    {3163904,"S??o Pedro da Uni??o"},
                    {3164100,"S??o Pedro do Sua??u??"},
                    {3164001,"S??o Pedro dos Ferros"},
                    {3164209,"S??o Rom??o"},
                    {3164308,"S??o Roque de Minas"},
                    {3164407,"S??o Sebasti??o da Bela Vista"},
                    {3164431,"S??o Sebasti??o da Vargem Alegre"},
                    {3164472,"S??o Sebasti??o do Anta"},
                    {3164506,"S??o Sebasti??o do Maranh??o"},
                    {3164605,"S??o Sebasti??o do Oeste"},
                    {3164704,"S??o Sebasti??o do Para??so"},
                    {3164803,"S??o Sebasti??o do Rio Preto"},
                    {3164902,"S??o Sebasti??o do Rio Verde"},
                    {3165008,"S??o Tiago"},
                    {3165107,"S??o Tom??s de Aquino"},
                    {3165206,"S??o Tom?? das Letras"},
                    {3165305,"S??o Vicente de Minas"},
                    {3165404,"Sapuca??-Mirim"},
                    {3165503,"Sardo??"},
                    {3165537,"Sarzedo"},
                    {3165560,"Sem-Peixe"},
                    {3165578,"Senador Amaral"},
                    {3165602,"Senador Cortes"},
                    {3165701,"Senador Firmino"},
                    {3165800,"Senador Jos?? Bento"},
                    {3165909,"Senador Modestino Gon??alves"},
                    {3166006,"Senhora de Oliveira"},
                    {3166105,"Senhora do Porto"},
                    {3166204,"Senhora dos Rem??dios"},
                    {3166303,"Sericita"},
                    {3166402,"Seritinga"},
                    {3166501,"Serra Azul de Minas"},
                    {3166600,"Serra da Saudade"},
                    {3166808,"Serra do Salitre"},
                    {3166709,"Serra dos Aimor??s"},
                    {3166907,"Serrania"},
                    {3166956,"Serran??polis de Minas"},
                    {3167004,"Serranos"},
                    {3167103,"Serro"},
                    {3167202,"Sete Lagoas"},
                    {3165552,"Setubinha"},
                    {3167301,"Silveir??nia"},
                    {3167400,"Silvian??polis"},
                    {3167509,"Sim??o Pereira"},
                    {3167608,"Simon??sia"},
                    {3167707,"Sobr??lia"},
                    {3167806,"Soledade de Minas"},
                    {3167905,"Tabuleiro"},
                    {3168002,"Taiobeiras"},
                    {3168051,"Taparuba"},
                    {3168101,"Tapira"},
                    {3168200,"Tapira??"},
                    {3168309,"Taquara??u de Minas"},
                    {3168408,"Tarumirim"},
                    {3168507,"Teixeiras"},
                    {3168606,"Te??filo Otoni"},
                    {3168705,"Tim??teo"},
                    {3168804,"Tiradentes"},
                    {3168903,"Tiros"},
                    {3169000,"Tocantins"},
                    {3169059,"Tocos do Moji"},
                    {3169109,"Toledo"},
                    {3169208,"Tombos"},
                    {3169307,"Tr??s Cora????es"},
                    {3169356,"Tr??s Marias"},
                    {3169406,"Tr??s Pontas"},
                    {3169505,"Tumiritinga"},
                    {3169604,"Tupaciguara"},
                    {3169703,"Turmalina"},
                    {3169802,"Turvol??ndia"},
                    {3169901,"Ub??"},
                    {3170008,"Uba??"},
                    {3170057,"Ubaporanga"},
                    {3170107,"Uberaba"},
                    {3170206,"Uberl??ndia"},
                    {3170305,"Umburatiba"},
                    {3170404,"Una??"},
                    {3170438,"Uni??o de Minas"},
                    {3170479,"Uruana de Minas"},
                    {3170503,"Uruc??nia"},
                    {3170529,"Urucuia"},
                    {3170578,"Vargem Alegre"},
                    {3170602,"Vargem Bonita"},
                    {3170651,"Vargem Grande do Rio Pardo"},
                    {3170701,"Varginha"},
                    {3170750,"Varj??o de Minas"},
                    {3170800,"V??rzea da Palma"},
                    {3170909,"Varzel??ndia"},
                    {3171006,"Vazante"},
                    {3171030,"Verdel??ndia"},
                    {3171071,"Veredinha"},
                    {3171105,"Ver??ssimo"},
                    {3171154,"Vermelho Novo"},
                    {3171204,"Vespasiano"},
                    {3171303,"Vi??osa"},
                    {3171402,"Vieiras"},
                    {3171600,"Virgem da Lapa"},
                    {3171709,"Virg??nia"},
                    {3171808,"Virgin??polis"},
                    {3171907,"Virgol??ndia"},
                    {3172004,"Visconde do Rio Branco"},
                    {3172103,"Volta Grande"},
                    {3172202,"Wenceslau Braz"},

                    /* Rio Grande do Sul */
                    //Code  ,City Name
                    {4300034,"Acegu??"},
                    {4300059,"??gua Santa"},
                    {4300109,"Agudo"},
                    {4300208,"Ajuricaba"},
                    {4300307,"Alecrim"},
                    {4300406,"Alegrete"},
                    {4300455,"Alegria"},
                    {4300471,"Almirante Tamandar?? do Sul"},
                    {4300505,"Alpestre"},
                    {4300554,"Alto Alegre"},
                    {4300570,"Alto Feliz"},
                    {4300604,"Alvorada"},
                    {4300638,"Amaral Ferrador"},
                    {4300646,"Ametista do Sul"},
                    {4300661,"Andr?? da Rocha"},
                    {4300703,"Anta Gorda"},
                    {4300802,"Ant??nio Prado"},
                    {4300851,"Arambar??"},
                    {4300877,"Araric??"},
                    {4300901,"Aratiba"},
                    {4301008,"Arroio do Meio"},
                    {4301073,"Arroio do Padre"},
                    {4301057,"Arroio do Sal"},
                    {4301206,"Arroio do Tigre"},
                    {4301107,"Arroio dos Ratos"},
                    {4301305,"Arroio Grande"},
                    {4301404,"Arvorezinha"},
                    {4301503,"Augusto Pestana"},
                    {4301552,"??urea"},
                    {4301602,"Bag??"},
                    {4301636,"Balne??rio Pinhal"},
                    {4301651,"Bar??o"},
                    {4301701,"Bar??o de Cotegipe"},
                    {4301750,"Bar??o do Triunfo"},
                    {4301859,"Barra do Guarita"},
                    {4301875,"Barra do Quara??"},
                    {4301909,"Barra do Ribeiro"},
                    {4301925,"Barra do Rio Azul"},
                    {4301958,"Barra Funda"},
                    {4301800,"Barrac??o"},
                    {4302006,"Barros Cassal"},
                    {4302055,"Benjamin Constant do Sul"},
                    {4302105,"Bento Gon??alves"},
                    {4302154,"Boa Vista das Miss??es"},
                    {4302204,"Boa Vista do Buric??"},
                    {4302220,"Boa Vista do Cadeado"},
                    {4302238,"Boa Vista do Incra"},
                    {4302253,"Boa Vista do Sul"},
                    {4302303,"Bom Jesus"},
                    {4302352,"Bom Princ??pio"},
                    {4302378,"Bom Progresso"},
                    {4302402,"Bom Retiro do Sul"},
                    {4302451,"Boqueir??o do Le??o"},
                    {4302501,"Bossoroca"},
                    {4302584,"Bozano"},
                    {4302600,"Braga"},
                    {4302659,"Brochier"},
                    {4302709,"Buti??"},
                    {4302808,"Ca??apava do Sul"},
                    {4302907,"Cacequi"},
                    {4303004,"Cachoeira do Sul"},
                    {4303103,"Cachoeirinha"},
                    {4303202,"Cacique Doble"},
                    {4303301,"Caibat??"},
                    {4303400,"Cai??ara"},
                    {4303509,"Camaqu??"},
                    {4303558,"Camargo"},
                    {4303608,"Cambar?? do Sul"},
                    {4303673,"Campestre da Serra"},
                    {4303707,"Campina das Miss??es"},
                    {4303806,"Campinas do Sul"},
                    {4303905,"Campo Bom"},
                    {4304002,"Campo Novo"},
                    {4304101,"Campos Borges"},
                    {4304200,"Candel??ria"},
                    {4304309,"C??ndido God??i"},
                    {4304358,"Candiota"},
                    {4304408,"Canela"},
                    {4304507,"Cangu??u"},
                    {4304606,"Canoas"},
                    {4304614,"Canudos do Vale"},
                    {4304622,"Cap??o Bonito do Sul"},
                    {4304630,"Cap??o da Canoa"},
                    {4304655,"Cap??o do Cip??"},
                    {4304663,"Cap??o do Le??o"},
                    {4304689,"Capela de Santana"},
                    {4304697,"Capit??o"},
                    {4304671,"Capivari do Sul"},
                    {4304713,"Cara??"},
                    {4304705,"Carazinho"},
                    {4304804,"Carlos Barbosa"},
                    {4304853,"Carlos Gomes"},
                    {4304903,"Casca"},
                    {4304952,"Caseiros"},
                    {4305009,"Catu??pe"},
                    {4305108,"Caxias do Sul"},
                    {4305116,"Centen??rio"},
                    {4305124,"Cerrito"},
                    {4305132,"Cerro Branco"},
                    {4305157,"Cerro Grande"},
                    {4305173,"Cerro Grande do Sul"},
                    {4305207,"Cerro Largo"},
                    {4305306,"Chapada"},
                    {4305355,"Charqueadas"},
                    {4305371,"Charrua"},
                    {4305405,"Chiapetta"},
                    {4305439,"Chu??"},
                    {4305447,"Chuvisca"},
                    {4305454,"Cidreira"},
                    {4305504,"Cir??aco"},
                    {4305587,"Colinas"},
                    {4305603,"Colorado"},
                    {4305702,"Condor"},
                    {4305801,"Constantina"},
                    {4305835,"Coqueiro Baixo"},
                    {4305850,"Coqueiros do Sul"},
                    {4305871,"Coronel Barros"},
                    {4305900,"Coronel Bicaco"},
                    {4305934,"Coronel Pilar"},
                    {4305959,"Cotipor??"},
                    {4305975,"Coxilha"},
                    {4306007,"Crissiumal"},
                    {4306056,"Cristal"},
                    {4306072,"Cristal do Sul"},
                    {4306106,"Cruz Alta"},
                    {4306130,"Cruzaltense"},
                    {4306205,"Cruzeiro do Sul"},
                    {4306304,"David Canabarro"},
                    {4306320,"Derrubadas"},
                    {4306353,"Dezesseis de Novembro"},
                    {4306379,"Dilermando de Aguiar"},
                    {4306403,"Dois Irm??os"},
                    {4306429,"Dois Irm??os das Miss??es"},
                    {4306452,"Dois Lajeados"},
                    {4306502,"Dom Feliciano"},
                    {4306601,"Dom Pedrito"},
                    {4306551,"Dom Pedro de Alc??ntara"},
                    {4306700,"Dona Francisca"},
                    {4306734,"Doutor Maur??cio Cardoso"},
                    {4306759,"Doutor Ricardo"},
                    {4306767,"Eldorado do Sul"},
                    {4306809,"Encantado"},
                    {4306908,"Encruzilhada do Sul"},
                    {4306924,"Engenho Velho"},
                    {4306957,"Entre Rios do Sul"},
                    {4306932,"Entre-Iju??s"},
                    {4306973,"Erebango"},
                    {4307005,"Erechim"},
                    {4307054,"Ernestina"},
                    {4307203,"Erval Grande"},
                    {4307302,"Erval Seco"},
                    {4307401,"Esmeralda"},
                    {4307450,"Esperan??a do Sul"},
                    {4307500,"Espumoso"},
                    {4307559,"Esta????o"},
                    {4307609,"Est??ncia Velha"},
                    {4307708,"Esteio"},
                    {4307807,"Estrela"},
                    {4307815,"Estrela Velha"},
                    {4307831,"Eug??nio de Castro"},
                    {4307864,"Fagundes Varela"},
                    {4307906,"Farroupilha"},
                    {4308003,"Faxinal do Soturno"},
                    {4308052,"Faxinalzinho"},
                    {4308078,"Fazenda Vilanova"},
                    {4308102,"Feliz"},
                    {4308201,"Flores da Cunha"},
                    {4308250,"Floriano Peixoto"},
                    {4308300,"Fontoura Xavier"},
                    {4308409,"Formigueiro"},
                    {4308433,"Forquetinha"},
                    {4308458,"Fortaleza dos Valos"},
                    {4308508,"Frederico Westphalen"},
                    {4308607,"Garibaldi"},
                    {4308656,"Garruchos"},
                    {4308706,"Gaurama"},
                    {4308805,"General C??mara"},
                    {4308854,"Gentil"},
                    {4308904,"Get??lio Vargas"},
                    {4309001,"Giru??"},
                    {4309050,"Glorinha"},
                    {4309100,"Gramado"},
                    {4309126,"Gramado dos Loureiros"},
                    {4309159,"Gramado Xavier"},
                    {4309209,"Gravata??"},
                    {4309258,"Guabiju"},
                    {4309308,"Gua??ba"},
                    {4309407,"Guapor??"},
                    {4309506,"Guarani das Miss??es"},
                    {4309555,"Harmonia"},
                    {4307104,"Herval"},
                    {4309571,"Herveiras"},
                    {4309605,"Horizontina"},
                    {4309654,"Hulha Negra"},
                    {4309704,"Humait??"},
                    {4309753,"Ibarama"},
                    {4309803,"Ibia????"},
                    {4309902,"Ibiraiaras"},
                    {4309951,"Ibirapuit??"},
                    {4310009,"Ibirub??"},
                    {4310108,"Igrejinha"},
                    {4310207,"Iju??"},
                    {4310306,"Il??polis"},
                    {4310330,"Imb??"},
                    {4310363,"Imigrante"},
                    {4310405,"Independ??ncia"},
                    {4310413,"Inhacor??"},
                    {4310439,"Ip??"},
                    {4310462,"Ipiranga do Sul"},
                    {4310504,"Ira??"},
                    {4310538,"Itaara"},
                    {4310553,"Itacurubi"},
                    {4310579,"Itapuca"},
                    {4310603,"Itaqui"},
                    {4310652,"Itati"},
                    {4310702,"Itatiba do Sul"},
                    {4310751,"Ivor??"},
                    {4310801,"Ivoti"},
                    {4310850,"Jaboticaba"},
                    {4310876,"Jacuizinho"},
                    {4310900,"Jacutinga"},
                    {4311007,"Jaguar??o"},
                    {4311106,"Jaguari"},
                    {4311122,"Jaquirana"},
                    {4311130,"Jari"},
                    {4311155,"J??ia"},
                    {4311205,"J??lio de Castilhos"},
                    {4311239,"Lagoa Bonita do Sul"},
                    {4311270,"Lagoa dos Tr??s Cantos"},
                    {4311304,"Lagoa Vermelha"},
                    {4311254,"Lago??o"},
                    {4311403,"Lajeado"},
                    {4311429,"Lajeado do Bugre"},
                    {4311502,"Lavras do Sul"},
                    {4311601,"Liberato Salzano"},
                    {4311627,"Lindolfo Collor"},
                    {4311643,"Linha Nova"},
                    {4311718,"Ma??ambar??"},
                    {4311700,"Machadinho"},
                    {4311734,"Mampituba"},
                    {4311759,"Manoel Viana"},
                    {4311775,"Maquin??"},
                    {4311791,"Marat??"},
                    {4311809,"Marau"},
                    {4311908,"Marcelino Ramos"},
                    {4311981,"Mariana Pimentel"},
                    {4312005,"Mariano Moro"},
                    {4312054,"Marques de Souza"},
                    {4312104,"Mata"},
                    {4312138,"Mato Castelhano"},
                    {4312153,"Mato Leit??o"},
                    {4312179,"Mato Queimado"},
                    {4312203,"Maximiliano de Almeida"},
                    {4312252,"Minas do Le??o"},
                    {4312302,"Miragua??"},
                    {4312351,"Montauri"},
                    {4312377,"Monte Alegre dos Campos"},
                    {4312385,"Monte Belo do Sul"},
                    {4312401,"Montenegro"},
                    {4312427,"Morma??o"},
                    {4312443,"Morrinhos do Sul"},
                    {4312450,"Morro Redondo"},
                    {4312476,"Morro Reuter"},
                    {4312500,"Mostardas"},
                    {4312609,"Mu??um"},
                    {4312617,"Muitos Cap??es"},
                    {4312625,"Muliterno"},
                    {4312658,"N??o-Me-Toque"},
                    {4312674,"Nicolau Vergueiro"},
                    {4312708,"Nonoai"},
                    {4312757,"Nova Alvorada"},
                    {4312807,"Nova Ara????"},
                    {4312906,"Nova Bassano"},
                    {4312955,"Nova Boa Vista"},
                    {4313003,"Nova Br??scia"},
                    {4313011,"Nova Candel??ria"},
                    {4313037,"Nova Esperan??a do Sul"},
                    {4313060,"Nova Hartz"},
                    {4313086,"Nova P??dua"},
                    {4313102,"Nova Palma"},
                    {4313201,"Nova Petr??polis"},
                    {4313300,"Nova Prata"},
                    {4313334,"Nova Ramada"},
                    {4313359,"Nova Roma do Sul"},
                    {4313375,"Nova Santa Rita"},
                    {4313490,"Novo Barreiro"},
                    {4313391,"Novo Cabrais"},
                    {4313409,"Novo Hamburgo"},
                    {4313425,"Novo Machado"},
                    {4313441,"Novo Tiradentes"},
                    {4313466,"Novo Xingu"},
                    {4313508,"Os??rio"},
                    {4313607,"Paim Filho"},
                    {4313656,"Palmares do Sul"},
                    {4313706,"Palmeira das Miss??es"},
                    {4313805,"Palmitinho"},
                    {4313904,"Panambi"},
                    {4313953,"Pantano Grande"},
                    {4314001,"Para??"},
                    {4314027,"Para??so do Sul"},
                    {4314035,"Pareci Novo"},
                    {4314050,"Parob??"},
                    {4314068,"Passa Sete"},
                    {4314076,"Passo do Sobrado"},
                    {4314100,"Passo Fundo"},
                    {4314134,"Paulo Bento"},
                    {4314159,"Paverama"},
                    {4314175,"Pedras Altas"},
                    {4314209,"Pedro Os??rio"},
                    {4314308,"Peju??ara"},
                    {4314407,"Pelotas"},
                    {4314423,"Picada Caf??"},
                    {4314456,"Pinhal"},
                    {4314464,"Pinhal da Serra"},
                    {4314472,"Pinhal Grande"},
                    {4314498,"Pinheirinho do Vale"},
                    {4314506,"Pinheiro Machado"},
                    {4314548,"Pinto Bandeira"},
                    {4314555,"Pirap??"},
                    {4314605,"Piratini"},
                    {4314704,"Planalto"},
                    {4314753,"Po??o das Antas"},
                    {4314779,"Pont??o"},
                    {4314787,"Ponte Preta"},
                    {4314803,"Port??o"},
                    {4314902,"Porto Alegre"},
                    {4315008,"Porto Lucena"},
                    {4315057,"Porto Mau??"},
                    {4315073,"Porto Vera Cruz"},
                    {4315107,"Porto Xavier"},
                    {4315131,"Pouso Novo"},
                    {4315149,"Presidente Lucena"},
                    {4315156,"Progresso"},
                    {4315172,"Prot??sio Alves"},
                    {4315206,"Putinga"},
                    {4315305,"Quara??"},
                    {4315313,"Quatro Irm??os"},
                    {4315321,"Quevedos"},
                    {4315354,"Quinze de Novembro"},
                    {4315404,"Redentora"},
                    {4315453,"Relvado"},
                    {4315503,"Restinga S??ca"},
                    {4315552,"Rio dos ??ndios"},
                    {4315602,"Rio Grande"},
                    {4315701,"Rio Pardo"},
                    {4315750,"Riozinho"},
                    {4315800,"Roca Sales"},
                    {4315909,"Rodeio Bonito"},
                    {4315958,"Rolador"},
                    {4316006,"Rolante"},
                    {4316105,"Ronda Alta"},
                    {4316204,"Rondinha"},
                    {4316303,"Roque Gonzales"},
                    {4316402,"Ros??rio do Sul"},
                    {4316428,"Sagrada Fam??lia"},
                    {4316436,"Saldanha Marinho"},
                    {4316451,"Salto do Jacu??"},
                    {4316477,"Salvador das Miss??es"},
                    {4316501,"Salvador do Sul"},
                    {4316600,"Sananduva"},
                    {4316709,"Santa B??rbara do Sul"},
                    {4316733,"Santa Cec??lia do Sul"},
                    {4316758,"Santa Clara do Sul"},
                    {4316808,"Santa Cruz do Sul"},
                    {4316972,"Santa Margarida do Sul"},
                    {4316907,"Santa Maria"},
                    {4316956,"Santa Maria do Herval"},
                    {4317202,"Santa Rosa"},
                    {4317251,"Santa Tereza"},
                    {4317301,"Santa Vit??ria do Palmar"},
                    {4317004,"Santana da Boa Vista"},
                    {4317103,"Sant'Ana do Livramento"},
                    {4317400,"Santiago"},
                    {4317509,"Santo ??ngelo"},
                    {4317608,"Santo Ant??nio da Patrulha"},
                    {4317707,"Santo Ant??nio das Miss??es"},
                    {4317558,"Santo Ant??nio do Palma"},
                    {4317756,"Santo Ant??nio do Planalto"},
                    {4317806,"Santo Augusto"},
                    {4317905,"Santo Cristo"},
                    {4317954,"Santo Expedito do Sul"},
                    {4318002,"S??o Borja"},
                    {4318051,"S??o Domingos do Sul"},
                    {4318101,"S??o Francisco de Assis"},
                    {4318200,"S??o Francisco de Paula"},
                    {4318309,"S??o Gabriel"},
                    {4318408,"S??o Jer??nimo"},
                    {4318424,"S??o Jo??o da Urtiga"},
                    {4318432,"S??o Jo??o do Pol??sine"},
                    {4318440,"S??o Jorge"},
                    {4318457,"S??o Jos?? das Miss??es"},
                    {4318465,"S??o Jos?? do Herval"},
                    {4318481,"S??o Jos?? do Hort??ncio"},
                    {4318499,"S??o Jos?? do Inhacor??"},
                    {4318507,"S??o Jos?? do Norte"},
                    {4318606,"S??o Jos?? do Ouro"},
                    {4318614,"S??o Jos?? do Sul"},
                    {4318622,"S??o Jos?? dos Ausentes"},
                    {4318705,"S??o Leopoldo"},
                    {4318804,"S??o Louren??o do Sul"},
                    {4318903,"S??o Luiz Gonzaga"},
                    {4319000,"S??o Marcos"},
                    {4319109,"S??o Martinho"},
                    {4319125,"S??o Martinho da Serra"},
                    {4319158,"S??o Miguel das Miss??es"},
                    {4319208,"S??o Nicolau"},
                    {4319307,"S??o Paulo das Miss??es"},
                    {4319356,"S??o Pedro da Serra"},
                    {4319364,"S??o Pedro das Miss??es"},
                    {4319372,"S??o Pedro do Buti??"},
                    {4319406,"S??o Pedro do Sul"},
                    {4319505,"S??o Sebasti??o do Ca??"},
                    {4319604,"S??o Sep??"},
                    {4319703,"S??o Valentim"},
                    {4319711,"S??o Valentim do Sul"},
                    {4319737,"S??o Val??rio do Sul"},
                    {4319752,"S??o Vendelino"},
                    {4319802,"S??o Vicente do Sul"},
                    {4319901,"Sapiranga"},
                    {4320008,"Sapucaia do Sul"},
                    {4320107,"Sarandi"},
                    {4320206,"Seberi"},
                    {4320230,"Sede Nova"},
                    {4320263,"Segredo"},
                    {4320305,"Selbach"},
                    {4320321,"Senador Salgado Filho"},
                    {4320354,"Sentinela do Sul"},
                    {4320404,"Serafina Corr??a"},
                    {4320453,"S??rio"},
                    {4320503,"Sert??o"},
                    {4320552,"Sert??o Santana"},
                    {4320578,"Sete de Setembro"},
                    {4320602,"Severiano de Almeida"},
                    {4320651,"Silveira Martins"},
                    {4320677,"Sinimbu"},
                    {4320701,"Sobradinho"},
                    {4320800,"Soledade"},
                    {4320859,"Taba??"},
                    {4320909,"Tapejara"},
                    {4321006,"Tapera"},
                    {4321105,"Tapes"},
                    {4321204,"Taquara"},
                    {4321303,"Taquari"},
                    {4321329,"Taquaru??u do Sul"},
                    {4321352,"Tavares"},
                    {4321402,"Tenente Portela"},
                    {4321436,"Terra de Areia"},
                    {4321451,"Teut??nia"},
                    {4321469,"Tio Hugo"},
                    {4321477,"Tiradentes do Sul"},
                    {4321493,"Toropi"},
                    {4321501,"Torres"},
                    {4321600,"Tramanda??"},
                    {4321626,"Travesseiro"},
                    {4321634,"Tr??s Arroios"},
                    {4321667,"Tr??s Cachoeiras"},
                    {4321709,"Tr??s Coroas"},
                    {4321808,"Tr??s de Maio"},
                    {4321832,"Tr??s Forquilhas"},
                    {4321857,"Tr??s Palmeiras"},
                    {4321907,"Tr??s Passos"},
                    {4321956,"Trindade do Sul"},
                    {4322004,"Triunfo"},
                    {4322103,"Tucunduva"},
                    {4322152,"Tunas"},
                    {4322186,"Tupanci do Sul"},
                    {4322202,"Tupanciret??"},
                    {4322251,"Tupandi"},
                    {4322301,"Tuparendi"},
                    {4322327,"Turu??u"},
                    {4322343,"Ubiretama"},
                    {4322350,"Uni??o da Serra"},
                    {4322376,"Unistalda"},
                    {4322400,"Uruguaiana"},
                    {4322509,"Vacaria"},
                    {4322533,"Vale do Sol"},
                    {4322541,"Vale Real"},
                    {4322525,"Vale Verde"},
                    {4322558,"Vanini"},
                    {4322608,"Ven??ncio Aires"},
                    {4322707,"Vera Cruz"},
                    {4322806,"Veran??polis"},
                    {4322855,"Vespasiano Corr??a"},
                    {4322905,"Viadutos"},
                    {4323002,"Viam??o"},
                    {4323101,"Vicente Dutra"},
                    {4323200,"Victor Graeff"},
                    {4323309,"Vila Flores"},
                    {4323358,"Vila L??ngaro"},
                    {4323408,"Vila Maria"},
                    {4323457,"Vila Nova do Sul"},
                    {4323507,"Vista Alegre"},
                    {4323606,"Vista Alegre do Prata"},
                    {4323705,"Vista Ga??cha"},
                    {4323754,"Vit??ria das Miss??es"},
                    {4323770,"Westf??lia"},
                    {4323804,"Xangri-l??"}
                };
                
                foreach (var city in Cities)
                {
                    string CodeCity = city.Key.ToString();
                    if (CodeCity.Substring(0, 2) == stateCode && Convert.ToInt32(CodeCity.Substring(2, 3)) == cityCode)
                        return true;
                }
                return false;
            }

        }
    }
  
    /// <summary>All Brazilian States. Its value is the Brazilian IBGE state's code.</summary>
    public enum BrazilianStates
    {
        /// <summary>Acre</summary>
        AC = 12,
        /// <summary>Alagoas</summary>
        AL = 27,
        /// <summary>Amap??</summary>
        AP = 16,
        /// <summary>Amazonas</summary>
        AM = 13,
        /// <summary>Bahia</summary>
        BA = 29,
        /// <summary>Cear??</summary>
        CE = 23,
        /// <summary>Federal District (PT-BR: Distrito Federal)</summary>
        DF = 53,
        /// <summary>Esp??rito Santo</summary>
        ES = 32,
        /// <summary>Goi??s</summary>
        GO = 52,
        /// <summary>Maranh??o</summary>
        MA = 21,
        /// <summary>Mato Grosso</summary>
        MT = 51,
        /// <summary>Mato Grosso do Sul</summary>
        MS = 50,
        /// <summary>Minas Gerais</summary>
        MG = 31,
        /// <summary>Par??</summary>
        PA = 15,
        /// <summary>Para??ba</summary>
        PB = 25,
        /// <summary>Paran??</summary>
        PR = 41,
        /// <summary>Pernambuco</summary>
        PE = 26,
        /// <summary>Piau??</summary>
        PI = 22,
        /// <summary>Rio Grande do Norte</summary>
        RN = 24,
        /// <summary>Rio Grande do Sul</summary>
        RS = 43,
        /// <summary>Rio de Janeiro</summary>
        RJ = 33,
        /// <summary>Rond??nia</summary>
        RO = 11,
        /// <summary>Roraima</summary>
        RR = 14,
        /// <summary>Santa Catarina</summary>
        SC = 42,
        /// <summary>S??o Paulo</summary>
        SP = 35,
        /// <summary>Sergipe</summary>
        SE = 28,
        /// <summary>Tocantins</summary>
        TO = 17,
        /// <summary>Other unknown state or Out of Brazil. The code is unofficial from Brazilian IBGE state's Code.</summary>
        ZZ = 99
    }
}