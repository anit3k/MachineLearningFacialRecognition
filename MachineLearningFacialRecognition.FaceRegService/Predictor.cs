using MachineLearningFacialRecognition.FaceRegService.Models;
using Microsoft.ML;

namespace MachineLearningFacialRecognition.FaceRegService
{
    public class Predictor : IPredictor
    {
        private readonly MLContext _mlContext;

        static string dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string _modelFolderPath = Path.Combine(dataFolder, "modeltrainer");
        private ITransformer _model;

        public Predictor(MLContext mlContext)
        {
            _mlContext = mlContext;
        }

        public ImagePrediction ClassifySingleImage(string imagePath)
        {
            LoadModel();
            if (_model == null)
            {
                throw new InvalidOperationException("Model has not been loaded. Call LoadModel method first.");
            }

            var imageData = new ImageData()
            {
                ImagePath = imagePath
            };

            var predictor = _mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(_model);
            var prediction = predictor.Predict(imageData);

            Console.WriteLine("=============== Making single image classification ===============");
            Console.WriteLine($"Image: {Path.GetFileName(imageData.ImagePath)} predicted as: {prediction.PredictedLabelValue} with score: {prediction.Score.Max()} ");

            return prediction;
        }
        private void LoadModel()
        {
            _model = _mlContext.Model.Load(_modelFolderPath, out var modelSchema);
        }
    }
}
