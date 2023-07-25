namespace SwClub.Web.Controllers.V1
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using SwClub.Common.Enums;
    using SwClub.Common.Helpers;
    using SwClub.Common.Messages;
    using SwClub.DataTransferObjects.Auth;
    using SwClub.Services.IServices;
    using SwClub.Web.Controllers;
    using SwClub.Web.Filters;
    using SwClub.Web.Responses.Auth;
    using SwClub.Web.Responses.Base;
    using SwClub.Web.Requests.Auth;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using SwClub.Controllers;
    using SwClub.Web.Requests.Auth;
    using Microsoft.AspNetCore.Identity;
    using SwClub.Entities.Models;
    using SwClub.DataTransferObjects.Base;
    using SwClub.Api.Responses.Auth;
    using Newtonsoft.Json.Linq;

    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : BaseApiController
    {
        private readonly IStringLocalizer<AuthController> _localizer;
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public AuthController(
            UserManager<User> userManager,
            IMapper mapper,
            IStringLocalizer<AuthController> localizer,
            IAuthService authService)
            : base(mapper)
        {
            this._localizer = localizer;
            this._authService = authService;
            this._userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<BaseRS<LoginRS>>> Login([FromBody] LoginRQ request)
        {
            var response = new BaseRS<LoginRS>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.UserName))
                {
                    response.Success = false;
                    response.ErrorCode = AuthControllerMS.Login.UserNameRequired;
                    response.Message = this._localizer[response.ErrorCode];
                    return Ok(response);
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    response.Success = false;
                    response.ErrorCode = AuthControllerMS.Login.PasswordRequired;
                    response.Message = this._localizer[response.ErrorCode];
                    return Ok(response);
                }

                var (actionStatus, errorCode, result) = await this._authService.Login(this.Mapper.Map<LoginRequestDTO>(request));
                if (actionStatus != ActionStatus.Success)
                {
                    response.Success = false;
                    response.ErrorCode = errorCode;
                    response.Message = this._localizer[response.ErrorCode];
                    return Ok(response);
                }

                response.Data = new LoginRS
                {
                    Token = result.Token,
                    Expiration = result.Expiration.ConvertToTime(),
                    IsFirstLogin = result.IsFirstLogin,
                };
                Response.Cookies.Append("X-Access-Token", response.Data.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return this.ServerError(AuthControllerMS.Login.Exception, _localizer[AuthControllerMS.Login.Exception], ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<BaseRS<RegisterRS>>> Register([FromBody] RegisterRequestDTO request)
        {

            var respone = new BaseRS<RegisterRS>();
            try
            {
                if (string.IsNullOrEmpty(request.UserName))
                {
                    respone.Success = false;
                    respone.ErrorCode = AuthControllerMS.Register.UserNameRequired;
                    respone.Message = this._localizer[respone.ErrorCode];
                }
                if (string.IsNullOrEmpty(request.FirstName))
                {
                    respone.Success = false;
                    respone.ErrorCode = AuthControllerMS.Register.FirstNameRequired;
                    respone.Message = this._localizer[respone.ErrorCode];
                }
                if (string.IsNullOrEmpty(request.LastName))
                {
                    respone.Success = false;
                    respone.ErrorCode = AuthControllerMS.Register.LastNameRequired;
                    respone.Message = this._localizer[respone.ErrorCode];
                }
                var (actionStatus, errorCode, result) = await _authService.Register(request);
                if (actionStatus != ActionStatus.Success)
                {
                    respone.Success = false;
                    respone.ErrorCode = errorCode;
                    respone.Message = this._localizer[respone.ErrorCode];              
                }
                if (ModelState.IsValid)
                {
                   // respone.ErrorCode = ModelState.err
                   //var x = ModelState.Select(x => x.)
                }
                respone.Data = new RegisterRS
                {
                  //  Token = result.Token,
                   // Expiration = result.Expiration.ConvertToTime(),
                };
                return Ok(respone);
            }
            catch (Exception ex)
            {
                return this.ServerError(AuthControllerMS.Login.Exception, _localizer[AuthControllerMS.Login.Exception], ex);
            }
            // var register = await _authService.Register(request);
            /*    var user = new User
                {
                    Email = "12322222222",
                    FirstName = "1222222222",
                    LastName = "222222222222222222",
                    UserName = "222222222222222222",
                    FullName = "222222222222222222",
                    AvatarUrl = "12333333333333333",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                var createUser = await _userManager.CreateAsync(user, "222222222222222222aAs!");

                if (!createUser.Succeeded)
                {
                    // return (ActionStatus.Failed, "add roll fail", null);
                    throw new Exception("fail");
                }*/

       
        }
            

        /*
        [HttpPut("change-password")]
        public async Task<ActionResult<BaseRS<bool>>> ChangePassword([FromBody] ChangePasswordRQ request)
        {
            var response = new BaseRS<bool>();

            try
            {
                if (string.IsNullOrWhiteSpace(request.UserName))
                {
                    response.Success = false;
                    response.ErrorCode = AuthControllerMS.ChangePassword.UserNameRequired;
                    response.Message = this._localizer[response.ErrorCode];
                    return Ok(response);
                }

                if (string.IsNullOrWhiteSpace(request.OldPassword))
                {
                    response.Success = false;
                    response.ErrorCode = AuthControllerMS.ChangePassword.PasswordRequired;
                    response.Message = this._localizer[response.ErrorCode];
                    return Ok(response);
                }

                if (!string.IsNullOrWhiteSpace(request.Password))
                {
                    request.Password = request.Password.Trim();
                }

                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    response.Success = false;
                    response.ErrorCode = AuthControllerMS.ChangePassword.PasswordRequired;
                    response.Message = this._localizer[response.ErrorCode];
                    return Ok(response);
                }

                var changePassswordRequest = this.Mapper.Map<ChangePassswordRequestDTO>(request);
                var (actionStatus, errorCode) = await this._authService.ChangePassword(this.UserInfo.UserId, changePassswordRequest);
                if (actionStatus != ActionStatus.Success)
                {
                    response.Success = false;
                    response.ErrorCode = errorCode;
                    response.Message = this._localizer[response.ErrorCode];
                    return Ok(response);
                }

                response.Data = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return this.ServerError(AuthControllerMS.ChangePassword.Exception, _localizer[AuthControllerMS.ChangePassword.Exception], ex);
            }
        }*/

        [HttpPost("reset-password")]
        [TypeFilter(typeof(PermissionFilter), Arguments = new object[] { RoleType.Administrator })]
        public async Task<ActionResult<BaseRS<ResetPasswordRS>>> ResetPassword([FromBody] ResetPasswordRQ request)
        {
            var response = new BaseRS<ResetPasswordRS>();

            try
            {
                request.UserName = request.UserName.Trim();
                if (string.IsNullOrWhiteSpace(request.UserName))
                {
                    response.Success = false;
                    response.ErrorCode = AuthControllerMS.ResetPassword.UserNameRequired;
                    response.Message = this._localizer[response.ErrorCode];
                    return Ok(response);
                }

                var (actionStatus, errorCode, result) = await this._authService.ResetPassword(this.Mapper.Map<ResetPasswordRequestDTO>(request));
                if (actionStatus != ActionStatus.Success)
                {
                    response.Success = false;
                    response.ErrorCode = errorCode;
                    response.Message = this._localizer[response.ErrorCode];
                    return Ok(response);
                }

                response.Data = new ResetPasswordRS()
                {
                    Password = result.Password,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return this.ServerError(AuthControllerMS.ResetPassword.Exception, _localizer[AuthControllerMS.ResetPassword.Exception], ex);
            }
        }
    }
}
