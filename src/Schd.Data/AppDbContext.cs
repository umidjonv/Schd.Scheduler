﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Schd.Data.Entity;
using Schd.Data.Entity.Base;
using Schd.Scheduler.Data.Entity.Schedules;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Schd.Scheduler.Data
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
            if (string.IsNullOrWhiteSpace(userIp))
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                userIp = host.AddressList
                    .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)?.ToString() ?? "::1";
            }

            foreach (var entry in entries)
            {
                ((AuditEntity)entry.Entity).ModifiedDate = DateTime.Now;
                ((AuditEntity)entry.Entity).ModifiedIp = userIp;
                ((AuditEntity)entry.Entity).ModifiedBy = userName;

                if (entry.State == EntityState.Added)
                {
                    ((AuditEntity)entry.Entity).CreatedDate = DateTime.Now;
                    ((AuditEntity)entry.Entity).CreatedIp = userIp;
                    ((AuditEntity)entry.Entity).CreatedBy = userName;
                }

                if (entry.State == EntityState.Deleted)
                {
                    ((AuditEntity)entry.Entity).IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }

        }
        public DbSet<AdImage> AdImages { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<OwnerTariff> OwnerTariffs { get; set; }
        public DbSet<ScheduledAd> ScheduledAds { get; set; }
        public DbSet<ScheduleTemplate> Schedules { get; }
        public DbSet<ScheduleTemplate> ScheduleTemplates { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AdImage>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
                e.HasOne(s => s.Ads).WithMany(s => s.AdImages).HasForeignKey(s => s.AdId);
            });

            builder.Entity<Ad>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
                e.HasOne(s => s.Owners).WithMany(s => s.Ads).HasForeignKey(s => s.OwnerId);
            });

            builder.Entity<Channel>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
                e.HasOne(s => s.Owners).WithMany(s => s.Channels).HasForeignKey(s => s.OwnerId);
            });

            builder.Entity<Owner>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
            });

            builder.Entity<OwnerTariff>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(e => e.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
                e.HasOne(s => s.Owners).WithMany(s => s.OwnerTariffs).HasForeignKey(s => s.OwnerId);
            });

            builder.Entity<ScheduleTemplate>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
            });

            builder.Entity<ScheduledAd>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.HasQueryFilter(a => !a.IsDeleted);
                e.HasOne<Ad>(s => s.Ads).WithMany(s => s.ScheduledAds).HasForeignKey(s => s.AdId);
                //e.HasOne<ScheduleTemplates>(s => s.ScheduleTemplates).WithMany(s => s.ScheduledAds).HasForeignKey(s => s.SchedulerId);
            });

            builder.Entity<Statistic>(e =>
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

            switch (entry.State)
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
