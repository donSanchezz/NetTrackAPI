using NetTrackAPI.Models;

namespace NetTrackAPI.Repositories.Auth
{
    public interface IAuthRepository
    {
        
        Task<User> Create(User user);

        Task<User> Login(string email, string password);
    }
}
