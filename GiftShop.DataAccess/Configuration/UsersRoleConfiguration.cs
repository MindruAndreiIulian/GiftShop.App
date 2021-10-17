using System;
using System.Collections.Generic;
using System.Text;
using GiftShop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftShop.DataAccess.Configuration
{
    public class UsersRoleConfiguration : IEntityTypeConfiguration<UsersRole>
    {
        public void Configure(EntityTypeBuilder<UsersRole> builder)
        {
            builder.HasKey(ur => new { ur.RoleId, ur.UserId })
                .IsClustered(false);

            builder.HasOne(d => d.Role)
                .WithMany()
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UsersRole__RoleI__48CFD27E")
            .IsRequired(true);

            builder.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UsersRole__UserI__47DBAE45")
                .IsRequired();
        }
    }
}
