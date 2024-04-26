using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EfCoreRepository
{
    public class EfCoreScorsRepository : EfCoreGenericRepository<Scors, AcademyContext>, IScorsDal
    {
        public List<Scors> GetTogetherList()
        {
            using (var _context = new AcademyContext())
            {
                return _context.Scors
                    .Include(x => x.Exam)
                    .ThenInclude(x => x.Subject)
                    .Include(x => x.Exam)
                    .ThenInclude(x => x.Lesson)
                    .Include(x => x.Exam)
                    .ThenInclude(x => x.Class)
                    .Include(x => x.User)
                    .ToList();
            }
        }
    }
}
