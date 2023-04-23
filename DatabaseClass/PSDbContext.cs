using Microsoft.EntityFrameworkCore;
using PerfectS;
using System.Collections.Generic;

public class PSDbContext : DbContext
{
    public PSDbContext() : base() { }
    public DbSet<User> Users { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<ShiftChoise> ShiftChoises { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:perfects.database.windows.net,1433;Initial Catalog=PerfectS;Persist Security Info=False;User ID=Neria;Password=0guzh4n-4YD1N;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
    public PSDbContext(DbContextOptions<PSDbContext> options) : base(options)
    {

    }
}