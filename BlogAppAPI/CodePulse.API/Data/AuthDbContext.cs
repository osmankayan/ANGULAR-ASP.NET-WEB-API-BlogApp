using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Creating roles
            var readerRoleId = "8fe66239-3a7e-41c7-a8cf-ee1a7ee83f74";
            var writerRoleId = "303eac30-4922-404f-aa0a-b3efd128aac5";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                    ConcurrencyStamp=readerRoleId
                },

                new IdentityRole() 
                {
                    Id=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                    ConcurrencyStamp=writerRoleId
                }
            };

            //Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //Create admin user
            var adminUserId = "997db4de-4d0f-4eaf-97ba-49ee07e25730";

            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin",
                Email = "admin@gmail.com",
                NormalizedUserName = "admin".ToUpper(),
                NormalizedEmail = "admin@gmailcom".ToUpper(),
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "123");
            //Seed the user
            builder.Entity<IdentityUser>().HasData(admin);


            //Give roles to admin
            var adminRoles = new List<IdentityUserRole<string>>() 
            {
                new()
                {
                    UserId= adminUserId,
                    RoleId=writerRoleId
                },
                 new()
                {
                    UserId= adminUserId,
                    RoleId=readerRoleId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
