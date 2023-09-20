namespace CrwnClothing.BLL.Helpers
{
    public class PathRegistry
    {
        private static PathRegistry _instance;

        public string ContentRootPath { get; private set; }

        public string WebRootPath { get; private set; }

        public string TemplatePath { get; set; }

        public string ImagesBasePath { get; set; } = string.Empty;

        public string CategoriesPath { get; set; } = string.Empty;

        public string ProductsPath { get; set; } = string.Empty;

        private PathRegistry(string contentRootPath, string webRootPath, string templateRootPath)
        {
            ContentRootPath = contentRootPath;
            WebRootPath = webRootPath;
            TemplatePath = templateRootPath;
            ImagesBasePath = Path.Combine(webRootPath, "images");
            CategoriesPath = "categories";
            ProductsPath = "products";
        }

        public static PathRegistry GetInstance(
            string contentRootPath = null, string webRootPath = null, string templateRootPath = null) =>
            _instance = _instance ?? new PathRegistry(contentRootPath, webRootPath, templateRootPath);
    }
}
