namespace CrwnClothing.BLL.Utils
{
    public static class CurrencyUtils
    {
        public static long ConvertDollarsToCents(decimal amount)
        {

            return Convert.ToInt64(amount * 100);
        }

        public static decimal ConvertCentsToDollars(long amount)
        {

            return Convert.ToDecimal(Convert.ToDecimal(amount) / 100.00m);
        }
    }
}
