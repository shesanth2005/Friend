using Friend.Domain.Entities;
using Friend.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Constraints and configurations
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.DisplayName).HasMaxLength(100).IsRequired();
                entity.Property(u => u.Email).IsRequired();
            });
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //}

        //dotnet new tool-manifest
        //dotnet tool install dotnet-ef

        //dotnet ef migrations add Create2 -- project Friend.Infrastructure -- startup-project Friend.API -- output-dir Migrations
        //dotnet ef migrations add Create2 --project Friend.Infrastructure --startup-project Friend.API --output-dir Persistence/Migrations

        //dotnet ef database update --project Friend.Infrastructure --startup-project Friend.API



    }
}
