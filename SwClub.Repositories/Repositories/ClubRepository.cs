namespace SwClub.Repositories.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SwClub.Entities.Models;
    using SwClub.Repositories.Interfaces;

    public class ClubRepository : BaseRepository<Club>, IClubRepository
    {
        public ClubRepository(DbContext context) 
            : base(context) { }
    }
}
