using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schd.Data.Entity;

namespace Schd.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<AdImages> AdImages { get; set; }
        public DbSet<Ads> Ads { get; set; }
        public DbSet<Channels> Channels { get; set; }
        public DbSet<Owners> Owners { get; set; }
        public DbSet<OwnerTariff> OwnerTariffs { get; set; }
        public DbSet<ScheduledAds> ScheduledAds { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AdImages>()
                .HasOne<Ads>(s => s.Ads)
                .WithMany(s => s.AdImages)
                .HasForeignKey(s => s.AdId);

            builder.Entity<Ads>()
                .HasOne<Owners>(s => s.Owners)
                .WithMany(s => s.Ads)
                .HasForeignKey(s => s.OwnerId);

            builder.Entity<Channels>()
                .HasOne<Owners>(s => s.Owners)
                .WithMany(s => s.Channels)
                .HasForeignKey(s => s.OwnerId);

            builder.Entity<OwnerTariff>()
                .HasOne<Owners>(s => s.Owners)
                .WithMany(s => s.OwnerTariffs)
                .HasForeignKey(s => s.OwnerId);

            builder.Entity<ScheduledAds>()
                .HasOne<Ads>(s => s.Ads)
                .WithMany(s => s.ScheduledAds)
                .HasForeignKey(s => s.AdId);

            builder.Entity<ScheduledAds>()
                .HasOne<Schedule>(s => s.Schedules)
                .WithMany(s => s.ScheduledAds)
                .HasForeignKey(s => s.SchedulerId);
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            ChangeTracker.DetectChanges();
            return await base.SaveChangesAsync();
        }
    }
}
