using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Schd.Data.Entity;
using Schd.Data.Entity.Base;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Schd.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly IHttpContextAccessor _httpContext;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContext) : base(options)
        {
            _httpContext = httpContext;
            ChangeTracker.LazyLoadingEnabled = true;
        }
        private void AuditValues()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is AuditEntity && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));
            // user
            var userName = _httpContext?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = Environment.UserName ?? "(local)";
            }
            //ip
            var userIp = _httpContext?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            if(string.IsNullOrWhiteSpace(userIp))
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                userIp = host.AddressList
                    .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)?.ToString() ?? "::1";
            }

            foreach(var entry in entries)
            {
                ((AuditEntity)entry.Entity).ModifiedDate = DateTime.Now;
                ((AuditEntity)entry.Entity).ModifiedIp = userIp;
                ((AuditEntity)entry.Entity).ModifiedBy = userName;

                if(entry.State == EntityState.Added)
                {
                    ((AuditEntity)entry.Entity).CreatedDate = DateTime.Now;
                    ((AuditEntity)entry.Entity).CreatedIp = userIp;
                    ((AuditEntity)entry.Entity).CreatedBy = userName;
                }

                if(entry.State == EntityState.Deleted)
                {
                    ((AuditEntity)entry.Entity).IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }

        }
        public DbSet<AdImages> AdImages { get; set; }
        public DbSet<Ads> Ads { get; set; }
        public DbSet<Channels> Channels { get; set; }
        public DbSet<Owners> Owners { get; set; }
        public DbSet<OwnerTariff> OwnerTariffs { get; set; }
        public DbSet<ScheduledAds> ScheduledAds { get; set; }
        public DbSet<ScheduleTemplates> Schedules { get; }
        public DbSet<ScheduleTemplates> ScheduleTemplates { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AdImages>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
                e.HasOne<Ads>(s=>s.Ads).WithMany(s => s.AdImages).HasForeignKey(s => s.AdId);
            });

            builder.Entity<Ads>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
                e.HasOne<Owners>(s => s.Owners).WithMany(s => s.Ads).HasForeignKey(s => s.OwnerId);
            });

            builder.Entity<Channels>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
                e.HasOne<Owners>(s => s.Owners).WithMany(s => s.Channels).HasForeignKey(s => s.OwnerId);
            });

            builder.Entity<Owners>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
            });

            builder.Entity<OwnerTariff>(e=>{
                e.HasKey(a => a.Id);
                e.Property(e => e.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
                e.HasOne<Owners>(s => s.Owners).WithMany(s => s.OwnerTariffs).HasForeignKey(s => s.OwnerId);
            });

            builder.Entity<ScheduleTemplates>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
            });

            builder.Entity<ScheduledAds>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
                e.HasOne<Ads>(s => s.Ads).WithMany(s => s.ScheduledAds).HasForeignKey(s => s.AdId);
                //e.HasOne<ScheduleTemplates>(s => s.ScheduleTemplates).WithMany(s => s.ScheduledAds).HasForeignKey(s => s.SchedulerId);
            });

            builder.Entity<Statistics>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<Tariff>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
            });

            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            AuditValues();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditValues();
            return base.SaveChangesAsync(cancellationToken);
        }
        public DatabaseFacade GetDatabase()
        {
            return Database;
        }

        public void UndoingChangesDbEntityLevel(AuditEntity entity)
        {
            var entry = Entry(entity);

            switch(entry.State)
            {
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }
}
