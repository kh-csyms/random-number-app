using Serilog;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using System.Security.Authentication;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
//    .WriteTo.Seq(builder.Configuration["log_url"]!, apiKey: builder.Configuration["log_key"])
    .WriteTo.Seq("https://logs.hstry.dev", apiKey: "5YbS1PWKFLrOOQhbB7nm")
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// Capture client IP
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

var random = new Random();

app.MapGet("/random-number", (HttpContext context) =>
{
    var number = random.Next(1, 101);
    var clientIp = context.Connection.RemoteIpAddress?.ToString();

    Log.Information("Button was pushed, the new number generated was: {Number}, IP: {IP}", number, clientIp);

    return Results.Ok(number);
})
.WithName("GetRandomNumber")
.WithOpenApi();

app.Run();
