using CrwnClothing.BLL.DTOs.UserDto;

namespace CrwnClothing.BLL.DTOs.ShoppingCartDto
{
    public class ShoppingCartDTO
    {
        public int Id { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UserId { get; set; }
    }
}
