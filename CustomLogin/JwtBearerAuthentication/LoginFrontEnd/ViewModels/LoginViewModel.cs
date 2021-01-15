using LoginFrontEnd.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LoginFrontEnd.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        private readonly IApiHelper _api;

        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {

        }
        public LoginViewModel(IApiHelper api)
        {
            _api = api;
        }

        public async Task LoginUser()
        {
            await _api.LoginAsync(EmailAddress, Password);
        }

        public void Logout()
        {
            _api.Logout();
        }
    }
}
