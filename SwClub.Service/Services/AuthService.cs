namespace SwClub.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using SwClub.DataTransferObjects.Auth;
    using SwClub.Services.IServices;
    using SwClub.Common.Constants;
    using SwClub.Common.Enums;
    using SwClub.Common.Helpers;
    using SwClub.Entities.Models;
    using SwClub.Repositories.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using SwClub.Common.Messages;

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly byte[] _securityKey;
        private readonly int _expiryTime;

        public AuthService(
            IConfiguration configuration,
            IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this._configuration = configuration;
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._signInManager = signInManager;

            this._securityKey = Encoding.UTF8.GetBytes("1212121212asasasasasasasqwqwqwqwqwqwqw");

            if (int.TryParse(this._configuration[GlobalConstant.Config.JWTExpiryTime], out int expiryTime))
            {
                this._expiryTime = expiryTime;
            }
            else
            {
                this._expiryTime = 60 * 24 * 90;
            }
        }
        public async Task<(ActionStatus, string,RegisterDTO)> Register(RegisterRequestDTO request)
        {
            var checkUser = await _userManager.FindByNameAsync(request.UserName);
            if (checkUser != null)
            {
                
                return (ActionStatus.Failed, "User Name Exist", null);
            }
            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                FullName = request.FirstName + " " + request.LastName,
                AvatarUrl = "111111111111111112332323",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            var createUser = await _userManager.CreateAsync(user,request.Password);

            if (!createUser.Succeeded)
            {
                return (ActionStatus.Failed, createUser.Errors.ToString(), null);
            }

            // foreach (var role in roles)
            //  {
            RoleType role = RoleType.User;
          // var addRole = await _userManager.AddToRoleAsync(user, role.ToString());
          /*
            if (!addRole.Succeeded)
            {
                return (ActionStatus.Failed, "add roll fail", null);

                //  }
            }
          */
            return (ActionStatus.Success, string.Empty, null); 
        }
        public async Task<(ActionStatus, string, LoginDTO)> Login(LoginRequestDTO request)
        {
            var isFirstLogin = false;
            var user = await this._userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return (ActionStatus.NotFound, AuthControllerMS.Login.Invalid, null);
            }
            else
            {
                var result = await this._userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return (ActionStatus.NotFound, AuthControllerMS.Login.Invalid, null);
                }
            }

            var validPassword = await this.ValidatePassword(request.UserName, request.Password);
            if (validPassword)
            {
                return (ActionStatus.Success, string.Empty, await this.GenerateToken(user, isFirstLogin));
            }

            return (ActionStatus.NotFound, AuthControllerMS.Login.Invalid, null);
        }

        public async Task<(ActionStatus, string)> ChangePassword(Guid userId, ChangePassswordRequestDTO changePassswordRequest)
        {
            var user = await this._unitOfWork.Users.FindById(userId);
            if (user == null)
            {
                return (ActionStatus.NotFound, AuthControllerMS.ChangePassword.UserNotFound);
            }

            if (user.UserName != changePassswordRequest.UserName)
            {
                return (ActionStatus.NotFound, AuthControllerMS.ChangePassword.UserNotFound);
            }

            var validPassword = await this.ValidatePassword(changePassswordRequest.UserName, changePassswordRequest.OldPassword);
            if (!validPassword)
            {
                return (ActionStatus.Invalid, AuthControllerMS.ChangePassword.Invalid);
            }

            user.PasswordHash = changePassswordRequest.Password.HashPassword();
            await this._unitOfWork.Users.Update(user);
            await this._unitOfWork.SaveChanges();

            return (ActionStatus.Success, string.Empty);
        }

        public async Task<(ActionStatus, string, ResetPasswordDTO)> ResetPassword(ResetPasswordRequestDTO resetPasswordRequest)
        {
            var user = await this._userManager.FindByNameAsync(resetPasswordRequest.UserName);
            if (user == null)
            {
                return (ActionStatus.NotFound, AuthControllerMS.ResetPassword.Invalid, null);
            }

            var newPassword = FunctionDataHelper.GeneratePassword(true, true, true, true, 10);

            user.PasswordHash = newPassword.HashPassword();
            await this._unitOfWork.Users.Update(user);
            await this._unitOfWork.SaveChanges();

            var result = new ResetPasswordDTO()
            {
                Password = newPassword,
            };

            return (ActionStatus.Success, string.Empty, result);
        }

        private async Task<bool> ValidatePassword(string userName, string password)
        {
            var result = await this._signInManager.PasswordSignInAsync(userName, password, false, true);

            return result.Succeeded;
        }

        private async Task<LoginDTO> GenerateToken(User user, bool isFirstLogin)
        {
            var authClaims = new List<Claim>();

            var userInfo = await this.GetUserInfo(user);

            authClaims.Add(new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(userInfo)));

            var authSigningKey = new SymmetricSecurityKey(this._securityKey);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(this._expiryTime),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new LoginDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo.ToLocalTime(),
                IsFirstLogin = isFirstLogin,
            };
        }

        private async Task<UserInfoDTO> GetUserInfo(User user)
        {
            var userInfo = new UserInfoDTO
            {
                UserId = user.Id,
                UserName = user.UserName,
            };

            return userInfo;
        }
    }
}
