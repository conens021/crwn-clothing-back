using CrwnClothing.BLL.DTOs.UserDto;
using CrwnClothing.BLL.DTOs;

namespace CrwnClothing.BLL.Services
{
    public interface IUserService : IBaseService<UserDTO, CreateUserDTO>
    {
        public UserDTO GetUserByUsername(string username);
        public UserDTO AuthUser(AuthUserDTO authUserDTO);
        public Task<UserDTO> VerifyEmail(UserDTO user, EmailVerificationCode code);
        public Task<string> SendEmailVerificationCode(UserDTO user);
        public Task<string> SendEmailVerificationCode(string email);
        Task<UserDTO> Create(ExternalUserInfoDTO externalUserInfoDTO);
    }
}
