using Microsoft.AspNetCore.Identity;
using SwClub.Entities.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwClub.Entities.Models
{
    [Table("UserTokens")]
    public class UserToken : IdentityUserToken<Guid>, IBaseModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual User User { get; set; }
    }
}
