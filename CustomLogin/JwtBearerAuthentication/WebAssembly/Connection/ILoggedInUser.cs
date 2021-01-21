namespace WebAssembly.Connection
{
    public interface ILoggedInUser
    {
        string EmailAddress { get; set; }
        string Id { get; set; }
        string Token { get; set; }

        void ResetUserModel();
    }
}