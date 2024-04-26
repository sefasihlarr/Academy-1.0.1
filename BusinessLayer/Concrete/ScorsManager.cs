using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ScorsManager : IScorsService
    {
        private readonly IScorsDal _scorsDal;

        public ScorsManager(IScorsDal scorsDal)
        {
            _scorsDal = scorsDal;
        }

        public void Create(Scors entity)
        {
            _scorsDal.Create(entity);
        }

        public void Delete(Scors entity)
        {
            _scorsDal.Delete(entity);
        }

        public List<Scors> GetAll()
        {
            return _scorsDal.GetAll().ToList();
        }

        public Scors GetById(int id)
        {
            return _scorsDal.GetById(id);
        }

        public List<Scors> GetTogetherList()
        {
            return _scorsDal.GetTogetherList().ToList();
        }

        public void Update(Scors entity)
        {
            _scorsDal.Update(entity);
        }
    }
}
