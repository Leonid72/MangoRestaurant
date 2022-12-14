using Mango.MessageBus;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using System.Collections;

namespace Mango.Services.ShoppingCartAPI.Messages
{
    public class CheckoutHeaderDto :BaseMessage
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string? CuponCode { get; set; }
        public double OrderTotal { get; set; }
        public double DiscountTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PickUpDateTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CSV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public int CartTotalItems { get; set; }
        public IEnumerable<CartDetailsDto>? CartDetails { get; set; }
    }
}
