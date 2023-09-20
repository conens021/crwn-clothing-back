
using CrwnClothing.BLL.DTOs.UserDto;

namespace CrwnClothing.BLL.DTOs.Custom
{
    public class ShoppingCartWithUserDTO
    {
        public int Id { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public int UserId { get; set; }

        public UserDTO User { get; set; } = new UserDTO();
    }
}
