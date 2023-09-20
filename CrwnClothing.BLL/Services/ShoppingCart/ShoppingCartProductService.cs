using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.ShoppingCartDto;
using CrwnClothing.BLL.DTOs.SortingDto;
using CrwnClothing.DAL.Repositories.ShoppingCartProductRepository;
using CrwnClothing.BLL.Mappers.ShoppingCartMappers;
using CrwnClothing.DAL.Entities;
using DroneDropshipping.BLL.Exceptions;
using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.ProductDto;
using CrwnClothing.BLL.DTOs.OrderDTOs;
using CrwnClothing.BLL.Services.Sizes;
using CrwnClothing.BLL.DTOs.SizesDTOs;

namespace CrwnClothing.BLL.Services.ShoppingCart
{
    public class ShoppingCartProductService : IShoppingCartProductService
    {
        private readonly IShoppingCartProductRepository _repository;
        private readonly IProductService _productService;
        private readonly ISizeService _sizeService;

        public ShoppingCartProductService(
                IShoppingCartProductRepository repository,
                IProductService productService,
                ISizeService sizeService)
        {
            _repository = repository;
            _productService = productService;
            _sizeService = sizeService;
        }

        #region[CRUD]
        public async Task<ShoppingCartProductDTO> Create(CreateShoppingCartProductDTO dto)
        {
            ShoppingCartProduct created = await _repository.Create(dto.ToEntity());


            return created.ToDTO();
        }

