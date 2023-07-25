namespace SwClub.Common.Messages
{
    public static class AuthControllerMS
    {
        public static class Login
        {
            public const string LoginTypeRequired = "Login.LoginTypeRequired";
            public const string UserNameRequired = "Login.UserNameRequired";
            public const string PasswordRequired = "Login.PasswordRequired";
            public const string Invalid = "Login.Invalid";
            public const string Exception = "Login.Exception";
        }
        public static class Register
        {
            public const string LoginTypeRequired = "Register.RegisterTypeRequired";
            public const string UserNameRequired = "Register.UserNameRequired";
            public const string PasswordRequired = "Register.PassworkRequired";
            public const string FirstNameRequired = "Register.FirstNameRequired";
            public const string LastNameRequired = "Register.LastNameRequired";
            public const string Invalid = "Register.Invalid";
            public const string Exception = "Login.Exception";
        }

        public static class ChangePassword
        {
            public const string Invalid = "ChangePassword.Invalid";
            public const string UserNameRequired = "ChangePassword.UserNameRequired";
            public const string OldPasswordRequired = "ChangePassword.OldPasswordRequired";
            public const string PasswordRequired = "ChangePassword.PasswordRequired";
            public const string UserNotFound = "ChangePassword.UserNotFound";
            public const string Exception = "ChangePassword.Exception";
        }

        public static class ResetPassword
        {
            public const string UserNameRequired = "ResetPassword.UserNameRequired";
            public const string Invalid = "ResetPassword.Invalid";
            public const string Exception = "ResetPassword.Exception";
        }
    }
}
