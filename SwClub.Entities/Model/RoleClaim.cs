using Microsoft.AspNetCore.Identity;
using SwClub.Entities.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwClub.Entities.Models
{
    [Table("RoleClaims")]
    public class RoleClaim : IdentityRoleClaim<Guid>, IBaseModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Role Role { get; set; }
    }
}
