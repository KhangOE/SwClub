namespace SwClub.Repositories.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        IClubRepository Clubs { get; }

        Task<int> SaveChanges();
    }
}
