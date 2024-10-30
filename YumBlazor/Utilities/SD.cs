using YumBlazor.Data;

namespace YumBlazor.Utilities
{
    public static class SD
    {
        public static string Admin_Role = "Admin";
        public static string Customer_Role  = "Customer";
        public static string StatusPending = "Pending";
        public static string StatusReadyForPickUp ="StatusReadyForPickUp";
		public static string StatusCompleted= "StatusCompleted";
		public static string StatusCanceled= "StatusCanceled";
        public static List<OrderDetail> converShoppingCartToOrderDetails(List<ShoppingCart> shoppingCarts)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var cart in shoppingCarts)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    ProductId = cart.ProductId,
                    Count = cart.Count,
                    Price = Convert.ToDouble(cart.Product.Price),
                    ProductName = cart.Product.Name
                };
                orderDetails.Add(orderDetail);
            }
            return orderDetails;
        }
	}
}
