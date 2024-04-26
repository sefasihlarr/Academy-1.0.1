using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IScorsDal : IGenericDal<Scors>
    {
        List<Scors> GetTogetherList();
    }
}
