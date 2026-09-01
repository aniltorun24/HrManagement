using HrManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.Infrastructure.Persistence.Configurations
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("UserAccounts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EmployeeId)
                .IsRequired();
                
            builder.Property(x => x.PasswordHash)
                .HasMaxLength(500);

             builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.LastLoginAt);
                
            builder.Property(x => x.EmailVerified)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.RoleId)
                .IsRequired();

            builder.HasOne(x => x.Role)
                .WithMany(x => x.UserAccounts)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
