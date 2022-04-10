using Microsoft.AspNetCore.Mvc;
using Movie_Application.Data.cart;
using Movie_Application.service.MovieService;
using Movie_Application.service.OrderService;
using System.Security.Claims;

namespace Movie_Application.Controllers
{
    public class OrderController : Controller
    {

        private readonly IMovieService _service;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;

        public OrderController(IMovieService service, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _service = service;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
       
            var order=await _orderService.GetOrderByUserIdAndRoleAsync(userId,userRole);
            return View(order);

        }
        public IActionResult ShoppingCart()
        {
            var item = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = item;

            var response = new ShoppingCartViewModel()
            {
                ShoppingCart=_shoppingCart,
                ShoppingCartTotal=_shoppingCart.GetShoppingCartTotal(),

            };
            return View(response);
        }

      
        public async Task<IActionResult> AddItemToShoppingCart(int Id)
        {
            var item = await _service.GetMovieByIdAsync(Id);

            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart)); 
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int Id)
        {
            var item = await _service.GetMovieByIdAsync(Id);

            if (item != null)
            {
                _shoppingCart.RemoveItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public  async Task<IActionResult> CompletOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAdddress = User.FindFirstValue(ClaimTypes.Email);
            

           await _orderService.StoreOrderAsync(items, userId, userEmailAdddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");

        }


    }
}
