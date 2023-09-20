namespace CrwnClothing.DAL.Entities
{
    public partial class User : BaseEntity
    {
        public User()
        {
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool? Verified { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? PaymentId { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
