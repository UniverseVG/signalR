using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add controllers and minimal SignalR services
builder.Services.AddControllers();
builder.Services.AddSignalR(); // Minimal SignalR services

// Add SignalR transport services (required for AddSignalRCore)
// Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
            // Uncomment if you need credentials
            // .AllowCredentials();
        });
});

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAllOrigins");

// Enable Swagger in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Configure routing and endpoints
app.UseRouting();

// Map controllers and SignalR hub
app.MapControllers();
app.MapHub<ProgressHub>("/progressHub");

app.Run();
