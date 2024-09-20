
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to to container

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssembly(assembly);
    x.AddOpenBehavior(typeof(ValidationBehavior<,>));
    x.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


builder.Services.AddValidatorsFromAssembly(assembly);


builder.Services.AddCarter();

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

builder.Services.AddMarten(option =>
{
    option.Connection(connectionString);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString);

var app = builder.Build();

// configure the http request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
