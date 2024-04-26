using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void Create(Message entity)
        {
            _messageDal.Create(entity);
        }

        public void Delete(Message entity)
        {
            _messageDal.Delete(entity);
        }

        public List<Message> GetAll()
        {
            return _messageDal.GetAll().ToList();
        }

        public Message GetById(int id)
        {
            return _messageDal.GetById(id);
        }

        public void Update(Message entity)
        {
            _messageDal.Update(entity);
        }
    }
}
