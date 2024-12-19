using AiDrivenSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AiDrivenSystem.Infrastructure;

public class AiDrivenSystemDbContext : DbContext
{
    public AiDrivenSystemDbContext(DbContextOptions<AiDrivenSystemDbContext> options)
        : base(options) { }

    public DbSet<NodeDbModel> Nodes { get; set; }

    public DbSet<EdgeDbModel> Edges { get; set; }

    public DbSet<UserControlDbModel> UserControls { get; set; }

    public DbSet<CardStackDbModel> CardStacks { get; set; }

    public DbSet<AiActionDbModel> AiActions { get; set; }
}
