namespace NZwalks.Models.Domain
{
    public class User_Role
    {
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }

    }
}
