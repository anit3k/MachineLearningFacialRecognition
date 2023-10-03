using MachineLearningFacialRecognition.Api.Models;
using MachineLearningFacialRecognition.FaceRegService;
using Microsoft.AspNetCore.Mvc;

namespace MachineLearningFacialRecognition.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainController : Controller
    {
        private readonly ITrainer _trainer;

        public TrainController(ITrainer trainer)
        {
            _trainer = trainer;
        }
        [HttpPost]
        [Route("upload")]
        public IActionResult Upload([FromBody] ImageTrainDto dto)
        {
            try
            {
                _trainer.TrainModel();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
            return Ok("Succes!");
        }
    }
}
