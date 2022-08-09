using System.Reflection;
using CrwnClothing.BLL.Models;
using CrwnClothing.BLL.Utils;

namespace CrwnClothing.BLL.Services.TemplateService
{
    public static class TemplateEngine<T>
    {
        public static async Task<string> GenerateFromFile(string templateFileUrl, T model)
        {
            var dictionary = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .ToDictionary(p => p.Name.ToCamelCase(), p => (p.GetValue(model, null) ?? string.Empty).ToString());

            string templateFile = await File.ReadAllTextAsync(templateFileUrl);


            foreach (KeyValuePair<string, string> entry in dictionary)
            {
                string templateString = "{" + entry.Key + "}";
                templateFile = templateFile.Replace(templateString, entry.Value);
            }


            return templateFile;
        }
    }
}
