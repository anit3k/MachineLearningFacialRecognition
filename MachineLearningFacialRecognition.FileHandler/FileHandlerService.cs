namespace MachineLearningFacialRecognition.FileHandler
{
    public class FileHandlerService : IFileHandlerService
    {
        private string _imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\images");
        private string _trainTagsTsv = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\images\\tags.tsv");
        public string SaveFile(string base64string)
        {
            byte[] imageBytes = Convert.FromBase64String(base64string);
            string uniqueFileName = GetNewFileName();
            string imagePath = Path.Combine(_imagesFolder, uniqueFileName);
            File.WriteAllBytes(imagePath, imageBytes);
            return imagePath;
        }

        private string GetNewFileName()
        {
            return Guid.NewGuid().ToString() + ".jpg";
        }

        public void DeleteImage(string fullpath)
        {
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
        }

        public void AddToTsvFile(string fileName, string tag)
        {
            string tsvLine = $"{fileName}\t{tag}";
            File.AppendAllText(_trainTagsTsv, tsvLine + Environment.NewLine);
        }
    }
}
