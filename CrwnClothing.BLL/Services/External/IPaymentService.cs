namespace CrwnClothing.BLL.Services.External
{
    public interface IPaymentService
    {
        public Task HandleEvent(string payload,string signature,string endpointSecret);
    }
}
