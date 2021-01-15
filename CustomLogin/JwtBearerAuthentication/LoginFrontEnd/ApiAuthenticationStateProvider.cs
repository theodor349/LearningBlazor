using LoginFrontEnd.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginFrontEnd
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILoginViewModel _currentUser;
        private readonly HttpClient _client;

        public ApiAuthenticationStateProvider(ILoginViewModel loginModel, HttpClient client)
        {
            _currentUser = loginModel;
            _client = client;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var response = await _client.GetAsync("token");
            if (response.IsSuccessStatusCode)
            {
                var isLoggedIn = await response.Content.ReadAsAsync<bool>();
                if (isLoggedIn)
                {
                    //create a claims
                    var claimEmailAddress = new Claim(ClaimTypes.Name, _currentUser.EmailAddress);
                    var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(_currentUser.EmailAddress));
                    //create claimsIdentity
                    var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier }, "serverAuth");
                    //create claimsPrincipal
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    return new AuthenticationState(claimsPrincipal);
                }
            }
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
