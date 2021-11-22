using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Schd.Notification.Data
{
    public interface IAppDbContext
    {

        DbSet<Notify> Notifies { get; set; }

        DbSet<Client> Clients { get; set; }

        DbSet<State> States { get; set; }

        DbSet<StateHistory> StateHistories { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
