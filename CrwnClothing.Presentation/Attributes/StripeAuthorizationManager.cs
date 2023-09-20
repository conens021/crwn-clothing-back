using CrwnClothing.Presentation.Helpers;
using DroneDropshipping.BLL.Exceptions;

namespace CrwnClothing.Presentation.Attributes
{
    public class StripeAuthorizationManager
    {
        private readonly HeaderParser _headerParser;

        public StripeAuthorizationManager(
         HeaderParser headerParser)
        {
            _headerParser = headerParser;
        }

        public string GetSignature()
        {
            string? signature = _headerParser.Get("Stripe-Signature");

            if (signature == null)
                throw new BusinessException("Signature key not provided!", 401);

            return signature;
        }
    }
}
