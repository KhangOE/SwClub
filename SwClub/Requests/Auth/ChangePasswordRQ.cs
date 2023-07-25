namespace SwClub.Web.Requests.Auth
{
    public class ChangePasswordRQ
    {
        public string UserName { get; set; }

        public string OldPassword { get; set; }

        public string Password { get; set; }
    }
}
