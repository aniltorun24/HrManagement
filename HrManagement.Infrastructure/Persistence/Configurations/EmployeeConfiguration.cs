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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(250);

            builder.Property(x => x.Phone)
               .HasMaxLength(30);

            builder.Property(x => x.HireDate)
               .IsRequired();


            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.HasOne(x => x.UserAccount)
                .WithOne(x => x.Employee)
                .HasForeignKey<UserAccount>(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
