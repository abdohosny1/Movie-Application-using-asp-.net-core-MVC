namespace Movie_Application.service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync(string UserId,string userRole)
        {
           var order=await _context.Orders.Include(e=>e.OrderItems).ThenInclude(tc=>tc.Movie)
                                     .Include(u=>u.User) .ToListAsync();
            if (userRole != "Admin")
            {
                order = order.Where(e => e.UserId == UserId).ToList();

            }
            return order;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId, string userEmailAddress)
        {
            var newOrder = new Order()
            {
                Email = userEmailAddress,
                UserId = UserId,
            };
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = newOrder.Id,
                    Price = item.Movie.Price,

                };

                await _context.OrderItems.AddAsync(orderItem);

            }
            await _context.SaveChangesAsync();

        }
    }
}
