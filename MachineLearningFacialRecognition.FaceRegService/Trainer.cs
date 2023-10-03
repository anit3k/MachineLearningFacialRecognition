using MachineLearningFacialRecognition.FaceRegService.Models;
using Microsoft.ML;

namespace MachineLearningFacialRecognition.FaceRegService
{
    public class Trainer : ITrainer
    {
        static string dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string _modelFolderpath = Path.Combine(dataFolder, "modeltrainer");

        private string _imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets//images");
        private string _trainTagsTsv = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets//images//tags.tsv");
        private string _inceptionTensorFlowModel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets//inception//tensorflow_inception_graph.pb");
        private readonly MLContext _mlContext;

        public Trainer(MLContext mLContext)
        {
            _mlContext = mLContext;
        }

        public void TrainModel()
        {
            IEstimator<ITransformer> pipeline = _mlContext.Transforms.LoadImages(outputColumnName: "input", imageFolder: _imagesFolder, inputColumnName: nameof(ImageData.ImagePath))
                            .Append(_mlContext.Transforms.ResizeImages(outputColumnName: "input", imageWidth: InceptionSettings.ImageWidth, imageHeight: InceptionSettings.ImageHeight, inputColumnName: "input"))
                            .Append(_mlContext.Transforms.ExtractPixels(outputColumnName: "input", interleavePixelColors: InceptionSettings.ChannelsLast, offsetImage: InceptionSettings.Mean))
                            .Append(_mlContext.Model.LoadTensorFlowModel(_inceptionTensorFlowModel).
                                ScoreTensorFlowModel(outputColumnNames: new[] { "softmax2_pre_activation" }, inputColumnNames: new[] { "input" }, addBatchDimensionInput: true))
                            .Append(_mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "LabelKey", inputColumnName: "Label"))
                            .Append(_mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "LabelKey", featureColumnName: "softmax2_pre_activation"))
                            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabelValue", "PredictedLabel"))
                            .AppendCacheCheckpoint(_mlContext);

            IDataView trainingData = _mlContext.Data.LoadFromTextFile<ImageData>(path: _trainTagsTsv, hasHeader: false);

            ITransformer model = pipeline.Fit(trainingData);

            _mlContext.Model.Save(model, trainingData.Schema, _modelFolderpath);
        }
    }
}
