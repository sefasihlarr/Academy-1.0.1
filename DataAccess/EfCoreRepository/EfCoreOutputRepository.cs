using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFreamwork
{
    public class EfCoreOutputRepository : EfCoreGenericRepository<Output, AcademyContext>, IOutputDal
    {
        public void Delete(int outputId, int subjectId)
        {
            using (var _context = new AcademyContext())
            {
                var cmd = @"delete from Outputs where Id=@p0 And SubjectId=@p1";
                _context.Database.ExecuteSqlRaw(cmd, outputId,subjectId);
            }
        }

        public List<Output> GetWithSubjectList()
        {
            using(var _context = new AcademyContext())
            {
                return _context.Outputs
                    .Include(x=>x.Subject)
                    .ToList();
            }
        }
    }
}
