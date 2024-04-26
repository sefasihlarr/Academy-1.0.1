using EntityLayer.Concrete;

namespace WebUI.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }

        public int UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
