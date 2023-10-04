using MachineLearningFacialRecognition.FaceRegService;
using MachineLearningFacialRecognition.FileHandler;
using MachineLearningFacialRecognition.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MachineLearningFacialRecognition.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionController : Controller
    {
        private readonly IPredictor _predictor;
        private readonly IFileHandlerService _fileHandler;

        public PredictionController(IPredictor predictor, IFileHandlerService fileHandler)
        {
            _predictor = predictor;
            _fileHandler = fileHandler;
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult Upload([FromBody] ImagePredictionDto dto)
        {
            var imageToPredict = _fileHandler.SaveFile(dto.Base64String);
            
            var result = _predictor.ClassifySingleImage(imageToPredict);

            _fileHandler.DeleteImage(imageToPredict);
            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
