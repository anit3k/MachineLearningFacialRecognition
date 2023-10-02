namespace MachineLearningFacialRecognition.Api.Models
{
    public class ImageTrainDto
    {
        public List<string> Base64Images { get; set; }
        public string Tag { get; set; }
    }
}
