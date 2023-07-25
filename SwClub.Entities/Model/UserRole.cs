using Microsoft.AspNetCore.Identity;
using SwClub.Entities.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwClub.Entities.Models
{
    [Table("UserRoles")]
    public class UserRole : IdentityUserRole<Guid> , IBaseModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
