namespace EntityLayer.Concrete
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }



        public List<Question> Questions { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public Boolean Condition { get; set; }

    }
}
