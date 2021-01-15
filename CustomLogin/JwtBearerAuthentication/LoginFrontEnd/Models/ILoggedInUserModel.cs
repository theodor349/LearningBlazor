namespace LoginFrontEnd.Models
{
    public interface ILoggedInUserModel
    {
        string EmailAddress { get; set; }
        string Id { get; set; }
        string Token { get; set; }

        void ResetUserModel();
    }
}