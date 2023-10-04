using System.ComponentModel.DataAnnotations;

namespace MachineLearningFacialRecognition.Web.Models
{
    public class FileUploadViewModel
    {
        [Required(ErrorMessage = "Please select at least one file.")]
        public List<IFormFile> Files { get; set; }

        [Required(ErrorMessage = "Please enter text.")]
        public string Tag { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
    }
}
