
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwClub.Common.Constants;
using SwClub.DataAccess.Configurations;
using SwClub.Entities.Models;

namespace SwClub.DataAccess.Contexts
{
   
    public class SwClubDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public static readonly ILoggerFactory DbContextLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
     
        public SwClubDbContext() { }
        public SwClubDbContext(DbContextOptions option) : base(option) { }
     
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", true, true);
                               
            var connectionString = Environment.GetEnvironmentVariable(GlobalConstant.Env.DATABASE_URL);
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = builder.Build().GetConnectionString("DefaultConnection");
            }

          //  optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=123456;Database=SwClub;");
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-SM73NQUS;Initial Catalog=SwClub;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            optionsBuilder.UseLoggerFactory(DbContextLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Identity
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new RoleClaimConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserClaimConfiguration());
            builder.ApplyConfiguration(new UserLoginConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new UserTokenConfiguration());

            builder.ApplyConfiguration(new UserCourseConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new UserCertificateConfiguration());
            builder.ApplyConfiguration(new UserClubConfiguration());

            // Others
            builder.ApplyConfiguration(new ApplicationSettingConfiguration());
        }

    }
}
