using System.Threading.Tasks;

namespace LoginFrontEnd.Data
{
    public interface ILogin
    {
        string EmailAddress { get; set; }
        string Password { get; set; }

        Task LoginUser();
    }
}