namespace SwClub.Web.Requests.Auth
{
    using System.ComponentModel.DataAnnotations;

    public class LoginRQ
    {
        /// <summary>
        /// CPS number.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }
    }
}
