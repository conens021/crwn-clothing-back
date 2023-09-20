using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.UserDto;
using CrwnClothing.BLL.Helpers;
using CrwnClothing.BLL.Models;
using CrwnClothing.BLL.Services.TemplateService;
using DroneDropshipping.BLL.Exceptions;

namespace CrwnClothing.BLL.Services.NotificationService
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly INotificationService _notificationService;

        public UserNotificationService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task SendEmailVerifyCode(UserDTO userDTO,string verifictionCode)
        {

            if (userDTO == null) throw new BusinessException("User not found", 404);


            string template = "EmailConfirmation.html";

            string templateRoot = PathRegistry.GetInstance().TemplatePath;

            string templateUrl = Path.Combine(templateRoot, template);


            string edited = await TemplateEngine<VerifyCodeTemplate>.GenerateFromFile(templateUrl,
                new VerifyCodeTemplate() { Username = userDTO.Username, Code = verifictionCode, ConfirmationLink = "" });

            await
                _notificationService.SendAsync(
                    "Verification code", userDTO.Username, userDTO.Email, edited);
        }
    }
}
