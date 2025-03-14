using BuildingBlocks.Exceptions.Handlres;
using Catalog.Api;
using HealthChecks.UI.Client;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddMediatr()
.AddValidationSchema()
.AddHealthCheckerWithUi(builder.Configuration);

builder.AddLogsIntoDb(builder.Configuration);

builder.Services.AddMarten(options =>
{
    var conStr = builder.Configuration.GetConnectionString("Database")!;
    options.Connection(conStr);
    options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddValidationSchema();


builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(opt => { });

app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

