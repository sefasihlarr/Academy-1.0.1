using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFreamwork
{
    public class EfCoreSolutionRepository : EfCoreGenericRepository<Solution, AcademyContext>, ISolutionDal
    {
        public List<Solution> GetWithQuestionList()
        {
            using (var _context = new AcademyContext())
            {
                return _context.Solutions
                    .Include(x => x.Question)
                    .Include(x => x.Option)
                    .ToList();
            }
        }

        public override void Update(Solution entity)
        {
            using (var _context = new AcademyContext())
            {
                _context.Update(entity);
                _context.SaveChanges();
            }
        }
    }
}
