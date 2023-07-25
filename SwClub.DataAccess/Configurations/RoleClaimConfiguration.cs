namespace SwClub.DataAccess.Configurations
{
    using SwClub.Entities.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.HasKey(roleClaim => roleClaim.Id);

            builder
                .HasOne(roleClaim => roleClaim.Role)
                .WithMany(role => role.RoleClaims)
                .HasForeignKey(roleClaim => roleClaim.RoleId);
        }
    }
}
