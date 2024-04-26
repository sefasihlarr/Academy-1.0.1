using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class LevelManager : ILevelService
    {
        private readonly ILevelDal _levelDal;

        public LevelManager(ILevelDal levelDal)
        {
            _levelDal = levelDal;
        }

        public void Create(Level entity)
        {
            _levelDal.Create(entity);
        }

        public void Delete(Level entity)
        {
            _levelDal.Delete(entity);
        }

        public List<Level> GetAll()
        {
            return _levelDal.GetAll().ToList();
        }

        public Level GetById(int id)
        {
            return _levelDal.GetById(id);
        }

        public void Update(Level entity)
        {
            _levelDal.Update(entity);
        }
    }
}
