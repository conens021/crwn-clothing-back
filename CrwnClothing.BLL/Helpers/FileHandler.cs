namespace CrwnClothing.BLL.Helpers
{
    public static class FileHandler
    {
        public async static Task<string> Save(string file, string folder,string fileType)
        {
            try
            {
                byte[] fileBytes = GetBytesFrom64Base(file, fileType);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = GetUniqueNameFrom64Base(file);

                string fullPath = Path.GetFullPath(Path.Combine(folder, fileName));

                await File.WriteAllBytesAsync(fullPath, fileBytes);


                return fileName;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<string> Save(byte[] fileBytes, string fileName, string folder)
        {
            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fullPath = Path.GetFullPath(Path.Combine(folder, fileName));

                await File.WriteAllBytesAsync(fullPath, fileBytes);


                return fullPath.Replace("\\", "/");

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Delete(string filePath)
        {
            if (!File.Exists(filePath)) throw new Exception("File does not exists");

            File.Delete(filePath);
        }

        private static byte[] GetBytesFrom64Base(string file64Base,string fileType)
        {
            try
            {

                if (Base64Decoder.GetFileType(file64Base) != fileType)
                    throw new Exception("Unsupported media type.");

                string fileContent = Base64Decoder.GetContent(file64Base);

                byte[] fileBytes = Convert.FromBase64String(fileContent);


                return fileBytes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        private static string GetUniqueNameFrom64Base(string file)
        {
            string fileExtension = Base64Decoder.GetExtension(file);

            string uniqueName = Guid.NewGuid().ToString() + "." + fileExtension;


            return uniqueName;
        }
    }
}
