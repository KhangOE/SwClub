using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwClub.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwClub.DataAccess.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasOne(course => course.Club)
                .WithMany(club => club.Courses)
                .HasForeignKey(course => course.ClubId);
            builder
                .HasOne(course => course.Certificate)
                .WithMany(Certificate => Certificate.Courses)
                .HasForeignKey(course => course.CertificateId);
        }
    }
}
