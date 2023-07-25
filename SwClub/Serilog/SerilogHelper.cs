namespace SwClub.Web.Serilog
{
    using System;
    using SwClub.DataTransferObjects.Auth;
    using SwClub.DataTransferObjects.Base;
    using global::Serilog;
    using global::Serilog.Context;
    using global::Serilog.Events;
    using SwClub.Common.Constants;
    using Microsoft.AspNetCore.Http;

    public class SerilogHelper
    {
        public static void WriteLog(HttpContext httpContext, UserInfoDTO userInfo, string message, Exception ex = null, LogEventLevel logEventLevel = LogEventLevel.Error)
        {
            LogContext.PushProperty(GlobalConstant.LoggingProperties.ClientIP, httpContext.Connection.RemoteIpAddress);
            LogContext.PushProperty(GlobalConstant.LoggingProperties.HostName, httpContext.Request.Host);
            LogContext.PushProperty(GlobalConstant.LoggingProperties.Protocol, httpContext.Request.Scheme);
            LogContext.PushProperty(GlobalConstant.LoggingProperties.Method, httpContext.Request.Method);
            LogContext.PushProperty(GlobalConstant.LoggingProperties.UserName, userInfo != null ? userInfo.UserName : string.Empty);

            if (ex != null)
            {
                switch (logEventLevel)
                {
                    case LogEventLevel.Error:
                        Log.Error(ex, message);
                        break;
                    default:
                        Log.Fatal(ex, message);
                        break;
                }
            }
            else
            {
                switch (logEventLevel)
                {
                    case LogEventLevel.Debug:
                        Log.Debug(message);
                        break;
                    case LogEventLevel.Information:
                        Log.Information(message);
                        break;
                    case LogEventLevel.Warning:
                        Log.Warning(message);
                        break;
                    case LogEventLevel.Error:
                        Log.Error(message);
                        break;
                    default:
                        Log.Fatal(message);
                        break;
                }
            }
        }

        public static void WriteLog(HttpContext httpContext, string message, Exception ex = null, ActionAuditPropertyDTO property = null, LogEventLevel logEventLevel = LogEventLevel.Error)
        {
            LogContext.PushProperty(GlobalConstant.LoggingProperties.ClientIP, httpContext.Connection.RemoteIpAddress);
            LogContext.PushProperty(GlobalConstant.LoggingProperties.HostName, httpContext.Request.Host);
            LogContext.PushProperty(GlobalConstant.LoggingProperties.Protocol, httpContext.Request.Scheme);
            LogContext.PushProperty(GlobalConstant.LoggingProperties.Method, httpContext.Request.Method);

            if (property != null)
            {
                LogContext.PushProperty(GlobalConstant.LoggingProperties.UserName, property.UserName);
                LogContext.PushProperty(GlobalConstant.LoggingProperties.EventEntity, property.ActionEventEntity);
            }

            if (ex != null)
            {
                switch (logEventLevel)
                {
                    case LogEventLevel.Error:
                        Log.Error(ex, message);
                        break;
                    default:
                        Log.Fatal(ex, message);
                        break;
                }
            }
            else
            {
                switch (logEventLevel)
                {
                    case LogEventLevel.Debug:
                        Log.Debug(message);
                        break;
                    case LogEventLevel.Information:
                        Log.Information(message);
                        break;
                    case LogEventLevel.Warning:
                        Log.Warning(message);
                        break;
                    case LogEventLevel.Error:
                        Log.Error(message);
                        break;
                    default:
                        Log.Fatal(message);
                        break;
                }
            }
        }
    }
}
