using MachineLearningFacialRecognition.FaceRegService;
using MachineLearningFacialRecognition.FileHandler;
using Microsoft.ML;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MLContext>();
builder.Services.AddScoped<ITrainer, Trainer>();
builder.Services.AddScoped<IPredictor, Predictor>();
builder.Services.AddScoped<IFileHandlerService, FileHandlerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
