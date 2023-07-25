namespace SwClub.Repositories.Repositories
{
    using SwClub.Entities.Models;
    using SwClub.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationSettingRepository : BaseRepository<ApplicationSetting>, IApplicationSettingRepository
    {
        public ApplicationSettingRepository(DbContext context)
            : base(context)
        {
        }
    }
}
