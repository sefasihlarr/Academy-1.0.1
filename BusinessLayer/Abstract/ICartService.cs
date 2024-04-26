using BusinessLayer.GenericService;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ICartService : IGenericService<Cart>
    {
        void InitializeCart(string userId);

        List<Cart> GetListCartItem();

        void DeleteFromCart(string userId, int examId);

        Cart GetCartByUserId(string userId);

        void AddToCart(string userId, int examId);

        void ClearCart(string cartId);
    }
}
