using dotnet_todo.Data;
using dotnet_todo.Models;
using dotnet_todo.Services.CharacterService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService, CharacterService>();
//builder.Services.AddDbContext<CharacterDbContext>(options => options.UseSqlite("Data Source=Characters.db"));
//builder.Services.AddScoped<IRepository<Character>, CharacterRepository<Character>>();
builder.Services.AddScoped<IRepository<Character>, ElasticCharacterRepository<Character>>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

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
