using SwClub.Common.Enums;
using SwClub.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwClub.Services.IServices
{
    public interface IClubService
    {
        Task<List<Club>> GetAll();

        Task<(ActionStatus,string,Club)> Create(Club club);
    }
}
