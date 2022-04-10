namespace Movie_Application.service.OrderService
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items,string UserId,string userEmailAddress);

        Task<List<Order>> GetOrderByUserIdAndRoleAsync(string UserId,string userRole);

     }
}
