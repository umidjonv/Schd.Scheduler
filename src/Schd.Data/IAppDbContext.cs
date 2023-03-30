using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Schd.Data.Entity;
using Schd.Data.Entity.Base;
using Schd.Scheduler.Data.Entity.Schedules;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Schd.Scheduler.Data
{
    public interface IAppDbContext : IDisposable
    {
        DbSet<AdImage> AdImages { get; }
        DbSet<Ad> Ads { get; }
        DbSet<Channel> Channels { get; }
        DbSet<Owner> Owners { get; }
        DbSet<OwnerTariff> OwnerTariffs { get; }
        DbSet<ScheduledAd> ScheduledAds { get; }
        DbSet<ScheduleTemplate> ScheduleTemplates { get; }
        DbSet<Statistic> Statistics { get; }
        DbSet<Tariff> Tariffs { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DatabaseFacade GetDatabase();
        void UndoingChangesDbEntityLevel(AuditEntity entity);
        EntityEntry Entry(object entity);
    }
}