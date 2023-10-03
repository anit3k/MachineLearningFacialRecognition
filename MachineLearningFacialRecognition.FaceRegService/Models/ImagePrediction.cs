namespace MachineLearningFacialRecognition.FaceRegService.Models
{
    public class ImagePrediction : ImageData
    {
        public float[] Score;

        public string PredictedLabelValue;
    }
}
