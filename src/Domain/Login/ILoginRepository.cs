namespace Domain.Login
{
    public interface ILoginRepository
    {
        Task<bool> ValidateUser(string username, string password);
    }
}
