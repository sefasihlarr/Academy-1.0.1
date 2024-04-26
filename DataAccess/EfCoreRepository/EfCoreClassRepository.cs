using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFreamwork
{
    public class EfCoreClassRepository : EfCoreGenericRepository<Class, AcademyContext>, IClassDal
    {
        public Class GetByIdWithBrances(int id)
        {
            using (var _context = new AcademyContext())
            {
                return _context.Classes
                    .Where(x => x.Id == id)
                    .Include(x => x.ClassBranches)
                    .ThenInclude(x => x.Branch)
                    .FirstOrDefault();
            }
        }

        public List<ClassBranch> GetClassBranchList()
        {
            using(var _context = new AcademyContext())
            {
                return _context.ClassBranches.ToList();
            }
        }

        public void Update(Class entity, int[] branchIds)
        {
            using (var _context = new AcademyContext())
            {
                var _class = _context.Classes
                    .Include(x => x.ClassBranches)
                    .FirstOrDefault(x => x.Id == entity.Id);

                if (_class != null)
                {
                    _class.Name = entity.Name;
                    _class.UpdatedDate = entity.UpdatedDate;
                    _class.Condition = entity.Condition;
                    _class.ClassBranches = branchIds.Select(x => new ClassBranch()
                    {
                        BranchId = x,
                        ClassId = entity.Id,
                    }).ToList();

                    _context.SaveChanges();

                }
            };
        }
    }
}
