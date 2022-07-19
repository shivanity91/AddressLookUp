using Api.Common.DataProvider;
using RdapWorker.Api.Models;
using RdapWorker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(_ =>
{
    RdapApiOptions rdapApiOptions = new();
    builder.Configuration.Bind("RdapApiOptions", rdapApiOptions);
    return rdapApiOptions;
});
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IDataProviderService, DataProviderService>();
builder.Services.AddSingleton<IRdapLookUpService, RdapLookUpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
