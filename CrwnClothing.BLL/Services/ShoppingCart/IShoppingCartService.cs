using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.ShoppingCartDto;
using CrwnClothing.BLL.DTOs.UserDto;

namespace CrwnClothing.BLL.Services.ShppingCart
{
    public interface IShoppingCartService : IBaseService<ShoppingCartDTO, CreateShoppingCartDTO>
    {
        public ShoppingCartDTO? GetByUserId(int userId);
        public Task<List<CartProductDTO>> AddToCart(AddToCartProductDTO cartProductDTO, UserDTO userDTO);
        public Task<List<CartProductDTO>> AddToCart(List<AddToCartProductDTO> cartProductDTO, UserDTO userDTO);
        public Task<List<CartProductDTO>> RemoveFromCart(DeleteShoppingCartItemDTO deleteDTO, UserDTO user);
        public Task<List<CartProductDTO>> ChangeQuantity(AddToCartProductDTO cartProductDTO, UserDTO user);
        public Task<ShoppingCartDTO> CreateUserShoppingCart(int userId);
        public decimal GetShoppingCartTotal(UserDTO user);
        public long GetShoppingCartTotalInCents(UserDTO user);
        public Task<ShoppingCartWithProductsDTO> GetUserCartWithCartProducts(UserDTO userDTO, ShoppingCartDTO? cart = null);
        public List<CartProductDTO> EmptyUserCart(UserDTO userDTO);
    }
}
