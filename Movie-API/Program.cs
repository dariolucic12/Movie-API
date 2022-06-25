
using Microsoft.EntityFrameworkCore;
using Movie_API.Models;
using Movie_API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IWatchlistRepo, WatchlistRepo>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();

builder.Services.AddDbContext<ApplicationContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("MovieDb"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
