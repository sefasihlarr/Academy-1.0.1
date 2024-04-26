using BusinessLayer.Concrete;
using DataAccessLayer.EntityFreamwork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CartController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        CartManager _cartManager = new CartManager(new EfCoreCartRepository());

        public CartController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cart = _cartManager.GetCartByUserId(_userManager.GetUserId(User));
            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(x => new CartItemModel()
                {
                    CartItemId = x.Id,
                    ExamId = x.ExamId,
                    Name = x.Exam.Title,
                }).ToList(),
            });
        }
        [HttpPost]
        public IActionResult AddToCart(int examId)
        {
            var userId = _userManager.GetUserId(User);
            _cartManager.AddToCart(userId, examId);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int examId)
        {
            _cartManager.DeleteFromCart(_userManager.GetUserId(User), examId);
            return RedirectToAction("Index", "Cart");

        }


        //sınav bittikten sonra yada sınava  başladığı zaman cart temizlenmeli Yada revize edilerek erişilemez hal getirilmeli
        public void ClearCart(string cartId)
        {
            _cartManager.ClearCart(cartId);
        }


    }
}
