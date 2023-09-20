
using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.OrderDTOs;
using CrwnClothing.BLL.DTOs.ShoppingCartDto;

namespace CrwnClothing.BLL.Services.ShoppingCart
{
    public interface IShoppingCartProductService : IBaseService<ShoppingCartProductDTO, CreateShoppingCartProductDTO>
    {
        public ShoppingCartProductDTO GetByProductIdAndShoppingCartId(int productId, int cartId);
        public Task<List<CartProductDTO>> AddToCart(AddToCartProductDTO addProductToCart, int cartId, bool overrite = false);
        public List<CartProductDTO> AddToCart(List<AddToCartProductDTO> addProductToCartList, int cartId);
        public List<CartProductDTO> RemoveFromCart(int productId, int sizeId, int cartId);
        public List<CartProductDTO> RemoveByCartId(int cartId);
        public List<ShoppingCartProductWithProductDTO> GetByShoppingCartIdWithProduct(int shoppingCartId);
        public List<ShoppingCartProductWithProductAndSizeDTO> GetByShoppingCartIdWithProductAndSize(int shoppingCartId);
        public Task<ShoppingCartProductDTO> UpdateQuntity(ShoppingCartProductDTO dto, int quantity);
        public decimal CalculateShoppingCartTotalByCartId(int shoppingCartId);
        public List<ShoppingCartProductDTO> GetShoppingCartId(int cartId);
    }
}
