namespace CrwnClothing.BLL.Helpers
{
    public class PathRegistry
    {
        private static PathRegistry _instance;

        public string ContentRootPath { get; private set; }

        public string WebRootPath { get; private set; }

        public string TemplatePath { get; set; }

        private PathRegistry(string contentRootPath, string webRootPath,string templateRootPath)
        {
            ContentRootPath = contentRootPath;
            WebRootPath = webRootPath;
            TemplatePath = templateRootPath;    
        }
   

        public static PathRegistry GetInstance(string contentRootPath = null, string webRootPath = null,string templateRootPath = null) =>
            _instance = _instance ?? new PathRegistry(contentRootPath, webRootPath,templateRootPath);
       
    }
}
