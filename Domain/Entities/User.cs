

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }
        public string PasswordHash { get; set; }
        public string AccountType { get; set; }
    }
}
