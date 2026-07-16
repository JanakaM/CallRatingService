
using CallRatingService.Api;
using CallRatingService.Application;
using CallRatingService.Infrastructure;
using Scalar.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register Command & Query handlers 
builder.Services.AddMediatR(configure =>
{
    var applicationAssembly = Assembly.Load("CallRatingService.Application");
    configure.RegisterServicesFromAssembly(applicationAssembly);
});

// setting up DBContext and Add DI
builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection("Infrastructure"));
var apiConfiguration = new ApiConfiguration();
builder.Configuration.GetSection("Infrastructure").Bind(apiConfiguration);
builder.Services.AddCallRatingDbContext(apiConfiguration.ConnectionString);

// DI
builder.Services.AddScoped<ICallDetailRepository, CallDetailRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");

app.MapEndpoints();

// Temp 

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CallRatingServiceDbContext>();

    // This creates the DB and tables if they aren't there, without using Migrations
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
