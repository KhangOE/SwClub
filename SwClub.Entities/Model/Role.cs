using Microsoft.AspNetCore.Identity;
using SwClub.Common.Enums;
using SwClub.Entities.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwClub.Entities.Models
{
    [Table("Roles")]
    public class Role : IdentityRole<Guid>, IBaseModel
    {
        public RoleType RoleType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
