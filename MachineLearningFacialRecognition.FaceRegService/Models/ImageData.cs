﻿using Microsoft.ML.Data;

namespace MachineLearningFacialRecognition.FaceRegService.Models
{
    public class ImageData
    {
        [LoadColumn(0)]
        public string ImagePath;

        [LoadColumn(1)]
        public string Label;
    }
}