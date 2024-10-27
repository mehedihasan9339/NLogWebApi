using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using NLogWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Register NLog
builder.Host.UseNLog();

var logger = LogManager.GetCurrentClassLogger(); // Initialize logger
logger.Info("Starting application...");

// Add services to the container
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NLogWebApi v1");
    c.RoutePrefix = "swagger"; // Access at /swagger
});

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

// Run the application
app.Run();
