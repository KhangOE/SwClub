
using SwClub.Entities.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwClub.Entities.Models
{
    [Table("Certificates")]
    public class Certificate : IBaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public virtual ICollection<UserCertificate> UserCertificates { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
