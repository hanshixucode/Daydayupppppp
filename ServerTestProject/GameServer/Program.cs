using CommonLib.MongoDB;
using GameServer.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
//依赖注入
builder.Services.AddScoped<PlayerService>();
//mongodb
builder.Services.Configure<MongoDBSetting>(builder.Configuration.GetSection("MongoDB"));
//builder.Services.TryAddSingleton<IMongoDb>((provider => new MongoDBService(provider, nameof(MongoDBSetting))));
builder.Services.AddSingleton<MongoDBService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();