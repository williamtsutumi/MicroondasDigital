using Domain.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMicroondasService, MicroondasService>();
builder.Services.AddScoped<IProgramaService, ProgramaService>();
builder.Services.AddScoped<IProgramaRepository, ProgramaRepository>();
builder.Services.AddScoped<IProgramaValidatorService, ProgramaValidatorService>();
builder.Services.AddScoped<IMicroondasValidatorService, MicroondasValidatorService>();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your_issuer",
            ValidAudience = "your_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
        };
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.WithOrigins("http://localhost:5206").AllowAnyMethod());

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (ex is ApiException apiException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new { error = apiException.Message });
        }
        else
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()) + "\\Infrastructure\\Logs\\";
            var log_name = path + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".txt";
            using (FileStream fs = File.Create(log_name))
            {
                byte[] bytes = Encoding.UTF8.GetBytes("Inner exception:\r\n" + ex.Message);
                fs.Write(bytes, 0, bytes.Length);

                bytes = Encoding.UTF8.GetBytes("\r\n\r\nOuter exception:\r\n" + ex.ToString());
                fs.Write(bytes, 0, bytes.Length);

                bytes = Encoding.UTF8.GetBytes("\r\n\r\nStack trace:\r\n" + ex.StackTrace);
                fs.Write(bytes, 0, bytes.Length);
            }
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = "Um erro inesperado aconteceu." });
        }
    });
});

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
