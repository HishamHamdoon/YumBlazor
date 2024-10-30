using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stripe.Checkout;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;
using YumBlazor.Utilities;

namespace YumBlazor.Services
{
    public class PaymentService
    {
        private readonly IOrderHeaderRpository _orderHeaderRpository;
        private readonly NavigationManager _navigationManager;

        public PaymentService(IOrderHeaderRpository orderHeaderRpository, NavigationManager navigationManager)
        {
            _orderHeaderRpository = orderHeaderRpository;
            _navigationManager = navigationManager;
        }
        public Session CreateStripeCheckOutSession(OrderHeader orderHeader)
        {
            var lineItems = orderHeader.OrderDetails.Select(order => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    UnitAmountDecimal = (decimal?)order.Price * 100,//10.10=>1010
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = order.ProductName,

                    }
                },
                Quantity = order.Count
            }).ToList();
            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{_navigationManager.BaseUri}order/confirmation/{{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{_navigationManager.BaseUri}cart",
                LineItems = lineItems,
                Mode = "payment",
            };

            var service = new SessionService();
            var session = service.Create(options);
            return session;
        }

        public async Task<OrderHeader> CheckPaymentStatusAndUpdateOrder(string sessionId)
        {
            OrderHeader orderHeader = await _orderHeaderRpository.GetOrderBySessionIdAsync(sessionId);
            var service = new SessionService();
            var session = service.Get(sessionId);
            if (session.PaymentStatus.ToLower() == "paid")
            {
                await _orderHeaderRpository.UpdateStatusAsync(SD.StatusCompleted, orderHeader.Id, session.PaymentIntentId);
            }
            return orderHeader;
        }
    }
}
