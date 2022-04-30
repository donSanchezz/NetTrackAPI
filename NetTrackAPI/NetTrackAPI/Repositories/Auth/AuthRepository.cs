using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetTrackAPI.Models;
using NetTrackAPI.ViewModels;

namespace NetTrackAPI.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        
        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = context;

        }

        public async Task<UserModel> Create([FromBody] UserModel user)
        {
            var userToCreate = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.Email,
                PhoneNumber = user.Phonenumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                Skin = user.Skin,
                Body = user.Body,
                Day = user.Day,
                Month = user.Month,
                Year = user.Year,
                Eye = user.Eye,
            };
            var flag = _userManager.CreateAsync(userToCreate, user.Password);
            if (flag.Result.Succeeded)
            {
                var createdUser = await _userManager.FindByEmailAsync(user.Email);
                foreach (var contact in user.contacts)
                {
                    contact.UserId = createdUser.Id;
                    _context.Contact.Add(contact);
                    _context.SaveChanges();
                }
                return new UserModel { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Phonenumber = user.Phonenumber, Body = user.Body, Skin = user.Skin, Day = user.Day, Month = user.Month, Year = user.Year, Eye = user.Eye, contacts = user.contacts };
            }

            //If we reach this point, something went wrong
            return null;
        }

        public async Task<UserModel?> Login([FromBody] LoginModel details)
        {
            
            var user = await _userManager.FindByEmailAsync(details.Email);
            var result = await _signInManager.PasswordSignInAsync(user, details.Password, false, false);
            if (result.Succeeded)
            {
                var contacts = _context.Contact.Where(c => c.UserId == user.Id).ToList();
                return new UserModel { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Phonenumber = user.PhoneNumber, contacts = contacts, Body = user.Body, Skin = user.Skin, Day = user.Day, Month = user.Month, Year = user.Year, Eye = user.Eye };
            }
            return null;
        }
    }
}
