using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoginFrontEnd.Api
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }

        Task LoginAsync(string username, string password);
        Task<string> GetLoggedInUsername();
        void Logout();
    }
}