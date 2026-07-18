
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
builder.Services.AddScoped<IRateCardRepository, RateCardRepository>();
builder.Services.AddScoped<ICallRateService, CallRateService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Register Global ExceptionHandle
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapEndpoints();

// This section is only for Demo perpose, In real world will be use Migration and update DB  
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CallRatingServiceDbContext>();

    // This creates the DB and tables if they aren't there, without using Migrations
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();
