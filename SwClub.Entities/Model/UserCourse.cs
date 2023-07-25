
using SwClub.Entities.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwClub.Entities.Models
{
    [Table("UserCourses")]
    public class UserCourse : IBaseModel
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public virtual User User { get; set; }
        public virtual Course Course { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}

    }
}
