using LoginFrontEnd.Models;
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
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        private HttpClient _httpClient;
        public LoginViewModel()
        {

        }
        public LoginViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoginUser()
        {
            var response = await _httpClient.PostAsJsonAsync<TokenRequestModel>("token", this);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsAsync<TokenResult>();
                Token = res.Access_Token;
            }
        }

        public static implicit operator TokenRequestModel(LoginViewModel model)
        {
            return new TokenRequestModel()
            {
                Username = model.EmailAddress,
                Password = model.Password,
            };
        }
    }
}
