using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Schd.Data.Entity;
using Schd.Data.Entity.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

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
        DbSet<ScheduleTemplates> Schedules { get; }
        DbSet<Statistics> Statistics { get; }
        DbSet<Tariff> Tariffs { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DatabaseFacade GetDatabase();
        void UndoingChangesDbEntityLevel(AuditEntity entity);
        EntityEntry Entry(object entity);
    }
}