        public Task<ShoppingCartProductDTO> Delete(ShoppingCartProductDTO entity)
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartProductDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartProductDTO> GetAll(PaginationDTO paginationDTO)
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartProductDTO> GetAll(PaginationDTO paginationDTO, SortingDTO sorting)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartProductDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartProductDTO GetSafeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingCartProductDTO> Update(ShoppingCartProductDTO entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        public ShoppingCartProductDTO GetByProductIdAndShoppingCartId(int productId, int cartId)
        {
            ShoppingCartProduct? shoppingCartProduct =
                _repository.GetByProductIdAndShoppingCartId(productId, cartId);

            if (shoppingCartProduct == null)
                throw new BusinessException("Shoping cart product not found!", 404);


            return shoppingCartProduct.ToDTO();
        }

        public async Task<List<CartProductDTO>> AddToCart
           (
               AddToCartProductDTO addProductToCart,
               int cartId,
               bool overrite = false
           )
        {
            List<ShoppingCartProductWithProductAndSizeDTO> cartProducts
                 = GetByShoppingCartIdWithProductAndSize(cartId);

            var existingCartProduct =
                   cartProducts.Find(cartProduct =>
                        cartProduct.Product.Id == addProductToCart.ProductId
                        &&
                        cartProduct.Size.Id == addProductToCart.SizeId);

            if (existingCartProduct != null)
            {
                existingCartProduct.Quantity = overrite
                    ?
                        addProductToCart.Quantity
                    :
                        addProductToCart.Quantity + existingCartProduct.Quantity;


                await _repository.Update(existingCartProduct.ToEntity());
            }

            else
            {
                ProductDTO productDTO = _productService.GetSafeById(addProductToCart.ProductId);
                SizeDTO sizeDTO = _sizeService.GetSafeById(addProductToCart.SizeId);

                var forCreation = new ShoppingCartProductWithProductAndSizeDTO()
                {
                    ProductId = addProductToCart.ProductId,
                    ShoppingCartId = cartId,
                    Quantity = addProductToCart.Quantity,
                    SizeId = addProductToCart.SizeId,
                    Size = sizeDTO,
                    Product = productDTO
                };

                cartProducts.Add(forCreation);


                await _repository.Create(forCreation.ToEntity());
            }


            return cartProducts.Select(cp => cp.ToCartProductDTO()).ToList();
        }

        public List<CartProductDTO> AddToCart
            (
                List<AddToCartProductDTO> addProductToCartList,
                int cartId
            )
        {

            List<ShoppingCartProductWithProductAndSizeDTO> cartProducts
                = GetByShoppingCartIdWithProductAndSize(cartId);

            List<ShoppingCartProductWithProductAndSizeDTO> forCreation = new List<ShoppingCartProductWithProductAndSizeDTO>();
            List<ShoppingCartProductWithProductAndSizeDTO> forUpdate = new List<ShoppingCartProductWithProductAndSizeDTO>();

            addProductToCartList.ForEach(aptc =>
            {
                var existingCartProduct =
                    cartProducts.Find(cartProduct =>
                        cartProduct.ProductId == aptc.ProductId
                        &&
                        cartProduct.SizeId == aptc.SizeId);

                if (existingCartProduct != null)
                {

                    existingCartProduct.Quantity += aptc.Quantity;
                    existingCartProduct.SizeId = aptc.SizeId;

                    forUpdate.Add(existingCartProduct);

                    cartProducts.Remove(existingCartProduct);
                }

                else
                {
                    ProductDTO productDTO = _productService.GetSafeById(aptc.ProductId);
                    SizeDTO sizeDTO = _sizeService.GetSafeById(aptc.SizeId);

                    var newShoppingCartProduct = new ShoppingCartProductWithProductAndSizeDTO()
                    {
                        ProductId = aptc.ProductId,
                        ShoppingCartId = cartId,
                        Quantity = aptc.Quantity,
                        Product = productDTO,
                        Size = sizeDTO,
                        SizeId = aptc.SizeId
                    };

                    forCreation.Add(newShoppingCartProduct);
                }
            });

            _repository.UpdateBulk(forUpdate.Select(fu => fu.ToEntity()).ToList());
            _repository.CreateBulk(forCreation.Select(fc => fc.ToEntity()).ToList());

            cartProducts.AddRange(forUpdate);
            cartProducts.AddRange(forCreation);

            return cartProducts.Select(cp => cp.ToCartProductDTO()).ToList();
        }

        public List<CartProductDTO> RemoveFromCart(int productId, int sizeId, int cartId)
        {
            List<ShoppingCartProductWithProductAndSizeDTO> cartProducts
               = _repository.GetByShoppingCartId(cartId)
                   .Select(entity => entity.ToDTOWithProductAndSize())
                   .ToList();

            var existingCartProduct =
                  cartProducts.Find(cartProduct =>
                    cartProduct.ProductId == productId
                    && cartProduct.SizeId == sizeId
                    );


            if (existingCartProduct != null)
            {
                _repository.Delete(existingCartProduct.ToEntity());
                cartProducts.Remove(existingCartProduct);
            }


            return cartProducts.Select(cp => cp.ToCartProductDTO()).ToList();
        }


        public async Task<ShoppingCartProductDTO> UpdateQuntity(ShoppingCartProductDTO dto, int quantity)
        {
            int newQuantity = dto.Quantity + quantity;

            if (newQuantity < 1)
            {
                ShoppingCartProduct deleted = await _repository.Delete(dto.ToEntity());


                return deleted.ToDTO();
            }
            else
            {
                dto.Quantity = newQuantity;

                ShoppingCartProduct updated =
                    await _repository.Update(dto.ToEntity());


                return updated.ToDTO();
            }
        }

        public List<CartProductDTO> RemoveByCartId(int cartId)
        {
            List<ShoppingCartProductWithProductAndSizeDTO> deleted =
                _repository.RemoveByCartId(cartId).Select(scp => scp.ToDTOWithProductAndSize()).ToList();


            return deleted.Select(e => e.ToCartProductDTO()).ToList();
        }

        public List<ShoppingCartProductWithProductDTO>
            GetByShoppingCartIdWithProduct(int shoppingCartId)
        {
            return _repository.GetByShoppingCartId(shoppingCartId)
                .Select(scp => scp.ToDTOWithProduct())
                .ToList();
        }

        public List<ShoppingCartProductWithProductAndSizeDTO>
         GetByShoppingCartIdWithProductAndSize(int shoppingCartId)
        {
            return _repository.GetByShoppingCartId(shoppingCartId)
                .Select(scp => scp.ToDTOWithProductAndSize())
                .ToList();
        }

        public List<ShoppingCartProductDTO> GetShoppingCartId(int cartId)
        {
            return _repository.GetByShoppingCartId(cartId)
               .Select(scp => scp.ToDTO())
               .ToList();
        }

        public decimal CalculateShoppingCartTotalByCartId(int shoppingCartId)
        {
            List<ShoppingCartProductWithProductDTO> shoppingCartProducts =
               this.GetByShoppingCartIdWithProduct(shoppingCartId);

            decimal cartTotal = shoppingCartProducts.Sum(scp => scp.Product.Price * scp.Quantity);


            return cartTotal;
        }
    }
}
