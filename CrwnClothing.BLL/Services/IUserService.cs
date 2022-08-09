using CrwnClothing.BLL.DTOs;

namespace CrwnClothing.BLL.Services
{
    public interface IUserService
    {
        public List<UserDTO> GetAll();
        public UserDTO GetUser(int id);
        public UserDTO GetSafeUser(int id);
        public UserDTO GetUserByUsername(string username);
        public UserDTO AuthUser(AuthUserDTO authUserDTO);
        public UserDTO CreateUser(UserDTO user);
        public UserDTO UpdateUser(UserDTO user);
        public UserDTO DeleteUser(UserDTO user);
        public Task<bool> VerifyEmail(UserDTO user, EmailVerificationCode code);
        public Task<string> SendEmailVerificationCode(UserDTO user);
    }
}
