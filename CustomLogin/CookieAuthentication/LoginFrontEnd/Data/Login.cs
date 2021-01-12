using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LoginFrontEnd.Data
{
    public class Login
    {
        private readonly HttpClient _client;

        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public Login()
        {

        }

        public Login(HttpClient client)
        {
            _client = client;
        }

        public async Task LoginUser()
        {
            await _client.PostAsJsonAsync<User>("user/loginuser", this);
        }

        public static implicit operator Login(User user)
        {
            return new Login
            {
                EmailAddress = user.EmailAddress,
                Password = user.Password
            };
        }

        public static implicit operator User(Login loginViewModel)
        {
            return new User
            {
                EmailAddress = loginViewModel.EmailAddress,
                Password = loginViewModel.Password
            };
        }

    }
}
