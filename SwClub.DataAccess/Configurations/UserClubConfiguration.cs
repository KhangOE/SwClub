using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SwClub.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwClub.DataAccess.Configurations
{

    public class UserClubConfiguration : IEntityTypeConfiguration<UserClub>
    {
        public void Configure(EntityTypeBuilder<UserClub> builder)
        {
            builder.HasKey(userClub => new { userClub.ClubId, userClub.UserId });
            
            builder
                .HasOne(userClub => userClub.User)
                .WithMany(user => user.UserClubs)
                .HasForeignKey(userClub => userClub.UserId);

            builder
                .HasOne(userClub => userClub.Club)
                .WithMany(club => club.UserClubs)
                .HasForeignKey(userClub => userClub.ClubId);
        }
    }
}
