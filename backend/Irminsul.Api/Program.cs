using Irminsul.Api.Extensions;
using Irminsul.Api.Middleware;
using Irminsul.Application.Interfaces;
using Irminsul.Application.Services;
using Irminsul.Infrastructure.External;
using Irminsul.Infrastructure.Persistence.Context;
using Irminsul.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Services

builder.Services.AddControllers();

// Database

builder.Services.AddDbContext<IrminsulContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Application

builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<CharacterService>();

builder.Services.AddApplicationValidation();

// External Services

builder.Services.AddHttpClient<IGenshinApiClient, GenshinApiClient>(client =>
{
    client.BaseAddress = new Uri(
        "https://genshin-db-api.vercel.app/"
    );
});

// OpenAPI

builder.Services.AddOpenApi();


// Build

var app = builder.Build();


// Middleware

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();