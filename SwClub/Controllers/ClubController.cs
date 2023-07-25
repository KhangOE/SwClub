using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SwClub.DataTransferObjects;
using SwClub.Entities.Models;
using SwClub.Repositories.Interfaces;
using SwClub.Services.IServices;
using SwClub.Web.Controllers.V1;
using SwClub.Web.Filters;
using SwClub.Common.Enums;
using SwClub.Common.Helpers;
using SwClub.Common.Messages;
namespace SwClub.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : BaseApiController
    {
        private readonly IStringLocalizer<AuthController> _localizer;
        private readonly IAuthService _authService;
        private readonly IClubService _clubService;
        private IUnitOfWork _unitOfWork;
        public ClubController(
            IMapper mapper,
            IStringLocalizer<AuthController> localizer,
            IAuthService authService,
            IClubService clubService,
            IUnitOfWork unitOfWork)
            
            : base(mapper)
        {
            _localizer = localizer;
            _authService = authService;
            _clubService = clubService;
            _unitOfWork = unitOfWork;
        }
        
        [AllowAnonymous]
       // [Authorize]
        [TypeFilter(typeof(PermissionFilter), Arguments = new object[] { RoleType.Administrator })]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var x = User.Identity.Name;
            var clubs = await _clubService.GetAll();
            var clubsRespone = Mapper.Map<List<ClubDTO>>(clubs);
            
            return Ok(clubsRespone);
        }
        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ClubRequestDTO request)
        {
           
            var club  = new Club() { 
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            };
            await this._unitOfWork.Clubs.Add(club);
            await this._unitOfWork.SaveChanges();
            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            
            var club = await _unitOfWork.Clubs.FindById(id);
            await _unitOfWork.Clubs.Delete(club);
            await this._unitOfWork.SaveChanges();

            return Ok(club);
        }

        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}
