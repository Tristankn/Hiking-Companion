using Microsoft.EntityFrameworkCore;

class GearDb : DbContext
{
    public GearDb(DbContextOptions<GearDb> options)
        : base(options) { }

    public DbSet<Gear> Gear => Set<Gear>();
}