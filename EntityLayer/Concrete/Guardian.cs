namespace EntityLayer.Concrete
{
    public class Guardian
    {
        public int Id { get; set; }
        public string GuardianName { get; set; }
        public string? GuardianName2 { get; set; }
        public string GuardianSurName { get; set; }
        public string? GuardianSurName2 { get; set; }
        public string GuardianPhone { get; set; }
        public string? GuardianPhone2 { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public bool GuardianCondition { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }
}
