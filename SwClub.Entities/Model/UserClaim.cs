using Microsoft.AspNetCore.Identity;
using SwClub.Entities.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwClub.Entities.Models
{
    [Table("UserClaims")]
    public class UserClaim : IdentityUserClaim<Guid>, IBaseModel
    {
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
    }
}
