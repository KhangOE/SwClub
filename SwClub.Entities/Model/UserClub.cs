using SwClub.Entities.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwClub.Entities.Models
{
    [Table("UserClubs")]
    public class UserClub :IBaseModel
    {
        public Guid UserId { get; set; }
        public Guid ClubId { get; set; }
        public virtual Club Club { get; set; }
        public virtual User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}

    }
}
