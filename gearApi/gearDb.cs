using Microsoft.EntityFrameworkCore;

using gearApi.Models;

class GearDb : DbContext
{
    public GearDb(DbContextOptions<GearDb> options)
        : base(options) { }

    public DbSet<Gear> Gear => Set<Gear>();
    public DbSet<Category> Categories => Set<Category>();
}