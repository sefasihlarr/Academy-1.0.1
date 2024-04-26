using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartDal _cartDal;

        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }


        public void AddToCart(string userId, int examId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                var index = cart.CartItems.FindIndex(x => x.ExamId == examId);
                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ExamId = examId,
                        CartId = cart.Id,
                    });
                }


                _cartDal.Update(cart);
            }
        }

        public void ClearCart(string cartId)
        {
            _cartDal.ClearCart(cartId);
        }

        public void DeleteFromCart(string userId, int examId)
        {

            var cart = GetCartByUserId(userId);
            if (cart != null)
            {

                _cartDal.DeleteFromCart(cart.Id, examId);

            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartDal.GetByUserId(userId);
        }

        public List<Cart> GetListCartItem()
        {
            return _cartDal.GetListCartItem();
        }

        public void InitializeCart(string userId)
        {
            _cartDal.Create(new Cart() { UserId = userId });
        }

        public void Create(Cart entity)
        {
            _cartDal.Create(entity);
        }

        public void Delete(Cart entity)
        {
            _cartDal.Delete(entity);
        }

        public List<Cart> GetAll()
        {
            return _cartDal.GetAll().ToList();
        }

        public Cart GetById(int id)
        {
            return _cartDal.GetById(id);
        }

        public void Update(Cart entity)
        {
            _cartDal.Update(entity);
        }
    }
}
