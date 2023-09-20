using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.SizesDTOs;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.BLL.Services.Sizes
{
    public interface ISizeService : IBaseService<SizeDTO, CreateSizeDTO>
    {
        public List<SizeDTO> GetAvailableSizesByProduct(int productId);

        public SizeWithQuantityDTO CheckProductQuantityAvailabilty(AddToCartProductDTO cartItem);
    }
}
