using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFrontEnd.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string EmailAddress { get; set; }

        public void ResetUserModel()
        {
            Token = "";
            Id = "";
            EmailAddress = "";
        }
    }
}
