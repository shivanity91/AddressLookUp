using Api.Common.DataProvider;
using GeoIpWorker.Api.Models;
using GeoIpWorker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(_ =>
{
    GeoIpApiOptions geoIpApiOptions = new();
    builder.Configuration.Bind("GeoIpApiOptions", geoIpApiOptions);
    return geoIpApiOptions;
});
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IDataProviderService, DataProviderService>();
builder.Services.AddSingleton<IGeoIpLookUpService, GeoIpLookUpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
