using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.ShoppingCartDto;
using CrwnClothing.BLL.DTOs.SortingDto;
using CrwnClothing.DAL.Repositories.ShoppingCartRepository;
using CrwnClothing.BLL.Mappers;
using CrwnClothing.BLL.Mappers.ShoppingCartMappers;
using CrwnClothing.BLL.DTOs.UserDto;
using CrwnClothing.BLL.Services.ShoppingCart;
using CrwnClothing.BLL.DTOs.Custom.Cart;
using DroneDropshipping.BLL.Exceptions;
using CrwnClothing.BLL.Utils;
using CrwnClothing.BLL.DTOs.OrderDTOs;
using CrwnClothing.BLL.Services.Sizes;

namespace CrwnClothing.BLL.Services.ShppingCart
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _repisotry;
        private readonly IShoppingCartProductService _shoppingCartProductService;

        public ShoppingCartService(
                IShoppingCartRepository repository,
                IShoppingCartProductService shoppingCartProductService
            )
        {
            _repisotry = repository;
            _shoppingCartProductService = shoppingCartProductService;
        }

        #region[CRUD]

        public async Task<ShoppingCartDTO> Create(CreateShoppingCartDTO createShoppingCartDTO)
        {
            DAL.Entities.ShoppingCart created =
                await _repisotry.Create(createShoppingCartDTO.ToEntity());


            return created.ToDTO();
        }

        public async Task<ShoppingCartDTO> Delete(ShoppingCartDTO shoppingCartDTO)
        {
            DAL.Entities.ShoppingCart deleted =
                await _repisotry.Delete(shoppingCartDTO.ToEntity());


            return deleted.ToDTO();
        }

        public List<ShoppingCartDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartDTO> GetAll(PaginationDTO paginationDTO)
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartDTO> GetAll
            (
                PaginationDTO paginationDTO,
                SortingDTO sorting
            )
        {
            throw new NotImplementedException();
        }

        public ShoppingCartDTO? GetById(int id)
        {
            DAL.Entities.ShoppingCart? shoppingCart = _repisotry.GetById(id);


            return shoppingCart?.ToDTO();
        }

        public ShoppingCartDTO GetSafeById(int id)
        {
            DAL.Entities.ShoppingCart? shoppingCart = _repisotry.GetById(id);

            if (shoppingCart == null) throw new BusinessException("User does not have a shopping cart!", 404);

            return shoppingCart.ToDTO();
        }

        public async Task<ShoppingCartDTO> Update(ShoppingCartDTO dto)
        {
            DAL.Entities.ShoppingCart updated = await _repisotry.Update(dto.ToEntity());

            return updated.ToDTO();
        }


        public ShoppingCartDTO? GetByUserId(int userId)
        {
            DAL.Entities.ShoppingCart? shoppingCart = _repisotry.GetByUserId(userId);

            if (shoppingCart == null)
                return null;


            return shoppingCart.ToDTO();
        }
        #endregion

        public async Task<List<CartProductDTO>> AddToCart
            (
                AddToCartProductDTO cartProductDTO,
                UserDTO userDTO
            )
        {
            var shoppingCart = await GetOrCreateUserCart(userDTO.Id);

            List<CartProductDTO> cartProductsDTO = await _shoppingCartProductService.AddToCart(
                cartProductDTO, shoppingCart.Id);


            return cartProductsDTO;
        }

        public async Task<List<CartProductDTO>> AddToCart(
            List<AddToCartProductDTO> cartProductDTO, UserDTO userDTO)
        {
            var cart = await GetOrCreateUserCart(userDTO.Id);

            List<CartProductDTO> cartProductsDTO =
                _shoppingCartProductService.AddToCart(cartProductDTO, cart.Id);


            return cartProductsDTO;
        }

        public async Task<List<CartProductDTO>> ChangeQuantity(
            AddToCartProductDTO cartProductDTO, UserDTO userDTO)
        {
            var cart = await GetOrCreateUserCart(userDTO.Id);

            List<CartProductDTO> cartProductsDTO = await
               _shoppingCartProductService.AddToCart(cartProductDTO, cart.Id, overrite: true);


            return cartProductsDTO;
        }

        public async Task<List<CartProductDTO>> RemoveFromCart(
            DeleteShoppingCartItemDTO deleteDTO, UserDTO userDTO)
        {
            var cart = await GetOrCreateUserCart(userDTO.Id);

            List<CartProductDTO> cartProductDTOs =
                _shoppingCartProductService.RemoveFromCart(deleteDTO.ProductId, deleteDTO.SizeId, cart.Id);

            return cartProductDTOs;
        }


        public List<CartProductDTO> EmptyUserCart(UserDTO userDTO)
        {
            ShoppingCartDTO? shoppingCartDTO = GetByUserId(userDTO.Id);

            if (shoppingCartDTO == null) return new List<CartProductDTO>();


            return _shoppingCartProductService.RemoveByCartId(shoppingCartDTO.Id);
        }

        public async Task<ShoppingCartWithProductsDTO>
            GetUserCartWithCartProducts(UserDTO userDTO, ShoppingCartDTO? cart = null)
        {
            if (cart == null)
                cart = await GetOrCreateUserCart(userDTO.Id);

            List<ShoppingCartProductWithProductAndSizeDTO> shoppingCartProducts =
               _shoppingCartProductService.GetByShoppingCartIdWithProductAndSize(cart.Id);

            List<CartProductDTO> products = shoppingCartProducts.ToCartProductsDTO();


            return new ShoppingCartWithProductsDTO()
            {
                ShoppingCart = cart,
                Products = products
            };
        }

        public async Task<ShoppingCartDTO> GetOrCreateUserCart(int userId)
        {
            ShoppingCartDTO? shoppingCart = this.GetByUserId(userId);

            if (shoppingCart == null)
                shoppingCart = await CreateUserShoppingCart(userId);

            return shoppingCart;
        }

        public async Task<ShoppingCartDTO> CreateUserShoppingCart(int userId)
        {
            CreateShoppingCartDTO shoppingCartDTO = new()
            {
                UserId = userId,
            };

            return await this.Create(shoppingCartDTO);
        }

        public long GetShoppingCartTotalInCents(UserDTO user)
        {
            decimal totalInDollars = GetShoppingCartTotal(user);

            long totalAmountInCents = CurrencyUtils.ConvertDollarsToCents(totalInDollars);

            return totalAmountInCents;
        }

        public decimal GetShoppingCartTotal(UserDTO user)
        {
            ShoppingCartDTO? shoppingCartDTO = this.GetByUserId(user.Id);

            if (shoppingCartDTO == null)
                throw new BusinessException("User cart does not exists!", 404);

            decimal totalAmount =
                _shoppingCartProductService.CalculateShoppingCartTotalByCartId(shoppingCartDTO.Id);

            return totalAmount;
        }
    }
}
