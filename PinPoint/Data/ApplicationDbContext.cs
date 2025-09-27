using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PinPoint.Migrations;

namespace PinPoint.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Symptom> Symptoms { get; set; }

        // Initialise DB with roles
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "b8a44de3-054d-44f4-8d22-b6c7286ce135",
                    Name = "Patient",
                    NormalizedName = "PATIENT"
                },
                new IdentityRole
                {
                    Id = "4bf6db49-c852-409f-900f-48a83f70047b",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Id = "7ff01497-5a99-4470-87ee-d8e8a267a99b",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new IdentityRole
                {
                    Id = "3f720a9f-e689-41fb-b3a9-52a07410bf5f",
                    Name = "Developer",
                    NormalizedName = "DEVELOPER"
                });

            // Hash password security
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>()
                .HasData(new ApplicationUser
                {
                    Id = "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4",
                    Email = "dev@pinpoint.com.au",
                    NormalizedEmail = "DEV@PINPOINT.COM.AU",
                    NormalizedUserName = "DEV@PINPOINT.COM.AU",
                    UserName = "dev@pinpoint.com.au",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true,
                    FirstName = "Default",
                    LastName = "Admin",
                    DateOfBirth = new DateOnly(1990,09,05)
                });

            // Assigns default user Developer role
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "3f720a9f-e689-41fb-b3a9-52a07410bf5f",
                    UserId = "ab3dca9e-4c1d-41e9-9c9b-b4e047cd12f4"
                });
        }
    }
}
