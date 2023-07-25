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
    public class UserCertificateConfiguration : IEntityTypeConfiguration<UserCertificate>
    {
        public void Configure(EntityTypeBuilder<UserCertificate> builder)
        {
            builder.HasKey(userCertificate => new {userCertificate.UserId,userCertificate.CertificateId});

            builder
                .HasOne(userCertificate => userCertificate.User)
                .WithMany(user => user.UserCertificates)
                .HasForeignKey(userCertificate => userCertificate.UserId);

            builder
                .HasOne(userCertificate => userCertificate.Certificate)
                .WithMany(certificate => certificate.UserCertificates)
                .HasForeignKey(userCertificate => userCertificate.CertificateId);
        }
    }
}
