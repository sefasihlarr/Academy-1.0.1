﻿using EntityLayer.Concrete;

namespace WebUI.Models
{
    public class SolutionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string VideoUrl { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int OptionId { get; set; }
        public Option? Option { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public bool Condition { get; set; }
    }
}
