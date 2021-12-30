using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using BrazilSharp.Exceptions;

namespace BrazilSharp {
    
    /// <summary>Set of static methods to validate brazilian's documents, like CPF, CNPJ, voter registration card, Driver License and others.</summary>
    public static partial class Validate {
      
        /// <summary>Use this method to validate a CPF number.</summary>
        /// <param name="Expression">Enter a CPF to validate it.</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is valid CPF. Otherwise, <see langword="false"/>.</returns>
        public static bool CPF(object Expression) {
            if(Expression == null)
                return false;
            string scpf;
            switch (Expression)
            {
                case char[] chArray:
                    scpf = new String(chArray);
                    break;
                case string str:
                    scpf = str;
                    break;
                case Array arr:
                    scpf = String.Join("", arr);
                    break;
                default:
                    scpf = Convert.ToString(Expression);
                    break;
            }
            scpf = Regex.Replace(scpf, @"[^0-9]+", string.Empty);
            if(scpf.Length == 0 || scpf.Length > 11)
                return false;
            while(scpf.Length < 11) 
                scpf = scpf.Insert(0, "0");
            int summation;
            int[] CheckingDigit = new int[2], gotDigit = {Convert.ToInt32(scpf.Substring(9, 1)),Convert.ToInt32(scpf.Substring(10, 1))};
            summation = 0;
            for (int index = 0, weight = 10; index < scpf.Length - 2; ++index, --weight) 
                summation += Convert.ToInt32(scpf.Substring(index, 1)) * weight;
            CheckingDigit[0] = summation % 11;
            CheckingDigit[0] = CheckingDigit[0] < 2 ? 0 : 11 - CheckingDigit[0];
            summation = 0;
            for (int index = 0, weight = 11; index < scpf.Length - 1; ++index, --weight)
                summation += Convert.ToInt32(scpf.Substring(index, 1)) * weight;
            CheckingDigit[1] = summation % 11;
            CheckingDigit[1] = CheckingDigit[1] < 2 ? 0 : 11 - CheckingDigit[1];
            return !IsRepeated(scpf) && gotDigit.SequenceEqual(CheckingDigit);
        }

        /// <summary>Use this method to validate a CNPJ number.</summary>
        /// <param name="Expression">Enter a CNPJ to validate it.</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is valid CNPJ document. Otherwise, <see langword="false"/>.</returns>
        public static bool CNPJ(object Expression) {
            if (Expression == null)
                return false;
            string scnpj;
            switch (Expression)
            {
                case char[] chArray:
                    scnpj = new String(chArray);
                    break;
                case string str:
                    scnpj = str;
                    break;
                case Array arr:
                    scnpj = String.Join("", arr);
                    break;
                default:
                    scnpj = Convert.ToString(Expression);
                    break;
            }
            scnpj = Regex.Replace(scnpj, @"[^0-9]+", string.Empty);
            if (scnpj.Length == 0 || scnpj.Length > 14)
                return false;
            while (scnpj.Length < 14)
                scnpj = scnpj.Insert(0, "0");
            int summation, weight;
            int[] CheckingDigit = new int[2];
            // Verificando o primeiro DV || Checking First digit
            summation = 0;
            weight = 2;
            for (int index = 11; index >= 0; --index) {
                summation += Convert.ToInt32(scnpj.Substring(index, 1)) * weight;
                weight = weight < 9 ? weight + 1 : 2;
            }
            CheckingDigit[0] = summation % 11;
            CheckingDigit[0] = CheckingDigit[0] >= 2 ? 11 - CheckingDigit[0] : 0;
            // Verificando o segundo DV || Checking second digit:
            summation = 0;
            weight = 2;
            for (int index = 12; index >= 0; --index) {
                summation += Convert.ToInt32(scnpj.Substring(index, 1)) * weight;
                weight = weight < 9 ? weight + 1 : 2;
            }
            CheckingDigit[1] = summation % 11;
            CheckingDigit[1] = CheckingDigit[1] >= 2 ? 11 - CheckingDigit[1] : 0;
            return !IsRepeated(scnpj) && Convert.ToInt32(scnpj.Substring(12, 1)) == CheckingDigit[0] && Convert.ToInt32(scnpj.Substring(13, 1)) == CheckingDigit[1];
        }

