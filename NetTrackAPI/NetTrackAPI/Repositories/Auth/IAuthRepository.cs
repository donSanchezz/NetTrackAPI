using NetTrackAPI.Models;
using NetTrackAPI.ViewModels;

namespace NetTrackAPI.Repositories.Auth
{
    public interface IAuthRepository
    {
        
        Task<UserModel> Create(UserModel user);

        Task<UserModel> Login(LoginModel details);
    }
}
