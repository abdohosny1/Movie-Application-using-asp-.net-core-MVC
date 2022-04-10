using Movie_Application.Data.cart;

namespace Movie_Application.Data.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartViewComponent(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public  IViewComponentResult Invoke()
        {
            var item =  _shoppingCart.GetShoppingCartItems();
            return View(item.Count);
        }
    }
}
