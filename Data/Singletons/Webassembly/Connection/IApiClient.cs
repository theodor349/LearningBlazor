using System.Threading.Tasks;

namespace Webassembly.Connection
{
    public interface IApiClient
    {
        ILoggedinUser GetUser();
        Task Login(string username, string password);
    }
}