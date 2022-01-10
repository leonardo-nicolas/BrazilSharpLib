using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using BrazilSharp.Exceptions;

namespace BrazilSharp
{

    /// <summary>Set of static methods to validate brazilian's documents, like CPF, CNPJ, voter registration card, Driver License and others.</summary>
    public static partial class Validate
    {

        /// <summary>Use this method to validate a CPF number.</summary>
        /// <param name="Expression">Enter a CPF to validate it.</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is valid CPF. Otherwise, <see langword="false"/>.</returns>
        public static bool CPF(object Expression)
        {
            if (Expression == null)
                return false;
            string strCpf = ConvertObjToString(Expression);
            strCpf = Utilities.TakeOnlyNumbers(strCpf);
            if (strCpf.Length == 0 || strCpf.Length > 11)
                return false;
            while (strCpf.Length < 11)
                strCpf = strCpf.Insert(0, "0");
            int summation = 0;
            int[] checkingDigit = new int[2],
                gotDigit = {
                    Convert.ToInt32(strCpf.Substring(9, 1)),
                    Convert.ToInt32(strCpf.Substring(10, 1))
                };
            for (int index = 0, weight = 10; index < strCpf.Length - 2; ++index, --weight)
                summation += Convert.ToInt32(strCpf.Substring(index, 1)) * weight;
            checkingDigit[0] = summation % 11;
            checkingDigit[0] = checkingDigit[0] < 2 ? 0 : 11 - checkingDigit[0];
            summation = 0;
            for (int index = 0, weight = 11; index < strCpf.Length - 1; ++index, --weight)
                summation += Convert.ToInt32(strCpf.Substring(index, 1)) * weight;
            checkingDigit[1] = summation % 11;
            checkingDigit[1] = checkingDigit[1] < 2 ? 0 : 11 - checkingDigit[1];
            bool result = !Utilities.IsRepeated(strCpf);
            result = result && gotDigit.SequenceEqual(checkingDigit);
            return result;
        }

        /// <summary>Use this method to validate a CNPJ number.</summary>
        /// <param name="Expression">Enter a CNPJ to validate it.</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is valid CNPJ document. Otherwise, <see langword="false"/>.</returns>
        public static bool CNPJ(object Expression)
        {
            if (Expression == null)
                return false;
            string strCnpj = ConvertObjToString(Expression);
            strCnpj = Utilities.TakeOnlyNumbers(strCnpj);
            if (strCnpj.Length == 0 || strCnpj.Length > 14)
                return false;
            while (strCnpj.Length < 14)
                strCnpj = strCnpj.Insert(0, "0");
            int summation = 0, weight = 2;
            int[] checkingDigit = new int[2];
            // Verificando o primeiro DV || Checking First digit
            for (int index = 11; index >= 0; --index)
            {
                summation += Convert.ToInt32(strCnpj.Substring(index, 1)) * weight;
                weight = weight < 9 ? weight + 1 : 2;
            }
            checkingDigit[0] = summation % 11;
            checkingDigit[0] = checkingDigit[0] >= 2 ? 11 - checkingDigit[0] : 0;
            // Verificando o segundo DV || Checking second digit:
            summation = 0;
            weight = 2;
            for (int index = 12; index >= 0; --index)
            {
                summation += Convert.ToInt32(strCnpj.Substring(index, 1)) * weight;
                weight = weight < 9 ? weight + 1 : 2;
            }
            checkingDigit[1] = summation % 11;
            checkingDigit[1] = checkingDigit[1] >= 2 ? 11 - checkingDigit[1] : 0;
            bool result = !Utilities.IsRepeated(strCnpj);
            result = result && Convert.ToInt32(strCnpj.Substring(12, 1)) == checkingDigit[0];
            result = result && Convert.ToInt32(strCnpj.Substring(13, 1)) == checkingDigit[1];
            return result;
        }

