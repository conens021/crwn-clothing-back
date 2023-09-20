
namespace CrwnClothing.BLL.DTOs.Custom
{
    public class OrderShippingDetailsDTO
    {
        public string ShippingAddress { get; set; } = null!;
        public string ShippingCity { get; set;} = null!;
        public string ShippingCountry { get; set; } = null!;
        public string ShippingZipCode { get; set; } = null!;
    }
}
