using Microsoft.AspNetCore.Mvc;
using NetTrackAPI.Models;
using NetTrackAPI.ViewModels;
using Twilio;
using Twilio.Rest.Api.V2010.Account;using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace NetTrackAPI.Repositories.Alert
{
    public class AlertRepository : IAlertRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public AlertRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment, IConfiguration configuration, UserManager<User> userManager)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
            this._environment = environment;
            this._configuration = configuration;
            this._userManager = userManager;
        }

        public Task SaveImage(HttpRequest request)
        {
            try
            {
                if (request.Form.Files.Any())
                {
                    foreach (var file in request.Form.Files)
                    {
                        //Creating the file path and image name
                        FileInfo fileInfo = new FileInfo(file.FileName);
                        var newFileName = "Image_" + DateTime.Now.TimeOfDay.TotalMilliseconds + "_" + fileInfo.Name;
                        var path = Path.Combine("", _environment.ContentRootPath + "\\Images\\" + newFileName);


                        //Resizing the image
                        using var imgToResize = SixLabors.ImageSharp.Image.Load(file.OpenReadStream());
                        imgToResize.Mutate(x => x.Resize(700, 462));
                        imgToResize.Save(path);
                        var userId = request.Form.Keys.First().ToString();
                         var alert =  _context.Alert.Where(x => x.UserId == userId && x.status == true).ToList().First();

                        //Editing the image details and saving the reference name to DB
                        Models.Image image = new Models.Image();
                        image.Name = newFileName;
                        image.AlertId = alert.Id;
                        _context.Image.Add(image);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //return NotFound("Select Files");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public Task Start(HttpRequest request)
        {

            var values = request.ReadFormAsync().Result;
            Models.Alert alert =
                System.Text.Json.JsonSerializer.Deserialize<Models.Alert>(values.Keys.FirstOrDefault());


            var Dbalert = _context.Alert.Where(x => x.UserId == alert.UserId && x.status == true).FirstOrDefault();

            if(Dbalert != null)
            {

                _context.Alert.Remove(Dbalert);
                _context.SaveChanges();
            }
            alert.status = true;
            var createdAlert = _context.Alert.Add(alert);
            _context.SaveChanges();
            var user = _userManager.FindByIdAsync(alert.UserId).Result;


            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure

            string accountSid = _configuration["Twilio:AccountSid"];
            string authToken = _configuration["Twilio:AuthToken"];
            var contacts = _context.Contact.Where(x => x.UserId == alert.UserId).ToList();
            var from = _configuration["Twilio:From"];
            foreach (var contact in contacts)
            {
                TwilioClient.Init(accountSid, authToken);



                var message = MessageResource.Create(
                    body: "An alert has been triggered for " + user.FirstName + " " + user.LastName + ", please visit the NetTrack application to view the alert and start tracking.",
                    from: new Twilio.Types.PhoneNumber(from),
                    to: new Twilio.Types.PhoneNumber($"+" + contact.Phone.ToString())
                );
            }
            return Task.CompletedTask;

        }


        
        public Task<Models.Alert> GetAlert(string userId)
        {
           var alert = _context.Alert.Where(x => x.UserId == userId && x.status == true).FirstOrDefault();

            return Task.FromResult(alert);
        }

        public Task StopAlert(HttpRequest request)
        {
            var values = request.ReadFormAsync().Result;
            Models.Alert alert =
                System.Text.Json.JsonSerializer.Deserialize<Models.Alert>(values.Keys.FirstOrDefault());
            
            var dbAlert = _context.Alert.Where(x => x.UserId == alert.UserId && x.status == true).FirstOrDefault();

            _context.Alert.Remove(dbAlert);
            _context.SaveChanges();

            return Task.FromResult(alert);
        }

        public Task UpdateAlert(HttpRequest request)
        {
            var values = request.ReadFormAsync().Result;
            Models.Alert alert =
                System.Text.Json.JsonSerializer.Deserialize<Models.Alert>(values.Keys.FirstOrDefault());


            var updateAlert = _context.Alert.Where(x => x.UserId == alert.UserId && x.status == true).FirstOrDefault();
            if(updateAlert != null)
            {
                updateAlert.Latitude = alert.Latitude;
                updateAlert.Longitude = alert.Longitude;

                _context.Alert.Update(updateAlert);
                _context.SaveChanges();
            }
           

            return Task.CompletedTask;
        }
    }
}