        /// <summary>Use this method to validates a national driver's license issued in any Brazilian state territory.</summary>
        /// <param name="Expression">Registry number of Driver License (PT-BR: Número de Registro da CNH) to validate</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is a valid registry number of driver license. Otherwise, <see langword="false"/>.</returns>
        public static bool DriverLicense(object Expression)
        {
            if (Expression == null)
                return false;
            string strBrDriveLicense = ConvertObjToString(Expression);
            strBrDriveLicense = Utilities.TakeOnlyNumbers(strBrDriveLicense);
            if (strBrDriveLicense.Length == 0 || strBrDriveLicense.Length > 11)
                return false;
            while (strBrDriveLicense.Length < 11)
                strBrDriveLicense = strBrDriveLicense.Insert(0, "0");
            int[] summation = new[] { 0, 0 };
            for (int index = 0, weightAsc = 1, weightDesc = 9; index < 9; ++index, --weightDesc, ++weightAsc)
            {
                summation[0] += Convert.ToInt32(strBrDriveLicense.Substring(index, 1)) * weightDesc;
                summation[1] += Convert.ToInt32(strBrDriveLicense.Substring(index, 1)) * weightAsc;
            }
            int[] checkingDigit = new int[] { summation[0] % 11, summation[1] % 11 };
            bool firstChkDigitIsGreaterThan9 = checkingDigit[0] > 9;
            checkingDigit[0] = checkingDigit[0] <= 9 ? checkingDigit[0] : 0;
            if (firstChkDigitIsGreaterThan9) //Differential rule for the remainder of the division of 11 of the first check digit is greater than 9
                checkingDigit[1] = checkingDigit[1] - 2 < 0 ? checkingDigit[1] + 9 : checkingDigit[1] - 2;
            checkingDigit[1] = checkingDigit[1] <= 9 ? checkingDigit[1] : 0; //Adjustment's end
            bool result = !Utilities.IsRepeated(strBrDriveLicense);
            result = result && Convert.ToInt32(strBrDriveLicense.Substring(9, 1)) == checkingDigit[0];
            result = result && Convert.ToInt32(strBrDriveLicense.Substring(10, 1)) == checkingDigit[1];
            return result;
        }

