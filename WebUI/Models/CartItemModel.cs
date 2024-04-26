namespace WebUI.Models
{
    public class CartItemModel
    {
        public int CartItemId { get; set; }
        public int ExamId { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public string ExamDate { get; set; }
        public string ClassName { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public string SubjectName { get; set; }

        //Sınav özellikleri buradan çekilebilir
    }
}
