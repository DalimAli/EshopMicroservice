
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

builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("DatabaseConnection"));
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// configure the http request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
