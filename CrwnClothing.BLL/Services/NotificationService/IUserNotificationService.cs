
using CrwnClothing.BLL.DTOs;

namespace CrwnClothing.BLL.Services.NotificationService
{
    public interface IUserNotificationService
    {
        public Task SendEmailVerifyCode(UserDTO userDTO,string code);
    }
}
