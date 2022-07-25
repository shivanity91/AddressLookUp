using AddressLookUp.Aggregator.Api.Models;
using AddressLookUp.Aggregator.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddSingleton(_ =>
{
    LookUpApiOptions lookUpApiOptions = new();
    builder.Configuration.Bind("LookUpApiOptions", lookUpApiOptions);
    return lookUpApiOptions;
});

builder.Services.AddSingleton<IAddressLookUpService, AddressLookUpService>();
builder.Services.AddSingleton<IPingWorkerService, PingWorkerService>();
builder.Services.AddSingleton<IRdapWorkerService, RdapWorkerService>();
builder.Services.AddSingleton<IGeoIpWorkerService, GeoIpWorkerService>();
builder.Services.AddSingleton<IReverseDnsWorkerService, ReverseDnsWorkerService>();
builder.Services.AddSingleton<IDomainAvailabilityWorkerService, DomainAvailabilityWorkerService>();

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
