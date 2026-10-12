using System;
using BrazilSharp.Exceptions;

namespace BrazilSharp
{
    /// <summary>
    /// Arithmetic-only verification of Brazilian state registrations (Inscricao Estadual).
    /// Checks format/check digits, NOT whether a registration exists or is active at SEFAZ.
    /// </summary>
    public static partial class Validate
    {
        /// <summary>Checks state-registration check digits without allocating a new string.</summary>
        /// <param name="expression">Registration number; ordinary punctuation is accepted.</param>
        /// <param name="state">Issuing Brazilian state, using the existing IBGE-based enum.</param>
        /// <returns>True if the supplied format and state-specific check digits are consistent.</returns>
        /// <exception cref="StateNotFoundException">The state is not one of the 27 Brazilian UFs.</exception>
        public static bool StateRegistration(ReadOnlySpan<char> expression, BrazilianStates state)
        {
            if (!StateRegistrationCore.IsSupportedState(state))
                throw new StateNotFoundException(ThrowHelper.GetMsgErrorStateRegistration());

            Span<char> digits = stackalloc char[14];
            if (!StateRegistrationCore.TryNormalize(expression, digits, out int length, out bool rural))
                return false;

            return StateRegistrationCore.IsValid(digits.Slice(0, length), state, rural);
        }

        /// <summary>String overload for state-registration validation.</summary>
        /// <param name="expression">Registration text, potentially formatted.</param>
        /// <param name="state">Issuing Brazilian state.</param>
        /// <returns>True if the mathematical validation succeeds.</returns>
        /// <exception cref="StateNotFoundException">Unknown or unsupported state.</exception>
        public static bool StateRegistration(string expression, BrazilianStates state)
        {
            if (expression == null)
                return false;

            return StateRegistration(expression.AsSpan(), state);
        }

        private static class StateRegistrationCore
        {
            internal static bool IsSupportedState(BrazilianStates state)
            {
                switch (state)
                {
                    case BrazilianStates.AC:
                    case BrazilianStates.AL:
                    case BrazilianStates.AP:
                    case BrazilianStates.AM:
                    case BrazilianStates.BA:
                    case BrazilianStates.CE:
                    case BrazilianStates.DF:
                    case BrazilianStates.ES:
                    case BrazilianStates.GO:
                    case BrazilianStates.MA:
                    case BrazilianStates.MT:
                    case BrazilianStates.MS:
                    case BrazilianStates.MG:
                    case BrazilianStates.PA:
                    case BrazilianStates.PB:
                    case BrazilianStates.PR:
                    case BrazilianStates.PE:
                    case BrazilianStates.PI:
                    case BrazilianStates.RJ:
                    case BrazilianStates.RN:
                    case BrazilianStates.RS:
                    case BrazilianStates.RO:
                    case BrazilianStates.RR:
                    case BrazilianStates.SC:
                    case BrazilianStates.SP:
                    case BrazilianStates.SE:
                    case BrazilianStates.TO:
                        return true;
                    default:
                        return false;
                }
            }

