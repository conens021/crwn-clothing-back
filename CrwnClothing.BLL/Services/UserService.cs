using CrwnClothing.BLL.DTOs;
using CrwnClothing.DAL.Repositories.UserRepository;
using CrwnClothing.BLL.Mappers;
using DroneDropshipping.BLL.Exceptions;
using CrwnClothing.DAL.Entities;
using CrwnClothing.BLL.Helpers;
using CrwnClothing.BLL.Services.TemplateService;
using CrwnClothing.BLL.Models;
using CrwnClothing.DAL.Redis;
using Microsoft.Extensions.Caching.Distributed;
using CrwnClothing.BLL.Services.NotificationService;
using CrwnClothing.DAL.Models;

namespace CrwnClothing.BLL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IDistributedCache _cache;
        private readonly IUserNotificationService _userNotificationService;


        public UserService(IUserRepository userRepository, IDistributedCache cache, IUserNotificationService userNotificationService)
        {
            _userRepository = userRepository;
            _cache = cache;
            _userNotificationService = userNotificationService;
        }

        public UserDTO CreateUser(UserDTO user)
        {
            User existingUser = _userRepository.GetUserByUsernameOrEmail(user.Username, user.Email);

            if (existingUser != null) throw new BusinessException("User with given username or email already exists!", 409);

            user.CreatedAt = DateTime.Now;
            user.Verified = false;

            User created = _userRepository.CreateUser(user.ToEntity());


            return created.ToDTO();
        }

        public UserDTO DeleteUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetUser(int id)
        {
            User user = _userRepository.GetUser(id);

            if (user == null) throw new BusinessException("User not found", 404);


            return user.ToDTO();
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

            if (user == null) throw new BusinessException("User with given username or email not found", 400);

            if (user.Password != authUserDTO.Password) throw new BusinessException("Wrong username or password", 401);


            return user.ToDTO();
        }


        public UserDTO UpdateUser(UserDTO user)
        {
            return _userRepository.UpdateUser(user.ToEntity()).ToDTO();
        }

        public List<UserDTO> GetAll()
        {
            IEnumerable<User> users = _userRepository.GetAll();

            return users.Select(user => user.ToDTO()).ToList();
        }

        public async Task<string> SendEmailVerificationCode(UserDTO user)
        {
            string verifictionCode = RandomGenterator.GetRandomDigitsCode(6);

            //give it timespan of 5minutes
            TimeSpan timeSpan = TimeSpan.FromSeconds(5 * 60);

            //save to cache
            await _cache.SetRecordAsync<UserCache>(
                user.Id.ToString() ?? "",
                new UserCache { EmailVerifictionCode = verifictionCode }, timeSpan);

            //send email to user
            await _userNotificationService.SendEmailVerifyCode(user, verifictionCode);


            return verifictionCode;
        }

        public async Task<bool> VerifyEmail(UserDTO user, EmailVerificationCode code)
        {
            UserDTO userDTO = this.GetSafeUser(user.Id ?? 0);

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

            this.UpdateUser(userDTO);

            return true;
        }

        public UserDTO GetSafeUser(int id)
        {
            User user = _userRepository.GetSafeUser(id);

            if (user == null) throw new BusinessException("User not found", 404);


            return user.ToDTO();
        }
    }
}
