using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliRestApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
        public string Token { get; set; }
    }
}
