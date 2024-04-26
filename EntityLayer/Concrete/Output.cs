namespace EntityLayer.Concrete
{
    public class Output
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public int SubjectId { get; set; }
        public Subject  Subject { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public Boolean Condition { get; set; }

    }
}
