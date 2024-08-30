using Light.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Light.Data;

public class SqlContext : DbContext
{
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<ScheduleItem> ScheduleItems { get; set; }
    public DbSet<Address> Addresses { get; set; }
    
    public SqlContext(DbContextOptions<SqlContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Schedule>()
            .HasMany(s => s.Items)
            .WithOne(si => si.Schedule)
            .HasForeignKey(si => si.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Schedule>()
            .HasMany(s => s.Addresses)
            .WithOne(si => si.Schedule)
            .HasForeignKey(si => si.ScheduleId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Address>()
            .HasIndex(u => u.AddressName);
    }
}

public class SqlContextFactory : IDesignTimeDbContextFactory<SqlContext>
{
    public SqlContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqlContext>();
        var connectionString = "User ID=myapi;Password=111;Server=localhost;Port=5432;Database=test;Pooling=true;";
        optionsBuilder.UseNpgsql(connectionString);

        return new SqlContext(optionsBuilder.Options);
    }
}