using System.Net.Http;
using System.Threading.Tasks;

namespace WebAssembly.Connection
{
    public interface IApiHelper
    {
        HttpClient Client { get; }

        Task<string> GetLoggedInUsername();
        Task LoginAsync(string username, string password);
        void Logout();
    }
}