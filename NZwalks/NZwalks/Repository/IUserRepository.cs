using NZwalks.Models.Domain;

namespace NZwalks.Repository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUser(string username, string password);
    }
}
