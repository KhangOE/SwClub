namespace SwClub.Web.Mappings
{
    using AutoMapper;
    using SwClub.DataTransferObjects.Auth;
    using SwClub.DataTransferObjects.Base;
    using SwClub.Web.Requests.Auth;
    using SwClub.Web.Responses.Auth;
    using SwClub.Web.Responses.Base;
    using SwClub.Web.Requests.Auth;
    using SwClub.Entities.Models;
    using SwClub.DataTransferObjects;
    using SwClub.Api.Requests.Auth;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Common
            this.CreateMap<SelectListItemDTO, SelectListItemRS>();

            // AuthController
            this.CreateMap<RegisterRQ, RegisterRequestDTO>();
            this.CreateMap<LoginRQ, LoginRequestDTO>();
            this.CreateMap<UserInfoRQ, UserInfoRequestDTO>();
            this.CreateMap<UserInfoDTO, UserInfoRS>();
            this.CreateMap<ChangePasswordRQ, ChangePassswordRequestDTO>();
            this.CreateMap<ForgotPasswordRQ, ForgotPasswordRequestDTO>();
            this.CreateMap<ResetPasswordRQ, ResetPasswordRequestDTO>();
            //ClubController
            this.CreateMap<Club,ClubDTO>();
        }
    }
}
