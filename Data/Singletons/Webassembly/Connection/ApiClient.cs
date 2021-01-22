using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webassembly.Connection
{
    public class ApiClient : IApiClient
    {
        private readonly ILoggedinUser _user;

        public ApiClient(ILoggedinUser user)
        {
            _user = user;
        }

        public async Task Login(string username, string password)
        {
            _user.Username = "Theodor";
            _user.Token = "This is a token";
        }

        public ILoggedinUser GetUser()
        {
            return _user;
        }
    }

    public class LoggedinUser : ILoggedinUser
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
