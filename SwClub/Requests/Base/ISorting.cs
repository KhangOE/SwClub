﻿namespace SwClub.Web.Requests.Base
{
    public interface ISorting
    {
        public string Column { get; set; }

        public string Direction { get; set; }
    }
}
