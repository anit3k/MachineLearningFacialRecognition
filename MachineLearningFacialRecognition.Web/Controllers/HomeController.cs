using MachineLearningFacialRecognition.FaceRegService;
using MachineLearningFacialRecognition.FileHandler;
using MachineLearningFacialRecognition.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.Data;
using System.Diagnostics;

namespace MachineLearningFacialRecognition.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPredictor _predictor;
        private readonly IFileHandlerService _fileHandler;
        private readonly ITrainer _trainer;

        public HomeController(ILogger<HomeController> logger, IPredictor predictor, IFileHandlerService fileHandler, ITrainer trainer)
        {
            _logger = logger;
            _predictor = predictor;
            _fileHandler = fileHandler;
            _trainer = trainer;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Prediction()
        {
            return View(new ImageUploadViewModel());
        }

        [HttpPost]
        public IActionResult Prediction(ImageUploadViewModel model)
        {
            var temp = ConvertIFormFileToBase64(model.Image);

            var imageToPredict = _fileHandler.SaveFile(temp);

            var result = _predictor.ClassifySingleImage(imageToPredict);

            model.PredictionLabel = result.PredictedLabelValue;

            model.PredictionPercentage = result.Score.Max();

            _fileHandler.DeleteImage(imageToPredict);

            return View(model);
        }

        private string ConvertIFormFileToBase64(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Copy the file content to memory stream
                file.CopyTo(memoryStream);

                // Get the byte array from memory stream
                byte[] bytes = memoryStream.ToArray();

                // Convert byte array to Base64 string
                string base64String = Convert.ToBase64String(bytes);

                return base64String;
            }
        }

        [HttpGet]
        public IActionResult Trainer()
        {
            return View(new FileUploadViewModel());
        }

        [HttpPost]
        public IActionResult Trainer(FileUploadViewModel model)
        {
            try
            {
                List<string> files = new List<string>();

                foreach (var file in model.Files)
                {
                    var temp = ConvertIFormFileToBase64(file);
                    files.Add(temp);
                }
                foreach (var item in files)
                {
                    var path = _fileHandler.SaveFile(item);
                    _fileHandler.AddToTsvFile(Path.GetFileName(path), model.Tag);
                }
                _trainer.TrainModel();
                model.StatusMessage = "Success!";
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                model.StatusMessage = "Error occured";
                throw;
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}