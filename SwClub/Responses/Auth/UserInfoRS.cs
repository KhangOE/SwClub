﻿namespace SwClub.Web.Responses.Auth
{
    using System;

    public class UserInfoRS
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string AvatarUrl { get; set; }
    }
}
