using System.Collections.Generic;

public class YourDbContext : DbContext
{
    public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
    {
    }

    public DbSet<Insuree> Insurees { get; set; }
}

