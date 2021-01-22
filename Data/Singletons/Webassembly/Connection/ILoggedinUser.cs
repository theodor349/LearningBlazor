namespace Webassembly.Connection
{
    public interface ILoggedinUser
    {
        string Token { get; set; }
        string Username { get; set; }
    }
}