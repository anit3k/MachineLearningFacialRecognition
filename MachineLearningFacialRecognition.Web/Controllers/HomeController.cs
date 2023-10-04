using MachineLearningFacialRecognition.FaceRegService;
using MachineLearningFacialRecognition.FaceRegService.Models;
using MachineLearningFacialRecognition.FileHandler;
using MachineLearningFacialRecognition.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MachineLearningFacialRecognition.Web.Controllers
{
    /// <summary>
    /// This class is used for the website as the only controller needed to handle
    /// the prediction and training of the ml model
    /// </summary>
    public class HomeController : Controller
    {
        #region fields
        private readonly ILogger<HomeController> _logger;
        private readonly IPredictor _predictor;
        private readonly IFileHandlerService _fileHandler;
        private readonly ITrainer _trainer;
        #endregion

        #region Constructor
        public HomeController(ILogger<HomeController> logger, IPredictor predictor, IFileHandlerService fileHandler, ITrainer trainer)
        {
            _logger = logger;
            _predictor = predictor;
            _fileHandler = fileHandler;
            _trainer = trainer;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }


        #region Prediction Page
        [HttpGet]
        public IActionResult Prediction()
        {
            return View(new ImageUploadViewModel());
        }

        [HttpPost]
        public IActionResult Prediction(ImageUploadViewModel model)
        {
            var result = PredictImage(model);            
            return View(AddResultToViewModel(model, result));
        }       
        #endregion

        #region Training Page
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
                AddImagesToTrainerModel(model);
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
        #endregion

        #region Error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region private methods
        private ImagePrediction PredictImage(ImageUploadViewModel model)
        {
            var base64string = ConvertIFormFileToBase64String(model.Image);
            var imageToPredict = _fileHandler.SaveFile(base64string);
            var result = _predictor.ClassifySingleImage(imageToPredict);
            _fileHandler.DeleteImage(imageToPredict);
            return result;
        }

        private ImageUploadViewModel AddResultToViewModel(ImageUploadViewModel model, ImagePrediction result)
        {
            model.PredictionLabel = result.PredictedLabelValue;
            model.PredictionPercentage = result.Score.Max();
            return model;
        }

        private void AddImagesToTrainerModel(FileUploadViewModel model)
        {
            List<string> files = GetBase64StringFromImages(model);
            foreach (var item in files)
            {
                var path = _fileHandler.SaveFile(item);
                _fileHandler.AddToTsvFile(Path.GetFileName(path), model.Tag);
            }
        }

        private List<string> GetBase64StringFromImages(FileUploadViewModel model)
        {
            List<string> files = new List<string>();
            foreach (var file in model.Files)
            {
                var temp = ConvertIFormFileToBase64String(file);
                files.Add(temp);
            }
            return files;
        }

        private string ConvertIFormFileToBase64String(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(bytes);
                return base64String;
            }
        }
        #endregion
    }
}