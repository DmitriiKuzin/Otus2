using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Otus._2.DAL.Model;

namespace Otus._2.DAL;

public class UserContext: DbContext
{

    public UserContext()
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
        base.OnConfiguring(optionsBuilder);
    }

    public UserContext(DbContextOptions<UserContext> options): base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
}