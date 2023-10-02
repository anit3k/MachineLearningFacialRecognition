using MachineLearningFacialRecognition.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MachineLearningFacialRecognition.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainController : Controller
    {
        [HttpPost]
        [Route("upload")]
        public IActionResult Upload([FromBody] ImageTrainDto dto)
        {

            return Ok("Succes!");
        }
    }
}
