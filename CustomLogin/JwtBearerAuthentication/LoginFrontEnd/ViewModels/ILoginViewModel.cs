using System.Threading.Tasks;

namespace LoginFrontEnd.ViewModels
{
    public interface ILoginViewModel
    {
        string EmailAddress { get; set; }
        string Password { get; set; }

        Task LoginUser();
        void Logout();
    }
}