
using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.Custom.Cart;
using CrwnClothing.BLL.DTOs.OrderDTOs;
using CrwnClothing.BLL.DTOs.SortingDto;
using CrwnClothing.BLL.DTOs.UserDto;
using CrwnClothing.BLL.Services.ShppingCart;
using CrwnClothing.BLL.Mappers.OrderMappers;
using CrwnClothing.DAL.Repositories.OrderRepository;
using CrwnClothing.DAL.Entities;
using CrwnClothing.BLL.Contracts.Enums;
using CrwnClothing.BLL.Services.Payments;
using CrwnClothing.BLL.Mappers.Payments;
using CrwnClothing.BLL.DTOs.Custom.Payments;
using CrwnClothing.BLL.DTOs.Custom;
using DroneDropshipping.BLL.Exceptions;

namespace CrwnClothing.BLL.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICcPaymentService _paymentService;
        private readonly IUserService _userService;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderProductRepository orderProductRepository,
            IShoppingCartService shoppingCartService,
            ICcPaymentService paymentService,
            IUserService userService)
        {
            _shoppingCartService = shoppingCartService;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _paymentService = paymentService;
            _userService = userService;
        }

        public async Task<OrderDTO> Create(CreateOrderDTO createOrderDTO)
        {
            Order order = await _orderRepository.Create(createOrderDTO.ToEntity());


            return order.ToDTO();
        }

        public Task<OrderDTO> Delete(OrderDTO entity)
        {
            throw new NotImplementedException();
        }

        public List<OrderDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<OrderDTO> GetAll(PaginationDTO paginationDTO)
        {
            throw new NotImplementedException();
        }

        public List<OrderDTO> GetAll(PaginationDTO paginationDTO, SortingDTO sorting)
        {
            throw new NotImplementedException();
        }

        public OrderDTO? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public OrderDTO GetSafeById(int id)
        {
            Order? order = _orderRepository.GetById(id);

            if (order == null) throw new BusinessException("Order does not exists!", 404);


            return order.ToDTO();
        }

        public async Task<OrderDTO> Update(OrderDTO dto)
        {
            Order updated = await _orderRepository.Update(dto.ToEntity());


            return updated.ToDTO();
        }

        public async Task<OrderDTO> Update(int orderId, OrderShippingDetailsDTO shippingDetailsDTO)
        {
            OrderDTO orderDTO = GetSafeById(orderId);

            OrderDTO forUpdating = shippingDetailsDTO.ToDTO(orderDTO);

            return await Update(forUpdating);
        }

        public async Task<OrderIntentWithCartProductsDTO> CreateOrderIntent(UserDTO user)
        {
            //we are accesing user from db because user from JWT token does not have payment id
            UserDTO userFomDB = _userService.GetSafeById(user.Id);

            OrderWithProductsDTO createdOrder = await CreateFromUser(userFomDB);

            CcPaymenIntetntDTO ccPaymenIntetntDTO =
                await CreatePaymentIntentFromOrder(createdOrder, userFomDB.PaymentId);

            createdOrder.PaymentIntentId = ccPaymenIntetntDTO.Id;

            await Update(createdOrder);


            return createdOrder.ToOrderIntentWithCartProductsDTO(ccPaymenIntetntDTO, createdOrder.Products);
        }

        public async Task<OrderWithProductsDTO> CreateFromUser(UserDTO userDTO)
        {
            OrderTotalWithProductsDTO orderTotalWithProductsDTO
                = await GetOrderTotalFromUserCartWithProducts(userDTO);

            int userId = userDTO.Id;
            List<CartProductDTO> cartProducts = orderTotalWithProductsDTO.CartProducts;

            CreateOrderDTO createOrderDTO = new()
            {
                CustomerId = userId,
                PaymentStatus = PaymentStatus.PAYMENT_INTENT,
                OrderStatus = OrderStatus.ORDER_INTENT,
                Subtotal = orderTotalWithProductsDTO.Subtotal,
                Total = orderTotalWithProductsDTO.Total,
            };


            OrderDTO createdOrderDTO = await Create(createOrderDTO);

            CreateOrderProducts(cartProducts, createdOrderDTO.Id);

            return createdOrderDTO.ToOrderWithProductsDTO(cartProducts);
        }

        public async Task<CcPaymenIntetntDTO> CreatePaymentIntentFromOrder(OrderDTO order, string? userPaymentId = null)
        {
            if (userPaymentId == null)
            {
                return await CreateGuestPaymentIntent(order);
            }
            else
            {
                return await CreateUserPaymentIntent(order, userPaymentId);
            }
        }

        public async Task<OrderIntentWithCartProductsDTO> UpdateOrderIntent(
            UserDTO userDTO, int orderId)
        {
            OrderDTO orderDTO = this.GetSafeById(orderId);

            AuthorizeUserOrder(orderDTO, userDTO);

            DeleteOrderProducts(userDTO, orderId);

            OrderTotalWithProductsDTO orderTotalWithProductsDTO
                = await GetOrderTotalFromUserCartWithProducts(userDTO);

            List<CartProductDTO> cartProducts = orderTotalWithProductsDTO.CartProducts;
            CreateOrderProducts(cartProducts, orderId);

            OrderDTO updatedOrder = await Update(orderDTO, orderTotalWithProductsDTO);

            if (orderDTO.PaymentIntentId == null)
                throw new BusinessException("Payment intent for this order does not exists!", 404);

            UpdatePaymentIntentDTO updatePaymentIntentDTO =
                orderDTO.ToUpdatePaymentIntentDTO(orderDTO.PaymentIntentId);

            //update payment intent
            CcPaymenIntetntDTO ccPaymenIntetntDTO =
                await _paymentService.UpdatePaymentIntent(updatePaymentIntentDTO);

            return updatedOrder.ToOrderIntentWithCartProductsDTO(ccPaymenIntetntDTO, cartProducts);
        }

        public async Task<OrderTotalWithProductsDTO>
            GetOrderTotalFromUserCartWithProducts(UserDTO userDTO)
        {
            ShoppingCartWithProductsDTO
                cartWithProducts = await _shoppingCartService.GetUserCartWithCartProducts(userDTO);

            List<CartProductDTO> cartProducts = cartWithProducts.Products;

            decimal orderTotal = GetTotalFromCartProducts(cartProducts);

            return new OrderTotalWithProductsDTO()
            {
                Total = orderTotal,
                Subtotal = orderTotal,
                CartProducts = cartProducts
            };
        }

        public List<OrderProductDTO> DeleteOrderProducts(UserDTO user, int orderId)
        {
            List<OrderProduct> ordersProducts = GetOrderProducts(orderId).Select(op => op.ToEntity()).ToList();

            _orderProductRepository.DeleteBulk(ordersProducts);


            return ordersProducts.Select(op => op.ToDTO()).ToList();
        }

        public List<OrderProductDTO> GetOrderProducts(int orderId)
        {
            return _orderProductRepository
                .GetByOrderId(orderId)
                .Select(op => op.ToDTO()).ToList();
        }

        public List<OrderProduct> CreateOrderProducts(List<CartProductDTO> cartProducts, int orderId)
        {
            List<CreateOrderProductDTO> forCreationOrderProducts =
             cartProducts.ToCreateOrderProductDTO(orderId);

            List<OrderProduct> createdOrderProducts =
                _orderProductRepository.CreateBulk(
                        forCreationOrderProducts.Select(op => op.ToEntity())
                        .ToList()
                    );

            return createdOrderProducts;
        }

        public async Task<OrderDTO> StartOrderRequest(int id, UserDTO user)
        {
            OrderDTO orderDTO = GetSafeById(id);

            if (orderDTO.OrderStatus > OrderStatus.ORDER_REQUEST)
                throw new BusinessException("Order request already started!", 409);

            AuthorizeUserOrder(orderDTO, user);

            orderDTO.OrderStatus = OrderStatus.ORDER_REQUEST;

            OrderDTO updated = await Update(orderDTO);


            return updated;
        }

        public async Task<OrderDTO> OrderRequestFailed(int id, UserDTO user)
        {
            OrderDTO orderDTO = GetSafeById(id);

            if (orderDTO.OrderStatus > OrderStatus.ORDER_IN_PROCCESS)
                throw new BusinessException("Order request already started!", 409);

            AuthorizeUserOrder(orderDTO, user);

            orderDTO.OrderStatus = OrderStatus.ORDER_REQUEST_FAILURE;

            OrderDTO updated = await Update(orderDTO);


            return updated;
        }

        public async Task UpdatePaymentStats(int orderId, PaymentStatus paymentStatus)
        {
            OrderDTO orderDTO = GetSafeById(orderId);

            orderDTO.PaymentStatus = paymentStatus;

            await Update(orderDTO);
        }


        public async Task<OrderDTO> UpdateTotalChaarged(int orderId, decimal totalCharged)
        {
            OrderDTO orderDTO = GetSafeById(orderId);

            if (totalCharged < orderDTO.Total)
                orderDTO.PaymentStatus = PaymentStatus.PARITAL_CHARGED;

            else
                orderDTO.PaymentStatus = PaymentStatus.CHARGED;


            orderDTO.ChargedTotal = totalCharged;


            return await Update(orderDTO);
        }

        #region[PRIVATE]
        private async Task<OrderDTO> Update(
            OrderDTO updating, OrderTotalWithProductsDTO orderTotalWithProductsDTO)
        {
            updating.Total = orderTotalWithProductsDTO.Total;
            updating.Subtotal = orderTotalWithProductsDTO.Subtotal;

            OrderDTO updated = await Update(updating);

            return updated;
        }

        private decimal GetTotalFromCartProducts(List<CartProductDTO> cartProducts)
        {

            return cartProducts.Sum(cp => cp.Quantity * cp.Product.Price);
        }

        private void AuthorizeUserOrder(OrderDTO orderDTO, UserDTO userDTO)
        {
            if (orderDTO.CustomerId != userDTO.Id)
                throw new BusinessException("you dont have permission to this order!", 401);
        }

        private async Task<CcPaymenIntetntDTO> CreateUserPaymentIntent(OrderDTO order, string userPaymentId)
        {
            CreatePaymentIntentWithCustomerDTO createPaymentIntentDTO =
               order.ToCreatePaymentIntentWithCustomerDTO(userPaymentId);

            CcPaymenIntetntDTO ccPaymenIntetntDTO =
                await _paymentService.CreatePaymentIntentWithCustomer(createPaymentIntentDTO);


            return ccPaymenIntetntDTO;
        }

        private async Task<CcPaymenIntetntDTO> CreateGuestPaymentIntent(OrderDTO order)
        {
            CreatePaymentIntentDTO createPaymentIntentDTO =
                    order.ToCreatePaymentIntentDTO();

            CcPaymenIntetntDTO ccPaymenIntetntDTO =
                await _paymentService.CreatePaymentIntent(createPaymentIntentDTO);

            return ccPaymenIntetntDTO;
        }

        #endregion

    }
}
