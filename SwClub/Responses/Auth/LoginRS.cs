namespace SwClub.Web.Responses.Auth
{
    using System;

    public class LoginRS
    {
        public string Token { get; set; }

        public string Expiration { get; set; }

        public bool IsFirstLogin { get; set; }

        public Guid? TreatmentPlanId { get; set; }

        public bool IsStart { get; set; }
    }
}
