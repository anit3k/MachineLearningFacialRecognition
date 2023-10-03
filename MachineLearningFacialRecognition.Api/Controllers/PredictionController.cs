using MachineLearningFacialRecognition.Api.Models;
using MachineLearningFacialRecognition.FaceRegService;
using MachineLearningFacialRecognition.FileHandler;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MachineLearningFacialRecognition.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionController : Controller
    {
        [HttpPost]
        [Route("upload")]
        public IActionResult Upload([FromBody] ImagePredictionDto dto)
        {
            FileHandlerService fileHandlerService = new FileHandlerService();
            var imageToPredict = fileHandlerService.SaveFile(dto.Base64String);
            FacialRecognitionService service = new FacialRecognitionService();
            var result = service.Run(imageToPredict);
            return Ok(JsonConvert.SerializeObject(result));
        }
    }
}
