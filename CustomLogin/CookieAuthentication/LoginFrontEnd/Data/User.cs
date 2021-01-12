using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFrontEnd.Data
{
    public class User
    {
        public long UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
