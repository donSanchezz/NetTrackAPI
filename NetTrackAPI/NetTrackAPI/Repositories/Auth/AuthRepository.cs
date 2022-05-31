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
            user.Phonenumber = user.Phonenumber.Replace("-","");
            user.Phonenumber = user.Phonenumber.Insert(0, "1");
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
                for (int i = 0; i < user.contacts.Count; i++)
                {
                    user.contacts[i].Phone = user.contacts[i].Phone.Replace("-", "");
                    user.contacts[i].Phone = user.contacts[i].Phone.Insert(0, "1");
                    user.contacts[i].UserId = createdUser.Id;
                    _context.Contact.Add(user.contacts[i]);
                    _context.SaveChanges();
                }

                var contact = new Contact
                {
                    FirstName = "Brandon",
                    LastName = "Fong",
                    Email = "fongbrandon2000@gmail.com",
                    Phone = "18768635988",
                    Message = "Help",
                    Primary = true,
                    UserId = createdUser.Id
                };
                _context.Contact.Add(contact);


                var protectee = new Contact
                {
                    FirstName = createdUser.FirstName,
                    LastName = createdUser.LastName,
                    Email = createdUser.Email,
                    Phone = createdUser.PhoneNumber,
                    Message = "Help",
                    Primary = true,
                    UserId = "b0cc970e-0627-47e5-8740-c36dba3c5c59"
                };
                _context.Contact.Add(protectee);
                _context.SaveChanges();

                var contacts = _context.Contact.Where(c => c.UserId == createdUser.Id).ToList();
                var protectees = _context.Contact.Where(c => c.Email == user.Email).ToList();
                for (int i = 0; i < protectees.Count(); i++)
                {
                    {
                        var localUser = await _userManager.FindByIdAsync(protectees[i].UserId);
                        protectees[i].FirstName = localUser.FirstName;
                        protectees[i].LastName = localUser.LastName;
                        var alert = _context.Alert.Where(x => x.UserId == protectees[i].UserId && x.status == true).FirstOrDefault();
                        if (alert is not null)
                        {
                            protectees[i].Active = true;

                            var images = _context.Image.Where(x => x.AlertId == alert.Id).Select(x => x.Name).ToList();
                            if (images is not null)
                                protectees[i].images.AddRange(images);
                        }
                    }
                }
                return new UserModel { Id = createdUser.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Phonenumber = user.Phonenumber, Body = user.Body, Skin = user.Skin, Day = user.Day, Month = user.Month, Year = user.Year, Eye = user.Eye, contacts = user.contacts, protectees = protectees };
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
                var protectees = _context.Contact.Where(c => c.Email == details.Email).ToList();
                for(int i = 0; i < protectees.Count(); i++)
                {
                    {
                        var localUser = await _userManager.FindByIdAsync(protectees[i].UserId);
                        protectees[i].FirstName = localUser.FirstName;
                        protectees[i].LastName = localUser.LastName;
                        var alert = _context.Alert.Where(x => x.UserId == protectees[i].UserId && x.status == true).FirstOrDefault();
                        if (alert is not null)
                        {
                            protectees[i].Active = true;

                            var images = _context.Image.Where(x => x.AlertId == alert.Id).Select(x => x.Name).ToList();
                            if(images is not null)
                                protectees[i].images.AddRange(images);
                        } 
                    }
                }
               
                return new UserModel { Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Phonenumber = user.PhoneNumber, protectees = protectees, contacts =contacts,  Body = user.Body, Skin = user.Skin, Day = user.Day, Month = user.Month, Year = user.Year, Eye = user.Eye };
            }
            return null;
        }
    }
}
