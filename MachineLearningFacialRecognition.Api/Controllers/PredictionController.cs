using MachineLearningFacialRecognition.Api.Models;
using MachineLearningFacialRecognition.FaceRegService;
using Microsoft.AspNetCore.Mvc;

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
            FacialRecognitionService service = new FacialRecognitionService();
            service.Run();
            return Ok("Succes!");
        }
    }
}
