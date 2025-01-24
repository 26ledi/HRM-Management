using HRManagement.Auth.API;
using HRManagement.Auth.API.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.ConfigureServices();
builder.Services.AddServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseCors();
await app.UseMigration();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
