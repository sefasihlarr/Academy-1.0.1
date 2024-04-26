using BusinessLayer.GenericService;
using EntityLayer.Concrete;


namespace BusinessLayer.Abstract
{
    public interface IScorsService : IGenericService<Scors>
    {
        List<Scors> GetTogetherList();
    }
}
