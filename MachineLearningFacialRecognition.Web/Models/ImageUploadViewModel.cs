using MachineLearningFacialRecognition.Web.Validation;
using System.ComponentModel.DataAnnotations;

namespace MachineLearningFacialRecognition.Web.Models
{
    /// <summary>
    /// This is the view model used in the prediction part of our website.
    /// </summary>
    public class ImageUploadViewModel
    {
        [Required(ErrorMessage = "Please select an image.")]
        [Display(Name = "Select Image")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" }, ErrorMessage = "Invalid file format. Allowed formats are .jpg, .jpeg, .png, and .gif.")]
        public IFormFile Image { get; set; }
        public string PredictionLabel { get; set; } = string.Empty;
        public float PredictionPercentage { get; set; }
    }
}
