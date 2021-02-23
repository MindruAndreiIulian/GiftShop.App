using System;
using System.Collections.Generic;
using System.Text;
using GiftShop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftShop.DataAccess.Configuration
{
    public class UserConfiguration: IEntityTypeConfiguration<User> { 
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(e => e.Email, "UQ__Users__A9D10534F204C79A")
                    .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Address).HasMaxLength(50);

            builder.Property(e => e.Birthdate).HasColumnType("datetime");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.FirstName).HasMaxLength(20);

            builder.Property(e => e.LastName).HasMaxLength(20);
        }

    }
}
