namespace SwClub.Services.IServices
{
    using System;
    using System.Threading.Tasks;
    using SwClub.DataTransferObjects.Auth;
    using SwClub.Common.Enums;

    public interface IAuthService
    {
        Task<(ActionStatus, string, RegisterDTO)> Register(RegisterRequestDTO request);
        Task<(ActionStatus, string, LoginDTO)> Login(LoginRequestDTO request);

        Task<(ActionStatus, string)> ChangePassword(Guid userId, ChangePassswordRequestDTO changePassswordRequest);

        Task<(ActionStatus, string, ResetPasswordDTO)> ResetPassword(ResetPasswordRequestDTO resetPasswordRequest);
    }
}
