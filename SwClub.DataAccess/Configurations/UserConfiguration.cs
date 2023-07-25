namespace SwClub.DataAccess.Configurations
{
    using SwClub.Entities.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.NormalizedUserName).HasDatabaseName("IX_Users_Name").IsUnique();
            builder.HasIndex(user => user.NormalizedEmail).HasDatabaseName("IX_Users_Email");
            builder.Property(user => user.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(user => user.UserName).HasMaxLength(256);
            builder.Property(user => user.NormalizedUserName).HasMaxLength(256);
            builder.Property(user => user.Email).HasMaxLength(256);
            builder.Property(user => user.NormalizedEmail).HasMaxLength(256);
        }
    }
}
