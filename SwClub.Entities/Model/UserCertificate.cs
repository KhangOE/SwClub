using SwClub.Entities.IModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwClub.Entities.Models
{
    [Table("UserCertificates")]
    public class UserCertificate : IBaseModel
    {
        public Guid UserId { get; set; }
        public Guid CertificateId { get; set; }
        public virtual User User { get; set; }
        public virtual Certificate Certificate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Score { get; set; }
        public bool IsPassed { get; set; }

    }
}
