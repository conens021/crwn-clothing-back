﻿
namespace CrwnClothing.BLL.Utils
{
    public static class StringUtils
    {
        public static string ToCamelCase(this string value)
        {
            return Char.ToLowerInvariant(value[0]) + value.Substring(1);
        }
    }
}
