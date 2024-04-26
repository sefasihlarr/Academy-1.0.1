namespace EntityLayer.Concrete
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public Boolean Condition { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
