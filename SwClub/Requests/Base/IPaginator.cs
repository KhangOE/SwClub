﻿namespace SwClub.Web.Requests.Base
{
    public interface IPaginator
    {
        public int Start { get; set; }

        public int Length { get; set; }
    }
}
