using NZwalks.Models.Domain;

namespace NZwalks.Repository
{
    public interface ITokenHandler
    {
        Task<string> CreateToken(User user);
    }
}
