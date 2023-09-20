
using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.SizesDTOs;
using CrwnClothing.BLL.DTOs.SortingDto;
using CrwnClothing.DAL.Repositories.SizeRepository;
using Microsoft.EntityFrameworkCore;
using CrwnClothing.BLL.Mappers.SizeMappers;
using CrwnClothing.DAL.Entities;
using DroneDropshipping.BLL.Exceptions;
using CrwnClothing.BLL.DTOs.Custom.Cart;

namespace CrwnClothing.BLL.Services.Sizes
{
    public class SizeService : ISizeService
    {
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly ISizeRepository _repository;


        public SizeService(
            IProductSizeRepository productSizeRepository,
            ISizeRepository repository)
        {
            _productSizeRepository = productSizeRepository;
            _repository = repository;
        }

        #region[CRUD]
        public Task<SizeDTO> Create(CreateSizeDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<SizeDTO> Delete(SizeDTO entity)
        {
            throw new NotImplementedException();
        }

        public List<SizeDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<SizeDTO> GetAll(PaginationDTO paginationDTO)
        {
            throw new NotImplementedException();
        }

        public List<SizeDTO> GetAll(PaginationDTO paginationDTO, SortingDTO sorting)
        {
            throw new NotImplementedException();
        }

        public SizeDTO? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SizeDTO> Update(SizeDTO entity)
        {
            throw new NotImplementedException();
        }


        public SizeDTO GetSafeById(int id)
        {
            Size? size = _repository.GetById(id);

            if (size == null)
                throw new BusinessException("Size not found!", 404);

            return size.ToDTO();
        }
        #endregion

        public List<SizeDTO> GetAvailableSizesByProduct(int productId)
        {
            return _productSizeRepository
                .GetAvailableSizesByProductId(productId)
                .OrderBy(ps => ps.Size.Value)
                .Select(ps => ps.Size.ToDTO())
                .ToList();
        }

        public SizeWithQuantityDTO CheckProductQuantityAvailabilty(AddToCartProductDTO cartItem)
        {
            ProductsSize? productsSize =
                _productSizeRepository.GetByProductIdAndSizeId(cartItem.ProductId, cartItem.SizeId);

            if (productsSize == null)
                throw new BusinessException("Product size not available!", 404);

            if (productsSize.QuantityAvailable < cartItem.Quantity)
                throw new BusinessException(
                    $"Product size quantity not available! Max. Available: {productsSize.QuantityAvailable}", 404);


            return productsSize.ToDTOWithQuantity();
        }
    }
}
