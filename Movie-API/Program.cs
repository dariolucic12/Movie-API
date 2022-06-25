
using Microsoft.EntityFrameworkCore;
using Movie_API;
using Movie_API.Logger;
using Movie_API.Models;
using Movie_API.Repository;
using Movie_API.Services;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IWatchlistRepo, WatchlistRepo>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Services.AddDbContext<ApplicationContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("MovieDb"))
    );

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