        /// <summary>Use this method to validates a national driver's license issued in any Brazilian state territory.</summary>
        /// <param name="Expression">Registry number of Driver License (PT-BR: Número de Registro da CNH) to validate</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is a valid registry number of driver license. Otherwise, <see langword="false"/>.</returns>
        public static bool DriverLicense(object Expression) {
            if(Expression == null)
                return false;
            string sdl;
            switch (Expression)
            {
                case char[] chArray:
                    sdl = new String(chArray);
                    break;
                case string str:
                    sdl = str;
                    break;
                case Array arr:
                    sdl = String.Join("", arr);
                    break;
                default:
                    sdl = Convert.ToString(Expression);
                    break;
            }
            sdl = Regex.Replace(sdl, @"[^0-9]+", string.Empty);
            if(sdl.Length == 0 || sdl.Length > 11)
                return false;
            while(sdl.Length < 11) 
                sdl = sdl.Insert(0, "0");
            int[] summation,CheckingDigit;
            summation = new int[] { 0, 0 };
            for (int index = 0, weightAsc = 1, weightDesc = 9; index < 9; ++index, --weightDesc, ++weightAsc) {
                summation[0] += Convert.ToInt32(sdl.Substring(index,1)) * weightDesc;
                summation[1] += Convert.ToInt32(sdl.Substring(index,1)) * weightAsc;
            }
            CheckingDigit = new int[] { summation[0] % 11, summation[1] % 11 };
            bool FirstChkDigitGreaterThanNine = CheckingDigit[0] > 9;
            CheckingDigit[0] = CheckingDigit[0] <= 9 ? CheckingDigit[0] : 0;
            if (FirstChkDigitGreaterThanNine) //Differential rule for the remainder of the division of 11 of the first check digit is greater than 9
                CheckingDigit[1] = CheckingDigit[1] - 2 < 0 ? CheckingDigit[1] + 9 : CheckingDigit[1] - 2;
            CheckingDigit[1] = CheckingDigit[1] <= 9 ? CheckingDigit[1] : 0; //Adjustment's end
            return !IsRepeated(sdl) && Convert.ToInt32(sdl.Substring(9,1)) == CheckingDigit[0] && Convert.ToInt32(sdl.Substring(10,1)) == CheckingDigit[1];
        }

        /// <summary>Validates any <see cref="String"/> and determinate it if is an valid e-mail address.</summary>
        /// <param name="Expression">An e-mail address to test</param>
        /// <returns>Returns <see langword="true"/> if email's <paramref name="Expression"/> is  valid. Otherwise, <see langword="false"/>.</returns>
        public static bool Email(object Expression)
        {
            string regex = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.(com|edu|info|gov|int|mil|net|org|biz|name|museum|online|coop|aero|pro|tv|[a-zA-Z]{2,3})$";
            string data;switch (Expression)
            {
                case null:
                    data = string.Empty;
                    break;
                case char[] chArray:
                    data = new String(chArray);
                    break;
                case string str:
                    data = str;
                    break;
                case Array arr:
                    data = String.Join("", arr);
                    break;
                default:
                    data = Convert.ToString(Expression);
                    break;
            }
            bool result;
            result = Regex.IsMatch(data, regex, RegexOptions.IgnoreCase);
            return result;
        }
        
