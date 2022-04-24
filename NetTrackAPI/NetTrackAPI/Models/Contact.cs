namespace NetTrackAPI.Models
{
    public class Contact
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public bool Primary { get; set; }

        public User User { get; set; }
    }
}
