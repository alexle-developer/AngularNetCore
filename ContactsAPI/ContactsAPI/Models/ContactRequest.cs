namespace ContactsAPI.Models
{
    public class ContactRequest
    {
        public string? Fullname { get; set; }
        public long Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Nickname { get; set; }
    }
}
