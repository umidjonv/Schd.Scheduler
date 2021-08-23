using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schd.Notification.Data
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        public DbSet<Notify> Notifies { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<StateHistory> StateHistories { get; set; }

        public AppDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

         
    }
}
