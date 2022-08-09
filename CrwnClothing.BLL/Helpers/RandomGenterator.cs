using System.Text;

namespace CrwnClothing.BLL.Helpers
{
    public static class RandomGenterator
    {


        public static string GetRandomDigitsCode(int length)
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(random.Next(0, 10).ToString());
            }

            return sb.ToString();
        }
    }
}
