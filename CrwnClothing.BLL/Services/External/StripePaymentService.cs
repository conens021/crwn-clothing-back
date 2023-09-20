using CrwnClothing.BLL.Contracts.Enums;
using CrwnClothing.BLL.Services.OrderService;
using DroneDropshipping.BLL.Exceptions;
using Stripe;

namespace CrwnClothing.BLL.Services.External
{
    public class StripePaymentService : IPaymentService
    {
        private readonly IOrderService _orderService;

        public StripePaymentService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task HandleEvent(string payload, string signature, string endpointSecret)
        {
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(payload,
               signature, endpointSecret);

                string evenTName = stripeEvent.Data.Object.Object;

                switch (evenTName)
                {
                    case "payment_intent":
                        await HandlePaymentIntentEvent(stripeEvent);

                        break;

                    case "charge":
                        await HandleChargeEvent(stripeEvent);

                        break;

                    default:
                        throw new BusinessException("Event not supported", 400);
                }
            }
            catch (Exception e)
            {
                throw new BusinessException(e.Message, 400);

            }
        }

        private async Task HandlePaymentIntentEvent(Event stripeEvent)
        {
            string status = stripeEvent.Type;

            PaymentIntent paymentIntent = (PaymentIntent)stripeEvent.Data.Object;

            Dictionary<string, string> metadata = paymentIntent.Metadata;

            string orderId = GetOrder(metadata);

            await UpdateOrderPaymentStatus(status, Convert.ToInt32(orderId));
        }

        private async Task HandleChargeEvent(Event stripeEvent)
        {
            string status = stripeEvent.Type;

            Charge charge = (Charge)stripeEvent.Data.Object;

            Dictionary<string, string> metadata = charge.Metadata;

            string orderId = GetOrder(metadata);

            await UpdateChargeStatus(status, Convert.ToInt32(orderId), charge);
        }

        private async Task UpdateChargeStatus(string status, int orderId, Charge charge)
        {
            switch (status)
            {
                case Events.ChargeSucceeded:
                    decimal totalCharged = GetTotalCharged(charge);
                    await _orderService.UpdateTotalChaarged(orderId, totalCharged);


                    break;

                default:
                    throw new BusinessException($"Unhandled event type: {status}", 400);

            }
        }

        private async Task UpdateOrderPaymentStatus(string stripeEvent, int orderId)
        {
            switch (stripeEvent)
            {
                case Events.PaymentIntentCreated:
                    await _orderService.UpdatePaymentStats(orderId, PaymentStatus.CREATED);

                    break;

                case Events.PaymentIntentSucceeded:
                    await _orderService.UpdatePaymentStats(orderId, PaymentStatus.SUCCESS);

                    break;

                case Events.PaymentIntentProcessing:
                    await _orderService.UpdatePaymentStats(orderId, PaymentStatus.IN_PROCCESS);

                    break;

                case Events.PaymentIntentCanceled:
                    await _orderService.UpdatePaymentStats(orderId, PaymentStatus.CANCELED);

                    break;

                case Events.PaymentIntentPaymentFailed:
                    await _orderService.UpdatePaymentStats(orderId, PaymentStatus.FAILURE);


                    break;

                case Events.PaymentIntentRequiresAction:
                    await _orderService.UpdatePaymentStats(orderId, PaymentStatus.REQUIRES_ACTION);


                    break;

                default:
                    throw new BusinessException($"Unhandled event type: {stripeEvent}", 400);

            }
        }

        private string GetOrder(Dictionary<string, string> metadata)
        {
            if (metadata.TryGetValue("OrderId", out string? orderId))
            {
                return orderId;
            }
            else
            {
                throw new BusinessException("Order id not provided!", 404);
            }
        }

        private decimal GetTotalCharged(Charge charge)
        {
            return charge.AmountCaptured  / 100.00m;
        }

    }
}