        /// <summary>Validates a State Registration number (PT-BR:Inscrição Estadual)</summary>
        /// <param name="Expression">State Registration to validate</param>
        /// <param name="state">A Brazilian State that <paramref name="Expression" /> belongs...</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is a valid State Registration. Otherwise, <see langword="false"/>.</returns>
        /// <exception cref="StateNotFoundException"><paramref name="state"/> isn't valid value.</exception>
        public static bool StateRegistration(object Expression, BrazilianStates state) {
            if (Expression == null)
                return false;
            BrazilianStates[] arrbs = (BrazilianStates[])Enum.GetValues(typeof(BrazilianStates));
            if (state == BrazilianStates.ZZ || (int)state == 99 || !arrbs.Contains(state))
                throw new StateNotFoundException(ThrowHelper.GetMsgErrorStateRegistration());
            string strExp;
            switch (Expression)
            {
                case char[] chArray:
                    strExp = new String(chArray);
                    break;
                case string str:
                    strExp = str;
                    break;
                case Array arr:
                    strExp = String.Join("", arr);
                    break;
                default:
                    strExp = Convert.ToString(Expression);
                    break;
            }
            bool result = strExp.Length > 0;
            result = result && !IsRepeated(strExp);
            result = result && Convert.ToBoolean(
                
                typeof(_InternalMethodsValidateIE)
                .GetTypeInfo()
                .GetMethod(
                    "ValidateIE_" + 
                    Enum.GetName(typeof(BrazilianStates), state)
                )
                .Invoke(
                    null, 
                    new string[] { strExp }
                )
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
                    result = DateTime.TryParse(new String(chArray), out _);
                    result = result || TimeSpan.TryParse(new String(chArray), out _);
                    break;
                case Array arr:
                    result = DateTime.TryParse(String.Join("", arr), out _);
                    result = result || TimeSpan.TryParse(String.Join("", arr), out _);
                    break;
                case string str:
                    result = DateTime.TryParse(str, out _);
                    result = result || TimeSpan.TryParse(str, out _);
                    break;
                case DateTime dt:
                case TimeSpan ts:
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
        public static bool VoterTitle(object Expression) {
            string svt ;
            switch (Expression)
            {
                case null:
                    svt = string.Empty;
                    break;
                case char[] chArray:
                    svt = new String(chArray);
                    break;
                case string str:
                    svt = str;
                    break;
                case Array arr:
                    svt = String.Join("", arr);
                    break;
                default:
                    svt = Convert.ToString(Expression);
                    break;
            }
            svt = Regex.Replace(svt, @"[^0-9]+", string.Empty);
            if(svt.Length == 0 || svt.Length > 12)
                return false;
            while(svt.Length < 12)
                svt = svt.Insert(0, "0");
            int[] CheckingDigit = new int[2], gotDigit={Convert.ToInt32(svt.Substring(10, 1)),Convert.ToInt32(svt.Substring(11,1))};
            int summation = 0;
            short stateCode = Convert.ToInt16(svt.Substring(8, 2));
            for (int index = 0, weight = 2; index < 8; ++index, ++weight)
                summation += weight * Convert.ToInt32(svt.Substring(index,1));
            CheckingDigit[0] = summation % 11;
            CheckingDigit[0] = CheckingDigit[0] > 9 ? 0 : CheckingDigit[0];
            summation = 0;
            for (int index = 8, weight = 7; index < 11; ++index, ++weight)
                summation += weight * Convert.ToInt32(svt.Substring(index,1));
            CheckingDigit[1] = summation % 11;
            CheckingDigit[1] = CheckingDigit[1] > 9 ? 0 : CheckingDigit[1];
            return gotDigit.SequenceEqual(CheckingDigit) &&
                    Utilities.TREBrazilianStatesCode.Values.Contains(stateCode);
        }

        /// <summary>Test any expression and tests it if an Brazilian's PIS number valid. This Method can validate PASEP too. (PT-BR: PIS => Programa de Integração Social / PASEP => Programa de Formação do Patrimônio do Servidor Público).</summary>
        /// <param name="Expression"></param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is a valid Brazilian's PIS number.</returns>
        public static bool PIS(object Expression) {
            string spis;
            switch (Expression)
            {
                case null:
                    spis = string.Empty;
                    break;
                case char[] chArray:
                    spis = new String(chArray);
                    break;
                case string str:
                    spis = str;
                    break;
                case Array arr:
                    spis = String.Join("", arr);
                    break;
                default:
                    spis = Convert.ToString(Expression);
                    break;
            }
            spis = Regex.Replace(spis, @"[^0-9]+", string.Empty);
            if(spis.Length == 0 || spis.Length > 11)
                return false;
            while(spis.Length < 11)
                spis = spis.Insert(0, "0");
            int summation = 0;
            for(int index = 9, weight = 2; index >= 0; --index, weight = weight < 9 ? weight + 1 : 2)
                summation += weight * Convert.ToInt32(spis.Substring(index, 1));
            int CheckingDigit = summation % 11;
            CheckingDigit = 11 - CheckingDigit;
            CheckingDigit = CheckingDigit > 9 ? 0 : CheckingDigit;
            return Convert.ToInt32(spis.Substring(10, 1)) == CheckingDigit;
        }

        /// <summary>Test any expression and tests it if an Brazilian's RENAVAM's Car number valid.</summary>
        /// <param name="Expression"></param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is a valid Brazilian's RENAVAM number.</returns>
        public static bool Renavam(object Expression) {
            string srenavam;
            switch (Expression)
            {
                case null:
                    srenavam = string.Empty;
                    break;
                case char[] chArray:
                    srenavam = new String(chArray);
                    break;
                case string str:
                    srenavam = str;
                    break;
                case Array arr:
                    srenavam = String.Join("", arr);
                    break;
                default:
                    srenavam = Convert.ToString(Expression);
                    break;
            }
            srenavam = Regex.Replace(srenavam, @"[^0-9]+", string.Empty);
            if(srenavam.Length == 0 || srenavam.Length > 11)
                return false;
            int summation = 0;
            for(int index = 9, weight = 2; index >= 0; --index, weight= weight < 9 ? weight + 1 : 2)
                summation += weight * Convert.ToInt32(srenavam.Substring(index, 1));
            int CheckingDigit = summation % 11;
            CheckingDigit = 11 - CheckingDigit;
            CheckingDigit = CheckingDigit > 9 ? 0 : CheckingDigit;
            return Convert.ToInt32(srenavam.Substring(10, 1)) == CheckingDigit;
        }

        /// <summary>Validates a Credit card number...</summary>
        /// <param name="Expression">Number of credit card</param>
        /// <param name="dueDate">A due date of credit card</param>
        /// <param name="cvc">The security code in the back of card (some cases, in front).</param>
        /// <returns>If the data in parameters is correctly, returns <see langword="true"/>. Otherwise, <see langword="false"/>.</returns>
        [Obsolete("This method will be available in the future!")]
        private static bool CreditCard(object Expression, DateTime dueDate, short cvc) {
            throw new NotImplementedException("This method will be available in the future.");
            // string svt = Expression switch {
            //     null => string.Empty,
            //     char[] chArray => new String(chArray),
            //     Array arr => String.Join("",arr),
            //     _ => Convert.ToString(Expression)
            // };            
            // return false;
        }

        /// <summary>Validates any expression from any object, to detect if its value is a numeric expression.</summary>
        /// <param name="Expression">An expression to validate it</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="Expression"/> is valid numeric (Can be Hex, Octal, decimal, integer or binary).</returns>
        public static bool Numeric(object Expression) {
            string str;
            bool result;
            switch (Expression) {
                case null: 
                    result = false; 
                    break;
                case string strE:
                    str = strE;
                Label1:
                    try {
                        // Checking if is Binaries number...
                        if (Regex.IsMatch(str, @"^(&B|0B)?[0-1]+(&|L|U|UL)?$",RegexOptions.IgnoreCase)) 
                            _ = Convert.ToInt64(Regex.Replace(str, @"[^0-1]", string.Empty), 2);
                        // Checking if is Visual Basic's Octal Number
                        else if (Regex.IsMatch(str, @"^(&O)?[0-7]+(&|L|U|UL)?$",RegexOptions.IgnoreCase)) 
                            _ = Convert.ToInt64(Regex.Replace(str, @"[^0-7]+", string.Empty), 8);
                        // Checking if is Integer or Regular Numbers
                        else if (Regex.IsMatch(str, @"^[0-9]+$")) 
                            _ = Convert.ToInt64(str, 10);
                        // Checking if is a Hex numbers (From many different languages)
                        else if (Regex.IsMatch(str, @"^(&H|0x|\#|\$)?[0-9a-fA-F]+(&|L|U|UL)?$",RegexOptions.IgnoreCase)) 
                            _ = Convert.ToInt64(Regex.Replace(str, @"[^0-9a-fA-F]+", string.Empty), 16);
                        // If isn't a integer number, then is an decimal number
                        else
                            _ = Convert.ToDouble(str);
                        result = true;
                    }
                    catch (OutOfMemoryException) { throw; }
                    catch { result = false; }
                    break;
                case char[] chArray:
                    str = new string(chArray);
                    goto Label1;
                case Array arr:
                    str = String.Join("", arr);
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

        private static bool IsRepeated(string str)
        {
            int summation = 1;
            for(int current = 1; current < str.Length; ++current)
                if(str[current] == str[0])
                    ++summation;
            return summation == str.Length;
        }
    }
}
