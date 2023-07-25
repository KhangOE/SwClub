namespace SwClub.Entities.Models
{
    using SwClub.Entities.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ApplicationSettings")]
    public class ApplicationSetting : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ApplicationSettingId { get; set; }

        [MaxLength(250)]
        [Required]
        public string SettingCode { get; set; }

        public string SettingValue { get; set; }

        public string Description { get; set; }
    }
}
