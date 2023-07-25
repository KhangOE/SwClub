namespace SwClub.DataTransferObjects.Auth
{
    using System;

    public class LoginDTO
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public bool IsFirstLogin { get; set; }
    }
}
