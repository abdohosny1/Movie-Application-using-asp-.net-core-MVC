using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



namespace Movie_Application.Data.cart
{
    public class ShoppingCart
    {
        public ApplicationDbContext _context;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;

        }

        //public static ShoppingCart GetShoppingCart(IServiceProvider service)
        //{
        //    ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        //    var context = service.GetService<ApplicationDbContext>();

        //    string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        //    session.SetString("cartId", cartId);
        //    return new ShoppingCart(context) { ShoppingCartId = cartId };
        //}
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem=_context.ShoppingCartItems
                                   .FirstOrDefault(x => x.Movie.Id == movie.Id
                                    && x.ShoppingCartId==ShoppingCartId );
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                   
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();

        }
        public void RemoveItemToCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems
                                   .FirstOrDefault(x => x.Movie.Id == movie.Id && x.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);

                }
            }
           
            _context.SaveChanges();

        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems
                                         .Where(e => e.ShoppingCartId == ShoppingCartId)
                                          .Include(e => e.Movie).ToList());


        }

        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(e => e.ShoppingCartId == ShoppingCartId)
                              .Select(n => n.Movie.Price * n.Amount).Sum();
            return total;
        }

        public async Task ClearShoppingCartAsync()
        {
            var items =await _context.ShoppingCartItems.Where(e => e.ShoppingCartId == ShoppingCartId)
                      .ToListAsync();

            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();

        }
    }
}
