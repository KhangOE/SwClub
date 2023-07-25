using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwClub.DataTransferObjects.Auth
{
    public class RegisterDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
