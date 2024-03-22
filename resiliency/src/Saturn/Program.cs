var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDaprClient();

// builder.Services.AddHttpClient("neptune", config =>
// {
//     config.BaseAddress = new Uri("http://localhost:3500");
//     config.DefaultRequestHeaders.Add("dapr-app-id", "neptune");
// });

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapHealthChecks("health");

app.Run();
