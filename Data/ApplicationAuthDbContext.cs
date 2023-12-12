using dotnet_todo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_todo.Data
{
    public class ApplicationAuthDbContext(DbContextOptions<ApplicationAuthDbContext> options) : IdentityDbContext(options)
    {
    }
}
