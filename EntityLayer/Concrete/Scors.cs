namespace EntityLayer.Concrete
{
    public class Scors
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int True { get; set; }
        public int False { get; set; }
        public int Null { get; set; }
        public decimal Average { get; set; }
        public decimal Scor { get; set; }
        public bool Condition { get; set; }
    }
}
