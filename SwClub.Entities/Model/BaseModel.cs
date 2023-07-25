namespace SwClub.Entities.Models
{
    using System;
    using SwClub.Entities.IModels;

    public class BaseModel : IBaseModel
    {
        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
