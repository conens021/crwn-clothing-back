using CrwnClothing.BLL.DTOs;
using CrwnClothing.DAL.Repositories.UserRepository;
using CrwnClothing.BLL.Mappers;
using DroneDropshipping.BLL.Exceptions;
using CrwnClothing.DAL.Entities;
using CrwnClothing.BLL.Helpers;
using CrwnClothing.DAL.Redis;
using Microsoft.Extensions.Caching.Distributed;
using CrwnClothing.BLL.Services.NotificationService;
using CrwnClothing.DAL.Models;
using CrwnClothing.BLL.DTOs.UserDto;
using CrwnClothing.BLL.DTOs.SortingDto;
using CrwnClothing.BLL.Services.ShppingCart;
using CrwnClothing.BLL.DTOs.ShoppingCartDto;
using CrwnClothing.BLL.Services.External;
using CrwnClothing.BLL.Services.Payments;

namespace CrwnClothing.BLL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IDistributedCache _cache;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICcPaymentService _paymentService;


        public UserService(IUserRepository userRepository,
                            IShoppingCartService shoppingCartService,
                            IDistributedCache cache,
                            IUserNotificationService userNotificationService,
                            ICcPaymentService paymentService)
        {
            _userRepository = userRepository;
            _shoppingCartService = shoppingCartService;
            _cache = cache;
            _userNotificationService = userNotificationService;
            _paymentService = paymentService;
        }

        #region[CRUD]
        public async Task<UserDTO> Create(CreateUserDTO user)
        {
            User existingUser = _userRepository.GetUserByUsernameOrEmail(user.Username, user.Email);

            if (existingUser != null)
                throw new BusinessException(
                    "User with given username or email already exists!", 409);

            User forCreation = user.ToEntity();

            await CreateCustomer(forCreation);

            User created = await _userRepository.Create(forCreation);

            await _shoppingCartService.CreateUserShoppingCart(created.Id);

            return created.ToDTO();
        }

        public async Task<UserDTO> Update(UserDTO user)
        {
            User updated = await _userRepository.Update(user.ToEntity());


            return updated.ToDTO();
        }

        public Task<UserDTO> Delete(UserDTO entity)
        {
            throw new NotImplementedException();
        }

        public UserDTO? GetById(int id)
        {
            User? user = _userRepository.GetById(id);


            return user?.ToDTO();
        }

        public UserDTO GetSafeById(int id)
        {
            User? user = _userRepository.GetById(id);

            if (user == null) throw new BusinessException("User not found", 404);


            return user.ToDTO();
        }
        #endregion

        public async Task<UserDTO> Create(ExternalUserInfoDTO externalUserInfoDTO)
        {
            User existingUser =
                _userRepository.GetUserByUsernameOrEmail(
                    externalUserInfoDTO.Username, externalUserInfoDTO.Email);

            if (existingUser != null)
                throw new BusinessException(
                    "User with given username or email already exists!", 409);

            User forCreation = externalUserInfoDTO.ToEntity();

            await CreateCustomer(forCreation);

            User created =
                await _userRepository.Create(forCreation);

            await _shoppingCartService.CreateUserShoppingCart(created.Id);


            return created.ToDTO();
        }

        public List<UserDTO> GetAll()
        {
            IEnumerable<User> users = _userRepository.GetAll();

            return users.Select(user => user.ToDTO()).ToList();
        }

        public List<UserDTO> GetAll(PaginationDTO paginationDTO)
        {
            throw new NotImplementedException();
        }

        public List<UserDTO> GetAll(PaginationDTO paginationDTO, SortingDTO sorting)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetUserByUsername(string username)
        {
            User user = _userRepository.GetUserByUsername(username);

            if (user == null) throw new BusinessException("User with given username does not exists.", 404);


            return user.ToDTO();
        }

        public UserDTO AuthUser(AuthUserDTO authUserDTO)
        {
            User user = _userRepository.GetUserByUsername(authUserDTO.Username);

            if (user == null) throw new BusinessException("User with given username or email not found", 404);

            if (user.Password != authUserDTO.Password) throw new BusinessException("Wrong username or password", 401);


            return user.ToDTO();
        }

        public async Task<string> SendEmailVerificationCode(UserDTO user)
        {
            string verifictionCode = await this.GetEmailVerificationCode(user.Id);

            //send email to user
            await _userNotificationService.SendEmailVerifyCode(user, verifictionCode);


            return verifictionCode;
        }

        public async Task<string> SendEmailVerificationCode(string email)
        {
            UserDTO userDTO = this.GetUserByUsername(email);


            string verifictionCode = await this.GetEmailVerificationCode(userDTO.Id);


            return verifictionCode;
        }

        public async Task<UserDTO> VerifyEmail(UserDTO user, EmailVerificationCode code)
        {
            UserDTO userDTO = this.GetSafeById(user.Id);

            UserCache userCache;

            try
            {
                userCache = await _cache.GetRecordAsync<UserCache>(user.Id.ToString() ?? "");

            }
            //if record is not found
            catch (Exception ex)
            {
                throw new BusinessException("User activation code doesn't exists or it's expired!", 404);
            }


            if (userCache.EmailVerifictionCode != code.Code)
            {
                throw new BusinessException("Wrong activation code!", 404);
            }

            //change user to verified
            userDTO.Verified = true;

            UserDTO updated = await this.Update(userDTO);


            return updated;
        }

        #region[PRIVATE]
        private async Task<string> GetEmailVerificationCode(int userId)
        {
            string verifictionCode = RandomGenerator.GetRandomDigitsCode(6);

            //give it timespan of 5minutes
            TimeSpan timeSpan = TimeSpan.FromSeconds(5 * 60);

            //save to cache
            await _cache.SetRecordAsync<UserCache>(
                userId.ToString() ?? "",
                new UserCache { EmailVerifictionCode = verifictionCode }, timeSpan);


            return verifictionCode;
        }

        private async Task CreateCustomer(User user)
        {
            string externalPaymentId = await _paymentService.CreateCustomer(user.ToDTO());

            user.PaymentId = externalPaymentId;
        }
        #endregion
    }
}
