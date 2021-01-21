using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAssembly.Connection
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IApiHelper _api;

        public ApiAuthenticationStateProvider(IApiHelper api)
        {
            _api = api;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string username = await _api.GetLoggedInUsername();
            if (username is not null)
            {
                //create a claims
                var claimEmailAddress = new Claim(ClaimTypes.Name, username);
                var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(username));
                //create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier }, "serverAuth");
                //create claimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return new AuthenticationState(claimsPrincipal);
            }
            else
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
