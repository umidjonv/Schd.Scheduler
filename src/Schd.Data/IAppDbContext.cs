using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Schd.Data.Entity;

namespace Schd.Data
{
    public interface IAppDbContext : IDisposable
    {
        DbSet<AdImages> AdImages { get; }
        DbSet<Ads> Ads { get; }
        DbSet<Channels> Channels { get; }
        DbSet<Owners> Owners { get; }
        DbSet<OwnerTariff> OwnerTariffs { get; }
        DbSet<ScheduledAds> ScheduledAds { get; }
        DbSet<Schedule> Schedules { get; }
        DbSet<Statistics> Statistics { get; }
        DbSet<Tariff> Tariffs { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}