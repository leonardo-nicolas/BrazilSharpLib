using System;

namespace BrazilSharp.Calculations
{
    // TODO: Add calculations for financial and another calculations for brazilians' labors.

    /// <summary>Set of static methods for calculating labor debts, based on the consolidation of Brazilian labor laws.</summary>
    public static class WorkersCalculation
    {
        /// <summary>Calculates the 13th proportional of an employee, based on Brazilian labor laws, for employees.</summary>
        /// <param name="CalculationBasis">Value of the employee's salary calculation basis.</param>
        /// <param name="EarlyWarning">In the case of unfair dismissal, Did the employee have prior notice?</param>
        /// <param name="Regime">Do you want to calculate based on days or months?</param>
        /// <param name="HowLongWorked">How long has the employee worked since the last vesting period?</param>
        /// <returns>The calculation made of the proportional 13th salary, based on the information provided in the parameters.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Its happen when <paramref name="Regime"/> is <see name="PeriodType.Day"/> and <paramref name="HowLongWorked"/> is less than 1 or greater than 31. Or else, its happen when <paramref name="Regime"/> is <see name="PeriodType.Month"/> and <paramref name="HowLongWorked"/> is less than 1 or greater than 12.</exception>
        public static double Thirteenth(double CalculationBasis, bool EarlyWarning, PeriodType Regime, short HowLongWorked) {
            if ((Regime == PeriodType.Day && (HowLongWorked < 1 || HowLongWorked > 31)) || (Regime == PeriodType.Month && (HowLongWorked < 1 || HowLongWorked > 12)))
                throw new ArgumentOutOfRangeException(ThrowHelper.GetMsgErrorThirteenthOutOfRange(Regime), nameof(HowLongWorked));
            double avos;
            if (Regime == PeriodType.Month)
                avos = (HowLongWorked + (EarlyWarning ? 1D : 0D)) / 12D;
            else if (Regime == PeriodType.Day)
                if (HowLongWorked < 15)
                    avos = 0D;
                else
                    avos = EarlyWarning ? 2D : 1D;
            else 
                avos = 1D;
            return avos * CalculationBasis;
        }
    }
}