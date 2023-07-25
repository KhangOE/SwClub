
namespace SwClub.Entities.IModels
{
    internal interface IBaseModel
    {
        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
