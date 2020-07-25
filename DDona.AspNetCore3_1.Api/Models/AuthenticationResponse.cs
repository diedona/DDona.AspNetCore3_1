using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDona.AspNetCore3_1.Api.Models
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse(User user, string token)
        {
            User = user;
            Token = token;
        }

        public User User { get; set; }
        public string Token { get; set; }
    }
}
