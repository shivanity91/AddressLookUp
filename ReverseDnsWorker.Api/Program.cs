using Api.Common.DataProvider;
using ReverseDnsWorker.Api.Models;
using ReverseDnsWorker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(_ =>
{
    ReverseDnsApiOptions reverseDnsApiOptions = new();
    builder.Configuration.Bind("ReverseDnsApiOptions", reverseDnsApiOptions);
    return reverseDnsApiOptions;
});
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IDataProviderService, DataProviderService>();
builder.Services.AddSingleton<IReverseDnsLookUpService, ReverseDnsLookUpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
