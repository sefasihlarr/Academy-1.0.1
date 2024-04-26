using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFreamwork
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart, AcademyContext>, ICartDal
    {
        public override void Update(Cart entity)
        {
            using (var contex = new AcademyContext())
            {
                contex.Carts.Update(entity);
                contex.SaveChanges();
            }
        }
        public Cart GetByUserId(string userId)
        {
            using (var context = new AcademyContext())
            {
                return context.Carts
                                    .Include(x => x.CartItems)
                                    .ThenInclude(x => x.Exam)
                                    .ThenInclude(x => x.Subject)
                                    .ThenInclude(x => x.Lesson)
                                    .ThenInclude(x => x.Class)
                                    .FirstOrDefault(x => x.UserId == userId);
            };
        }
        //ilişkili tablolarda veri silme işlemi
        public void DeleteFromCart(int cartId, int examId)
        {
            using (var context = new AcademyContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0 And examId=@p1";
                context.Database.ExecuteSqlRaw(cmd, cartId, examId);
            }
        }

        public void ClearCart(string cartId)
        {
            using (var context = new AcademyContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0";
                context.Database.ExecuteSqlRaw(cmd, cartId, cartId);
            }
        }

        public List<Cart> GetListCartItem()
        {
            using (var context = new AcademyContext())
            {
                return context.Carts
                    .Include(x => x.CartItems).ToList();

            }


        }
    }
}
