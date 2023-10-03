using MachineLearningFacialRecognition.FaceRegService.Models;

namespace MachineLearningFacialRecognition.FaceRegService
{
    public interface IPredictor
    {
        ImagePrediction ClassifySingleImage(string imagePath);
    }
}