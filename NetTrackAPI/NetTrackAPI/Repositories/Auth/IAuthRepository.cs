using NetTrackAPI.Models;

namespace NetTrackAPI.Repositories.Auth
{
    public interface IAuthRepository
    {
        
        Task<User> Create(JsonUser user);

        Task<User> Login(string email, string password);
    }
}
