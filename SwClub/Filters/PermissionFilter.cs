namespace SwClub.Web.Filters
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using SwClub.Common.Constants;
    using SwClub.Common.Enums;
    using SwClub.DataTransferObjects.Auth;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class PermissionFilter : ActionFilterAttribute
    {
        private readonly ILogger<PermissionFilter> _logger;
        private readonly RoleType _role;

        public PermissionFilter(ILogger<PermissionFilter> logger, RoleType role)
        {
            this._logger = logger;
            this._role = role;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var check = context.HttpContext.Request.Cookies["token"];
            if(check != null){
                Console.WriteLine(check);
            }
            else{
                Console.WriteLine("check null hew");
            }
            var tokenEncodedString = context.HttpContext.Request.Headers[GlobalConstant.Authorize.Authorization].ToString();
            if(string.IsNullOrEmpty(tokenEncodedString)){
                this._logger.LogError(GlobalConstant.AuthorizeMessage.Status403Forbidden);
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.HttpContext.Response.WriteAsync(GlobalConstant.AuthorizeMessage.Status403Forbidden);
                return;
            }
            var token = new JwtSecurityToken(tokenEncodedString[7..]);
            var userInfo = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            Console.WriteLine("filter ");
            Console.WriteLine("filter token : " + tokenEncodedString);
            Console.WriteLine("user infor : " + userInfo);

            if (string.IsNullOrEmpty(userInfo))
            {
                
                this._logger.LogError(GlobalConstant.AuthorizeMessage.Status403Forbidden);
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.HttpContext.Response.WriteAsync(GlobalConstant.AuthorizeMessage.Status403Forbidden);
                return;
            }

            var userInfoViewModel = JsonConvert.DeserializeObject<UserInfoDTO>(userInfo);
            if (this._role == RoleType.Administrator)
            {
                // TODO check user's role 
            }

            await next();
        }
    }
}
