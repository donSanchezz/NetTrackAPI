using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetTrackAPI.Models;

namespace NetTrackAPI.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;

        public AuthRepository(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }
        public Task<User> Create([FromBody] JsonUser user)
        {
            Console.WriteLine(user);
            //var flag = _userManager.CreateAsync(user, user.Password);
            // if (flag.Result.Succeeded)
            // {
            //     return Task.FromResult(user);
            // }

            // return Task.FromResult<User>(null);
            return null;
        }

        public Task<User> Login(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
