namespace EntityLayer.Concrete
{
    public class Lesson
    {
        public int Id { get; set; }
        public String? Name { get; set; }

        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public Boolean Condition { get; set; }


    }
}
