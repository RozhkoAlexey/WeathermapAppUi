using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using WeathermapApp;
using WeathermapApp.Core;
using WeathermapApp.Core.Middlewares;
using WeathermapApp.DAL;
using WeathermapApp.DAL.Dto;
using WeathermapApp.DAL.Mapping;
using WeathermapApp.DAL.Repositories;
using WeathermapApp.DAL.Repositories.Interfaces;
using WeathermapApp.DAL.Services;
using WeathermapApp.DAL.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var corsName = "corsapp";

// Add services to the container.
builder.Services.AddAutoMapper(typeof(HistoryQueryProfile));

builder.Services.AddControllers();


builder.Services.AddCors(p => p.AddPolicy(corsName, builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddLog4Net("log4net.config");
    logging.AddConsole();
});

builder.Services.AddHttpClient<IWeatherService, WeatherService>();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection(nameof(ApiSettings)));
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<WeatherDbContext>(
    options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services.AddTransient<IHistoryQueryRepository, HistoryQueryRepository> ();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

InitializationDataBase.Init(app);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(corsName);
app.UseAuthorization();

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();

