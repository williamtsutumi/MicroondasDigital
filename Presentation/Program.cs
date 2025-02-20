using Domain.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Version: {0}", Environment.Version.ToString());

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
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
        }
    });
});

app.UseAuthorization();

app.MapControllers();

app.Run();
