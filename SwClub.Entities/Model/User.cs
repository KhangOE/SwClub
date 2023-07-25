using Microsoft.AspNetCore.Identity;
using SwClub.Entities.IModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwClub.Entities.Models
{
    [Table("Users")]
    public class User : IdentityUser<Guid>, IBaseModel
    {
        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(256)]
        public string FullName { get; set; }

        public string? AvatarUrl { get; set; }

        public virtual ICollection<UserCertificate> UserCertificates { get; set; }

        public virtual ICollection<UserClub> UserClubs { get; set; }

        public virtual ICollection<UserCourse> UserCourses { get; set; }

        public virtual ICollection<UserClaim> UserClaims { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }

        public virtual ICollection<UserToken> UserTokens { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
