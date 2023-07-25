namespace SwClub.Repositories.Repositories
{
    using SwClub.Entities.Models;
    using SwClub.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }
    }
}