        /// <summary>Validates any <see cref="object"/> and determinate it if is an valid e-mail address.</summary>
        /// <param name="Expression">An e-mail address to test</param>
        /// <returns>Returns <see langword="true"/> if email's <paramref name="Expression"/> is  valid. Otherwise, <see langword="false"/>.</returns>
        public static bool Email(object Expression)
        {
            string regex = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.(com|edu|info|gov|int|mil|net|org|biz|name|museum|online|coop|aero|pro|tv|[a-zA-Z]{2,3})$";
            string data = ConvertObjToString(Expression);
            bool result = Regex.IsMatch(data, regex, RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>Validates a State Registration number (PT-BR:Inscrição Estadual)</summary>
        /// <param name="Expression">State Registration to validate</param>
        /// <param name="state">A Brazilian State that <paramref name="Expression" /> belongs...</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is a valid State Registration. Otherwise, <see langword="false"/>.</returns>
        /// <exception cref="StateNotFoundException"><paramref name="state"/> isn't valid value.</exception>
        public static bool StateRegistration(object Expression, BrazilianStates state)
        {
            if (Expression == null)
                return false;
            var arrBrStates = ((BrazilianStates[])Enum.GetValues(typeof(BrazilianStates))).Where(stt => stt != BrazilianStates.ZZ);
            if (!arrBrStates.Contains(state))
                throw new StateNotFoundException(ThrowHelper.GetMsgErrorStateRegistration());
            string strExp = ConvertObjToString(Expression);
            bool result = strExp.Length > 0;
            result = result && !Utilities.IsRepeated(strExp);
            result = result && Convert.ToBoolean(
                typeof(InternalMethodsValidateIE)
                .GetTypeInfo()?
                .GetMethod("ValidateIE_" + Enum.GetName(typeof(BrazilianStates), state))?
                .Invoke(null, new object[] { strExp })
            );
            return result;
        }

        /// <summary>Test any expression or object and determinate it if is a valid Date or Time!</summary>
        /// <param name="Expression">An expression or object to test</param>
        /// <returns>Returns <see langword="true"/> if is a valid expression for Date or Time or <paramref name="Expression"/> param is <see cref="DateTime"/> or <see cref="TimeSpan"/> object.</returns>
        public static bool DateOrTime(object Expression)
        {
            bool result;
            switch (Expression)
            {
                case char[] chArray:
                    result = DateTime.TryParse(new string(chArray), out _);
                    result = result || TimeSpan.TryParse(new string(chArray), out _);
                    break;
                case Array arr:
                    result = DateTime.TryParse(string.Join("", arr), out _);
                    result = result || TimeSpan.TryParse(string.Join("", arr), out _);
                    break;
                case string str:
                    result = DateTime.TryParse(str, out _);
                    result = result || TimeSpan.TryParse(str, out _);
                    break;
                case DateTime _:
                case TimeSpan _:
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        /// <summary>Test any expression or object and determinate it if is a valid Brazilian Voter Title (Known as "Título de Eleitor" in PT-BR).</summary>
        /// <param name="Expression">Voter Title's number to validate it</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is valid Voter Title document. Otherwise, <see langword="false"/>.</returns>
        public static bool VoterTitle(object Expression)
        {
            string strVoterTitle = ConvertObjToString(Expression);
            strVoterTitle = Utilities.TakeOnlyNumbers(strVoterTitle);
            if (strVoterTitle.Length == 0 || strVoterTitle.Length > 12)
                return false;
            while (strVoterTitle.Length < 12)
                strVoterTitle = strVoterTitle.Insert(0, "0");
            int[] checkingDigit = new int[2],
            gotDigit = {
                Convert.ToInt32(strVoterTitle.Substring(10, 1)),
                Convert.ToInt32(strVoterTitle.Substring(11, 1))
            };
            int summation = 0;
            short stateCode = Convert.ToInt16(strVoterTitle.Substring(8, 2));
            for (int index = 0, weight = 2; index < 8; ++index, ++weight)
                summation += weight * Convert.ToInt32(strVoterTitle.Substring(index, 1));
            checkingDigit[0] = summation % 11;
            checkingDigit[0] = checkingDigit[0] > 9 ? 0 : checkingDigit[0];
            summation = 0;
            for (int index = 8, weight = 7; index < 11; ++index, ++weight)
                summation += weight * Convert.ToInt32(strVoterTitle.Substring(index, 1));
            checkingDigit[1] = summation % 11;
            checkingDigit[1] = checkingDigit[1] > 9 ? 0 : checkingDigit[1];
            bool result = gotDigit.SequenceEqual(checkingDigit) &&
                    Utilities.TREBrazilianStatesCode.Values.Contains(stateCode);
            return result;
        }

        /// <summary>Test any expression and tests it if an Brazilian's PIS number valid. This Method can validate PASEP too. (PT-BR: PIS => Programa de Integração Social / PASEP => Programa de Formação do Patrimônio do Servidor Público).</summary>
        /// <param name="Expression"></param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is a valid Brazilian's PIS number.</returns>
        public static bool PIS(object Expression)
        {
            string strPis = ConvertObjToString(Expression);
            strPis = Utilities.TakeOnlyNumbers(strPis);
            if (strPis.Length == 0 || strPis.Length > 11)
                return false;
            while (strPis.Length < 11)
                strPis = strPis.Insert(0, "0");
            int summation = 0;
            for (int index = 9, weight = 2; index >= 0; --index, weight = weight < 9 ? weight + 1 : 2)
                summation += weight * Convert.ToInt32(strPis.Substring(index, 1));
            int checkingDigit = summation % 11;
            checkingDigit = 11 - checkingDigit;
            checkingDigit = checkingDigit > 9 ? 0 : checkingDigit;
            return Convert.ToInt32(strPis.Substring(10, 1)) == checkingDigit;
        }

        /// <summary>Test any expression and tests it if an Brazilian's RENAVAM's Car number valid.</summary>
        /// <param name="Expression"></param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is a valid Brazilian's RENAVAM number.</returns>
        public static bool Renavam(object Expression)
        {
            string strRenavam = ConvertObjToString(Expression);
            strRenavam = Utilities.TakeOnlyNumbers(strRenavam);
            if (strRenavam.Length == 0 || strRenavam.Length > 11)
                return false;
            int summation = 0;
            for (int index = 9, weight = 2; index >= 0; --index, weight = weight < 9 ? weight + 1 : 2)
                summation += weight * Convert.ToInt32(strRenavam.Substring(index, 1));
            int checkingDigit = summation % 11;
            checkingDigit = 11 - checkingDigit;
            checkingDigit = checkingDigit > 9 ? 0 : checkingDigit;
            bool result = Convert.ToInt32(strRenavam.Substring(10, 1)) == checkingDigit;
            return result;
        }

        /// <summary>Validates a Credit card number issued in Brazil.</summary>
        /// <param name="expression">Number of credit card</param>
        /// <param name="cvc">The security code in the back of card (some cases, in front).</param>
        /// <returns>If the data in parameters is correctly, returns <see langword="true"/>. Otherwise, <see langword="false"/>.</returns>
        public static bool CreditCard(object expression, short cvc)
        {
            // TODO: Add Validation for ELO card
            string strCC = ConvertObjToString(expression);
            strCC = Utilities.TakeOnlyNumbers(strCC);
            if(strCC.Length == 0 || Utilities.IsRepeated(strCC))
                return false;
            var rxCC = new Dictionary<string,(string fullFlag, string rxPattern)>()
            {
                {"mc",("MasterCard",@"^5[1-5][0-9]{14}$|^2(?:2(?:2[1-9]|[3-9][0-9])|[3-6][0-9][0-9]|7(?:[01][0-9]|20))[0-9]{12}$")},
                {"amex",("American Express", @"^3[47][0-9]{13}$/")},
                {"visa",("Visa", @"^4[0-9]{12}(?:[0-9]{3})?$")},
                {"discover",("Discover", @"^65[4-9][0-9]{13}|64[4-9][0-9]{13}|6011[0-9]{12}|(622(?:12[6-9]|1[3-9][0-9]|[2-8][0-9][0-9]|9[01][0-9]|92[0-5])[0-9]{10})$")},
                {"jcb",("JCB", @"^(?:2131|1800|35[0-9]{3})[0-9]{11}$")},
                {"dc",("Dinners Club", @"^3(?:0[0-5]|[68][0-9])[0-9]{11}$")},
            };
            string gotCardFlag = "ukn";
            foreach (var rx in rxCC)
            {
                var ( _ , pattern ) = rx.Value;
                if (Regex.IsMatch(strCC, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline))
                    gotCardFlag = rx.Key;
            }
            bool defaultCvcConditional = cvc >= 0 && cvc <= 999;
            bool[] checkingConditionalCards = new[] {
                // MasterCard, Visa and Discover is default: 16 digits number on its credit card and 3 digits cvc
                (new[] {"mc", "visa", "discover"}).Contains(gotCardFlag) && strCC.Length == 16 && defaultCvcConditional,
                // JCB can have 15 or 16 digits number on its credit card.
                gotCardFlag == "jcb" && (new[]{15,16}).Contains(strCC.Length) && defaultCvcConditional,
                // Amex has 4 digit CVC and 15 digits number on its credit card
                gotCardFlag == "amex" && strCC.Length == 15 && cvc >= 0 && cvc <= 9999,
                // Dinners Club has 14 digits number on its credit card.
                gotCardFlag == "dc" && strCC.Length == 14 && defaultCvcConditional
            };
            // "False" is sure to be obtained. 
            // If don't have any "True" result, it means that credit card number entered hasn't the regex pattern on "rxCC" variable.
            if(!checkingConditionalCards.Contains(true))
                return false;

            int summation = Convert.ToInt32(strCC.Substring(strCC.Length - 1, 1));
            int parity = strCC.Length - 1;
            parity %= 2;
            for (int position = 0; position < (strCC.Length - 1); ++position)
            {
                int currentDigitComputed = Convert.ToInt32(strCC.Substring(position, 1));
                currentDigitComputed *= ((position % 2) == parity) ? 2 : 1;
                currentDigitComputed -= currentDigitComputed > 9 ? 9 : 0;
                summation += currentDigitComputed;
            }
            bool result = (summation % 10) == 0;
            return result;

            throw new NotImplementedException("This method will be available in the future.");
        }

        /// <summary>Validates any expression from any object, to detect if its value is a numeric expression.</summary>
        /// <param name="Expression">An expression to validate it</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is valid numeric (Can be Hex, Octal, decimal, integer or binary).</returns>
        public static bool Numeric(object Expression)
        {
            string strNum;
            bool result;
            switch (Expression)
            {
                case null:
                    result = false;
                    break;
                case string str:
                    strNum = str;
                Label1:
                    try
                    {
                        // Checking if is Binaries number...
                        if (Regex.IsMatch(strNum, @"^(&B|0B)?[0-1]+(&|L|U|UL)?$", RegexOptions.IgnoreCase))
                        {
                            string strBin = strNum;
                            if(strNum.StartsWith("0b") || strNum.StartsWith("0B"))
                                strBin = strNum.Substring(2);
                            strBin = Utilities.TakeOnlyNumbers(strBin);
                            _ = Convert.ToInt64(strBin, 2);
                            result = true;
                        }
                        // Checking if is Visual Basic's Octal Number
                        else if (Regex.IsMatch(strNum, @"^(&O)?[0-7]+(&|L|U|UL)?$", RegexOptions.IgnoreCase))
                        {
                            string strOctNum = strNum;
                            if(strNum.StartsWith("&o") || strNum.StartsWith("&O"))
                                strOctNum = strNum.Substring(2);
                            strOctNum = Utilities.TakeOnlyNumbers(strOctNum);
                            _ = Convert.ToInt64(strOctNum, 8);
                            result = true;
                        }
                        // Checking if is Integer or Regular Numbers
                        else if (Regex.IsMatch(strNum, @"^[0-9]+$"))
                            result = long.TryParse(strNum, out _);
                        // Checking if is a Hex numbers (From many different languages)
                        else if (Regex.IsMatch(strNum, @"^(&H|0x|\#|\$)?[0-9a-fA-F]+(&|L|U|UL)?$", RegexOptions.IgnoreCase))
                        {
                            string strHexNum = strNum;
                            bool startsWithHexId = strNum.StartsWith("0x") || strNum.StartsWith("0X");
                            startsWithHexId = startsWithHexId || strNum.StartsWith("&h") || strNum.StartsWith("&H");
                            if(startsWithHexId)
                                strHexNum = strNum.Substring(2);
                            startsWithHexId = strNum.StartsWith("#") || strNum.StartsWith("$");
                            if(startsWithHexId)
                                strHexNum = strNum.Substring(1);
                            strHexNum = Regex.Replace(strHexNum, @"[^0-9a-fA-F]+", string.Empty);
                            result = long.TryParse(strHexNum, out _);
                        }
                        // If isn't a integer number, then is an decimal number
                        else
                            result = double.TryParse(strNum, NumberStyles.Any, CultureInfo.CurrentCulture, out _);
                    }
                    catch (StackOverflowException) { throw; }
                    catch (ThreadAbortException) { throw; }
                    catch (OutOfMemoryException) { throw; }
                    catch { result = false; }
                    break;
                case char[] chArray:
                    strNum = new string(chArray);
                    goto Label1;
                case Array arr:
                    strNum = string.Join("", arr);
                    goto Label1;
                default:
                    switch (Type.GetTypeCode(Expression.GetType()))
                    {
                        case TypeCode.Byte:
                        case TypeCode.Decimal:
                        case TypeCode.Double:
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.SByte:
                        case TypeCode.Single:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                            result = true;
                            break;
                        case TypeCode.Char:
                            result = char.IsNumber((char)Expression);
                            break;
                        default:
                            result = false;
                            break;
                    }
                    break;
            }
            return result;
        }
    
        private static string ConvertObjToString(object obj)
        {
            switch (obj)
            {
                case null:
                    return string.Empty;
                case char[] chArray:
                    return new string(chArray);
                case string str:
                    return str;
                case Array arr:
                    return string.Join("", arr);
                default:
                    return Convert.ToString(obj);
            }
        }
    }
}