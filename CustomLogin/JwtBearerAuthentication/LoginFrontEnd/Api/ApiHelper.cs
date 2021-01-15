using LoginFrontEnd.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginFrontEnd.Api
{
    public class ApiHelper : IApiHelper
    {
        public HttpClient ApiClient => _apiClient;
        private HttpClient _apiClient;
        private readonly ILoggedInUserModel _loggedInUser;

        public ApiHelper(ILoggedInUserModel loggedInUser)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;
        }

        private void InitializeClient()
        {
            string api = "https://localhost:5001/api/";

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task LoginAsync(string username, string password)
        {
            var request = new TokenRequestModel(username, password);
            var response = await _apiClient.PostAsJsonAsync("token", request);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsAsync<TokenResult>();
                _loggedInUser.Id = res.Id;
                _loggedInUser.EmailAddress = res.UserName;
                _loggedInUser.Token = res.Access_Token;
                SetHeadders(res.Access_Token);
            }
        }

        public void Logout()
        {
            _loggedInUser.ResetUserModel();
            _apiClient.DefaultRequestHeaders.Clear();
        }

        public async Task<string> GetLoggedInUsername()
        {
            var res = await _apiClient.GetAsync("token");
            if (res.IsSuccessStatusCode)
            {
                var username = await res.Content.ReadAsAsync<string>();
                return username;
            }
            return null;
        }

        private void SetHeadders(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");
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
}
