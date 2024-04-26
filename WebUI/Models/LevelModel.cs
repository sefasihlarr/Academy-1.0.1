namespace WebUI.Models
{
    public class LevelModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public bool Condition { get; set; }
    }
}
