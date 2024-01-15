using Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class UserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(100);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var superadmin = new AppUser
            {
                Id = Guid.Parse("3C59641E-B5E6-40CA-98F4-D7E489EB2588"),
                Email = "superadmin@gmail.com",
                NormalizedEmail= "SUPERADMIN@GMAIL.COM",
                EmailConfirmed = true,
                UserName = "superadmin@gmail.com",
                NormalizedUserName = "SUPERADMIN@GMAIL.COM",
                PhoneNumber = "+905559998888",
                PhoneNumberConfirmed = true,
                FirstName = "cagla",
                LastName = "fikir",
                SecurityStamp = Guid.NewGuid().ToString(),
                ImageId = Guid.Parse("3BD57681-D153-4934-83F7-3458A9716648"),
            };
            superadmin.PasswordHash = CreatePasswordHash(superadmin, "123456");

            var admin = new AppUser
            {
                Id = Guid.Parse("C7DE37C5-4666-4B76-8933-A4871C8A114B"),
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = false,
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                PhoneNumber = "+905559998877",
                PhoneNumberConfirmed = false,
                FirstName = "admin",
                LastName = "user",
                SecurityStamp = Guid.NewGuid().ToString(),
                ImageId = Guid.Parse("F144F00B-5A1E-4624-A3CC-DD26B2E0CEE9"),
            };
            admin.PasswordHash = CreatePasswordHash(admin, "123456");

            builder.HasData(superadmin, admin);
        
            
        }
        private static string CreatePasswordHash(AppUser user, string password)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
