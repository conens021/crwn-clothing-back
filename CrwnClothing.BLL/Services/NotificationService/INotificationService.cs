namespace CrwnClothing.BLL.Services.NotificationService
{
    public interface INotificationService
    {
        public Task SendAsync(string subject,string recipientName, string recipientAddress, string template);
    }
}
