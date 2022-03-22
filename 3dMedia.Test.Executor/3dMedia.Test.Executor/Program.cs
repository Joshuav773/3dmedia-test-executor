using _3dMedia.Test.Executor.Config;
using _3dMedia.Test.Executor.Data.Context;
using _3dMedia.Test.Executor.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var _config = new ConfigurationBuilder()
    .AddJsonFile(@"appsettings.json")
    .Build();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
      "CorsPolicy",
      builder => builder.WithOrigins("http://localhost:4200")
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials());
});

builder.Services.Configure<AppSettings>(_config.GetSection(nameof(AppSettings)));
builder.Services.Configure<JenkinsSettings>(_config.GetSection(nameof(JenkinsSettings)));

builder.Services.AddTransient<IJenkinsService, JenkinsService>();

builder.Services.AddDbContext<BeamDbContext>(options => options.UseNpgsql(_config.GetConnectionString("BeamDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
