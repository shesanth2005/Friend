using Friend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Infrastructure.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.DisplayName)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(u => u.Email)
                   .IsRequired();
        }
    }
}
