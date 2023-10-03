namespace MachineLearningFacialRecognition.FileHandler
{
    public interface IFileHandlerService
    {
        void DeleteImage(string fullpath);
        string SaveFile(string base64string);
        void AddToTsvFile(string fileName, string tag);
    }
}