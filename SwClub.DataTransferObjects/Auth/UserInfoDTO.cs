namespace SwClub.DataTransferObjects.Auth
{
    using System;

    public class UserInfoDTO
    {
        /// <summary>
        /// UserId.
        /// Required for current user.
        /// </summary>
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public bool IsFirstLogin { get; set; }
    }
}
