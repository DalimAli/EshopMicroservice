using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to to container

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssembly(assembly);
    x.AddOpenBehavior(typeof(ValidationBehavior<,>));
});


builder.Services.AddValidatorsFromAssembly(assembly);


builder.Services.AddCarter();

builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("DatabaseConnection"));
}).UseLightweightSessions();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// configure the http request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
