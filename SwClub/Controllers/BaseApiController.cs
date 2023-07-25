namespace SwClub.Web.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using AutoMapper;
    using SwClub.Common.Constants;
    using SwClub.DataTransferObjects.Auth;
    using SwClub.Web.Responses.Base;
    using SwClub.Web.Serilog;
    using global::Serilog.Events;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Security.Claims;

    [Authorize]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController(
            IMapper mapper)
        {
            this.Mapper = mapper;
        }

        public UserInfoDTO UserInfo
        {
            get
            {
                try
                {
                    var tokenEncodedString = this.HttpContext.Request.Headers[GlobalConstant.Authorize.Authorization].ToString();
                    // trim 'Bearer ' from the start since its just a prefix for the token string
                    var token = new JwtSecurityToken(tokenEncodedString[7..]);
                    var userInfo = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                    if (string.IsNullOrEmpty(userInfo))
                    {
                        return null;
                    }

                    return JsonConvert.DeserializeObject<UserInfoDTO>(userInfo);
                }
                catch
                {
                    return null;
                }
            }
        }

        public string AcceptLanguage
        {
            get
            {
                try
                {
                    var language = this.HttpContext.Request.Headers["Accept-Language"].ToString();
                    if (string.IsNullOrEmpty(language))
                    {
                        return GlobalConstant.Languages.EnglishKey;
                    }

                    language = language?.Trim();
                    if (language != GlobalConstant.Languages.EnglishKey)
                    {
                        return GlobalConstant.Languages.EnglishKey;
                    }

                    return language;
                }
                catch
                {
                    return GlobalConstant.Languages.EnglishKey;
                }
            }
        }

        public bool DebugMessage
        {
            get
            {
                return Environment.GetEnvironmentVariable(GlobalConstant.Env.DEBUG_MESSAGE_ENABLED) == "True";
            }
        }

        protected IMapper Mapper { get; }

        protected void WriteLog(string message, Exception ex = null, LogEventLevel logEventLevel = LogEventLevel.Error)
        {
            SerilogHelper.WriteLog(this.HttpContext, this.UserInfo, message, ex, logEventLevel);
        }

        protected ActionResult ServerError(string errorCode, string message, Exception ex)
        {
            this.WriteLog(ex.Message, ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ExceptionRS(HttpStatusCode.InternalServerError, errorCode, message, ex.Message));
        }
    }
}
