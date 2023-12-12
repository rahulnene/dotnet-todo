using dotnet_todo.Data;
using dotnet_todo.Models;
using dotnet_todo.Services.CharacterService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationAuthDbContext>();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService, CharacterService>();

//Comment/Uncomment the below lines as needed to use SQLite(first two) or Elastic
builder.Services.AddDbContext<CharacterDbContext>(options => options.UseSqlite("Data Source=Characters.db"));
builder.Services.AddScoped<IRepository<Character>, CharacterRepository<Character>>();
//builder.Services.AddScoped<IRepository<Character>, ElasticCharacterRepository<Character>>();

builder.Services.AddDbContext<ApplicationAuthDbContext>(op => op.UseSqlite("Data Source=Users.db"));

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

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
