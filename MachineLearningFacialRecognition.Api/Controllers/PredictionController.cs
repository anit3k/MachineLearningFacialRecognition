using MachineLearningFacialRecognition.Api.Models;
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

            return Ok("Succes!");
        }
    }
}
