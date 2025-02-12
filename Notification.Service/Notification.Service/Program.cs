using Notification.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.ConfigureCrossOriginRessourceSharing();
builder.Services.AddEndpointsApiExplorer();
builder.AddConfigurations();
builder.Services.AddServices();
builder.Services.AddMapperServices();
builder.ConfigureMassTransit();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
