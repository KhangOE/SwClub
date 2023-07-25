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
    public class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasKey(userCourse => new { userCourse.UserId, userCourse.CourseId });

            builder
                .HasOne(userCourse => userCourse.User)
                .WithMany(user => user.UserCourses)
                .HasForeignKey(userCourse => userCourse.UserId);

            builder
                .HasOne(userCourse => userCourse.Course)
                .WithMany(course => course.UserCourses)
                .HasForeignKey(userCourse => userCourse.CourseId);
               
        }
    }
}