            // Only strip recognized formatting; never silently delete unrelated letters.
            internal static bool TryNormalize(
                ReadOnlySpan<char> expression,
                Span<char> destination,
                out int length,
                out bool rural)
            {
                length = 0;
                rural = false;

                if (expression.IsEmpty || expression.Length > 64)
                    return false;

                for (int index = 0; index < expression.Length; index++)
                {
                    char current = expression[index];

                    if (current >= '0' && current <= '9')
                    {
                        if (length == destination.Length)
                            return false;

                        destination[length++] = current;
                    }
                    else if ((current == 'P' || current == 'p') && !rural && length == 0)
                    {
                        rural = true;
                    }
                    else if (current == '.' || current == '-' || current == '/' || char.IsWhiteSpace(current))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (length == 0)
                    return false;

                // A repeated number has no useful identifying content.
                bool repeated = true;
                for (int index = 1; index < length; index++)
                {
                    if (destination[index] != destination[0])
                    {
                        repeated = false;
                        break;
                    }
                }

                return !repeated;
            }

            internal static bool IsValid(ReadOnlySpan<char> number, BrazilianStates state, bool rural)
            {
                if (rural)
                    return state == BrazilianStates.SP && IsSpRural(number);

                switch (state)
                {
                    case BrazilianStates.AC: return IsAc(number);
                    case BrazilianStates.AL: return IsAl(number);
                    case BrazilianStates.AP: return IsAp(number);
                    case BrazilianStates.AM: return IsAm(number);
                    case BrazilianStates.BA: return IsBa(number);
                    case BrazilianStates.CE: return IsCe(number);
                    case BrazilianStates.DF: return IsDf(number);
                    case BrazilianStates.ES: return IsEs(number);
                    case BrazilianStates.GO: return IsGo(number);
                    case BrazilianStates.MA: return IsMa(number);
                    case BrazilianStates.MT: return IsMt(number);
                    case BrazilianStates.MS: return IsMs(number);
                    case BrazilianStates.MG: return IsMg(number);
                    case BrazilianStates.PA: return IsPa(number);
                    case BrazilianStates.PB: return IsPb(number);
                    case BrazilianStates.PR: return IsPr(number);
                    case BrazilianStates.PE: return IsPe(number);
                    case BrazilianStates.PI: return IsPi(number);
                    case BrazilianStates.RJ: return IsRj(number);
                    case BrazilianStates.RN: return IsRn(number);
                    case BrazilianStates.RS: return IsRs(number);
                    case BrazilianStates.RO: return IsRo(number);
                    case BrazilianStates.RR: return IsRr(number);
                    case BrazilianStates.SC: return IsSc(number);
                    case BrazilianStates.SP: return IsSp(number);
                    case BrazilianStates.SE: return IsSe(number);
                    case BrazilianStates.TO: return IsTo(number);
                    default: return false;
                }
            }

            private static int Digit(ReadOnlySpan<char> value, int index) => value[index] - '0';

            private static int Mod11(int sum)
            {
                int remainder = sum % 11;
                return remainder < 2 ? 0 : 11 - remainder;
            }

            private static int Mod11ComplementWithLastDigit(int sum) => (11 - sum % 11) % 10;

            private static int ModulusComplement(int sum, int modulus)
            {
                int remainder = sum % modulus;
                return remainder == 0 || (modulus == 11 && remainder == 1)
                    ? 0
                    : modulus - remainder;
            }

            private static int DescendingSum(ReadOnlySpan<char> digits, int length, int firstWeight)
            {
                int sum = 0;
                for (int index = 0; index < length; index++)
                    sum += Digit(digits, index) * (firstWeight - index);
                return sum;
            }

            private static int AscendingRightSum(
                ReadOnlySpan<char> digits,
                int length,
                int lastWeight = 9,
                int resetWeight = 2)
            {
                int sum = 0;
                int weight = 2;
                for (int index = length - 1; index >= 0; index--)
                {
                    sum += Digit(digits, index) * weight;
                    weight = weight == lastWeight ? resetWeight : weight + 1;
                }
                return sum;
            }

            private static bool IsSimpleNine(ReadOnlySpan<char> digits) =>
                digits.Length == 9 && Mod11(DescendingSum(digits, 8, 9)) == Digit(digits, 8);

            private static bool IsAc(ReadOnlySpan<char> d) =>
                d.Length == 13 && d[0] == '0' && d[1] == '1' &&
                Mod11(AscendingRightSum(d, 11)) == Digit(d, 11) &&
                Mod11(AscendingRightSum(d, 12)) == Digit(d, 12);

            private static bool IsAl(ReadOnlySpan<char> d)
            {
                if (d.Length != 9 || d[0] != '2' || d[1] != '4')
                    return false;

                char kind = d[2];
                if (kind != '0' && kind != '3' && kind != '5' && kind != '7' && kind != '8')
                    return false;

                return (DescendingSum(d, 8, 9) * 10 % 11) % 10 == Digit(d, 8);
            }

            private static bool IsAm(ReadOnlySpan<char> d) => IsSimpleNine(d);

            private static bool IsAp(ReadOnlySpan<char> d)
            {
                if (d.Length != 9 || d[0] != '0' || d[1] != '3')
                    return false;

                int number = 0;
                for (int index = 0; index < 8; index++)
                    number = number * 10 + Digit(d, index);

                if (number < 3000001)
                    return false;

                int p = number <= 3017000 ? 5 : number <= 3019022 ? 9 : 0;
                int specialDigit = number > 3017000 && number <= 3019022 ? 1 : 0;
                int complement = 11 - (DescendingSum(d, 8, 9) + p) % 11;
                int expected = complement == 11 ? specialDigit : complement == 10 ? 0 : complement;

                return expected == Digit(d, 8);
            }

            private static bool IsBa(ReadOnlySpan<char> d)
            {
                if (d.Length != 8 && d.Length != 9)
                    return false;

                // Historical 8-digit numbers use the first digit;
                // 9-digit numbers use the second to choose modulo 10/11.
                int selector = Digit(d, d.Length == 8 ? 0 : 1);
                int modulus = selector == 6 || selector == 7 || selector == 9 ? 11 : 10;
                int baseLength = d.Length - 2;
                int second = ModulusComplement(
                    DescendingSum(d, baseLength, d.Length - 1), modulus);
                int first = ModulusComplement(
                    DescendingSum(d, baseLength, d.Length) + second * 2, modulus);

                return first == Digit(d, baseLength) && second == Digit(d, baseLength + 1);
            }

            private static bool IsCe(ReadOnlySpan<char> d) => IsSimpleNine(d);

            private static bool IsDf(ReadOnlySpan<char> d) =>
                d.Length == 13 && d[0] == '0' && (d[1] == '7' || d[1] == '8') &&
                Mod11(AscendingRightSum(d, 11)) == Digit(d, 11) &&
                Mod11(AscendingRightSum(d, 12)) == Digit(d, 12);

            private static bool IsEs(ReadOnlySpan<char> d) => IsSimpleNine(d);

            private static bool IsGo(ReadOnlySpan<char> d)
            {
                if (d.Length != 9 || !(
                    (d[0] == '1' && (d[1] == '0' || d[1] == '1' || d[1] == '5'))))
                    return false;

                int baseNumber = 0;
                for (int index = 0; index < 8; index++)
                    baseNumber = baseNumber * 10 + Digit(d, index);

                if (baseNumber == 11094402)
                    return d[8] == '0' || d[8] == '1';

                int remainder = DescendingSum(d, 8, 9) % 11;
                int expected = remainder == 0 ? 0
                    : remainder == 1 ? (baseNumber >= 10103105 && baseNumber <= 10119997 ? 1 : 0)
                    : 11 - remainder;
                return Digit(d, 8) == expected;
            }

            private static bool IsMa(ReadOnlySpan<char> d) =>
                d.Length == 9 && d[0] == '1' && d[1] == '2' && IsSimpleNine(d);

            private static bool IsMt(ReadOnlySpan<char> d)
            {
                // MT accepts the 9-digit canonical form and the 11-digit
                // historical transport representation (two initial zeros).
                if (d.Length == 9)
                {
                    Span<char> padded = stackalloc char[11];
                    padded[0] = '0';
                    padded[1] = '0';
                    d.CopyTo(padded.Slice(2));
                    d = padded;
                }
                else if (d.Length != 11)
                {
                    return false;
                }

                return Mod11(AscendingRightSum(d, 10)) == Digit(d, 10);
            }

            private static bool IsMs(ReadOnlySpan<char> d) =>
                d.Length == 9 && d[0] == '2' && d[1] == '8' && IsSimpleNine(d);

            private static bool IsMg(ReadOnlySpan<char> d)
            {
                if (d.Length != 13)
                    return false;

                // The first check digit is a Mod 10 calculation with an
                // inserted 0 after the third digit and alternating weights 1,2.
                int sum = 0;
                for (int index = 0; index < 12; index++)
                {
                    int digit = index == 3 ? 0 : Digit(d, index < 3 ? index : index - 1);
                    int product = digit * (index % 2 == 0 ? 1 : 2);
                    sum += product / 10 + product % 10;
                }

                int first = (10 - sum % 10) % 10;
                return first == Digit(d, 11) &&
                       Mod11(AscendingRightSum(d, 12, 11)) == Digit(d, 12);
            }

            private static bool IsPa(ReadOnlySpan<char> d) =>
                d.Length == 9 && d[0] == '1' && d[1] == '5' && IsSimpleNine(d);

            private static bool IsPb(ReadOnlySpan<char> d) => IsSimpleNine(d);

            private static bool IsPr(ReadOnlySpan<char> d) =>
                d.Length == 10 &&
                Mod11(AscendingRightSum(d, 8, 7)) == Digit(d, 8) &&
                Mod11(AscendingRightSum(d, 9, 7)) == Digit(d, 9);

            private static bool IsPe(ReadOnlySpan<char> d)
            {
                if (d.Length == 9)
                    return Mod11(DescendingSum(d, 7, 8)) == Digit(d, 7) &&
                           Mod11(DescendingSum(d, 8, 9)) == Digit(d, 8);

                if (d.Length != 14)
                    return false;

                int sum = 0;
                int weight = 2;
                for (int index = 12; index >= 0; index--)
                {
                    sum += Digit(d, index) * weight;
                    weight = weight == 9 ? 1 : weight + 1;
                }

                return Mod11ComplementWithLastDigit(sum) == Digit(d, 13);
            }

            private static bool IsPi(ReadOnlySpan<char> d) => IsSimpleNine(d);

            private static bool IsRj(ReadOnlySpan<char> d) =>
                d.Length == 8 && Mod11(AscendingRightSum(d, 7, 7)) == Digit(d, 7);

            private static bool IsRn(ReadOnlySpan<char> d)
            {
                if ((d.Length != 9 && d.Length != 10) || d[0] != '2' || d[1] != '0')
                    return false;

                int sum = 0;
                for (int index = d.Length - 2, weight = 2; index >= 0; index--, weight++)
                    sum += Digit(d, index) * weight;

                return (sum * 10 % 11) % 10 == Digit(d, d.Length - 1);
            }

            private static bool IsRo(ReadOnlySpan<char> d) =>
                d.Length == 14 &&
                Mod11ComplementWithLastDigit(AscendingRightSum(d, 13)) == Digit(d, 13);

            private static bool IsRr(ReadOnlySpan<char> d)
            {
                if (d.Length != 9 || d[0] != '2' || d[1] != '4')
                    return false;

                int sum = 0;
                for (int index = 0; index < 8; index++)
                    sum += Digit(d, index) * (index + 1);

                return sum % 9 == Digit(d, 8);
            }

            private static bool IsRs(ReadOnlySpan<char> d) =>
                d.Length == 10 && Mod11(AscendingRightSum(d, 9)) == Digit(d, 9);

            private static bool IsSc(ReadOnlySpan<char> d) =>
                d.Length == 9 && Mod11(AscendingRightSum(d, 8)) == Digit(d, 8);

            private static int SpFirstDigit(ReadOnlySpan<char> d)
            {
                ReadOnlySpan<byte> weights = stackalloc byte[] { 1, 3, 4, 5, 6, 7, 8, 10 };
                int sum = 0;
                for (int index = 0; index < 8; index++)
                    sum += Digit(d, index) * weights[index];

                return sum % 11 % 10;
            }

            private static bool IsSpRural(ReadOnlySpan<char> d) =>
                d.Length == 12 && SpFirstDigit(d) == Digit(d, 8);

            private static bool IsSp(ReadOnlySpan<char> d)
            {
                if (d.Length != 12 || SpFirstDigit(d) != Digit(d, 8))
                    return false;

                ReadOnlySpan<byte> weights = stackalloc byte[] { 3, 2, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int sum = 0;
                for (int index = 0; index < 11; index++)
                    sum += Digit(d, index) * weights[index];

                return sum % 11 % 10 == Digit(d, 11);
            }

            private static bool IsSe(ReadOnlySpan<char> d) => IsSimpleNine(d);

            private static bool IsTo(ReadOnlySpan<char> d)
            {
                if (d.Length == 9)
                    return IsSimpleNine(d);

                if (d.Length != 11 ||
                    !(d[2] == '0' && (d[3] == '1' || d[3] == '2' || d[3] == '3') ||
                      d[2] == '9' && d[3] == '9'))
                    return false;

                // The two activity digits (positions 3 and 4) do not enter the DV.
                int sum = Digit(d, 0) * 9 + Digit(d, 1) * 8;
                for (int index = 4, weight = 7; index <= 9; index++, weight--)
                    sum += Digit(d, index) * weight;

                return Mod11(sum) == Digit(d, 10);
            }
        }
    }
}
