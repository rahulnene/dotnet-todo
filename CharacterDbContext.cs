using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_todo.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_todo
{
    public class CharacterDbContext : DbContext
    {

        public DbSet<Character> Characters { get; set; }
        public CharacterDbContext(DbContextOptions<CharacterDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Characters.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }

    }
}