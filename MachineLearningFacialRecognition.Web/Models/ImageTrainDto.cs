namespace MachineLearningFacialRecognition.Web.Models
{
    public class ImageTrainDto
    {
        public List<string> Base64Images { get; set; }
        public string Tag { get; set; }
    }
}
