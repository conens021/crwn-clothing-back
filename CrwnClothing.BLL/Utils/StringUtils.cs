
using CrwnClothing.BLL.Helpers;
using System.Text;

namespace CrwnClothing.BLL.Utils
{
    public static class StringUtils
    {
        public static string ToCamelCase(this string value)
        {
            return Char.ToLowerInvariant(value[0]) + value.Substring(1);
        }

        public static string FullNameToUserName(this string value)
        {
            try
            {
                string fullNameLower = value.Trim().ToLower();

                List<string> fullNameArray = fullNameLower.Split(" ").ToList<string>();

                string? firstName = fullNameArray.FirstOrDefault();

                string? lastName = fullNameArray.LastOrDefault();

                StringBuilder sb = new StringBuilder();

                sb.Append(lastName == null || lastName == string.Empty ? "" : lastName[0]);

                sb.Append(lastName == null ? "" : "_");

                sb.Append(firstName ?? "user");

                string sixDigitsCode = RandomGenerator.GetRandomDigitsCode(6);

                sb.Append(sixDigitsCode);


                return sb.ToString();

            }
            catch (Exception)
            {
                string tenDigitsCode = RandomGenerator.GetRandomDigitsCode(6);


                return "user" + tenDigitsCode;
            }
        }

    }
}
