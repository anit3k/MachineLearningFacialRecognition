using MachineLearningFacialRecognition.Api.Models;
using MachineLearningFacialRecognition.FaceRegService;
using MachineLearningFacialRecognition.FileHandler;
using Microsoft.AspNetCore.Mvc;

namespace MachineLearningFacialRecognition.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainController : Controller
    {
        private readonly ITrainer _trainer;
        private readonly IFileHandlerService _fileHandler;

        public TrainController(ITrainer trainer, IFileHandlerService fileHandler )
        {
            _trainer = trainer;
            _fileHandler = fileHandler;
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult Upload([FromBody] ImageTrainDto dto)
        {
            try
            {
                foreach (var item in dto.Base64Images)
                {
                    var path = _fileHandler.SaveFile(item);
                    _fileHandler.AddToTsvFile(Path.GetFileName(path), dto.Tag);
                }
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
