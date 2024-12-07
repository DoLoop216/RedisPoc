using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RedisPOC.Entities;

namespace RedisPOC;

public class RedisPOCDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql("Host=localhost;Database=RedisPOC;Username=postgres;Password=Test123.");
    }
}