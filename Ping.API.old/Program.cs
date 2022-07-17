using AddressLookUp.Aggregator.Api.Services;
using Api.Common.DataProvider;
using Ping.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(_ =>
{
    PingApiOptions options = new();
    builder.Configuration.Bind("PingApiOptions", options);
    return options;
});
builder.Services.AddSingleton<IDataProviderService, DataProviderService>();
builder.Services.AddSingleton<IPingLookUpService, PingLookUpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
