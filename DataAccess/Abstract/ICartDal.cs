using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface ICartDal : IGenericDal<Cart>
    {
        void ClearCart(string cartId);
        void DeleteFromCart(int cartId, int examId);
        Cart GetByUserId(string userId);
        List<Cart> GetListCartItem();
    }
}
