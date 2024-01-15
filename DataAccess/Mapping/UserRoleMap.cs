using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {

            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(new AppUserRole
            {
                UserId = Guid.Parse("3C59641E-B5E6-40CA-98F4-D7E489EB2588"),
                RoleId = Guid.Parse("6BAB186A-9A62-40A8-B5F5-609802F8B205")
            },
            new AppUserRole
            {
                UserId = Guid.Parse("C7DE37C5-4666-4B76-8933-A4871C8A114B"),
                RoleId = Guid.Parse("4ECC6421-5323-4174-8D62-1CDF553918CC")
            });
        }
    }
}
