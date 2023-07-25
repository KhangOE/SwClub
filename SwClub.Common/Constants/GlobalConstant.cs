namespace SwClub.Common.Constants
{
    public static class GlobalConstant
    {
        public const string RegexCharacterAndDigit = @"[a-zA-Z0-9]+";
        public const string LowerCharacters = "abcdefghjkmnopqursuvwxyz";
        public const string UpperCharacters = "ABCDEFGHJKMNOPQRSTUVWXYZ";
        public const string Digits = "123456789";
        public const string SpecialCharacters = @"!@#$%^&*()";

        public static class DecimalNumbers
        {
            public const int OneDecimalNumber = 1;
            public const int TwoDecimalNumber = 2;
        }

        public static class MaxLengthCharacter
        {
            public const int Length20 = 20;
            public const int Length100 = 100;
            public const int Length256 = 256;
            public const int Length500 = 500;
        }

        public static class PageConfig
        {
            public const int Start = 1;
            public const int Length = 10;
            public const int MaxLength = 500;
        }

        public static class LoggingType
        {
            public const string Tracking = "Tracking";
            public const string Log = "Log";
            public const string Debug = "Debug";
        }

        public static class LoggingProperties
        {
            public const string ClientIP = "ClientIP";
            public const string HostName = "HostName";
            public const string Protocol = "Protocol";
            public const string Method = "Method";
            public const string UserName = "UserName";
            public const string UserId = "UserId";
            public const string SuperApp = "SuperApp";
            public const string MiniApp = "MiniApp";
            public const string Event = "Event";
            public const string EventEntity = "EventEntity";
            public const string EventName = "EventName";
            public const string Type = "Type";
            public const string DataInput = "DataInput";
            public const string Data = "Data";
            public const string ActionStatus = "ActionStatus";
        }

        public static class Config
        {
            public const string JWTSecret = "JWT:Secret";
            public const string JWTExpiryTime = "JWT:ExpiryTime";
        }

        public static class Env
        {
            public const string SQL_VERSION = "SQL_VERSION";
            public const string DATABASE_URL = "DATABASE_URL";

            public const string SWAGGER_ENABLED = "SWAGGER_ENABLED";
            public const string HTTPS_REDIRECT = "HTTPS_REDIRECT";
            public const string AUTO_MIGRATE = "AUTO_MIGRATE";
            public const string DEBUG_MESSAGE_ENABLED = "DEBUG_MESSAGE_ENABLED";
            public const string LOG_DEBUG_ENABLED = "LOG_DEBUG_ENABLED";
        }

        public static class ContentType
        {
            public const string ApplicationJson = "application/json";
        }

        public static class Authorize
        {
            public const string Authorization = "Authorization";
            public const string Bearer = "Bearer";
            public const string Basic = "Basic";
            public const string GrantType = "grant_type";
            public const string ClientCredentials = "client_credentials";
            public const string XAccessToken = "x-access-token";
        }

        public static class FolderFile
        {
            public const string ResourceFolder = "Resources";
        }

        public static class AuthorizeMessage
        {
            public const string Status403Forbidden = "You don't have the user rights to do this action!";
        }

        public static class ImageExtentions
        {
            public const string Jpg = ".jpg";
            public const string Jpeg = ".jpeg";
            public const string Png = ".png";
        }

        public static class Languages
        {
            public const string EnglishKey = "en";
        }

        public static class OrderBy
        {
            public const string DESC = "desc";
            public const string ASC = "asc";
        }
    }
}
