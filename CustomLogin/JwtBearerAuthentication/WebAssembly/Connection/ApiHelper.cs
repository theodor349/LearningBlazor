using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebAssembly.Connection
{
    public class ApiHelper : IApiHelper
    {
        public HttpClient Client => _client;
        private HttpClient _client;
        private readonly ILoggedInUser _user;

        public ApiHelper(ILoggedInUser user)
        {
            _user = user;

            InitializeClient();
        }

        private void InitializeClient()
        {
            string api = "https://localhost:5001/api/";

            _client = new HttpClient();
            _client.BaseAddress = new Uri(api);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task LoginAsync(string username, string password)
        {
            var request = new TokenRequestModel(username, password);
            var response = await _client.PostAsJsonAsync("token", request);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsAsync<TokenResult>();
                _user.Id = res.Id;
                _user.EmailAddress = res.UserName;
                _user.Token = res.Access_Token;
                SetHeadders(res.Access_Token);
            }
        }

        public void Logout()
        {
            _user.ResetUserModel();
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<string> GetLoggedInUsername()
        {
            var res = await _client.GetAsync("token");
            if (res.IsSuccessStatusCode)
            {
                var username = await res.Content.ReadAsAsync<string>();
                return username;
            }
            return null;
        }

        private void SetHeadders(string token)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");
        }
    }
    class TokenRequestModel
    {
        public TokenRequestModel(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }

    class TokenResult
    {
        public string Id { get; set; }
        public string Access_Token { get; set; }
        public string UserName { get; set; }
    }

    public class LoggedInUser : ILoggedInUser
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
