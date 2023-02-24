using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Schd.Data;
using Schd.Scheduler.Data;

namespace Schd.Migrations
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private readonly string _connectionString;
        private const string NamespaceName = "Schd.Scheduler.Migrations";

        public AppDbContextFactory()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(_connectionString, b => b.MigrationsAssembly(NamespaceName));

            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();

            return new AppDbContext(optionsBuilder.Options, httpContextAccessor);
        }
    }
}
