using Api.Common.DataProvider;
using DomainAvailabilityWorker.Api.Models;
using DomainAvailabilityWorker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(_ =>
{
    DomainAvailabilityApiOptions domainAvailabilityApiOptions = new();
    builder.Configuration.Bind("DomainAvailabilityApiOptions", domainAvailabilityApiOptions);
    return domainAvailabilityApiOptions;
});
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IDataProviderService, DataProviderService>();
builder.Services.AddSingleton<IDomainAvailabilityService, DomainAvailabilityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
