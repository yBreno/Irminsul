using Irminsul.Api.Extensions;
using Irminsul.Api.Middleware;
using Irminsul.Application.Interfaces;
using Irminsul.Application.Services;
using Irminsul.Infrastructure.External;
using Irminsul.Infrastructure.Persistence.Context;
using Irminsul.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IrminsulContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<CharacterService>();

builder.Services.AddApplicationValidation();

builder.Services.AddHttpClient<IGenshinApiClient, GenshinApiClient>(client =>
{
    client.BaseAddress = new Uri("https://genshin-db-api.vercel.app/");
});


var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
