using System.Threading.Tasks;

namespace LoginFrontEnd.ViewModels
{
    public interface ILoginViewModel
    {
        string EmailAddress { get; set; }
        string Password { get; set; }
        string Token { get; set; }

        Task LoginUser();
    }
}