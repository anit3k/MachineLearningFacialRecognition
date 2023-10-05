namespace MachineLearningFacialRecognition.Web.Models
{
    /// <summary>
    /// This is our dto model used in the api when our flutter application is trying to predict an image
    /// </summary>
    public class ImagePredictionDto
    {
        public string Base64String { get; set; }
    }
}
