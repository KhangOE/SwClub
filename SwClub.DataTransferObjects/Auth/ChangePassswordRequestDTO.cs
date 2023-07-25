namespace SwClub.DataTransferObjects.Auth
{
    public class ChangePassswordRequestDTO
    {
        public string UserName { get; set; }

        public string OldPassword { get; set; }

        public string Password { get; set; }
    }
}
