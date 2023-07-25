namespace SwClub.Web.Requests.Auth
{
    public class UserInfoRQ
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarBase64 { get; set; }

        public bool IsRemoveAvatar { get; set; }
    }
}
