namespace Auth.Domain.Entities
{
    public partial class User
    {
        public Guid IdUser { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
