using SwClub.Common.Enums;
using SwClub.Entities.Models;
using SwClub.Repositories.Interfaces;
using SwClub.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwClub.Services.Services
{
    public class ClubService : IClubService
    {
        private IUnitOfWork _unitOfWork;

        public ClubService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<Club>> GetAll()
        {
            var clubs = await _unitOfWork.Clubs.QueryAll();
            return clubs.ToList();
        }

        
        public async Task<(ActionStatus,string,Club)> Create(Club club)
        {
            _unitOfWork.Clubs.Add(club);
            _unitOfWork.SaveChanges();
            return (ActionStatus.Success, string.Empty,club);
        }
    }
}
