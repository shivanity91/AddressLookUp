using Api.Common.DataProvider;
using PingWorker.Api.Models;
using PingWorker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(_ =>
{
    PingApiOptions pingApiOptions = new();
    builder.Configuration.Bind("PingApiOptions", pingApiOptions);
    return pingApiOptions;
});
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IDataProviderService, DataProviderService>();
builder.Services.AddSingleton<IPingLookUpService, PingLookUpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
