namespace MachineLearningFacialRecognition.FileHandler
{
    public class FileHandlerService
    {
        private string _imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets//images");
        public string SaveFile(string base64string)
        {
            byte[] imageBytes = Convert.FromBase64String(base64string);
            string uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
            string imagePath = Path.Combine(_imagesFolder, uniqueFileName);
            File.WriteAllBytes(imagePath, imageBytes);
            return imagePath;
        }

        public void DeleteImage(string fullpath)
        {
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
        }
    }
}
