namespace MachineLearningFacialRecognition.Web.Models
{
    /// <summary>
    /// View model for errors
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}