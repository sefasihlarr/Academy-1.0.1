namespace EntityLayer.Concrete
{
    public class Message
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public Boolean Condition { get; set; }

    }
}
