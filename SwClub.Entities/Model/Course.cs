
using SwClub.Entities.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwClub.Entities.Models
{
    [Table("Course")]
    public class Course : IBaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ClubId { get; set; }
        public Guid CertificateId { get; set; }
        public virtual ICollection<UserCourse> UserCourses { get; set; }
        public virtual Club Club { get; set; }
        public virtual Certificate Certificate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
