namespace MachineLearningFacialRecognition.Web.Models
{
    /// <summary>
    /// This is our dto object when training the ml model uisng the api request
    /// </summary>
    public class ImageTrainDto
    {
        public List<string> Base64Images { get; set; }
        public string Tag { get; set; }
    }
}
