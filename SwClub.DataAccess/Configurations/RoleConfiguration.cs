namespace SwClub.DataAccess.Configurations
{
    using SwClub.Entities.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
   

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(role => role.Id);
            builder.HasIndex(role => role.NormalizedName).HasDatabaseName("IX_Roles_Name").IsUnique();
            builder.Property(role => role.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(role => role.Name).HasMaxLength(256);
            builder.Property(role => role.NormalizedName).HasMaxLength(256);
        }
    }
}
