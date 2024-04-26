using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EfCoreRepository
{
    public class EfCoreAppUserRepostory : EfCoreGenericRepository<AppUser, AcademyContext>, IAppUserDal
    {
        public List<AppUser> ListTogether()
        {
            using (var _context = new AcademyContext())
            {
                return _context.Users
                    .Include(x => x.Branch)
                    .Include(x => x.Class)
                    .ToList();
            }
        }
    }
}
