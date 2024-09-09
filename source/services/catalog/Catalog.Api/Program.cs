var builder = WebApplication.CreateBuilder(args);

// Add services to to container
builder.Services.AddCarter();
builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// configure the http request pipeline
app.MapCarter();
app.Run();
