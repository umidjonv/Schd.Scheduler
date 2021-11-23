using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Schd.Notification.Data
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        public DbSet<Log> Logs { get; set; }
        
        public DbSet<Command> Commands { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<StateHistory> StateHistories { get; set; }

        public AppDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        private void InitModels()
        {

            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added ||
                                                       e.State == EntityState.Modified ||
                                                       e.State == EntityState.Deleted));

            foreach (var entityEntry in entries)
            {

                ((BaseEntity) entityEntry.Entity).ModifiedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity) entityEntry.Entity).CreationDate = DateTime.Now;
                }
                
                if (entityEntry.State == EntityState.Deleted)
                {
                    ((BaseEntity) entityEntry.Entity).IsDeleted = true;
                }
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<Notify>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasQueryFilter(e => !e.IsDeleted);

                entity.HasOne(e => e.Client).WithMany(e => e.Notifies).HasForeignKey(e => e.ClientId);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasQueryFilter(e => !e.IsDeleted);

                entity.HasOne(e => e.Notify).WithMany(e => e.States).HasForeignKey(e => e.NotifyId);

                entity.HasOne(e => e.Client).WithMany(e => e.States).HasForeignKey(e => e.ClientId);
            });

            modelBuilder.Entity<StateHistory>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasQueryFilter(e => !e.IsDeleted);

                entity.HasOne(e => e.Client).WithMany(e => e.StateHistories).HasForeignKey(e => e.ClientId);

                entity.HasOne(e => e.Notify).WithMany(e => e.StateHistories).HasForeignKey(e => e.NotifyId);

            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            InitModels();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            InitModels();

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
